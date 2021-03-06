﻿namespace NeoSync
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabPageOutros = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkEnviarTodosOsClientesParaTodosOsVendedores = new System.Windows.Forms.CheckBox();
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes = new System.Windows.Forms.CheckBox();
            this.txtPedidoQtdMaxAntesSincronizacao = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lstLoja = new System.Windows.Forms.ListBox();
            this.chkPermitirEscolherTabelaDePreco = new System.Windows.Forms.CheckBox();
            this.chkPermitirPedidoComEstoqueZerado = new System.Windows.Forms.CheckBox();
            this.chkVerificarCreditosParaVendaAPrazo = new System.Windows.Forms.CheckBox();
            this.chkVendaClienteSemReferencia = new System.Windows.Forms.CheckBox();
            this.TabPagePrincipal = new System.Windows.Forms.TabPage();
            this.TxtMsg = new System.Windows.Forms.TextBox();
            this.TabPageFtp = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTempoLimiteEspera = new System.Windows.Forms.MaskedTextBox();
            this.txtTempoEntreSicronizacoes = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtServidor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDiretorio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProximaExecucao = new System.Windows.Forms.Label();
            this.BtnSincronizar = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnReverter = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tmrSincronizacao = new System.Windows.Forms.Timer(this.components);
            this.tmrPiscar = new System.Windows.Forms.Timer(this.components);
            this.btnChegouCadastro = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.TabPageOutros.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.TabPagePrincipal.SuspendLayout();
            this.TabPageFtp.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 466);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(665, 22);
            this.statusStrip.TabIndex = 0;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(124, 17);
            this.lblStatus.Text = "Mensagem de status";
            // 
            // TabControl
            // 
            this.TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl.Controls.Add(this.TabPageOutros);
            this.TabControl.Controls.Add(this.TabPagePrincipal);
            this.TabControl.Controls.Add(this.TabPageFtp);
            this.TabControl.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl.Location = new System.Drawing.Point(0, 1);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(667, 358);
            this.TabControl.TabIndex = 1;
            // 
            // TabPageOutros
            // 
            this.TabPageOutros.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TabPageOutros.BackgroundImage")));
            this.TabPageOutros.Controls.Add(this.groupBox3);
            this.TabPageOutros.Location = new System.Drawing.Point(4, 22);
            this.TabPageOutros.Name = "TabPageOutros";
            this.TabPageOutros.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageOutros.Size = new System.Drawing.Size(659, 332);
            this.TabPageOutros.TabIndex = 2;
            this.TabPageOutros.Text = "Loja";
            this.TabPageOutros.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkEnviarTodosOsClientesParaTodosOsVendedores);
            this.groupBox3.Controls.Add(this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes);
            this.groupBox3.Controls.Add(this.txtPedidoQtdMaxAntesSincronizacao);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lstLoja);
            this.groupBox3.Controls.Add(this.chkPermitirEscolherTabelaDePreco);
            this.groupBox3.Controls.Add(this.chkPermitirPedidoComEstoqueZerado);
            this.groupBox3.Controls.Add(this.chkVerificarCreditosParaVendaAPrazo);
            this.groupBox3.Controls.Add(this.chkVendaClienteSemReferencia);
            this.groupBox3.Location = new System.Drawing.Point(9, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(640, 307);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Parâmetros do Neo Store";
            // 
            // chkEnviarTodosOsClientesParaTodosOsVendedores
            // 
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.AutoSize = true;
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.Location = new System.Drawing.Point(23, 257);
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.Name = "chkEnviarTodosOsClientesParaTodosOsVendedores";
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.Size = new System.Drawing.Size(314, 17);
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.TabIndex = 10;
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.Text = "Enviar todos os clientes para todos os vendedores";
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.UseVisualStyleBackColor = true;
            this.chkEnviarTodosOsClientesParaTodosOsVendedores.CheckedChanged += new System.EventHandler(this.chkEnviarTodosOsClientesParaTodosOsVendedores_CheckedChanged);
            // 
            // chkPeriodicamenteEnviarCNPJDeTodosOsClientes
            // 
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.AutoSize = true;
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Location = new System.Drawing.Point(23, 230);
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Name = "chkPeriodicamenteEnviarCNPJDeTodosOsClientes";
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Size = new System.Drawing.Size(304, 17);
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.TabIndex = 9;
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Text = "Periodicamente enviar CNPJ de todos os clientes";
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.UseVisualStyleBackColor = true;
            this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes.CheckedChanged += new System.EventHandler(this.chkPeriodicamenteEnviarCNPJDeTodosOsClientes_CheckedChanged);
            // 
            // txtPedidoQtdMaxAntesSincronizacao
            // 
            this.txtPedidoQtdMaxAntesSincronizacao.Location = new System.Drawing.Point(249, 280);
            this.txtPedidoQtdMaxAntesSincronizacao.Mask = "999";
            this.txtPedidoQtdMaxAntesSincronizacao.Name = "txtPedidoQtdMaxAntesSincronizacao";
            this.txtPedidoQtdMaxAntesSincronizacao.Size = new System.Drawing.Size(42, 21);
            this.txtPedidoQtdMaxAntesSincronizacao.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(21, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(223, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Qtd máx pedidos antes de sincronizar";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Selecione a loja:";
            // 
            // lstLoja
            // 
            this.lstLoja.FormattingEnabled = true;
            this.lstLoja.Location = new System.Drawing.Point(23, 47);
            this.lstLoja.Name = "lstLoja";
            this.lstLoja.Size = new System.Drawing.Size(255, 95);
            this.lstLoja.TabIndex = 4;
            // 
            // chkPermitirEscolherTabelaDePreco
            // 
            this.chkPermitirEscolherTabelaDePreco.AutoSize = true;
            this.chkPermitirEscolherTabelaDePreco.Location = new System.Drawing.Point(353, 206);
            this.chkPermitirEscolherTabelaDePreco.Name = "chkPermitirEscolherTabelaDePreco";
            this.chkPermitirEscolherTabelaDePreco.Size = new System.Drawing.Size(216, 17);
            this.chkPermitirEscolherTabelaDePreco.TabIndex = 3;
            this.chkPermitirEscolherTabelaDePreco.Text = "Permitir escolher tabela de preço";
            this.chkPermitirEscolherTabelaDePreco.UseVisualStyleBackColor = true;
            this.chkPermitirEscolherTabelaDePreco.Visible = false;
            // 
            // chkPermitirPedidoComEstoqueZerado
            // 
            this.chkPermitirPedidoComEstoqueZerado.AutoSize = true;
            this.chkPermitirPedidoComEstoqueZerado.Location = new System.Drawing.Point(23, 199);
            this.chkPermitirPedidoComEstoqueZerado.Name = "chkPermitirPedidoComEstoqueZerado";
            this.chkPermitirPedidoComEstoqueZerado.Size = new System.Drawing.Size(190, 17);
            this.chkPermitirPedidoComEstoqueZerado.TabIndex = 2;
            this.chkPermitirPedidoComEstoqueZerado.Text = "Permitir pedido sem estoque";
            this.chkPermitirPedidoComEstoqueZerado.UseVisualStyleBackColor = true;
            // 
            // chkVerificarCreditosParaVendaAPrazo
            // 
            this.chkVerificarCreditosParaVendaAPrazo.AutoSize = true;
            this.chkVerificarCreditosParaVendaAPrazo.Location = new System.Drawing.Point(23, 167);
            this.chkVerificarCreditosParaVendaAPrazo.Name = "chkVerificarCreditosParaVendaAPrazo";
            this.chkVerificarCreditosParaVendaAPrazo.Size = new System.Drawing.Size(239, 17);
            this.chkVerificarCreditosParaVendaAPrazo.TabIndex = 1;
            this.chkVerificarCreditosParaVendaAPrazo.Text = "Verificar créditos para venda a prazo";
            this.chkVerificarCreditosParaVendaAPrazo.UseVisualStyleBackColor = true;
            // 
            // chkVendaClienteSemReferencia
            // 
            this.chkVendaClienteSemReferencia.AutoSize = true;
            this.chkVendaClienteSemReferencia.Location = new System.Drawing.Point(353, 242);
            this.chkVendaClienteSemReferencia.Name = "chkVendaClienteSemReferencia";
            this.chkVendaClienteSemReferencia.Size = new System.Drawing.Size(223, 17);
            this.chkVendaClienteSemReferencia.TabIndex = 0;
            this.chkVendaClienteSemReferencia.Text = "Venda para cliente sem referencia";
            this.chkVendaClienteSemReferencia.UseVisualStyleBackColor = true;
            this.chkVendaClienteSemReferencia.Visible = false;
            // 
            // TabPagePrincipal
            // 
            this.TabPagePrincipal.Controls.Add(this.TxtMsg);
            this.TabPagePrincipal.Location = new System.Drawing.Point(4, 22);
            this.TabPagePrincipal.Name = "TabPagePrincipal";
            this.TabPagePrincipal.Padding = new System.Windows.Forms.Padding(3);
            this.TabPagePrincipal.Size = new System.Drawing.Size(659, 332);
            this.TabPagePrincipal.TabIndex = 0;
            this.TabPagePrincipal.Text = "Monitor de eventos";
            this.TabPagePrincipal.UseVisualStyleBackColor = true;
            // 
            // TxtMsg
            // 
            this.TxtMsg.BackColor = System.Drawing.Color.White;
            this.TxtMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtMsg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtMsg.Location = new System.Drawing.Point(3, 16);
            this.TxtMsg.Multiline = true;
            this.TxtMsg.Name = "TxtMsg";
            this.TxtMsg.ReadOnly = true;
            this.TxtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtMsg.Size = new System.Drawing.Size(653, 313);
            this.TxtMsg.TabIndex = 3;
            // 
            // TabPageFtp
            // 
            this.TabPageFtp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TabPageFtp.BackgroundImage")));
            this.TabPageFtp.Controls.Add(this.groupBox2);
            this.TabPageFtp.Controls.Add(this.groupBox1);
            this.TabPageFtp.Location = new System.Drawing.Point(4, 22);
            this.TabPageFtp.Name = "TabPageFtp";
            this.TabPageFtp.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageFtp.Size = new System.Drawing.Size(659, 332);
            this.TabPageFtp.TabIndex = 1;
            this.TabPageFtp.Text = "Configurações do servidor FTP";
            this.TabPageFtp.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTempoLimiteEspera);
            this.groupBox2.Controls.Add(this.txtTempoEntreSicronizacoes);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(9, 209);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(640, 108);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Miscelâneos";
            this.groupBox2.Visible = false;
            // 
            // txtTempoLimiteEspera
            // 
            this.txtTempoLimiteEspera.Location = new System.Drawing.Point(515, 68);
            this.txtTempoLimiteEspera.Mask = "999";
            this.txtTempoLimiteEspera.Name = "txtTempoLimiteEspera";
            this.txtTempoLimiteEspera.Size = new System.Drawing.Size(100, 21);
            this.txtTempoLimiteEspera.TabIndex = 7;
            this.txtTempoLimiteEspera.Visible = false;
            // 
            // txtTempoEntreSicronizacoes
            // 
            this.txtTempoEntreSicronizacoes.Location = new System.Drawing.Point(515, 33);
            this.txtTempoEntreSicronizacoes.Mask = "999";
            this.txtTempoEntreSicronizacoes.Name = "txtTempoEntreSicronizacoes";
            this.txtTempoEntreSicronizacoes.Size = new System.Drawing.Size(100, 21);
            this.txtTempoEntreSicronizacoes.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(371, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Limite de tempo de espera para resposta do servidor (minutos)";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(219, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tempo entre sicronizações (minutos)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSenha);
            this.groupBox1.Controls.Add(this.txtUsuario);
            this.groupBox1.Controls.Add(this.txtServidor);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDiretorio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(8, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dados para conexão FTP";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(268, 90);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(348, 21);
            this.txtSenha.TabIndex = 7;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(268, 57);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(348, 21);
            this.txtUsuario.TabIndex = 6;
            // 
            // txtServidor
            // 
            this.txtServidor.Location = new System.Drawing.Point(268, 25);
            this.txtServidor.Name = "txtServidor";
            this.txtServidor.Size = new System.Drawing.Size(348, 21);
            this.txtServidor.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Servidor";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Senha";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuário";
            // 
            // txtDiretorio
            // 
            this.txtDiretorio.Location = new System.Drawing.Point(268, 178);
            this.txtDiretorio.Name = "txtDiretorio";
            this.txtDiretorio.Size = new System.Drawing.Size(348, 21);
            this.txtDiretorio.TabIndex = 5;
            this.txtDiretorio.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Diretório do servidor";
            this.label2.Visible = false;
            // 
            // lblProximaExecucao
            // 
            this.lblProximaExecucao.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProximaExecucao.AutoSize = true;
            this.lblProximaExecucao.BackColor = System.Drawing.Color.Transparent;
            this.lblProximaExecucao.Location = new System.Drawing.Point(15, 376);
            this.lblProximaExecucao.Name = "lblProximaExecucao";
            this.lblProximaExecucao.Size = new System.Drawing.Size(201, 13);
            this.lblProximaExecucao.TabIndex = 3;
            this.lblProximaExecucao.Text = "Próxima execução automática as:";
            this.lblProximaExecucao.Visible = false;
            // 
            // BtnSincronizar
            // 
            this.BtnSincronizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnSincronizar.Location = new System.Drawing.Point(8, 20);
            this.BtnSincronizar.Name = "BtnSincronizar";
            this.BtnSincronizar.Size = new System.Drawing.Size(151, 23);
            this.BtnSincronizar.TabIndex = 4;
            this.BtnSincronizar.Text = "Sincronizar agora";
            this.BtnSincronizar.UseVisualStyleBackColor = true;
            this.BtnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(9, 21);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(151, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "Gravar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReverter
            // 
            this.btnReverter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReverter.Location = new System.Drawing.Point(167, 20);
            this.btnReverter.Name = "btnReverter";
            this.btnReverter.Size = new System.Drawing.Size(151, 23);
            this.btnReverter.TabIndex = 6;
            this.btnReverter.Text = "Reverter";
            this.btnReverter.UseVisualStyleBackColor = true;
            this.btnReverter.Click += new System.EventHandler(this.btnReverter_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.BtnSincronizar);
            this.groupBox4.Location = new System.Drawing.Point(7, 408);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 50);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sincronização";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.btnOK);
            this.groupBox5.Controls.Add(this.btnReverter);
            this.groupBox5.Location = new System.Drawing.Point(334, 408);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(326, 50);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Configuração";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Location = new System.Drawing.Point(180, 433);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(56, 13);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Ver 1.05";
            // 
            // tmrSincronizacao
            // 
            this.tmrSincronizacao.Interval = 10000;
            this.tmrSincronizacao.Tick += new System.EventHandler(this.tmrSincronização_Tick);
            // 
            // tmrPiscar
            // 
            this.tmrPiscar.Interval = 200;
            this.tmrPiscar.Tick += new System.EventHandler(this.tmrPiscar_Tick);
            // 
            // btnChegouCadastro
            // 
            this.btnChegouCadastro.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChegouCadastro.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChegouCadastro.ForeColor = System.Drawing.Color.Red;
            this.btnChegouCadastro.Location = new System.Drawing.Point(13, 365);
            this.btnChegouCadastro.Name = "btnChegouCadastro";
            this.btnChegouCadastro.Size = new System.Drawing.Size(585, 34);
            this.btnChegouCadastro.TabIndex = 10;
            this.btnChegouCadastro.Text = "Chegaram pedidos ou clientes novos!!!";
            this.btnChegouCadastro.UseVisualStyleBackColor = true;
            this.btnChegouCadastro.Click += new System.EventHandler(this.btnChegouCadastro_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(665, 488);
            this.Controls.Add(this.btnChegouCadastro);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lblProximaExecucao);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "NeoSync - Sincronização de dados entre NeoStore e dispositivos móveis";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.TabPageOutros.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.TabPagePrincipal.ResumeLayout(false);
            this.TabPagePrincipal.PerformLayout();
            this.TabPageFtp.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        public System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TabPageFtp;
        public System.Windows.Forms.TabPage TabPagePrincipal;
        private System.Windows.Forms.Label lblProximaExecucao;
        private System.Windows.Forms.TabPage TabPageOutros;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnReverter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtTempoLimiteEspera;
        private System.Windows.Forms.MaskedTextBox txtTempoEntreSicronizacoes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtDiretorio;
        private System.Windows.Forms.TextBox txtServidor;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.TextBox TxtMsg;
        public System.Windows.Forms.Button BtnSincronizar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkVendaClienteSemReferencia;
        private System.Windows.Forms.CheckBox chkPermitirEscolherTabelaDePreco;
        private System.Windows.Forms.CheckBox chkPermitirPedidoComEstoqueZerado;
        private System.Windows.Forms.CheckBox chkVerificarCreditosParaVendaAPrazo;
        private System.Windows.Forms.ListBox lstLoja;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox txtPedidoQtdMaxAntesSincronizacao;
        private System.Windows.Forms.Timer tmrSincronizacao;
        private System.Windows.Forms.Timer tmrPiscar;
        private System.Windows.Forms.Button btnChegouCadastro;
        private System.Windows.Forms.CheckBox chkPeriodicamenteEnviarCNPJDeTodosOsClientes;
        private System.Windows.Forms.CheckBox chkEnviarTodosOsClientesParaTodosOsVendedores;
    }
}

