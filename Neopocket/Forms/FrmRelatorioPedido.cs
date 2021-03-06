﻿using System;
using System.Data;
using System.Data.SqlServerCe;
using Neopocket.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using System.Windows.Forms;
using System.ComponentModel;

namespace Neopocket
{
    /// <summary>
    /// Relatório de pedidos
    /// </summary>
    public partial class FrmRelatorioPedido : FormBase
    {
        #region [ Atributos ]

        private DataTable dtPedido;

        #endregion

        #region [ Construtor ]

        public FrmRelatorioPedido()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Load ]

        private void FrmRelatorios_Load(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RelatorioVer;
            cboRelatorios.SelectedIndex = 0;
            GridRefrescar();
        }

        #endregion

        #region [ Carrega as informações na grid ]

        private void GridRefrescar()
        {
            try
            {
                dtPedido = D.Bd.DataTablePreenche(@"SELECT pedido.id_pedido,pedido.id_funcionario, pedido.data, pedido.status, pedido.valor, pedido.id_cliente_store, cliente.cliente_nome_reduzido
                                                FROM pedido INNER JOIN  cliente ON pedido.id_cliente_store = cliente.id_cliente
                                                where data BETWEEN (GETDATE() - 30) AND GETDATE()+1 ORDER BY data DESC", "pedido");
                grdRelatorios.DataSource = dtPedido;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Voltar ]

        private void mnuVoltar_Click(object sender, EventArgs e)
        {
            Util.FormExibir(new FrmPrincipal());
            this.Close();
        }

        #endregion

        #region [ Mudança do filtro ]

        private void cboRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = @"SELECT pedido.id_funcionario, pedido.data, pedido.status, pedido.valor, pedido.id_cliente_store, cliente.cliente_nome_reduzido
                           FROM pedido INNER JOIN cliente ON pedido.id_cliente_store = cliente.id_cliente where";
                string sqlSoma = "Select count(id_cliente_pocket), COALESCE(sum(valor),0) from pedido where";
                if (cboRelatorios.SelectedIndex < 5)
                {
                    sql += " data BETWEEN (GETDATE() - " + (30 * (cboRelatorios.SelectedIndex + 1)) + " ) AND GETDATE()+1 ORDER BY data DESC";
                    sqlSoma += " data BETWEEN (GETDATE() - " + (30 * (cboRelatorios.SelectedIndex + 1)) + " ) AND GETDATE()+1 ";
                }
                else
                {
                    sql = @"SELECT pedido.id_funcionario, pedido.data, pedido.status, pedido.valor, pedido.id_cliente_store, cliente.cliente_nome_reduzido
                        FROM pedido INNER JOIN cliente ON pedido.id_cliente_store = cliente.id_cliente ORDER BY data DESC";
                    sqlSoma = "Select count(id_cliente_pocket), COALESCE(sum(valor),0) from pedido";
                }

                SqlCeCommand cmdPedido = new SqlCeCommand(sql, D.Bd.Con);
                SqlCeDataAdapter daPedido = new SqlCeDataAdapter(cmdPedido);
                DataSet dsPedido = new DataSet();
                daPedido.Fill(dsPedido, "pedido");
                DataTable dtPedido = dsPedido.Tables[0];
                grdRelatorios.DataSource = dtPedido;

                DataTable dtTotal = D.Bd.DataTablePreenche(sqlSoma);
                lblTotalClientes.Text = dtTotal.Rows[0][0].ToString();
                lblTotalPedidos.Text = Convert.ToDouble(dtTotal.Rows[0][1]).ToString("C", D.CultureInfoBRA);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Exibição do pedido ]

        private void grd_DoubleClick(object sender, EventArgs e)
        {
            ShowPedido();
        }

        private void ShowPedido()
        {
            int i;
            try
            {
                i = grdRelatorios.CurrentRowIndex;
            }
            catch
            {
                return;
            }
            if (i == -1)
                return;

            Cliente cl = new Cliente();
            cl.Carregar(Guid.Empty, (int)dtPedido.Rows[i]["id_cliente_store"],Cliente.IdTipoEnum.IdStore);
            D.Cliente = cl;
            Pedido pedidoToShow = new Pedido(true);
            pedidoToShow.Carregar((Guid)dtPedido.Rows[i]["id_pedido"]);
            FrmPedido frmPedido = new FrmPedido(pedidoToShow);
            Cursor.Current = Cursors.WaitCursor;
            frmPedido.Show();
            Cursor.Current = Cursors.Default;
        }

        #endregion

    }
}