﻿namespace Neopocket.Forms
{
    partial class FrmPedido
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grdProduto = new Neo.Pocket.Controls.NeoDataGrid();
            this.neoPager = new Neo.Pocket.Controls.NeoDataGridPager();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuConcluir = new System.Windows.Forms.MenuItem();
            this.mnuCancelar = new System.Windows.Forms.MenuItem();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.tbpObservacao = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.tbpItens = new System.Windows.Forms.TabPage();
            this.txtTotalPedidos = new System.Windows.Forms.TextBox();
            this.grdItem = new System.Windows.Forms.DataGrid();
            this.dgsItem = new System.Windows.Forms.DataGridTableStyle();
            this.gcsItemIdProduto = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemReferencia = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemNome = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemValorUnitario = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemPrecoVenda = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemQuantidade = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsItemDesconto = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblTT = new System.Windows.Forms.Label();
            this.lblLimite = new System.Windows.Forms.Label();
            this.lblLim = new System.Windows.Forms.Label();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.tbpProdutos = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radCodigo = new System.Windows.Forms.RadioButton();
            this.radNome = new System.Windows.Forms.RadioButton();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.txtValidador = new System.Windows.Forms.TextBox();
            this.btBuscar = new System.Windows.Forms.Button();
            this.lblBusca = new System.Windows.Forms.Label();
            this.tbpCapa = new System.Windows.Forms.TabPage();
            this.lblDesconto = new System.Windows.Forms.LinkLabel();
            this.lblBdi = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrecoFinal = new System.Windows.Forms.Label();
            this.lblPrecoDoPedido = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.txtDesconto = new System.Windows.Forms.TextBox();
            this.txtBdi = new System.Windows.Forms.TextBox();
            this.cboFormaPagamento = new System.Windows.Forms.ComboBox();
            this.cboTabelaPreco = new System.Windows.Forms.ComboBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtCodVendedor = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.lblTabela = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.lblCodVendedor = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.tbcPedido = new System.Windows.Forms.TabControl();
            this.tbpTeste = new System.Windows.Forms.TabPage();
            this.pnlTesteCarga = new System.Windows.Forms.Panel();
            this.btnTesteCargaInserir = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTesteCargaQtdProdutos = new System.Windows.Forms.TextBox();
            this.pnlRolaTela = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduto)).BeginInit();
            this.tbpObservacao.SuspendLayout();
            this.tbpItens.SuspendLayout();
            this.tbpProdutos.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tbpCapa.SuspendLayout();
            this.tbcPedido.SuspendLayout();
            this.tbpTeste.SuspendLayout();
            this.pnlTesteCarga.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdProduto
            // 
            this.grdProduto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdProduto.Location = new System.Drawing.Point(0, 48);
            this.grdProduto.Name = "grdProduto";
            this.grdProduto.Pager = this.neoPager;
            this.grdProduto.Size = new System.Drawing.Size(227, 165);
            this.grdProduto.TabIndex = 14;
            this.grdProduto.TableStyles.Add(this.NeoTableStyle);
            this.grdProduto.DoubleClick += new System.EventHandler(this.grdProduto_DoubleClick);
            // 
            // neoPager
            // 
            this.neoPager.BackColor = System.Drawing.SystemColors.Control;
            this.neoPager.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.neoPager.Location = new System.Drawing.Point(3, 217);
            this.neoPager.Name = "neoPager";
            this.neoPager.Owner = this.grdProduto;
            this.neoPager.PageIndex = 0;
            this.neoPager.PageSize = 100;
            this.neoPager.Size = new System.Drawing.Size(221, 23);
            this.neoPager.TabIndex = 14;
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuConcluir);
            this.mainMenu.MenuItems.Add(this.mnuCancelar);
            // 
            // mnuConcluir
            // 
            this.mnuConcluir.Text = "Concluir";
            this.mnuConcluir.Click += new System.EventHandler(this.mnuConcluir_Click);
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Text = "Cancelar";
            this.mnuCancelar.Click += new System.EventHandler(this.mnuVoltar_Click);
            // 
            // notification
            // 
            this.notification.Text = "notification";
            // 
            // tbpObservacao
            // 
            this.tbpObservacao.Controls.Add(this.label1);
            this.tbpObservacao.Controls.Add(this.txtObservacao);
            this.tbpObservacao.Location = new System.Drawing.Point(0, 0);
            this.tbpObservacao.Name = "tbpObservacao";
            this.tbpObservacao.Size = new System.Drawing.Size(219, 239);
            this.tbpObservacao.Text = "Observação";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(41, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 54);
            this.label1.Text = "Observações para o pedido:";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(0, 124);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(224, 118);
            this.txtObservacao.TabIndex = 12;
            // 
            // tbpItens
            // 
            this.tbpItens.Controls.Add(this.txtTotalPedidos);
            this.tbpItens.Controls.Add(this.grdItem);
            this.tbpItens.Controls.Add(this.lblTT);
            this.tbpItens.Controls.Add(this.lblLimite);
            this.tbpItens.Controls.Add(this.lblLim);
            this.tbpItens.Controls.Add(this.btnExcluir);
            this.tbpItens.Controls.Add(this.btnEditar);
            this.tbpItens.Location = new System.Drawing.Point(0, 0);
            this.tbpItens.Name = "tbpItens";
            this.tbpItens.Size = new System.Drawing.Size(219, 239);
            this.tbpItens.Text = "Itens";
            // 
            // txtTotalPedidos
            // 
            this.txtTotalPedidos.Location = new System.Drawing.Point(149, 218);
            this.txtTotalPedidos.Name = "txtTotalPedidos";
            this.txtTotalPedidos.Size = new System.Drawing.Size(72, 21);
            this.txtTotalPedidos.TabIndex = 6;
            // 
            // grdItem
            // 
            this.grdItem.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdItem.Location = new System.Drawing.Point(0, 0);
            this.grdItem.Name = "grdItem";
            this.grdItem.Size = new System.Drawing.Size(224, 191);
            this.grdItem.TabIndex = 0;
            this.grdItem.TableStyles.Add(this.dgsItem);
            // 
            // dgsItem
            // 
            this.dgsItem.GridColumnStyles.Add(this.gcsItemIdProduto);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemReferencia);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemNome);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemValorUnitario);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemPrecoVenda);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemQuantidade);
            this.dgsItem.GridColumnStyles.Add(this.gcsItemDesconto);
            // 
            // gcsItemIdProduto
            // 
            this.gcsItemIdProduto.Format = "";
            this.gcsItemIdProduto.HeaderText = "Cod";
            this.gcsItemIdProduto.MappingName = "id_produto";
            // 
            // gcsItemReferencia
            // 
            this.gcsItemReferencia.Format = "";
            this.gcsItemReferencia.HeaderText = "Referência";
            this.gcsItemReferencia.MappingName = "referencia";
            this.gcsItemReferencia.Width = 65;
            // 
            // gcsItemNome
            // 
            this.gcsItemNome.Format = "";
            this.gcsItemNome.HeaderText = "Produto";
            this.gcsItemNome.MappingName = "nome";
            this.gcsItemNome.Width = 55;
            // 
            // gcsItemValorUnitario
            // 
            this.gcsItemValorUnitario.Format = "";
            this.gcsItemValorUnitario.HeaderText = "Preço";
            this.gcsItemValorUnitario.MappingName = "valor_unitario";
            this.gcsItemValorUnitario.Width = 47;
            // 
            // gcsItemPrecoVenda
            // 
            this.gcsItemPrecoVenda.Format = "";
            this.gcsItemPrecoVenda.HeaderText = "Total";
            this.gcsItemPrecoVenda.MappingName = "preco_venda";
            this.gcsItemPrecoVenda.Width = 55;
            // 
            // gcsItemQuantidade
            // 
            this.gcsItemQuantidade.Format = "";
            this.gcsItemQuantidade.HeaderText = "Qtd";
            this.gcsItemQuantidade.MappingName = "quantidade";
            this.gcsItemQuantidade.Width = 40;
            // 
            // gcsItemDesconto
            // 
            this.gcsItemDesconto.Format = "";
            this.gcsItemDesconto.HeaderText = "Desconto";
            this.gcsItemDesconto.MappingName = "desconto";
            this.gcsItemDesconto.Width = 55;
            // 
            // lblTT
            // 
            this.lblTT.Location = new System.Drawing.Point(110, 219);
            this.lblTT.Name = "lblTT";
            this.lblTT.Size = new System.Drawing.Size(43, 20);
            this.lblTT.Text = "Total:";
            // 
            // lblLimite
            // 
            this.lblLimite.Location = new System.Drawing.Point(43, 222);
            this.lblLimite.Name = "lblLimite";
            this.lblLimite.Size = new System.Drawing.Size(81, 20);
            // 
            // lblLim
            // 
            this.lblLim.Location = new System.Drawing.Point(3, 222);
            this.lblLim.Name = "lblLim";
            this.lblLim.Size = new System.Drawing.Size(49, 20);
            this.lblLim.Text = "Limite:";
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(150, 195);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(72, 20);
            this.btnExcluir.TabIndex = 2;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(3, 197);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 20);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // tbpProdutos
            // 
            this.tbpProdutos.BackColor = System.Drawing.SystemColors.Control;
            this.tbpProdutos.Controls.Add(this.neoPager);
            this.tbpProdutos.Controls.Add(this.panel1);
            this.tbpProdutos.Controls.Add(this.grdProduto);
            this.tbpProdutos.Controls.Add(this.txtProduto);
            this.tbpProdutos.Controls.Add(this.txtValidador);
            this.tbpProdutos.Controls.Add(this.btBuscar);
            this.tbpProdutos.Controls.Add(this.lblBusca);
            this.tbpProdutos.Location = new System.Drawing.Point(0, 0);
            this.tbpProdutos.Name = "tbpProdutos";
            this.tbpProdutos.Size = new System.Drawing.Size(219, 239);
            this.tbpProdutos.Text = "Produtos";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.radCodigo);
            this.panel1.Controls.Add(this.radNome);
            this.panel1.Location = new System.Drawing.Point(3, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 22);
            // 
            // radCodigo
            // 
            this.radCodigo.Checked = true;
            this.radCodigo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.radCodigo.Location = new System.Drawing.Point(66, 1);
            this.radCodigo.Name = "radCodigo";
            this.radCodigo.Size = new System.Drawing.Size(67, 20);
            this.radCodigo.TabIndex = 2;
            this.radCodigo.Text = "Código";
            this.radCodigo.CheckedChanged += new System.EventHandler(this.radCodigo_CheckedChanged);
            // 
            // radNome
            // 
            this.radNome.ForeColor = System.Drawing.SystemColors.MenuText;
            this.radNome.Location = new System.Drawing.Point(3, 1);
            this.radNome.Name = "radNome";
            this.radNome.Size = new System.Drawing.Size(60, 20);
            this.radNome.TabIndex = 1;
            this.radNome.TabStop = false;
            this.radNome.Text = "Nome";
            this.radNome.CheckedChanged += new System.EventHandler(this.radNome_CheckedChanged);
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(68, 1);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(158, 21);
            this.txtProduto.TabIndex = 0;
            this.txtProduto.GotFocus += new System.EventHandler(this.txtProduto_GotFocus);
            this.txtProduto.LostFocus += new System.EventHandler(this.txtProduto_LostFocus);
            // 
            // txtValidador
            // 
            this.txtValidador.Location = new System.Drawing.Point(106, 111);
            this.txtValidador.Name = "txtValidador";
            this.txtValidador.Size = new System.Drawing.Size(28, 21);
            this.txtValidador.TabIndex = 27;
            // 
            // btBuscar
            // 
            this.btBuscar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btBuscar.Location = new System.Drawing.Point(171, 24);
            this.btBuscar.Name = "btBuscar";
            this.btBuscar.Size = new System.Drawing.Size(53, 22);
            this.btBuscar.TabIndex = 4;
            this.btBuscar.Text = "Buscar";
            this.btBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // lblBusca
            // 
            this.lblBusca.Location = new System.Drawing.Point(1, 3);
            this.lblBusca.Name = "lblBusca";
            this.lblBusca.Size = new System.Drawing.Size(56, 20);
            this.lblBusca.Text = "Produto";
            this.lblBusca.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbpCapa
            // 
            this.tbpCapa.Controls.Add(this.lblDesconto);
            this.tbpCapa.Controls.Add(this.lblBdi);
            this.tbpCapa.Controls.Add(this.label4);
            this.tbpCapa.Controls.Add(this.lblPrecoFinal);
            this.tbpCapa.Controls.Add(this.lblPrecoDoPedido);
            this.tbpCapa.Controls.Add(this.lblPreco);
            this.tbpCapa.Controls.Add(this.txtDesconto);
            this.tbpCapa.Controls.Add(this.txtBdi);
            this.tbpCapa.Controls.Add(this.cboFormaPagamento);
            this.tbpCapa.Controls.Add(this.cboTabelaPreco);
            this.tbpCapa.Controls.Add(this.txtData);
            this.tbpCapa.Controls.Add(this.txtCodVendedor);
            this.tbpCapa.Controls.Add(this.txtCodigo);
            this.tbpCapa.Controls.Add(this.txtCliente);
            this.tbpCapa.Controls.Add(this.lblFormaPagamento);
            this.tbpCapa.Controls.Add(this.lblTabela);
            this.tbpCapa.Controls.Add(this.lblData);
            this.tbpCapa.Controls.Add(this.lblCodVendedor);
            this.tbpCapa.Controls.Add(this.lblCodigo);
            this.tbpCapa.Controls.Add(this.lblCliente);
            this.tbpCapa.Location = new System.Drawing.Point(0, 0);
            this.tbpCapa.Name = "tbpCapa";
            this.tbpCapa.Size = new System.Drawing.Size(227, 242);
            this.tbpCapa.Text = "Capa";
            // 
            // lblDesconto
            // 
            this.lblDesconto.ForeColor = System.Drawing.Color.Navy;
            this.lblDesconto.Location = new System.Drawing.Point(1, 219);
            this.lblDesconto.Name = "lblDesconto";
            this.lblDesconto.Size = new System.Drawing.Size(45, 20);
            this.lblDesconto.TabIndex = 36;
            this.lblDesconto.Text = "Des %";
            this.lblDesconto.Click += new System.EventHandler(this.lblDesconto_Click);
            // 
            // lblBdi
            // 
            this.lblBdi.ForeColor = System.Drawing.Color.Navy;
            this.lblBdi.Location = new System.Drawing.Point(1, 193);
            this.lblBdi.Name = "lblBdi";
            this.lblBdi.Size = new System.Drawing.Size(42, 20);
            this.lblBdi.TabIndex = 35;
            this.lblBdi.Text = "BDI %";
            this.lblBdi.Click += new System.EventHandler(this.lblBdi_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(101, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 20);
            this.label4.Text = "Total";
            // 
            // lblPrecoFinal
            // 
            this.lblPrecoFinal.Location = new System.Drawing.Point(134, 222);
            this.lblPrecoFinal.Name = "lblPrecoFinal";
            this.lblPrecoFinal.Size = new System.Drawing.Size(67, 20);
            this.lblPrecoFinal.Text = "R$ 0,0";
            // 
            // lblPrecoDoPedido
            // 
            this.lblPrecoDoPedido.Location = new System.Drawing.Point(101, 193);
            this.lblPrecoDoPedido.Name = "lblPrecoDoPedido";
            this.lblPrecoDoPedido.Size = new System.Drawing.Size(32, 20);
            this.lblPrecoDoPedido.Text = "Itens";
            // 
            // lblPreco
            // 
            this.lblPreco.Location = new System.Drawing.Point(134, 193);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(64, 20);
            this.lblPreco.Text = "R$ 0,0";
            // 
            // txtDesconto
            // 
            this.txtDesconto.Location = new System.Drawing.Point(48, 218);
            this.txtDesconto.Name = "txtDesconto";
            this.txtDesconto.Size = new System.Drawing.Size(49, 21);
            this.txtDesconto.TabIndex = 26;
            this.txtDesconto.Validating += new System.ComponentModel.CancelEventHandler(this.txtDesconto_Validating);
            // 
            // txtBdi
            // 
            this.txtBdi.Location = new System.Drawing.Point(48, 191);
            this.txtBdi.Name = "txtBdi";
            this.txtBdi.Size = new System.Drawing.Size(49, 21);
            this.txtBdi.TabIndex = 21;
            this.txtBdi.Validating += new System.ComponentModel.CancelEventHandler(this.txtBdi_Validating);
            // 
            // cboFormaPagamento
            // 
            this.cboFormaPagamento.Items.Add("(nenhuma)");
            this.cboFormaPagamento.Location = new System.Drawing.Point(48, 163);
            this.cboFormaPagamento.Name = "cboFormaPagamento";
            this.cboFormaPagamento.Size = new System.Drawing.Size(175, 22);
            this.cboFormaPagamento.TabIndex = 12;
            this.cboFormaPagamento.SelectedIndexChanged += new System.EventHandler(this.cboFormaPagamento_SelectedIndexChanged);
            // 
            // cboTabelaPreco
            // 
            this.cboTabelaPreco.Items.Add("(nenhuma)");
            this.cboTabelaPreco.Location = new System.Drawing.Point(48, 131);
            this.cboTabelaPreco.Name = "cboTabelaPreco";
            this.cboTabelaPreco.Size = new System.Drawing.Size(175, 22);
            this.cboTabelaPreco.TabIndex = 11;
            this.cboTabelaPreco.SelectedIndexChanged += new System.EventHandler(this.cboTabelaPreco_SelectedIndexChanged);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(48, 100);
            this.txtData.Name = "txtData";
            this.txtData.ReadOnly = true;
            this.txtData.Size = new System.Drawing.Size(175, 21);
            this.txtData.TabIndex = 10;
            // 
            // txtCodVendedor
            // 
            this.txtCodVendedor.Location = new System.Drawing.Point(48, 67);
            this.txtCodVendedor.Name = "txtCodVendedor";
            this.txtCodVendedor.ReadOnly = true;
            this.txtCodVendedor.Size = new System.Drawing.Size(175, 21);
            this.txtCodVendedor.TabIndex = 9;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(48, 34);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(175, 21);
            this.txtCodigo.TabIndex = 8;
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(48, 3);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.ReadOnly = true;
            this.txtCliente.Size = new System.Drawing.Size(175, 21);
            this.txtCliente.TabIndex = 7;
            // 
            // lblFormaPagamento
            // 
            this.lblFormaPagamento.Location = new System.Drawing.Point(3, 161);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(37, 20);
            this.lblFormaPagamento.Text = "Pgto:";
            // 
            // lblTabela
            // 
            this.lblTabela.Location = new System.Drawing.Point(3, 130);
            this.lblTabela.Name = "lblTabela";
            this.lblTabela.Size = new System.Drawing.Size(48, 20);
            this.lblTabela.Text = "Tabela:";
            // 
            // lblData
            // 
            this.lblData.Location = new System.Drawing.Point(3, 99);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(37, 20);
            this.lblData.Text = "Data:";
            // 
            // lblCodVendedor
            // 
            this.lblCodVendedor.Location = new System.Drawing.Point(3, 68);
            this.lblCodVendedor.Name = "lblCodVendedor";
            this.lblCodVendedor.Size = new System.Drawing.Size(37, 20);
            this.lblCodVendedor.Text = "Vend:";
            // 
            // lblCodigo
            // 
            this.lblCodigo.Location = new System.Drawing.Point(3, 37);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(67, 20);
            this.lblCodigo.Text = "Código:";
            // 
            // lblCliente
            // 
            this.lblCliente.Location = new System.Drawing.Point(3, 6);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(57, 20);
            this.lblCliente.Text = "Cliente:";
            // 
            // tbcPedido
            // 
            this.tbcPedido.Controls.Add(this.tbpCapa);
            this.tbcPedido.Controls.Add(this.tbpProdutos);
            this.tbcPedido.Controls.Add(this.tbpItens);
            this.tbcPedido.Controls.Add(this.tbpObservacao);
            this.tbcPedido.Controls.Add(this.tbpTeste);
            this.tbcPedido.Location = new System.Drawing.Point(0, 0);
            this.tbcPedido.Name = "tbcPedido";
            this.tbcPedido.SelectedIndex = 0;
            this.tbcPedido.Size = new System.Drawing.Size(227, 265);
            this.tbcPedido.TabIndex = 0;
            this.tbcPedido.SelectedIndexChanged += new System.EventHandler(this.tbcPedido_SelectedIndexChanged);
            // 
            // tbpTeste
            // 
            this.tbpTeste.Controls.Add(this.pnlTesteCarga);
            this.tbpTeste.Location = new System.Drawing.Point(0, 0);
            this.tbpTeste.Name = "tbpTeste";
            this.tbpTeste.Size = new System.Drawing.Size(227, 242);
            this.tbpTeste.Text = "Testes";
            // 
            // pnlTesteCarga
            // 
            this.pnlTesteCarga.BackColor = System.Drawing.Color.SpringGreen;
            this.pnlTesteCarga.Controls.Add(this.btnTesteCargaInserir);
            this.pnlTesteCarga.Controls.Add(this.label5);
            this.pnlTesteCarga.Controls.Add(this.label3);
            this.pnlTesteCarga.Controls.Add(this.txtTesteCargaQtdProdutos);
            this.pnlTesteCarga.Location = new System.Drawing.Point(3, 4);
            this.pnlTesteCarga.Name = "pnlTesteCarga";
            this.pnlTesteCarga.Size = new System.Drawing.Size(221, 142);
            this.pnlTesteCarga.Visible = false;
            // 
            // btnTesteCargaInserir
            // 
            this.btnTesteCargaInserir.Location = new System.Drawing.Point(66, 110);
            this.btnTesteCargaInserir.Name = "btnTesteCargaInserir";
            this.btnTesteCargaInserir.Size = new System.Drawing.Size(72, 20);
            this.btnTesteCargaInserir.TabIndex = 3;
            this.btnTesteCargaInserir.Text = "Inserir";
            this.btnTesteCargaInserir.Click += new System.EventHandler(this.btnTesteCargaInserir_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(5, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 59);
            this.label5.Text = "Número de produtos a serem inseridos:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.Text = "Teste de carga:";
            // 
            // txtTesteCargaQtdProdutos
            // 
            this.txtTesteCargaQtdProdutos.Location = new System.Drawing.Point(148, 52);
            this.txtTesteCargaQtdProdutos.Name = "txtTesteCargaQtdProdutos";
            this.txtTesteCargaQtdProdutos.Size = new System.Drawing.Size(68, 21);
            this.txtTesteCargaQtdProdutos.TabIndex = 0;
            this.txtTesteCargaQtdProdutos.Text = "100";
            // 
            // pnlRolaTela
            // 
            this.pnlRolaTela.Location = new System.Drawing.Point(3, 271);
            this.pnlRolaTela.Name = "pnlRolaTela";
            this.pnlRolaTela.Size = new System.Drawing.Size(195, 100);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "label2";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(217, 79);
            // 
            // FrmPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 350);
            this.ControlBox = false;
            this.Controls.Add(this.pnlRolaTela);
            this.Controls.Add(this.tbcPedido);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmPedido";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmPedido_Load);
            this.GotFocus += new System.EventHandler(this.FrmPedido_GotFocus);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduto)).EndInit();
            this.tbpObservacao.ResumeLayout(false);
            this.tbpItens.ResumeLayout(false);
            this.tbpProdutos.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tbpCapa.ResumeLayout(false);
            this.tbcPedido.ResumeLayout(false);
            this.tbpTeste.ResumeLayout(false);
            this.pnlTesteCarga.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuConcluir;
        private System.Windows.Forms.MenuItem mnuCancelar;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.TabPage tbpObservacao;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.TabPage tbpItens;
        private System.Windows.Forms.TextBox txtTotalPedidos;
        private System.Windows.Forms.DataGrid grdItem;
        private System.Windows.Forms.DataGridTableStyle dgsItem;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemIdProduto;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemReferencia;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemNome;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemValorUnitario;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemPrecoVenda;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemQuantidade;
        private System.Windows.Forms.DataGridTextBoxColumn gcsItemDesconto;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.Label lblLimite;
        private System.Windows.Forms.Label lblLim;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TabPage tbpProdutos;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radCodigo;
        private System.Windows.Forms.RadioButton radNome;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.TextBox txtValidador;
        private System.Windows.Forms.Button btBuscar;
        private System.Windows.Forms.Label lblBusca;
        private System.Windows.Forms.TabPage tbpCapa;
        private System.Windows.Forms.LinkLabel lblDesconto;
        private System.Windows.Forms.LinkLabel lblBdi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPrecoFinal;
        private System.Windows.Forms.Label lblPrecoDoPedido;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.TextBox txtDesconto;
        private System.Windows.Forms.TextBox txtBdi;
        private System.Windows.Forms.ComboBox cboFormaPagamento;
        private System.Windows.Forms.ComboBox cboTabelaPreco;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtCodVendedor;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.Label lblTabela;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblCodVendedor;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TabControl tbcPedido;
        private System.Windows.Forms.Panel pnlRolaTela;
        private System.Windows.Forms.Label label1;
        private Neo.Pocket.Controls.NeoDataGrid grdProduto;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
        private Neo.Pocket.Controls.NeoDataGridPager neoPager;
        private System.Windows.Forms.TabPage tbpTeste;
        private System.Windows.Forms.Panel pnlTesteCarga;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTesteCargaInserir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTesteCargaQtdProdutos;
    }
}
