namespace Neopocket.Forms
{
    partial class FrmClienteCadastro2
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
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuCancelar = new System.Windows.Forms.MenuItem();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnProximo = new System.Windows.Forms.Button();
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.pnlValidacao = new System.Windows.Forms.Panel();
            this.txtCEP = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.cboCidade = new System.Windows.Forms.ComboBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.lblCep = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblCidade = new System.Windows.Forms.Label();
            this.lblBairro = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblAutoScroll = new System.Windows.Forms.Label();
            this.pnlValidacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuCancelar);
            // 
            // mnuCancelar
            // 
            this.mnuCancelar.Text = "Cancelar";
            this.mnuCancelar.Click += new System.EventHandler(this.mnuCancelar_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnAnterior.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnAnterior.Location = new System.Drawing.Point(17, 209);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(58, 29);
            this.btnAnterior.TabIndex = 15;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnProximo
            // 
            this.btnProximo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnProximo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnProximo.Location = new System.Drawing.Point(165, 209);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(58, 29);
            this.btnProximo.TabIndex = 16;
            this.btnProximo.Text = "Próximo";
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // notification
            // 
            this.notification.Text = "notification1";
            // 
            // pnlValidacao
            // 
            this.pnlValidacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlValidacao.Controls.Add(this.txtCEP);
            this.pnlValidacao.Controls.Add(this.txtTelefone);
            this.pnlValidacao.Controls.Add(this.cboCidade);
            this.pnlValidacao.Controls.Add(this.txtBairro);
            this.pnlValidacao.Controls.Add(this.txtEndereco);
            this.pnlValidacao.Controls.Add(this.lblCep);
            this.pnlValidacao.Controls.Add(this.lblTelefone);
            this.pnlValidacao.Controls.Add(this.lblCidade);
            this.pnlValidacao.Controls.Add(this.lblBairro);
            this.pnlValidacao.Controls.Add(this.lblEndereco);
            this.pnlValidacao.Location = new System.Drawing.Point(17, 4);
            this.pnlValidacao.Name = "pnlValidacao";
            this.pnlValidacao.Size = new System.Drawing.Size(210, 199);
            // 
            // txtCEP
            // 
            this.txtCEP.Location = new System.Drawing.Point(130, 163);
            this.txtCEP.MaxLength = 8;
            this.txtCEP.Name = "txtCEP";
            this.txtCEP.Size = new System.Drawing.Size(76, 21);
            this.txtCEP.TabIndex = 5;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(0, 164);
            this.txtTelefone.MaxLength = 11;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(100, 21);
            this.txtTelefone.TabIndex = 4;
            // 
            // cboCidade
            // 
            this.cboCidade.Location = new System.Drawing.Point(0, 114);
            this.cboCidade.Name = "cboCidade";
            this.cboCidade.Size = new System.Drawing.Size(206, 22);
            this.cboCidade.TabIndex = 3;
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(0, 69);
            this.txtBairro.MaxLength = 30;
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(206, 21);
            this.txtBairro.TabIndex = 2;
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(0, 25);
            this.txtEndereco.MaxLength = 50;
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(206, 21);
            this.txtEndereco.TabIndex = 1;
            // 
            // lblCep
            // 
            this.lblCep.Location = new System.Drawing.Point(130, 149);
            this.lblCep.Name = "lblCep";
            this.lblCep.Size = new System.Drawing.Size(100, 20);
            this.lblCep.Text = "CEP:";
            // 
            // lblTelefone
            // 
            this.lblTelefone.Location = new System.Drawing.Point(0, 149);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(100, 20);
            this.lblTelefone.Text = "Telefone:";
            // 
            // lblCidade
            // 
            this.lblCidade.Location = new System.Drawing.Point(0, 98);
            this.lblCidade.Name = "lblCidade";
            this.lblCidade.Size = new System.Drawing.Size(50, 19);
            this.lblCidade.Text = "Cidade:";
            // 
            // lblBairro
            // 
            this.lblBairro.Location = new System.Drawing.Point(0, 53);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(50, 21);
            this.lblBairro.Text = "Bairro:";
            // 
            // lblEndereco
            // 
            this.lblEndereco.Location = new System.Drawing.Point(0, 8);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(63, 15);
            this.lblEndereco.Text = "Endereço:";
            // 
            // lblAutoScroll
            // 
            this.lblAutoScroll.Location = new System.Drawing.Point(74, 337);
            this.lblAutoScroll.Name = "lblAutoScroll";
            this.lblAutoScroll.Size = new System.Drawing.Size(100, 20);
            // 
            // FrmClienteCadastro2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 350);
            this.Controls.Add(this.lblAutoScroll);
            this.Controls.Add(this.pnlValidacao);
            this.Controls.Add(this.btnProximo);
            this.Controls.Add(this.btnAnterior);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmClienteCadastro2";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmClienteCadastro2_Load);
            this.pnlValidacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.MenuItem mnuCancelar;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProximo;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.Panel pnlValidacao;
        private System.Windows.Forms.TextBox txtCEP;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.ComboBox cboCidade;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.Label lblCep;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblCidade;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.Label lblAutoScroll;
    }
}