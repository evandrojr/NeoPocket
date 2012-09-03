using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using Microsoft.WindowsCE.Forms;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de listagem de clientes
    /// </summary>
    public partial class FrmClienteLista : FormBase
    {
        #region [ Construtor ]

        public FrmClienteLista()
            : base(false)
        {
            InitializeComponent();
        }

        #endregion

        #region [ Load ]

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            #region Orientação da tela

            //if (SystemSettings.ScreenOrientation != ScreenOrientation.Angle90)
            //    SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;

            #endregion

            #region Trata visibilidade da tela

            if (D.Acao == D.AcaoEnum.PedidoADefinir)
            {
                btnEditar.Visible = false;
                btnNovo.Visible = false;
                btnPedido.Visible = true;
                btnPedido.Location = btnNovo.Location;
            }
            else if (D.Acao == D.AcaoEnum.ClienteADefinir)
            {
                btnEditar.Visible = Parametro.ClienteEdicaoPermitida;
                btnNovo.Visible = true;
                btnPedido.Visible = false;
            }

            #endregion

            #region Cria as colunas da NeoGrid

            // Index 0

            #region id_cliente

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colIdCliente = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colIdCliente.Width = 40;
            colIdCliente.HeaderText = "Cód";
            colIdCliente.Owner = grdCliente;
            colIdCliente.MappingName = "id_cliente";
            colIdCliente.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colIdCliente);

            #endregion


            // Index 1
            #region cliente_nome_reduzido

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colNomeReduzido = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colNomeReduzido.Width = 90;
            colNomeReduzido.HeaderText = "Nome Fantasia";
            colNomeReduzido.Owner = grdCliente;
            colNomeReduzido.MappingName = "cliente_nome_reduzido";
            colNomeReduzido.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colNomeReduzido);

            #endregion

            // Index 2
            #region cliente_nome

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colNome = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colNome.Width = 80;
            colNome.HeaderText = "Razão social";
            colNome.Owner = grdCliente;
            colNome.MappingName = "cliente_nome";
            colNome.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colNome);

            #endregion

            // Index 3
            #region id_cliente_pocket

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colIdStore = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colIdStore.HeaderText = "ID Store";
            colIdStore.ReadOnly = true;
            colIdStore.Owner = grdCliente;
            colIdStore.MappingName = "id_cliente_pocket";
            colIdStore.Width = 0;
            NeoTableStyle.GridColumnStyles.Add(colIdStore);


            #endregion

            #endregion

            UpdateView();
            // Pequeno workaround para fazer a busca por nome funcionar
            // {
            //      Adicionado por Tiago  
            //      Não entendi com funcionava, mas deixei
            // } Evandro
            radNome.Checked = true;
            radCodigo.Checked = true;
            inputPanel.Enabled = false;
        }

        #endregion

        #region [ Novo cliente ]

        private void btNovo_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.ClienteCadastro;
            D.Cliente = new Cliente();
            Util.FormExibir(new FrmClienteCadastro1());
            this.Close();
        }

        #endregion

        #region [ Pega o cliente selecionado da listagem ]

        /// <summary>
        /// Busca o cliente selecionado na listagem, caso o cliente tenha o campo
        /// id_cliente_pocket com valor, busca o cliente por este campo, senão busca o cliente
        /// pelo campo id_cliente.
        /// </summary>
        /// <returns>Cliente</returns>
        private void GetSelectedCliente()
        {
            Int32 i = grdCliente.CurrentRowIndex;
            if (i == -1)
                return;

            D.Cliente = new Cliente();

            // Coluna 0 = id_cliente (Int32)
            // Coluna 3 = id_cliente_pocket  (Guid)
            if (!(grdCliente[i, 3] is DBNull))
                D.Cliente.Carregar((Guid)(grdCliente[i, 3]), 0, Cliente.IdTipoEnum.IdPocket);
            else
                D.Cliente.Carregar(new Guid(), (Int32)(grdCliente[i, 0]), Cliente.IdTipoEnum.IdStore);
        }

        #endregion

        #region [ Edição de um cliente ]

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetSelectedCliente();

                if (D.Cliente != null)
                {
                    D.Acao = D.AcaoEnum.ClienteEdicao;
                    Util.FormExibir(new FrmClienteCadastro1());
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        //#region [ Exclusão de um cliente ]

        //Testar antes de descomentar

        //private void btnExcluir_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Int32 i = grdCliente.CurrentRowIndex;
        //        if (i == -1)
        //            return;

        //        Globals.Cliente = new Cliente();

        //        if (grdCliente[i, 0] != null)
        //        {
        //            Globals.Cliente.Id = (Guid)grdCliente[i, 3];

        //            if (Globals.Cliente.Excluir(Globals.Cliente.Id))
        //            {
        //                MessageBox.Show("Dados excluidos com sucesso!");
        //            }
        //            else
        //            {
        //                MessageBox.Show("Dados não excluidos!");
        //            }

        //            this.UpdateView();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Só é possível excluir clientes cadastrados no pocket!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogBuilder.DEPRECIADO_Append(Globals.APP_LOGDIRECTORY + Globals.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
        //        FE.Show(ex);
        //    }
        //}

        //#endregion

        #region [ Busca ]

        private void btBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                inputPanel.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;
                Int32 colIndex = 0;

                if (radCodigo.Checked)
                {
                    colIndex = 0;
                }

                if (radNome.Checked)
                {
                    colIndex = 2;
                }

                grdCliente.Search(colIndex, txtCliente.Text, Neo.Pocket.Controls.NeoDataGridSearchMode.Like);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Listagem dos pedidos de um cliente ]

        private void ListarPedidos()
        {
            try
            {
                this.GetSelectedCliente();

                if (D.Cliente != null)
                {
                    D.Pedido = new Pedido();
                    Pedido.IdClientePocket = D.Cliente.Id;

                    try
                    {
                        Pedido.IdClienteStore = D.Cliente.IdStore;
                    }
                    catch
                    {
                        MessageBox.Show("Observação, este é um cliente que não foi cadastrado ainda no NeoStore.", "Observação");
                    }

                    Util.FormExibir(new FrmPedidoLista(this));
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Duplo click na listagem ]

        private void grdCliente_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (D.Acao == D.AcaoEnum.PedidoADefinir)
                    {
                        ListarPedidos();
                    }
                    else//Edição de cliente
                    {
                        D.Cliente = null;
                        this.GetSelectedCliente();
                        if (D.Cliente != null)
                        {
                            D.Acao = D.AcaoEnum.ClienteEdicao;
                            Util.FormExibir(new FrmClienteCadastro1());
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                    FE.Show(ex);
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Pedido ]

        private void btnPedido_Click(object sender, EventArgs e)
        {
            Int32 i = grdCliente.CurrentRowIndex;
            if (i == -1)
                return;

            if (D.Acao == D.AcaoEnum.PedidoADefinir)
            {
                ListarPedidos();
            }
        }

        #endregion

        #region [ Carrega a listagem ]

        private void UpdateView()
        {
            try
            {
                DataTable dtClientes;
                dtClientes = D.Bd.DataTablePreenche(
              @"Select
                        id_cliente, cliente_nome_reduzido,cliente_nome, id_cliente_pocket
               from
                        cliente
               where
                         ativo = 1 
               order by
                        cliente_nome_reduzido", "cliente");
                if (dtClientes.Rows.Count == 0)
                    grdCliente.Enabled = false;

                string qryAsterisco;
                //Marcar os clientes que já fizeram pedido mas nao sincroniza
                qryAsterisco = "Select id_cliente_pocket from pedido where status='N';";

                List<Guid> lstClientesComPedido = D.Bd.ListGuid(qryAsterisco);

                qryAsterisco = "Select id_cliente_store from pedido where status='N'";

                List<int> lstClientesComPedidoInt = D.Bd.ListInt(qryAsterisco);

                //Esses try deixam a carga dos clientes bem mais lenta
                //for (int i = 0; i < dtClientes.Rows.Count; ++i)
                //{
                //    try
                //    {
                //        if (lstClientesComPedido.Contains((Guid)dtClientes.Rows[i]["id_cliente_pocket"]))
                //        {
                //            dtClientes.Rows[i]["cliente_nome_reduzido"] = "*" + dtClientes.Rows[i]["cliente_nome_reduzido"];
                //            continue;
                //        }
                //    }
                //    catch { }
                //    try
                //    {
                //        if (lstClientesComPedidoInt.Contains((int)dtClientes.Rows[i]["id_cliente"]))
                //            dtClientes.Rows[i]["cliente_nome_reduzido"] = "*" + dtClientes.Rows[i]["cliente_nome_reduzido"];
                //    }
                //    catch { }
                //}
        

                NeoTableStyle.MappingName = "cliente";
                grdCliente.SetBackupDataSource(dtClientes);
                grdCliente.DataSource = dtClientes;
                grdCliente.Pager = NeoPager;
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
            this.Close();
        }

        #endregion

        #region [ Focus dos componentes ]

        private void txtCliente_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtCliente_LostFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
        }

        #endregion

        #region [ Radio buttons ]

        private void radCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtCliente.Focus();
        }

        private void radNome_CheckedChanged(object sender, EventArgs e)
        {
            txtCliente.Focus();
        }

        #endregion

        #region [ Recusa ]

        private void mnuRecusa_Click(object sender, EventArgs e)
        {
            try
            {
                int i = grdCliente.CurrentRowIndex;
                if (i == -1)
                    return;

                D.Recusa = new Recusa();
                if (grdCliente[i, 0] == System.DBNull.Value)
                {
                    MessageBox.Show("Não se pode registrar recusa de clientes novos. \n Sincronize o dispositivo primeiro.");
                }
                else
                {
                    Recusa.IdCliente = Convert.ToInt32(grdCliente[i, 1]);
                    D.Cliente = new Cliente();
                    D.Cliente.Carregar(new Guid(), Recusa.IdCliente, Cliente.IdTipoEnum.IdStore);
                    Util.FormExibir(new FrmRecusaLista());
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Detalhar cliente na listagem ]

        private void grdCliente_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetSelectedCliente();

                if (D.Cliente != null)
                {
                    notification.Text = D.Cliente.CidadeNome + ", " + D.Cliente.Bairro + ",  " + D.Cliente.Endereco;
                    notification.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

    }
}
