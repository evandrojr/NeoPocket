namespace Neopocket.Forms
{
    partial class FrmPedidoLista
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
            this.mnuMarcarVisitaPendente = new System.Windows.Forms.MenuItem();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grdPedido = new System.Windows.Forms.DataGrid();
            this.dgsPedido = new System.Windows.Forms.DataGridTableStyle();
            this.gcsPedidoIdPedido = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsPedidoData = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsPedidoStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsPedidoValor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.lblText = new System.Windows.Forms.Label();
            this.lblLimite = new System.Windows.Forms.Label();
            this.lblDebito = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCredito = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuMarcarVisitaPendente);
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            // 
            // mnuMarcarVisitaPendente
            // 
            this.mnuMarcarVisitaPendente.Text = "Pendente";
            this.mnuMarcarVisitaPendente.Click += new System.EventHandler(this.mnuMarcarVisitaPendente_Click);
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            this.mnuVoltar.Click += new System.EventHandler(this.mnuVoltar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(243, 265);
            // 
            // grdPedido
            // 
            this.grdPedido.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdPedido.DataSource = this.bindingSource;
            this.grdPedido.Location = new System.Drawing.Point(0, 134);
            this.grdPedido.Name = "grdPedido";
            this.grdPedido.Size = new System.Drawing.Size(240, 98);
            this.grdPedido.TabIndex = 1;
            this.grdPedido.TableStyles.Add(this.dgsPedido);
            // 
            // dgsPedido
            // 
            this.dgsPedido.GridColumnStyles.Add(this.gcsPedidoIdPedido);
            this.dgsPedido.GridColumnStyles.Add(this.gcsPedidoData);
            this.dgsPedido.GridColumnStyles.Add(this.gcsPedidoStatus);
            this.dgsPedido.GridColumnStyles.Add(this.gcsPedidoValor);
            this.dgsPedido.MappingName = "pedido";
            // 
            // gcsPedidoIdPedido
            // 
            this.gcsPedidoIdPedido.Format = "";
            this.gcsPedidoIdPedido.HeaderText = "Nº";
            this.gcsPedidoIdPedido.MappingName = "id_pedido";
            // 
            // gcsPedidoData
            // 
            this.gcsPedidoData.Format = "";
            this.gcsPedidoData.HeaderText = "Data";
            this.gcsPedidoData.MappingName = "data";
            // 
            // gcsPedidoStatus
            // 
            this.gcsPedidoStatus.Format = "";
            this.gcsPedidoStatus.HeaderText = "Status";
            this.gcsPedidoStatus.MappingName = "status";
            // 
            // gcsPedidoValor
            // 
            this.gcsPedidoValor.Format = "c";
            this.gcsPedidoValor.HeaderText = "Valor";
            this.gcsPedidoValor.MappingName = "valor";
            // 
            // btnNovo
            // 
            this.btnNovo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnNovo.Location = new System.Drawing.Point(5, 238);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(72, 20);
            this.btnNovo.TabIndex = 2;
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnEditar.Location = new System.Drawing.Point(83, 238);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 20);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnExcluir.Location = new System.Drawing.Point(159, 238);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(72, 20);
            this.btnExcluir.TabIndex = 4;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.BackColor = System.Drawing.SystemColors.Control;
            this.lblCliente.Location = new System.Drawing.Point(2, 3);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(235, 41);
            this.lblCliente.Text = "José Carlos Agridantas";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblText
            // 
            this.lblText.BackColor = System.Drawing.SystemColors.Control;
            this.lblText.Location = new System.Drawing.Point(2, 105);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(96, 26);
            this.lblText.Text = "Crédito restante:";
            // 
            // lblLimite
            // 
            this.lblLimite.BackColor = System.Drawing.SystemColors.Control;
            this.lblLimite.Location = new System.Drawing.Point(102, 105);
            this.lblLimite.Name = "lblLimite";
            this.lblLimite.Size = new System.Drawing.Size(135, 26);
            // 
            // lblDebito
            // 
            this.lblDebito.BackColor = System.Drawing.SystemColors.Control;
            this.lblDebito.Location = new System.Drawing.Point(102, 76);
            this.lblDebito.Name = "lblDebito";
            this.lblDebito.Size = new System.Drawing.Size(135, 26);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(2, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 26);
            this.label2.Text = "Débito:";
            // 
            // lblCredito
            // 
            this.lblCredito.BackColor = System.Drawing.SystemColors.Control;
            this.lblCredito.Location = new System.Drawing.Point(102, 47);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(135, 26);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(2, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 26);
            this.label3.Text = "Crédito:";
            // 
            // FrmPedidoLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblCredito);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblDebito);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblLimite);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.grdPedido);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmPedidoLista";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmPedidoLista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuMarcarVisitaPendente;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGrid grdPedido;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.BindingSource bindingSource;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.DataGridTableStyle dgsPedido;
        private System.Windows.Forms.DataGridTextBoxColumn gcsPedidoIdPedido;
        private System.Windows.Forms.DataGridTextBoxColumn gcsPedidoData;
        private System.Windows.Forms.DataGridTextBoxColumn gcsPedidoStatus;
        private System.Windows.Forms.DataGridTextBoxColumn gcsPedidoValor;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblLimite;
        private System.Windows.Forms.Label lblDebito;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label label3;
    }
}
