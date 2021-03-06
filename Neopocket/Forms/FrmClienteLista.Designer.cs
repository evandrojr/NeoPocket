﻿namespace Neopocket.Forms
{
    partial class FrmClienteLista
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
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.mnuRecusa = new System.Windows.Forms.MenuItem();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.NeoPager = new Neo.Pocket.Controls.NeoDataGridPager();
            this.grdCliente = new Neo.Pocket.Controls.NeoDataGrid();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.pnlFiltro = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.pnlFiltroInterno = new System.Windows.Forms.Panel();
            this.radNome = new System.Windows.Forms.RadioButton();
            this.radCodigo = new System.Windows.Forms.RadioButton();
            this.lblProduto = new System.Windows.Forms.Label();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.pnlBotoes = new System.Windows.Forms.Panel();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnPedido = new System.Windows.Forms.Button();
            this.pnlPaginacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCliente)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.pnlFiltro.SuspendLayout();
            this.pnlFiltroInterno.SuspendLayout();
            this.pnlBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            this.mainMenu.MenuItems.Add(this.mnuRecusa);
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            this.mnuVoltar.Click += new System.EventHandler(this.mnuVoltar_Click);
            // 
            // mnuRecusa
            // 
            this.mnuRecusa.Text = "Recusa";
            this.mnuRecusa.Click += new System.EventHandler(this.mnuRecusa_Click);
            // 
            // notification
            // 
            this.notification.Caption = "Detalhes do cliente:";
            this.notification.InitialDuration = 2;
            this.notification.Text = "";
            // 
            // pnlPaginacao
            // 
            this.pnlPaginacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlPaginacao.Controls.Add(this.NeoPager);
            this.pnlPaginacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaginacao.Location = new System.Drawing.Point(0, 214);
            this.pnlPaginacao.Name = "pnlPaginacao";
            this.pnlPaginacao.Size = new System.Drawing.Size(240, 23);
            // 
            // NeoPager
            // 
            this.NeoPager.BackColor = System.Drawing.SystemColors.Control;
            this.NeoPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NeoPager.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.NeoPager.Location = new System.Drawing.Point(0, 0);
            this.NeoPager.Name = "NeoPager";
            this.NeoPager.Owner = this.grdCliente;
            this.NeoPager.PageIndex = 0;
            this.NeoPager.PageSize = 100;
            this.NeoPager.Size = new System.Drawing.Size(240, 23);
            this.NeoPager.TabIndex = 12;
            // 
            // grdCliente
            // 
            this.grdCliente.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdCliente.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCliente.Location = new System.Drawing.Point(0, 0);
            this.grdCliente.Name = "grdCliente";
            this.grdCliente.Pager = this.NeoPager;
            this.grdCliente.Size = new System.Drawing.Size(240, 158);
            this.grdCliente.TabIndex = 11;
            this.grdCliente.TableStyles.Add(this.NeoTableStyle);
            this.grdCliente.DoubleClick += new System.EventHandler(this.grdCliente_DoubleClick);
            this.grdCliente.Click += new System.EventHandler(this.grdCliente_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlGrid.Controls.Add(this.grdCliente);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 56);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(240, 158);
            // 
            // pnlFiltro
            // 
            this.pnlFiltro.BackColor = System.Drawing.SystemColors.Control;
            this.pnlFiltro.Controls.Add(this.btnBuscar);
            this.pnlFiltro.Controls.Add(this.pnlFiltroInterno);
            this.pnlFiltro.Controls.Add(this.lblProduto);
            this.pnlFiltro.Controls.Add(this.txtCliente);
            this.pnlFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltro.Location = new System.Drawing.Point(0, 0);
            this.pnlFiltro.Name = "pnlFiltro";
            this.pnlFiltro.Size = new System.Drawing.Size(240, 56);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnBuscar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnBuscar.Location = new System.Drawing.Point(182, 28);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(52, 22);
            this.btnBuscar.TabIndex = 6;
            this.btnBuscar.Text = " Buscar";
            this.btnBuscar.Click += new System.EventHandler(this.btBuscar_Click);
            // 
            // pnlFiltroInterno
            // 
            this.pnlFiltroInterno.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlFiltroInterno.Controls.Add(this.radNome);
            this.pnlFiltroInterno.Controls.Add(this.radCodigo);
            this.pnlFiltroInterno.Location = new System.Drawing.Point(5, 28);
            this.pnlFiltroInterno.Name = "pnlFiltroInterno";
            this.pnlFiltroInterno.Size = new System.Drawing.Size(132, 22);
            // 
            // radNome
            // 
            this.radNome.ForeColor = System.Drawing.SystemColors.MenuText;
            this.radNome.Location = new System.Drawing.Point(69, 2);
            this.radNome.Name = "radNome";
            this.radNome.Size = new System.Drawing.Size(58, 17);
            this.radNome.TabIndex = 1;
            this.radNome.TabStop = false;
            this.radNome.Text = "Nome ";
            this.radNome.CheckedChanged += new System.EventHandler(this.radNome_CheckedChanged);
            // 
            // radCodigo
            // 
            this.radCodigo.Checked = true;
            this.radCodigo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.radCodigo.Location = new System.Drawing.Point(5, 1);
            this.radCodigo.Name = "radCodigo";
            this.radCodigo.Size = new System.Drawing.Size(68, 20);
            this.radCodigo.TabIndex = 2;
            this.radCodigo.Text = "Código";
            this.radCodigo.CheckedChanged += new System.EventHandler(this.radCodigo_CheckedChanged);
            // 
            // lblProduto
            // 
            this.lblProduto.Location = new System.Drawing.Point(5, 5);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(49, 17);
            this.lblProduto.Text = "Cliente";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(60, 4);
            this.txtCliente.Multiline = true;
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(174, 18);
            this.txtCliente.TabIndex = 8;
            this.txtCliente.GotFocus += new System.EventHandler(this.txtCliente_GotFocus);
            this.txtCliente.LostFocus += new System.EventHandler(this.txtCliente_LostFocus);
            // 
            // pnlBotoes
            // 
            this.pnlBotoes.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBotoes.Controls.Add(this.btnNovo);
            this.pnlBotoes.Controls.Add(this.btnEditar);
            this.pnlBotoes.Controls.Add(this.btnPedido);
            this.pnlBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBotoes.Location = new System.Drawing.Point(0, 237);
            this.pnlBotoes.Name = "pnlBotoes";
            this.pnlBotoes.Size = new System.Drawing.Size(240, 31);
            // 
            // btnNovo
            // 
            this.btnNovo.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnNovo.Location = new System.Drawing.Point(5, 6);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(49, 20);
            this.btnNovo.TabIndex = 5;
            this.btnNovo.Text = "Novo";
            this.btnNovo.Visible = false;
            this.btnNovo.Click += new System.EventHandler(this.btNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnEditar.Location = new System.Drawing.Point(60, 6);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(49, 20);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Visible = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnPedido
            // 
            this.btnPedido.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnPedido.Location = new System.Drawing.Point(115, 6);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(49, 20);
            this.btnPedido.TabIndex = 10;
            this.btnPedido.Text = "Pedido";
            this.btnPedido.Visible = false;
            this.btnPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // FrmClienteLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlPaginacao);
            this.Controls.Add(this.pnlBotoes);
            this.Controls.Add(this.pnlFiltro);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmClienteLista";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmCliente_Load);
            this.pnlPaginacao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCliente)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.pnlFiltro.ResumeLayout(false);
            this.pnlFiltroInterno.ResumeLayout(false);
            this.pnlBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.MenuItem mnuRecusa;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.Panel pnlPaginacao;
        private Neo.Pocket.Controls.NeoDataGridPager NeoPager;
        private Neo.Pocket.Controls.NeoDataGrid grdCliente;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Panel pnlFiltro;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Panel pnlFiltroInterno;
        private System.Windows.Forms.RadioButton radNome;
        private System.Windows.Forms.RadioButton radCodigo;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.TextBox txtCliente;
        private System.Windows.Forms.Panel pnlBotoes;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnPedido;
    }
}
