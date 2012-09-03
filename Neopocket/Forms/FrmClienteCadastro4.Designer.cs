namespace Neopocket.Forms
{
    partial class FrmClienteCadastro4
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
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lblRefBanco = new System.Windows.Forms.Label();
            this.lblAgencia = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblRefComercial1 = new System.Windows.Forms.Label();
            this.lblTelefone1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTelefone2 = new System.Windows.Forms.Label();
            this.txtRefBanco = new System.Windows.Forms.TextBox();
            this.txtAgencia = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.txtRefComercial1 = new System.Windows.Forms.TextBox();
            this.txtTelefone1 = new System.Windows.Forms.TextBox();
            this.txtRefComercial2 = new System.Windows.Forms.TextBox();
            this.txtTelefone2 = new System.Windows.Forms.TextBox();
            this.pnlValidacao = new System.Windows.Forms.Panel();
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
            // notification
            // 
            this.notification.Text = "notification";
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnAnterior.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnAnterior.Location = new System.Drawing.Point(15, 226);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(58, 29);
            this.btnAnterior.TabIndex = 21;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnSalvar.Location = new System.Drawing.Point(163, 225);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(58, 29);
            this.btnSalvar.TabIndex = 22;
            this.btnSalvar.Text = "Salvar";
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lblRefBanco
            // 
            this.lblRefBanco.Location = new System.Drawing.Point(15, 1);
            this.lblRefBanco.Name = "lblRefBanco";
            this.lblRefBanco.Size = new System.Drawing.Size(100, 20);
            this.lblRefBanco.Text = "Ref. Banco:";
            // 
            // lblAgencia
            // 
            this.lblAgencia.Location = new System.Drawing.Point(15, 40);
            this.lblAgencia.Name = "lblAgencia";
            this.lblAgencia.Size = new System.Drawing.Size(100, 20);
            this.lblAgencia.Text = "Agencia:";
            // 
            // lblTelefone
            // 
            this.lblTelefone.Location = new System.Drawing.Point(121, 40);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(100, 20);
            this.lblTelefone.Text = "Telefone:";
            // 
            // lblRefComercial1
            // 
            this.lblRefComercial1.Location = new System.Drawing.Point(15, 80);
            this.lblRefComercial1.Name = "lblRefComercial1";
            this.lblRefComercial1.Size = new System.Drawing.Size(100, 18);
            this.lblRefComercial1.Text = "Ref. Comercial:";
            // 
            // lblTelefone1
            // 
            this.lblTelefone1.Location = new System.Drawing.Point(15, 126);
            this.lblTelefone1.Name = "lblTelefone1";
            this.lblTelefone1.Size = new System.Drawing.Size(100, 20);
            this.lblTelefone1.Text = "Telefone:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.Text = "Ref. Comercial:";
            // 
            // lblTelefone2
            // 
            this.lblTelefone2.Location = new System.Drawing.Point(15, 195);
            this.lblTelefone2.Name = "lblTelefone2";
            this.lblTelefone2.Size = new System.Drawing.Size(100, 20);
            this.lblTelefone2.Text = "Telefone:";
            // 
            // txtRefBanco
            // 
            this.txtRefBanco.Location = new System.Drawing.Point(15, 16);
            this.txtRefBanco.MaxLength = 2;
            this.txtRefBanco.Name = "txtRefBanco";
            this.txtRefBanco.Size = new System.Drawing.Size(88, 21);
            this.txtRefBanco.TabIndex = 1;
            // 
            // txtAgencia
            // 
            this.txtAgencia.Location = new System.Drawing.Point(15, 54);
            this.txtAgencia.MaxLength = 10;
            this.txtAgencia.Name = "txtAgencia";
            this.txtAgencia.Size = new System.Drawing.Size(88, 21);
            this.txtAgencia.TabIndex = 2;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(121, 54);
            this.txtTelefone.MaxLength = 11;
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(100, 21);
            this.txtTelefone.TabIndex = 3;
            // 
            // txtRefComercial1
            // 
            this.txtRefComercial1.Location = new System.Drawing.Point(15, 94);
            this.txtRefComercial1.MaxLength = 60;
            this.txtRefComercial1.Name = "txtRefComercial1";
            this.txtRefComercial1.Size = new System.Drawing.Size(206, 21);
            this.txtRefComercial1.TabIndex = 4;
            // 
            // txtTelefone1
            // 
            this.txtTelefone1.Location = new System.Drawing.Point(100, 124);
            this.txtTelefone1.MaxLength = 11;
            this.txtTelefone1.Name = "txtTelefone1";
            this.txtTelefone1.Size = new System.Drawing.Size(121, 21);
            this.txtTelefone1.TabIndex = 5;
            // 
            // txtRefComercial2
            // 
            this.txtRefComercial2.Location = new System.Drawing.Point(15, 162);
            this.txtRefComercial2.MaxLength = 60;
            this.txtRefComercial2.Name = "txtRefComercial2";
            this.txtRefComercial2.Size = new System.Drawing.Size(206, 21);
            this.txtRefComercial2.TabIndex = 6;
            // 
            // txtTelefone2
            // 
            this.txtTelefone2.Location = new System.Drawing.Point(100, 192);
            this.txtTelefone2.MaxLength = 11;
            this.txtTelefone2.Name = "txtTelefone2";
            this.txtTelefone2.Size = new System.Drawing.Size(121, 21);
            this.txtTelefone2.TabIndex = 7;
            // 
            // pnlValidacao
            // 
            this.pnlValidacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlValidacao.Controls.Add(this.txtTelefone2);
            this.pnlValidacao.Controls.Add(this.txtRefComercial2);
            this.pnlValidacao.Controls.Add(this.txtTelefone1);
            this.pnlValidacao.Controls.Add(this.txtRefComercial1);
            this.pnlValidacao.Controls.Add(this.txtTelefone);
            this.pnlValidacao.Controls.Add(this.txtAgencia);
            this.pnlValidacao.Controls.Add(this.txtRefBanco);
            this.pnlValidacao.Controls.Add(this.lblTelefone2);
            this.pnlValidacao.Controls.Add(this.label6);
            this.pnlValidacao.Controls.Add(this.lblTelefone1);
            this.pnlValidacao.Controls.Add(this.lblRefComercial1);
            this.pnlValidacao.Controls.Add(this.lblTelefone);
            this.pnlValidacao.Controls.Add(this.lblAgencia);
            this.pnlValidacao.Controls.Add(this.lblRefBanco);
            this.pnlValidacao.Location = new System.Drawing.Point(0, 4);
            this.pnlValidacao.Name = "pnlValidacao";
            this.pnlValidacao.Size = new System.Drawing.Size(221, 216);
            // 
            // lblAutoScroll
            // 
            this.lblAutoScroll.Location = new System.Drawing.Point(77, 336);
            this.lblAutoScroll.Name = "lblAutoScroll";
            this.lblAutoScroll.Size = new System.Drawing.Size(100, 20);
            // 
            // FrmClienteCadastro4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(240, 350);
            this.Controls.Add(this.lblAutoScroll);
            this.Controls.Add(this.pnlValidacao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnAnterior);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmClienteCadastro4";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmClienteCadastro4_Load);
            this.pnlValidacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsCE.Forms.Notification notification;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.MenuItem mnuCancelar;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lblRefBanco;
        private System.Windows.Forms.Label lblAgencia;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblRefComercial1;
        private System.Windows.Forms.Label lblTelefone1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTelefone2;
        private System.Windows.Forms.TextBox txtRefBanco;
        private System.Windows.Forms.TextBox txtAgencia;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.TextBox txtRefComercial1;
        private System.Windows.Forms.TextBox txtTelefone1;
        private System.Windows.Forms.TextBox txtRefComercial2;
        private System.Windows.Forms.TextBox txtTelefone2;
        private System.Windows.Forms.Panel pnlValidacao;
        private System.Windows.Forms.Label lblAutoScroll;
    }
}