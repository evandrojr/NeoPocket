﻿namespace Neopocket.Forms
{
    partial class FrmConfiguracaoDeAcesso
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracaoDeAcesso));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.txtCodigoAcesso = new System.Windows.Forms.TextBox();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlAcesso = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlConfiguracao = new System.Windows.Forms.Panel();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.tbcCadastro = new System.Windows.Forms.TabControl();
            this.tbpCadastro = new System.Windows.Forms.TabPage();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.tbpVendedor = new System.Windows.Forms.TabPage();
            this.txtCodigoVendedor = new System.Windows.Forms.TextBox();
            this.lblCodigoVendedor = new System.Windows.Forms.Label();
            this.tbpFtp = new System.Windows.Forms.TabPage();
            this.txtSenhaFTP = new System.Windows.Forms.TextBox();
            this.txtUsuarioFTP = new System.Windows.Forms.TextBox();
            this.lblUsuarioFtp = new System.Windows.Forms.Label();
            this.lblSenhaFtp = new System.Windows.Forms.Label();
            this.tabAtualizacao = new System.Windows.Forms.TabPage();
            this.btnForcarAtualizacao = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlAcesso.SuspendLayout();
            this.pnlConfiguracao.SuspendLayout();
            this.tbcCadastro.SuspendLayout();
            this.tbpCadastro.SuspendLayout();
            this.tbpVendedor.SuspendLayout();
            this.tbpFtp.SuspendLayout();
            this.tabAtualizacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigoAcesso
            // 
            this.txtCodigoAcesso.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtCodigoAcesso.Location = new System.Drawing.Point(19, 59);
            this.txtCodigoAcesso.Name = "txtCodigoAcesso";
            this.txtCodigoAcesso.PasswordChar = '*';
            this.txtCodigoAcesso.Size = new System.Drawing.Size(150, 26);
            this.txtCodigoAcesso.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblCodigo.Location = new System.Drawing.Point(19, 36);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(109, 20);
            this.lblCodigo.Text = "Digite código:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(19, 94);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 20);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(97, 94);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 20);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlAcesso
            // 
            this.pnlAcesso.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlAcesso.Controls.Add(this.txtCodigoAcesso);
            this.pnlAcesso.Controls.Add(this.lblCodigo);
            this.pnlAcesso.Controls.Add(this.pictureBox2);
            this.pnlAcesso.Controls.Add(this.btnCancelar);
            this.pnlAcesso.Controls.Add(this.btnOk);
            this.pnlAcesso.Location = new System.Drawing.Point(14, 56);
            this.pnlAcesso.Name = "pnlAcesso";
            this.pnlAcesso.Size = new System.Drawing.Size(213, 137);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(173, 56);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // pnlConfiguracao
            // 
            this.pnlConfiguracao.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlConfiguracao.Controls.Add(this.btnLimpar);
            this.pnlConfiguracao.Controls.Add(this.tbcCadastro);
            this.pnlConfiguracao.Controls.Add(this.btnSalvar);
            this.pnlConfiguracao.Location = new System.Drawing.Point(14, 56);
            this.pnlConfiguracao.Name = "pnlConfiguracao";
            this.pnlConfiguracao.Size = new System.Drawing.Size(213, 170);
            this.pnlConfiguracao.Visible = false;
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(10, 143);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(103, 22);
            this.btnLimpar.TabIndex = 12;
            this.btnLimpar.Text = "Apagar dados";
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // tbcCadastro
            // 
            this.tbcCadastro.Controls.Add(this.tbpCadastro);
            this.tbcCadastro.Controls.Add(this.tbpVendedor);
            this.tbcCadastro.Controls.Add(this.tbpFtp);
            this.tbcCadastro.Controls.Add(this.tabAtualizacao);
            this.tbcCadastro.Location = new System.Drawing.Point(0, 0);
            this.tbcCadastro.Name = "tbcCadastro";
            this.tbcCadastro.SelectedIndex = 0;
            this.tbcCadastro.Size = new System.Drawing.Size(213, 137);
            this.tbcCadastro.TabIndex = 10;
            // 
            // tbpCadastro
            // 
            this.tbpCadastro.Controls.Add(this.txtSenha);
            this.tbpCadastro.Controls.Add(this.lblNome);
            this.tbpCadastro.Controls.Add(this.txtNome);
            this.tbpCadastro.Controls.Add(this.lblSenha);
            this.tbpCadastro.Location = new System.Drawing.Point(0, 0);
            this.tbpCadastro.Name = "tbpCadastro";
            this.tbpCadastro.Size = new System.Drawing.Size(213, 114);
            this.tbpCadastro.Text = "Cadastro de login";
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(68, 64);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(129, 21);
            this.txtSenha.TabIndex = 2;
            // 
            // lblNome
            // 
            this.lblNome.Location = new System.Drawing.Point(26, 41);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(42, 20);
            this.lblNome.Text = "Nome:";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(68, 40);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(129, 21);
            this.txtNome.TabIndex = 1;
            // 
            // lblSenha
            // 
            this.lblSenha.Location = new System.Drawing.Point(24, 66);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(50, 20);
            this.lblSenha.Text = "Senha:";
            // 
            // tbpVendedor
            // 
            this.tbpVendedor.Controls.Add(this.txtCodigoVendedor);
            this.tbpVendedor.Controls.Add(this.lblCodigoVendedor);
            this.tbpVendedor.Location = new System.Drawing.Point(0, 0);
            this.tbpVendedor.Name = "tbpVendedor";
            this.tbpVendedor.Size = new System.Drawing.Size(205, 111);
            this.tbpVendedor.Text = "Vendedor";
            // 
            // txtCodigoVendedor
            // 
            this.txtCodigoVendedor.Location = new System.Drawing.Point(65, 40);
            this.txtCodigoVendedor.Name = "txtCodigoVendedor";
            this.txtCodigoVendedor.Size = new System.Drawing.Size(132, 21);
            this.txtCodigoVendedor.TabIndex = 3;
            // 
            // lblCodigoVendedor
            // 
            this.lblCodigoVendedor.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.lblCodigoVendedor.Location = new System.Drawing.Point(15, 41);
            this.lblCodigoVendedor.Name = "lblCodigoVendedor";
            this.lblCodigoVendedor.Size = new System.Drawing.Size(55, 20);
            this.lblCodigoVendedor.Text = "Código:";
            // 
            // tbpFtp
            // 
            this.tbpFtp.Controls.Add(this.txtSenhaFTP);
            this.tbpFtp.Controls.Add(this.txtUsuarioFTP);
            this.tbpFtp.Controls.Add(this.lblUsuarioFtp);
            this.tbpFtp.Controls.Add(this.lblSenhaFtp);
            this.tbpFtp.Location = new System.Drawing.Point(0, 0);
            this.tbpFtp.Name = "tbpFtp";
            this.tbpFtp.Size = new System.Drawing.Size(205, 111);
            this.tbpFtp.Text = "FTP";
            // 
            // txtSenhaFTP
            // 
            this.txtSenhaFTP.Location = new System.Drawing.Point(6, 76);
            this.txtSenhaFTP.Name = "txtSenhaFTP";
            this.txtSenhaFTP.Size = new System.Drawing.Size(198, 21);
            this.txtSenhaFTP.TabIndex = 5;
            // 
            // txtUsuarioFTP
            // 
            this.txtUsuarioFTP.Location = new System.Drawing.Point(5, 28);
            this.txtUsuarioFTP.Name = "txtUsuarioFTP";
            this.txtUsuarioFTP.Size = new System.Drawing.Size(199, 21);
            this.txtUsuarioFTP.TabIndex = 3;
            // 
            // lblUsuarioFtp
            // 
            this.lblUsuarioFtp.Location = new System.Drawing.Point(7, 11);
            this.lblUsuarioFtp.Name = "lblUsuarioFtp";
            this.lblUsuarioFtp.Size = new System.Drawing.Size(55, 20);
            this.lblUsuarioFtp.Text = "Usuário:";
            // 
            // lblSenhaFtp
            // 
            this.lblSenhaFtp.Location = new System.Drawing.Point(6, 62);
            this.lblSenhaFtp.Name = "lblSenhaFtp";
            this.lblSenhaFtp.Size = new System.Drawing.Size(55, 20);
            this.lblSenhaFtp.Text = "Senha:";
            // 
            // tabAtualizacao
            // 
            this.tabAtualizacao.Controls.Add(this.btnForcarAtualizacao);
            this.tabAtualizacao.Location = new System.Drawing.Point(0, 0);
            this.tabAtualizacao.Name = "tabAtualizacao";
            this.tabAtualizacao.Size = new System.Drawing.Size(205, 111);
            this.tabAtualizacao.Text = "Atualização";
            // 
            // btnForcarAtualizacao
            // 
            this.btnForcarAtualizacao.Location = new System.Drawing.Point(41, 53);
            this.btnForcarAtualizacao.Name = "btnForcarAtualizacao";
            this.btnForcarAtualizacao.Size = new System.Drawing.Size(136, 20);
            this.btnForcarAtualizacao.TabIndex = 0;
            this.btnForcarAtualizacao.Text = "Forçar atualização";
            this.btnForcarAtualizacao.Click += new System.EventHandler(this.btnForcarAtualizacao_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(156, 143);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(53, 22);
            this.btnSalvar.TabIndex = 11;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(240, 268);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // FrmConfiguracaoDeAcesso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlAcesso);
            this.Controls.Add(this.pnlConfiguracao);
            this.Controls.Add(this.pictureBox3);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FrmConfiguracaoDeAcesso";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmConfiguracaoDeAcesso_Load);
            this.pnlAcesso.ResumeLayout(false);
            this.pnlConfiguracao.ResumeLayout(false);
            this.tbcCadastro.ResumeLayout(false);
            this.tbpCadastro.ResumeLayout(false);
            this.tbpVendedor.ResumeLayout(false);
            this.tbpFtp.ResumeLayout(false);
            this.tabAtualizacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtCodigoAcesso;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnlAcesso;
        private System.Windows.Forms.Panel pnlConfiguracao;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.TabControl tbcCadastro;
        private System.Windows.Forms.TabPage tbpVendedor;
        private System.Windows.Forms.TextBox txtCodigoVendedor;
        private System.Windows.Forms.Label lblCodigoVendedor;
        private System.Windows.Forms.TabPage tbpCadastro;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TabPage tbpFtp;
        private System.Windows.Forms.TextBox txtSenhaFTP;
        private System.Windows.Forms.Label lblSenhaFtp;
        private System.Windows.Forms.TextBox txtUsuarioFTP;
        private System.Windows.Forms.Label lblUsuarioFtp;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.TabPage tabAtualizacao;
        private System.Windows.Forms.Button btnForcarAtualizacao;
    }
}
