using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using System.Reflection;
using Microsoft.WindowsCE.Forms;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de realização de pedido
    /// </summary>
    public partial class FrmPedido : FormBase
    {
        private Pedido pedidoToShow = null;
        // Irá ler das lista de Itens e mostrar no dataGrid
        private DataTable dtItem = new DataTable();
        private bool combosAtivar = false;
        private bool buscaExecutadaPrecisaPreencherGrid = false;

        //Coloquei aqui manualmente porque o designer não foi capaz de colocar automaticamente


        public bool CombosAtivar
        {
            get { return combosAtivar; }
            set
            {
                combosAtivar = value;
                if (combosAtivar)
                {
                    try
                    {
//                        cboTabelaPreco.Enabled = true;
//                        cboFormaPagamento.Enabled = true;
                        cboTabelaPreco.SelectedIndexChanged += this.cboTabelaPreco_SelectedIndexChanged;
                        cboFormaPagamento.SelectedIndexChanged += this.cboFormaPagamento_SelectedIndexChanged;
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        cboTabelaPreco.SelectedIndexChanged -= this.cboTabelaPreco_SelectedIndexChanged;
                        cboFormaPagamento.SelectedIndexChanged -= this.cboFormaPagamento_SelectedIndexChanged;
//                        cboTabelaPreco.Enabled = false;
//                        cboFormaPagamento.Enabled = false;
                    }
                    catch { }
                }
            }
        }

        private void dtItemMontar()
        {
            dtItem.Columns.Add("id_produto");
            dtItem.Columns.Add("referencia");
            dtItem.Columns.Add("nome");
            dtItem.Columns.Add("quantidade");
            dtItem.Columns.Add("valor_unitario");
            dtItem.Columns.Add("desconto");
            dtItem.Columns.Add("preco_venda");
        }

        #region [ Construtor ]

        public FrmPedido()
        {
            InitializeComponent();
        }

        public FrmPedido(Pedido pedidoToShow)
        {
            InitializeComponent();
            this.pedidoToShow = pedidoToShow;
            D.Pedido = pedidoToShow;
        }

        #endregion

        #region [ Load ]

        
        #region Monta o grid para produtos
        private void gridProductBuild(){

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

            gridProductUpdateView();

        }

#endregion

        private void FrmPedido_Load(object sender, EventArgs e)
        {
            CombosAtivar = false;
            //Troca o cancelar por sair se estiver apenas vendo relatório de pedido
            if (D.Pedido.SomenteParaLeitura)
                mnuCancelar.Text = "Sair";
            gridProductBuild();



            if (D.Cliente.Bdi == 9.99)
            {
                txtBdi.Visible = false;
                lblBdi.Visible = false;
            }
            inputPanel.Enabled = false;

            txtBdi.Text = D.Pedido.Bdi.ToString();
            gridProductUpdateView();
            try
            {
                txtDesconto.Text = D.Pedido.Desconto.ToString();
            }
            catch
            {
                txtDesconto.Text = "0";
            }
            txtCliente.Text = D.Cliente.NomeFantasia;

            if (D.Cliente.ClienteSemIdStore == true)
                txtCodigo.Text = "Sem cadastro";
            else
                txtCodigo.Text = D.Cliente.IdStore.ToString();

            txtCodVendedor.Text = D.Funcionario.Id.ToString();
            if (D.Acao == D.AcaoEnum.PedidoCadastro)
                txtData.Text = DateTime.Today.ToString("dd/MM/yyyy");
            else
                txtData.Text = D.Pedido.Data.ToString("dd/MM/yyyy");
            txtObservacao.Text = D.Pedido.Observacao;

            //Carrega os combos baseados na regra de negócio armazenadas na classe cliente
            D.Cliente.TabPrecoEFrmPagCbosPopularDefEspFinanc(Cliente.EnumComboAtualizar.Ambos);

            cboFormaPagamento.DataSource = D.Cliente.DtFormaPagamento;
            cboFormaPagamento.DisplayMember = "descricao";
            cboFormaPagamento.ValueMember = "id_forma_pagamento";

            cboTabelaPreco.DataSource = D.Cliente.DtTabelaPreco;
            cboTabelaPreco.DisplayMember = "descricao";
            cboTabelaPreco.ValueMember = "id_tabela_preco";

            if (D.Pedido.IdFormaPagamento != 0)
            {
                cboFormaPagamento.SelectedValue = (int)D.Pedido.IdFormaPagamento;
            }
            if (D.Pedido.IdTabelaPreco != 0)
            {
                cboTabelaPreco.SelectedValue = (int)D.Pedido.IdTabelaPreco;
            }

            if (!Parametro.UsarReferenciaProduto)
            {
                this.dgsItem.GridColumnStyles.Remove(gcsItemReferencia);
                //this.dgsProduto.GridColumnStyles.Remove(gcsProdutoReferencia);
            }
            else
            {
                radCodigo.Text = "Referência";
                //gcsProdutoIdProduto.Width = 0;
                gcsItemIdProduto.Width = 0;
            }
            //Preenche o grid dos Itens
            dtItemMontar();
            grdItem.DataSource = dtItem;


            PedidoDisplayAtualizar();

            CombosAtivar = true;


            // Trava os componentes para readonly em caso de exibição
            if (pedidoToShow != null)
            {
                LockForm();
            }
        }

        #endregion

        #region [ Helpers ]

        protected void LockForm()
        {
            mnuConcluir.Enabled = false;
            cboTabelaPreco.Enabled = false;
            cboFormaPagamento.Enabled = false;
            txtBdi.Enabled = false;
            txtDesconto.Enabled = false;
            btnEditar.Enabled = false;
            btnExcluir.Enabled = false;
            txtObservacao.Enabled = false;
            grdProduto.Enabled = false;
            btBuscar.Enabled = false;

            radCodigo.Enabled = false;
            radNome.Enabled = false;
            txtProduto.Enabled = false;
        }
 

        #endregion

        #region [ Busca ]

        private void btBuscar_Click(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;


            string restricaoPessoaFisica = "";
            if(D.Cliente.ContribuinteIcms==false)
                    restricaoPessoaFisica = " permitir_venda_nao_contribuinte = 1 and ";
            Cursor.Current = Cursors.WaitCursor;
            string sql = @"
            Select 
                    id_produto,
                    referencia,
                    nome,
                    CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                    estoque 
            from
                    produto
            where 
                   " + restricaoPessoaFisica;
            if (radNome.Checked)
                sql += " nome like '%" + Bd.S(txtProduto.Text) + "%' order by nome";
            else
            {
                if(!Validator.IsInteger(txtProduto.Text)){
                    MessageBox.Show("O código do produto só pode conter números");
                    return;
                }
                if (!Parametro.UsarReferenciaProduto)
                {
                    sql += " id_produto = '" + Bd.S(txtProduto.Text) + "' order by nome";
                }
                else
                {
                    sql += " referencia = '" + Bd.S(txtProduto.Text) + "' order by nome";
                }
            }


            if (txtProduto.Text == "")
            {
                restricaoPessoaFisica = "";
                if (D.Cliente.ContribuinteIcms == false)
                    restricaoPessoaFisica = " where permitir_venda_nao_contribuinte = 1 ";
                sql = @"SELECT
                            TOP (100) id_produto,
                                       referencia,
                                       nome,
                                       CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, " + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
                                       estoque 
                            FROM
                                produto
                           " + restricaoPessoaFisica + @"
                           ORDER BY
                                nome";
            }
            SqlCeCommand cmdProduto = new SqlCeCommand(sql, D.Bd.Con);
            SqlCeDataAdapter daProduto = new SqlCeDataAdapter(cmdProduto);
            DataSet dsProduto = new DataSet();
            daProduto.Fill(dsProduto, "produto");
            DataTable dtProdutos = dsProduto.Tables[0];
            grdProduto.DataSource = dtProdutos;
            Cursor.Current = Cursors.Default;
            if (dtProdutos.Rows.Count == 1)
            {
                adicionarProdutoDaBusca();
            }
            buscaExecutadaPrecisaPreencherGrid = true;
        }

        #endregion

        #region [ Adição do produto ]

        public int lastProductAddedIndex = -1;
        public static bool IsProductAdded = false;

        private void AddProduto()
        {
            int i;
            try
            {
                i = grdProduto.CurrentRowIndex;
                lastProductAddedIndex = i;
            }
            catch
            {
                return;
            }
            if (i == -1)
                return;

            Produto p = new Produto(D.Pedido);
            p.CarregarProduto(Convert.ToInt32(grdProduto[i, 0]));
            p.AcaoProduto = Produto.EnumAcaoProduto.ItemAdicionar;
            //FrmItem f = new FrmItem(p); // Reativar para que vá ao próximo item a ser vendido
            Util.FormExibirDialog(new FrmItem(p));
            //f.Closing += new CancelEventHandler(f_Closing);
        }

        //Está desativado aqui, pois estava dando erro
        /* Atualiza a posição do grid de produtos para o próximo produto abaixo do adicionado */
        //void f_Closing(object sender, CancelEventArgs e)
        //{
        //    if (IsProductAdded)
        //    {
        //        IsProductAdded = false;
        //        grdProduto.UnSelect(lastProductAddedIndex);
        //        grdProduto.Select(lastProductAddedIndex + 1);
        //        grdProduto.CurrentRowIndex = lastProductAddedIndex + 1;
        //    }
        //}

        #endregion

        private void adicionarProdutoDaBusca()
        {
            Produto p = new Produto(D.Pedido);
            p.CarregarProduto(Convert.ToInt32(grdProduto[0, 0]));
            p.AcaoProduto = Produto.EnumAcaoProduto.ItemAdicionar;
            FrmItem f = new FrmItem(p);
            f.Show();
        }

        private void mnuVoltar_Click(object sender, EventArgs e)
          {
            //Sai sem pedir confirmação se estiver apenas vendo relatório de pedido
              if (D.Pedido.SomenteParaLeitura)
                  Close();
              else
              {
                  DialogResult result = MessageBox.Show("Deseja realmente cancelar o cadastro do pedido?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                  if (result == DialogResult.Yes)
                      Close();
              }
        }

        private void PedidoDisplayAtualizar()
        {
            dtItem.Clear();
            for (int j = 0; j < D.Pedido.LstItem.Count; ++j)
            {
                Produto prd = D.Pedido.LstItem[j];
                if (prd.AcaoProduto == Produto.EnumAcaoProduto.ItemExcluir)
                    continue;

                DataRow linha = dtItem.NewRow();
                linha["id_produto"] = prd.Id;
                linha["nome"] = prd.Nome;
                linha["quantidade"] = prd.QuantidadeRequerida;
                linha["preco_venda"] = Fcn.DinheiroFormata(prd.PrecoVendaTotal);
                linha["valor_unitario"] = Fcn.DinheiroFormata(prd.PrecoUtilizado);
                linha["desconto"] = prd.Desconto;
                linha["referencia"] = prd.Referencia;
                dtItem.Rows.Add(linha);
            }
            txtTotalPedidos.Text = Fcn.DinheiroFormata(D.Pedido.Valor);
            lblPreco.Text = Fcn.DinheiroFormata(D.Pedido.ValorAntesBDIeDescontoPedido);
            lblPrecoFinal.Text = Fcn.DinheiroFormata(D.Pedido.Valor);
            txtDesconto.Text = Fcn.TruncateWithDecimals(D.Pedido.Desconto, 4).ToString();

            if (D.Pedido.IdEspecieFinanceira != 0)
            {
                if (D.Pedido.EspecieFinanceiraVerificaCredito == false || Parametro.VerificarCreditoVendaAPrazo == false)
                {
                    lblLimite.Text = "ilimitado";
                }
                else
                {
                    lblLimite.Text = Fcn.DinheiroFormata(D.Pedido.CreditoRestante);
                }
            }
        }

        private void gridProductUpdateView()
        {
            try
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
                        CASE unidade_fator WHEN 0 THEN 0 ELSE CAST(preco_venda / unidade_fator AS NUMERIC(10, "  + Parametro.ProdutoPrecoCasasDecimais + @")) END AS preco_venda,
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
                grdProduto.Pager = neoPager;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }

        }

        private void FrmPedido_GotFocus(object sender, EventArgs e)
        {
            //if (buscaExecutadaPrecisaPreencherGrid)
            //{
            //    buscaExecutadaPrecisaPreencherGrid = false;
            //    gridProductUpdateView();
            //}
            PedidoDisplayAtualizar();
        }

        private void btnExcluir_Click(object sender, EventArgs e) /* Excluir item de pedido - Funciona  */
        {
            int i = grdItem.CurrentRowIndex;
            if (i == -1)
                return;
            Produto p;

            if (MessageBox.Show("Deseja apagar o item do pedido?", "Atenção",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int j = 0; j < D.Pedido.LstItem.Count; ++j)
                {
                    p = D.Pedido.LstItem[j];
                    if (p.Id == Convert.ToInt32(grdItem[i, 0]))
                    {
                        p.AcaoProduto = Produto.EnumAcaoProduto.ItemExcluir;
                    }
                }
                PedidoDisplayAtualizar();
            }
        }

        private void txtProduto_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtProduto_LostFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int i = grdItem.CurrentRowIndex;
            if (i == -1)
                return;
            Produto p = null;

            for (int j = 0; j < D.Pedido.LstItem.Count; ++j)
            {
                p = D.Pedido.LstItem[j];
                if (p.Id == Convert.ToInt32(grdItem[i, 0]))
                {
                    p.AcaoProduto = Produto.EnumAcaoProduto.ItemAlterar;
                    break;
                }
            }
            FrmItem f = new FrmItem(p);
            Cursor.Current = Cursors.WaitCursor;
            f.Show();
            Cursor.Current = Cursors.Default;
        }

        private void mnuConcluir_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja realmente fechar o pedido?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                return;
            
            //Apenas para forçar a chamada do invalidate do desconto e da quantidade
            txtValidador.Focus();

            //Validar combos ....................
            if ((int)cboFormaPagamento.SelectedValue == 0)
            {
                MessageBox.Show("Selecione forma de pagamento !", "Neo");
                return;
            }

            //...................................

            // Verificar se tem itens para exluir
            int contaProdutosParaExcluir = 0;
            foreach (Produto p in D.Pedido.LstItem)
            {
                if (p.AcaoProduto == Produto.EnumAcaoProduto.ItemExcluir)
                {
                    contaProdutosParaExcluir++;
                }
            }

            if (contaProdutosParaExcluir < D.Pedido.LstItem.Count)
            {
                //Verifica se encontra algum erro no pedido
                string problemaDetectadoNoPedido = D.Pedido.ValidadePedidoChecar();
                if (problemaDetectadoNoPedido != String.Empty)
                {
                    MessageBox.Show(problemaDetectadoNoPedido);
                    return;
                }

                D.Pedido.Observacao = txtObservacao.Text;
                if (D.Pedido.ExecutarTarefasNoBd())
                    MessageBox.Show("Pedido salvo", "Neo");
                else
                    MessageBox.Show("Erro salvando pedido", "Neo");
                Close();
            }
            else
            {
                // Se tiver edição deletar pedido sem itens (pedido vazio)
                if (D.Acao == D.AcaoEnum.PedidoEdicao)
                {
                    result = MessageBox.Show("Deseja deletar o pedido?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        if (D.Pedido.PedidoExcluir())
                        {
                            D.Acao = D.AcaoEnum.PedidoADefinir;
                        }
                        else
                        {
                            MessageBox.Show("Erro ao deletar pedido", "Neo");
                        }
                    }
                }
            }
            //..Se não estiver Item no pedido..........
            if (D.Pedido.LstItem.Count == 0)
            {
                MessageBox.Show("Pedido sem item!", "Neo");
                return;
            }
            Close();
        }



        private void radNome_CheckedChanged(object sender, EventArgs e)
        {
            txtProduto.Focus();
        }

        private void radCodigo_CheckedChanged(object sender, EventArgs e)
        {
            txtProduto.Focus();
        }


        private void cboFormaPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdFormaPagamentoBk = D.Pedido.IdFormaPagamento;

            if (D.Pedido.IdFormaPagamento == (int)cboFormaPagamento.SelectedValue)
                return;
            //Teoricamente nunca deveria entrar no return;
            if (!CombosAtivar)
                return;
            try
            {
                CombosAtivar = false;
                D.Pedido.IdFormaPagamento = (int)cboFormaPagamento.SelectedValue;
            }
            catch (Exception ex)
            {
                CombosAtivar = false;
                D.Pedido.IdFormaPagamento = 0;
                D.Pedido.IdFormaPagamento = IdFormaPagamentoBk;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CombosAtivar = true;
            }

            //Tenta definir a espécie financeira
            if (D.Pedido.IdFormaPagamento > 0)
            {
                string sqlEspecieFinanceira = @"
                    SELECT
                            id_especie_financeira
                    FROM    
                            item_forma_pagamento
                    WHERE    
                            id_forma_pagamento = " + D.Pedido.IdFormaPagamento;
                try
                {
                    D.Pedido.IdEspecieFinanceira = D.Bd.I(sqlEspecieFinanceira);
                }
                catch (Exception ex)
                {
                    CombosAtivar = false;
                    D.Pedido.IdFormaPagamento = 0;
                    D.Pedido.IdFormaPagamento = IdFormaPagamentoBk;
                    if (cboFormaPagamento.Items.Count > 0)
                    {
                        try
                        {
                            //Tenta retornar ao que estava
                            cboFormaPagamento.SelectedValue = (object)D.Pedido.IdFormaPagamento;
                        }
                        catch
                        {
                            //Vai para a primeira opção se não conseguir
                            cboFormaPagamento.SelectedIndex = 0;
                        }
                    }
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    CombosAtivar = true;
                }
            }
        }


        private void atualizar()
        {
            lblPrecoFinal.Text = Fcn.DinheiroFormata(D.Pedido.Valor);
        }

        private void tbcPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcPedido.SelectedIndex != 0)
            {
                if ((int)cboFormaPagamento.SelectedValue == 0)
                {
                    MessageBox.Show("É necessário selecionar a forma de pagamento", "Neo");
                    tbcPedido.SelectedIndex = 0;
                    return;
                }
                try
                {
                    D.Pedido.IdFormaPagamento = (int)cboFormaPagamento.SelectedValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tbcPedido.SelectedIndex = 0;
                    return;
                }

                //Não deixa passar para próxima fase sem D.Pedido.IdEspecieFinanceira
                if (D.Pedido.IdEspecieFinanceira == 0)
                {
                    string sqlEspecieFinanceira = @"
                    SELECT
                            id_especie_financeira
                    FROM    
                            item_forma_pagamento
                    WHERE    
                            id_forma_pagamento = " + cboFormaPagamento.SelectedValue;
                    try
                    {
                        D.Pedido.IdEspecieFinanceira = D.Bd.I(sqlEspecieFinanceira);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        tbcPedido.SelectedIndex = 0;
                        return;
                    }

                    //Itens
                    if (tbcPedido.SelectedIndex == 2)
                    {
                        PedidoDisplayAtualizar();
                    }
                }
            }

            ////Recalcular o preço do pedido
            //if (tbcPedido.SelectedIndex == 3)
            //    if (Globals.Pedido.ValorAntesBDIeDescontoPedido > 0)
            PedidoDisplayAtualizar();
        }

        private void txtBdi_Validating(object sender, CancelEventArgs e)
        {
            double v = 0;

            try
            {
                v = Double.Parse(((TextBox)sender).Text, D.CultureInfoBRA);
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("BDI inválido", "Neo");
            }

            try
            {
                D.Pedido.Bdi = v;
            }
            catch (Exception ex)
            {
                //Coloquei pois o valor não estava voltando sozinho
                txtBdi.Text = D.Pedido.Bdi.ToString(D.CultureInfoBRA);

                e.Cancel = true;
                MessageBox.Show(ex.Message, "Neo");
            }
            PedidoDisplayAtualizar();
        }


        private void txtDesconto_Validating(object sender, CancelEventArgs e)
        {
            double v = 0;

            try
            {
                v = Double.Parse(((TextBox)sender).Text, D.CultureInfoBRA);
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("Desconto inválido", "Neo");
            }

            try
            {
                D.Pedido.Desconto = v;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message, "Neo");
            }
            PedidoDisplayAtualizar();
        }

        private void cboTabelaPreco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!CombosAtivar)
                return;
            try
            {
                if (D.Pedido.IdTabelaPreco == (int)cboTabelaPreco.SelectedValue)
                    return;
                CombosAtivar = false;
                D.Pedido.IdTabelaPreco = (int)cboTabelaPreco.SelectedValue;
                //Carrega os combos baseados na regra de negócio armazenadas na classe cliente
                D.Cliente.TabPrecoEFrmPagCbosPopularDefEspFinanc(Cliente.EnumComboAtualizar.FormaPagamento);

                cboFormaPagamento.DataSource = D.Cliente.DtFormaPagamento;
                cboFormaPagamento.DisplayMember = "descricao";
                cboFormaPagamento.ValueMember = "id_forma_pagamento";

                cboTabelaPreco.DataSource = D.Cliente.DtTabelaPreco;
                cboTabelaPreco.DisplayMember = "descricao";
                cboTabelaPreco.ValueMember = "id_tabela_preco";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CombosAtivar = true;
            }
        }

        private void lblBdi_Click(object sender, EventArgs e)
        {
            txtValidador.Focus();
            MessageBox.Show("Valor do BDI: " + Fcn.DinheiroFormata(D.Pedido.ValorAntesBDIeDescontoPedido * (D.Pedido.Bdi / 100)));
        }

        private void lblDesconto_Click(object sender, EventArgs e)
        {
            double valorComBdi;
            txtValidador.Focus();

            valorComBdi = D.Pedido.ValorAntesBDIeDescontoPedido + D.Pedido.ValorAntesBDIeDescontoPedido * (D.Pedido.Bdi / 100);
            MessageBox.Show("Valor do desconto: " + Fcn.DinheiroFormata(valorComBdi * (D.Pedido.Desconto / 100)));
        }

        private void grdProduto_DoubleClick(object sender, EventArgs e)
        {
            AddProduto();
        }

        private void btnTesteCargaInserir_Click(object sender, EventArgs e)
        {
            string qryProd =@"
            Select distinct top (" + txtTesteCargaQtdProdutos.Text + @")
                    *
            from
                    produto
            ";
            Produto p;
            DataTable dt = D.Bd.DataTablePreenche(qryProd);
            Cursor.Current = Cursors.WaitCursor;
            //double preco;
            int produtosNaoIncluidos = 0;
            int quantidadeRequerida = 0;
            int estoque;
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                estoque = Convert.ToInt32(dt.Rows[i]["estoque"]);
                //preco =
                //p = new Produto(D.Pedido, Convert.ToInt32(dt.Rows[i]["estoque"]), Convert.ToDouble(dt.Rows[i]["preco_venda"]), 0);
                if (!Parametro.VenderSemEstoque && estoque < 0)
                {
                    produtosNaoIncluidos++;
                    continue;
                }
                p = new Produto(D.Pedido);
                p.CarregarProduto(Convert.ToInt32(dt.Rows[i]["id_produto"]));
                p.AcaoProduto = Produto.EnumAcaoProduto.ItemAdicionar;
                if (Parametro.VenderSemEstoque && estoque < 0)
                    quantidadeRequerida = 1;
                else
                    quantidadeRequerida = estoque;
                try
                {
                    p.QuantidadeRequerida = quantidadeRequerida;
                    D.Pedido.LstItem.Add(p);
                }
                catch
                {
                    produtosNaoIncluidos++;
                    continue;
                }
            }
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Número de produtos adicionados: " + (dt.Rows.Count - produtosNaoIncluidos));
        }
    }
}

