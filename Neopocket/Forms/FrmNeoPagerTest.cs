using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using Microsoft.WindowsCE.Forms;

namespace Neopocket.Forms
{
    public partial class FrmNeoPagerTest : Form
    {
        public FrmNeoPagerTest()
        {
            InitializeComponent();
        }

        private void grdCliente_DoubleClick(object sender, EventArgs e)
        {

        }

        private void grdCliente_Click(object sender, EventArgs e)
        {

        }

        private void mnuVoltar_Click(object sender, EventArgs e)
        {

        }

        private void mnuRecusa_Click(object sender, EventArgs e)
        {

        }

        private void FrmNeoPagerTest_Load(object sender, EventArgs e)
        {
            #region id_produto

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colIdProduto = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colIdProduto.HeaderText = "Cod";
            colIdProduto.ReadOnly = true;
            colIdProduto.Owner = grdProduto;
            colIdProduto.MappingName = "id_produto";
            NeoTableStyle.GridColumnStyles.Add(colIdProduto);

            #endregion

            #region referencia

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colReferencia = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colReferencia.HeaderText = "ID";
            colReferencia.Owner = grdProduto;
            colReferencia.MappingName = "id_cliente";
            colReferencia.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colReferencia);

            #endregion


            #region nome

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colNome = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colNome.Width = 90;
            colNome.HeaderText = "Produto";
            colNome.Owner = grdProduto;
            colNome.MappingName = "nome";
            colNome.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colNome);

            #endregion

            #region preco_venda

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colPrecoVenda = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colPrecoVenda.Width = 80;
            colPrecoVenda.HeaderText = "Preço";
            colPrecoVenda.Owner = grdProduto;
            colPrecoVenda.MappingName = "preco_venda";
            colPrecoVenda.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colPrecoVenda);

            #endregion

            #region estoque

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colEstoque = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colEstoque.HeaderText = "Estoque";
            colEstoque.Owner = grdProduto;
            colEstoque.MappingName = "estoque";
            colEstoque.ReadOnly = true;
            NeoTableStyle.GridColumnStyles.Add(colEstoque);

            #endregion

            UpdateView();
            inputPanel.Enabled = false;
       }

        #region [ Carrega a listagem ]



        private void grdProdutoRefrescar()
        {
            DataTable dtProdutos;
            string restricaoPessoaFisica = "";
            if (!Parametro.VenderSemEstoque)
            {
                if (D.Cliente.ContribuinteIcms == false)
                    restricaoPessoaFisica = " permitir_venda_nao_contribuinte = 1 and ";
                dtProdutos = D.Bd.DataTablePreenche(@"
                Select 
                        id_produto,
                        nome,
                        CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                        estoque,
                        referencia
                from
                        produto
                where
                        " + restricaoPessoaFisica + @" estoque > 0  
                order 
                        by nome",
                        "produto");
            }
            else
            {
                if (D.Cliente.ContribuinteIcms == false)
                    restricaoPessoaFisica = " where permitir_venda_nao_contribuinte = 1 ";
                dtProdutos = D.Bd.DataTablePreenche(@"
                Select
                        id_produto,
                        nome,
                        CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                        estoque,
                        referencia
                 from
                        produto
                 " + restricaoPessoaFisica + @"   
                 order by
                        nome",
                 "produto");
            }
            grdProduto.DataSource = dtProdutos;
        }






        private void UpdateView()
        {
            try
            {
                DataTable dtProdutos;
//                dtProdutos = Globals.Bd.DataTablePreenche(@"Select id_cliente_pocket, cliente_nome_reduzido,cliente_nome,id_cliente 
//                                                  from cliente order by cliente_nome_reduzido", "cliente");

                D.Cliente = new Cliente();
                D.Cliente.Carregar(Guid.Empty, 100055, Cliente.IdTipoEnum.IdStore);

                string restricaoPessoaFisica = "";
                if (!Parametro.VenderSemEstoque)
                {
                    if (D.Cliente.ContribuinteIcms == false)
                        restricaoPessoaFisica = " permitir_venda_nao_contribuinte = 1 and ";
                    dtProdutos = D.Bd.DataTablePreenche(@"
                Select 
                        id_produto,
                        nome,
                        CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                        estoque,
                        referencia
                from
                        produto
                where
                        " + restricaoPessoaFisica + @" estoque > 0  
                order 
                        by nome",
                            "produto");
                }
                else
                {
                    if (D.Cliente.ContribuinteIcms == false)
                        restricaoPessoaFisica = " where permitir_venda_nao_contribuinte = 1 ";
                    dtProdutos = D.Bd.DataTablePreenche(@"
                Select
                        id_produto,
                        nome,
                        CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                        estoque,
                        referencia
                 from
                        produto
                 " + restricaoPessoaFisica + @"   
                 order by
                        nome",
                     "produto");
                }
                

                NeoTableStyle.MappingName = "produto";
                grdProduto.SetBackupDataSource(dtProdutos);
                grdProduto.DataSource = dtProdutos;
                grdProduto.Pager = NeoPager;
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