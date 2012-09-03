namespace Neopocket.Forms
{
    partial class FrmClienteCadastro1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnuItem;

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
            this.mnuItem = new System.Windows.Forms.MainMenu();
            this.mnuCancelar = new System.Windows.Forms.MenuItem();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.lblNomeFantasia = new System.Windows.Forms.Label();
            this.lblTipoPessoa = new System.Windows.Forms.Label();
            this.lblCNPJCPF = new System.Windows.Forms.Label();
            this.lblRG = new System.Windows.Forms.Label();
            this.txtNomeFantasia = new System.Windows.Forms.TextBox();
            this.txtRazaoSocial = new System.Windows.Forms.TextBox();
            this.txtRG = new System.Windows.Forms.TextBox();
            this.txtCNPJCPF = new System.Windows.Forms.TextBox();
            this.cboTipoPessoa = new System.Windows.Forms.ComboBox();
            this.lblRazaoSocial = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.pnlValidacao = new System.Windows.Forms.Panel();
            this.btnProximo = new System.Windows.Forms.Button();
            this.lblAutoScroll = new System.Windows.Forms.Label();
            this.pnlValidacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuItem
            // 
            this.mnuItem.MenuItems.Add(this.mnuCancelar);
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Text = "Cancelar";
            this.mnuCancelar.Click += new System.EventHandler(this.mnuCancelar_Click);
            // 
            // notification
            // 
            this.notification.Text = "notification";
            // 
            // lblNomeFantasia
            // 
            this.lblNomeFantasia.Location = new System.Drawing.Point(92, 10);
            this.lblNomeFantasia.Name = "lblNomeFantasia";
            this.lblNomeFantasia.Size = new System.Drawing.Size(93, 20);
            this.lblNomeFantasia.Text = "Nome Fantasia:";
            // 
            // lblTipoPessoa
            // 
            this.lblTipoPessoa.Location = new System.Drawing.Point(8, 98);
            this.lblTipoPessoa.Name = "lblTipoPessoa";
            this.lblTipoPessoa.Size = new System.Drawing.Size(100, 20);
            this.lblTipoPessoa.Text = "Tipo de Pessoa:";
            // 
            // lblCNPJCPF
            // 
            this.lblCNPJCPF.Location = new System.Drawing.Point(8, 145);
            this.lblCNPJCPF.Name = "lblCNPJCPF";
            this.lblCNPJCPF.Size = new System.Drawing.Size(100, 20);
            this.lblCNPJCPF.Text = "CNPJ :";
            // 
            // lblRG
            // 
            this.lblRG.Location = new System.Drawing.Point(125, 145);
            this.lblRG.Name = "lblRG";
            this.lblRG.Size = new System.Drawing.Size(100, 20);
            this.lblRG.Text = "Insc. Est:";
            // 
            // txtNomeFantasia
            // 
            this.txtNomeFantasia.Location = new System.Drawing.Point(92, 25);
            this.txtNomeFantasia.MaxLength = 30;
            this.txtNomeFantasia.Name = "txtNomeFantasia";
            this.txtNomeFantasia.Size = new System.Drawing.Size(130, 21);
            this.txtNomeFantasia.TabIndex = 1;
            // 
            // txtRazaoSocial
            // 
            this.txtRazaoSocial.Location = new System.Drawing.Point(8, 71);
            this.txtRazaoSocial.MaxLength = 60;
            this.txtRazaoSocial.Name = "txtRazaoSocial";
            this.txtRazaoSocial.Size = new System.Drawing.Size(214, 21);
            this.txtRazaoSocial.TabIndex = 2;
            // 
            // txtRG
            // 
            this.txtRG.Location = new System.Drawing.Point(122, 162);
            this.txtRG.MaxLength = 15;
            this.txtRG.Name = "txtRG";
            this.txtRG.Size = new System.Drawing.Size(100, 21);
            this.txtRG.TabIndex = 5;
            // 
            // txtCNPJCPF
            // 
            this.txtCNPJCPF.Location = new System.Drawing.Point(8, 162);
            this.txtCNPJCPF.MaxLength = 14;
            this.txtCNPJCPF.Name = "txtCNPJCPF";
            this.txtCNPJCPF.Size = new System.Drawing.Size(93, 21);
            this.txtCNPJCPF.TabIndex = 4;
            // 
            // cboTipoPessoa
            // 
            this.cboTipoPessoa.Items.Add("Pessoa Física");
            this.cboTipoPessoa.Items.Add("Pessoa Jurídica");
            this.cboTipoPessoa.Location = new System.Drawing.Point(8, 114);
            this.cboTipoPessoa.Name = "cboTipoPessoa";
            this.cboTipoPessoa.Size = new System.Drawing.Size(214, 22);
            this.cboTipoPessoa.TabIndex = 3;
            this.cboTipoPessoa.SelectedIndexChanged += new System.EventHandler(this.cboTipoPessoa_SelectedIndexChanged);
            // 
            // lblRazaoSocial
            // 
            this.lblRazaoSocial.Location = new System.Drawing.Point(8, 55);
            this.lblRazaoSocial.Name = "lblRazaoSocial";
            this.lblRazaoSocial.Size = new System.Drawing.Size(77, 13);
            this.lblRazaoSocial.Text = "Razão Social:";
            // 
            // lblCodigo
            // 
            this.lblCodigo.Location = new System.Drawing.Point(8, 10);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(56, 20);
            this.lblCodigo.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(10, 25);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.ReadOnly = true;
            this.txtCodigo.Size = new System.Drawing.Size(63, 21);
            this.txtCodigo.TabIndex = 11;
            // 
            // pnlValidacao
            // 
            this.pnlValidacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlValidacao.Controls.Add(this.txtCodigo);
            this.pnlValidacao.Controls.Add(this.lblCodigo);
            this.pnlValidacao.Controls.Add(this.lblRazaoSocial);
            this.pnlValidacao.Controls.Add(this.cboTipoPessoa);
            this.pnlValidacao.Controls.Add(this.txtCNPJCPF);
            this.pnlValidacao.Controls.Add(this.txtRG);
            this.pnlValidacao.Controls.Add(this.txtRazaoSocial);
            this.pnlValidacao.Controls.Add(this.txtNomeFantasia);
            this.pnlValidacao.Controls.Add(this.lblRG);
            this.pnlValidacao.Controls.Add(this.lblCNPJCPF);
            this.pnlValidacao.Controls.Add(this.lblTipoPessoa);
            this.pnlValidacao.Controls.Add(this.lblNomeFantasia);
            this.pnlValidacao.Location = new System.Drawing.Point(3, 10);
            this.pnlValidacao.Name = "pnlValidacao";
            this.pnlValidacao.Size = new System.Drawing.Size(222, 187);
            // 
            // btnProximo
            // 
            this.btnProximo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnProximo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnProximo.Location = new System.Drawing.Point(163, 217);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(62, 29);
            this.btnProximo.TabIndex = 19;
            this.btnProximo.Text = "Próximo ";
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click_1);
            // 
            // lblAutoScroll
            // 
            this.lblAutoScroll.Location = new System.Drawing.Point(66, 339);
            this.lblAutoScroll.Name = "lblAutoScroll";
            this.lblAutoScroll.Size = new System.Drawing.Size(100, 20);
            // 
            // FrmClienteCadastro1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 350);
            this.Controls.Add(this.lblAutoScroll);
            this.Controls.Add(this.btnProximo);
            this.Controls.Add(this.pnlValidacao);
            this.Menu = this.mnuItem;
            this.MinimizeBox = false;
            this.Name = "FrmClienteCadastro1";
            this.Text = "NeoPocket";
            this.pnlValidacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancelar;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.Label lblNomeFantasia;
        private System.Windows.Forms.Label lblTipoPessoa;
        private System.Windows.Forms.Label lblCNPJCPF;
        private System.Windows.Forms.Label lblRG;
        private System.Windows.Forms.TextBox txtNomeFantasia;
        private System.Windows.Forms.TextBox txtRazaoSocial;
        private System.Windows.Forms.TextBox txtRG;
        private System.Windows.Forms.TextBox txtCNPJCPF;
        private System.Windows.Forms.ComboBox cboTipoPessoa;
        private System.Windows.Forms.Label lblRazaoSocial;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Panel pnlValidacao;
        private System.Windows.Forms.Button btnProximo;
        private System.Windows.Forms.Label lblAutoScroll;
    }
}