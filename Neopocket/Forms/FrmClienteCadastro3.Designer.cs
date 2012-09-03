namespace Neopocket.Forms
{
    partial class FrmClienteCadastro3
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
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnProximo = new System.Windows.Forms.Button();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.pnlValidacao = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDataNascimento = new System.Windows.Forms.DateTimePicker();
            this.cboIntervaloVisita = new System.Windows.Forms.ComboBox();
            this.cboDiaVisita = new System.Windows.Forms.ComboBox();
            this.cboPrazo = new System.Windows.Forms.ComboBox();
            this.txtLimite = new System.Windows.Forms.TextBox();
            this.txtComprador = new System.Windows.Forms.TextBox();
            this.lblIntervaloVisita = new System.Windows.Forms.Label();
            this.lblVisita = new System.Windows.Forms.Label();
            this.lblPrazo = new System.Windows.Forms.Label();
            this.lblLimite = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.lblComprador = new System.Windows.Forms.Label();
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
            this.btnAnterior.Location = new System.Drawing.Point(17, 208);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(58, 29);
            this.btnAnterior.TabIndex = 19;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnProximo
            // 
            this.btnProximo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnProximo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnProximo.Location = new System.Drawing.Point(163, 208);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.Size = new System.Drawing.Size(58, 29);
            this.btnProximo.TabIndex = 20;
            this.btnProximo.Text = "Próximo";
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click_1);
            // 
            // notification
            // 
            this.notification.Text = "";
            // 
            // pnlValidacao
            // 
            this.pnlValidacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlValidacao.Controls.Add(this.label1);
            this.pnlValidacao.Controls.Add(this.dtpDataNascimento);
            this.pnlValidacao.Controls.Add(this.cboIntervaloVisita);
            this.pnlValidacao.Controls.Add(this.cboDiaVisita);
            this.pnlValidacao.Controls.Add(this.cboPrazo);
            this.pnlValidacao.Controls.Add(this.txtLimite);
            this.pnlValidacao.Controls.Add(this.txtComprador);
            this.pnlValidacao.Controls.Add(this.lblIntervaloVisita);
            this.pnlValidacao.Controls.Add(this.lblVisita);
            this.pnlValidacao.Controls.Add(this.lblPrazo);
            this.pnlValidacao.Controls.Add(this.lblLimite);
            this.pnlValidacao.Controls.Add(this.lblData);
            this.pnlValidacao.Controls.Add(this.lblComprador);
            this.pnlValidacao.Location = new System.Drawing.Point(7, 7);
            this.pnlValidacao.Name = "pnlValidacao";
            this.pnlValidacao.Size = new System.Drawing.Size(214, 201);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(125, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.Text = " R$";
            // 
            // dtpDataNascimento
            // 
            this.dtpDataNascimento.CustomFormat = "dd/MM/yyyy";
            this.dtpDataNascimento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataNascimento.Location = new System.Drawing.Point(10, 71);
            this.dtpDataNascimento.Name = "dtpDataNascimento";
            this.dtpDataNascimento.Size = new System.Drawing.Size(83, 22);
            this.dtpDataNascimento.TabIndex = 2;
            // 
            // cboIntervaloVisita
            // 
            this.cboIntervaloVisita.Items.Add("Semanal");
            this.cboIntervaloVisita.Items.Add("Quinzenal");
            this.cboIntervaloVisita.Items.Add("Mensal");
            this.cboIntervaloVisita.Location = new System.Drawing.Point(10, 172);
            this.cboIntervaloVisita.Name = "cboIntervaloVisita";
            this.cboIntervaloVisita.Size = new System.Drawing.Size(202, 22);
            this.cboIntervaloVisita.TabIndex = 6;
            // 
            // cboDiaVisita
            // 
            this.cboDiaVisita.Items.Add("Segunda");
            this.cboDiaVisita.Items.Add("Terça");
            this.cboDiaVisita.Items.Add("Quarta");
            this.cboDiaVisita.Items.Add("Quinta");
            this.cboDiaVisita.Items.Add("Sexta");
            this.cboDiaVisita.Items.Add("Sábado");
            this.cboDiaVisita.Items.Add("Domingo");
            this.cboDiaVisita.Location = new System.Drawing.Point(128, 125);
            this.cboDiaVisita.Name = "cboDiaVisita";
            this.cboDiaVisita.Size = new System.Drawing.Size(84, 22);
            this.cboDiaVisita.TabIndex = 5;
            // 
            // cboPrazo
            // 
            this.cboPrazo.Location = new System.Drawing.Point(10, 124);
            this.cboPrazo.Name = "cboPrazo";
            this.cboPrazo.Size = new System.Drawing.Size(85, 22);
            this.cboPrazo.TabIndex = 4;
            // 
            // txtLimite
            // 
            this.txtLimite.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLimite.Location = new System.Drawing.Point(150, 73);
            this.txtLimite.MaxLength = 8;
            this.txtLimite.Name = "txtLimite";
            this.txtLimite.ReadOnly = true;
            this.txtLimite.Size = new System.Drawing.Size(62, 21);
            this.txtLimite.TabIndex = 3;
            // 
            // txtComprador
            // 
            this.txtComprador.Location = new System.Drawing.Point(10, 22);
            this.txtComprador.MaxLength = 60;
            this.txtComprador.Name = "txtComprador";
            this.txtComprador.Size = new System.Drawing.Size(202, 21);
            this.txtComprador.TabIndex = 1;
            // 
            // lblIntervaloVisita
            // 
            this.lblIntervaloVisita.Location = new System.Drawing.Point(10, 157);
            this.lblIntervaloVisita.Name = "lblIntervaloVisita";
            this.lblIntervaloVisita.Size = new System.Drawing.Size(100, 20);
            this.lblIntervaloVisita.Text = "Intervalo Visita:";
            // 
            // lblVisita
            // 
            this.lblVisita.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblVisita.Location = new System.Drawing.Point(128, 110);
            this.lblVisita.Name = "lblVisita";
            this.lblVisita.Size = new System.Drawing.Size(84, 20);
            this.lblVisita.Text = "Dia Visita:";
            // 
            // lblPrazo
            // 
            this.lblPrazo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPrazo.Location = new System.Drawing.Point(10, 109);
            this.lblPrazo.Name = "lblPrazo";
            this.lblPrazo.Size = new System.Drawing.Size(85, 20);
            this.lblPrazo.Text = "Prazo p/ pgtº:";
            // 
            // lblLimite
            // 
            this.lblLimite.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblLimite.Location = new System.Drawing.Point(133, 58);
            this.lblLimite.Name = "lblLimite";
            this.lblLimite.Size = new System.Drawing.Size(84, 20);
            this.lblLimite.Text = "Limite de Cred.:";
            // 
            // lblData
            // 
            this.lblData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblData.Location = new System.Drawing.Point(10, 58);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(100, 20);
            this.lblData.Text = "Data de Nascim.:";
            // 
            // lblComprador
            // 
            this.lblComprador.Location = new System.Drawing.Point(10, 7);
            this.lblComprador.Name = "lblComprador";
            this.lblComprador.Size = new System.Drawing.Size(131, 20);
            this.lblComprador.Text = "Comprador Nome:";
            // 
            // lblAutoScroll
            // 
            this.lblAutoScroll.Location = new System.Drawing.Point(72, 340);
            this.lblAutoScroll.Name = "lblAutoScroll";
            this.lblAutoScroll.Size = new System.Drawing.Size(100, 20);
            // 
            // FrmClienteCadastro3
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
            this.KeyPreview = true;
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmClienteCadastro3";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmClienteCadastro3_Load);
            this.pnlValidacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuCancelar;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.Button btnProximo;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.Panel pnlValidacao;
        private System.Windows.Forms.DateTimePicker dtpDataNascimento;
        private System.Windows.Forms.ComboBox cboIntervaloVisita;
        private System.Windows.Forms.ComboBox cboDiaVisita;
        private System.Windows.Forms.ComboBox cboPrazo;
        private System.Windows.Forms.TextBox txtLimite;
        private System.Windows.Forms.TextBox txtComprador;
        private System.Windows.Forms.Label lblIntervaloVisita;
        private System.Windows.Forms.Label lblVisita;
        private System.Windows.Forms.Label lblPrazo;
        private System.Windows.Forms.Label lblLimite;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label lblComprador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAutoScroll;
    }
}