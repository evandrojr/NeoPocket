namespace Neopocket.Forms
{
    #region Using

    using System;
    using System.Data;
    using System.Windows.Forms;
    using System.Data.SqlServerCe;
    using Microsoft.WindowsCE.Forms;
    using Neopocket.Core;
    using Neopocket.Utils;
    using System.Text;
    using System.Drawing;

    #endregion

    /// <summary>
    /// Formulário de listagem de produtos
    /// </summary>
    public partial class FrmProdutoLista : FormBase
    {
        #region [ Construtor ]

        public FrmProdutoLista()
            : base(false)
        {
            InitializeComponent();
        }

        #endregion

        #region [ Load ]

        private void FrmProdutoLista_Load(object sender, EventArgs e)
        {
            try
            {
                #region Orientação da tela

                if (SystemSettings.ScreenOrientation != ScreenOrientation.Angle90)
                    SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;

                #endregion

                radCodigo.Checked = true;

                #region Cria as colunas da NeoGrid

                //Index 0 condicional
                if (Parametro.UsarReferenciaProduto)
                {
                    radCodigo.Text = "Referência";

                    #region referencia

                    Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colReferencia = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
                    colReferencia.HeaderText = "Ref";
                    colReferencia.Owner = grdProduto;
                    colReferencia.MappingName = "referencia";
                    colReferencia.ReadOnly = true;
                    NeoTableStyle.GridColumnStyles.Add(colReferencia);

                    #endregion
                }
                else // Usar código
                {
                    //Index 0
                    #region id_produto

                    Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colIdProduto = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
                    colIdProduto.HeaderText = "Cód";
                    colIdProduto.ReadOnly = true;
                    colIdProduto.Owner = grdProduto;
                    colIdProduto.MappingName = "id_produto";
                    NeoTableStyle.GridColumnStyles.Add(colIdProduto);
                }

                #endregion
                //Index 1
                #region nome

                Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colNome = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
                colNome.HeaderText = "Nome";
                colNome.Owner = grdProduto;
                colNome.MappingName = "nome";
                colNome.ReadOnly = true;
                colNome.Width = 100;
                NeoTableStyle.GridColumnStyles.Add(colNome);

                #endregion

                //Index 2
                #region preco_venda

                Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colPrecoVenda = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
                colPrecoVenda.HeaderText = "Preço";
                colPrecoVenda.Owner = grdProduto;
                colPrecoVenda.MappingName = "preco_venda";
                colPrecoVenda.ReadOnly = true;
                NeoTableStyle.GridColumnStyles.Add(colPrecoVenda);

                #endregion

                //Index 3
                #region estoque

                Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colEstoque = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
                colEstoque.HeaderText = "Estoque";
                colEstoque.Owner = grdProduto;
                colEstoque.MappingName = "estoque";
                colEstoque.ReadOnly = true;
                colEstoque.OnDrawColumnCell += new Neo.Pocket.Controls.NeoDataGridCustomColumnBase.NeoDataGridTextBoxColumnEventHandler(colEstoque_OnDrawColumnCell);
                NeoTableStyle.GridColumnStyles.Add(colEstoque);

                #endregion



                #endregion

                UpdateView();
                inputPanel.Enabled = false;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Cores da grid ]

        void colEstoque_OnDrawColumnCell(object sender, Neo.Pocket.Controls.NeoDataGridCustomColumnEventArgs e)
        {
            /*
            
            Int32? estoque = null;

            try
            {
                estoque = Int32.Parse(e.Column.Text);
            }
            catch { }

            if (estoque != null)
            {
                if (estoque < 10)
                    e.Column.BackColor = Color.Yellow;
                else if (estoque == 0)
                    e.Column.BackColor = Color.Red;
            }
             */
        }

        #endregion

        #region [ Atualiza a listagem ]

        private void UpdateView()
        {
            try
            {
                StringBuilder QueryBuilder = new StringBuilder();
                DataTable QueryResult;

                QueryBuilder.AppendLine(@"SELECT id_produto, nome,");
                QueryBuilder.AppendLine(@"CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, 4)) END AS preco_venda,");
                QueryBuilder.AppendLine(@"estoque,");
                QueryBuilder.AppendLine(@"referencia");
                QueryBuilder.AppendLine(@"FROM produto");
                QueryBuilder.AppendLine(@"ORDER BY nome");

                QueryResult = D.Bd.DataTablePreenche(QueryBuilder.ToString(), "produto");

                NeoTableStyle.MappingName = "produto";
                grdProduto.SetBackupDataSource(QueryResult);
                grdProduto.DataSource = QueryResult;
                grdProduto.Pager = NeoPager;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Pesquisa ]

        private void btnBuscar_Click(object sender, EventArgs e)
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
                else
                {
                    colIndex = 1;
                }

                grdProduto.Search(colIndex, txtProduto.Text, Neo.Pocket.Controls.NeoDataGridSearchMode.Like);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Focus do campo de busca do produto ]

        private void txtProduto_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtProduto_LostFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
        }

        #endregion

        #region [ Radio buttons ]

        private void radNome_CheckedChanged(object sender, EventArgs e)
        {
            txtProduto.Focus();
        }

        private void radCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtProduto.Focus();
        }

        #endregion
    }
}