namespace Neopocket.Forms
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemRelPedidos = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemRelClienteOrdCidade = new System.Windows.Forms.MenuItem();
            this.menuItemRelClienteSemPedido = new System.Windows.Forms.MenuItem();
            this.menuItemSair = new System.Windows.Forms.MenuItem();
            this.picTelaPrincipal = new System.Windows.Forms.PictureBox();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnPedido = new System.Windows.Forms.Button();
            this.btnProduto = new System.Windows.Forms.Button();
            this.picConfiguracoesDeAcesso = new System.Windows.Forms.PictureBox();
            this.lblPedidosAEnviar = new System.Windows.Forms.Label();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.lblBateria = new System.Windows.Forms.Label();
            this.batteryLife = new OpenNETCF.Windows.Forms.BatteryLife();
            this.btnRota = new System.Windows.Forms.Button();
            this.btnNoticias = new System.Windows.Forms.Button();
            this.btnSincronizar = new System.Windows.Forms.Button();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lblVersaoTexto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.menuItem1);
            this.mainMenu.MenuItems.Add(this.menuItemSair);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItemRelPedidos);
            this.menuItem1.MenuItems.Add(this.menuItem4);
            this.menuItem1.Text = "Relatórios";
            // 
            // menuItemRelPedidos
            // 
            this.menuItemRelPedidos.Text = "Pedidos";
            this.menuItemRelPedidos.Click += new System.EventHandler(this.menuItemRelPedidos_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.MenuItems.Add(this.menuItemRelClienteOrdCidade);
            this.menuItem4.MenuItems.Add(this.menuItemRelClienteSemPedido);
            this.menuItem4.Text = "Clientes";
            // 
            // menuItemRelClienteOrdCidade
            // 
            this.menuItemRelClienteOrdCidade.Text = "ordenado por cidade";
            this.menuItemRelClienteOrdCidade.Click += new System.EventHandler(this.menuItemRelClienteOrdCidade_Click);
            // 
            // menuItemRelClienteSemPedido
            // 
            this.menuItemRelClienteSemPedido.Text = "sem pedidos";
            this.menuItemRelClienteSemPedido.Click += new System.EventHandler(this.menuItemRelClienteSemPedido_Click);
            // 
            // menuItemSair
            // 
            this.menuItemSair.Text = "Sair";
            this.menuItemSair.Click += new System.EventHandler(this.menuItemSair_Click);
            // 
            // picTelaPrincipal
            // 
            this.picTelaPrincipal.Image = ((System.Drawing.Image)(resources.GetObject("picTelaPrincipal.Image")));
            this.picTelaPrincipal.Location = new System.Drawing.Point(0, 0);
            this.picTelaPrincipal.Name = "picTelaPrincipal";
            this.picTelaPrincipal.Size = new System.Drawing.Size(240, 268);
            // 
            // btnCliente
            // 
            this.btnCliente.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnCliente.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnCliente.Location = new System.Drawing.Point(148, 9);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(72, 33);
            this.btnCliente.TabIndex = 1;
            this.btnCliente.Text = "Clientes";
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnPedido
            // 
            this.btnPedido.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnPedido.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnPedido.Location = new System.Drawing.Point(148, 48);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(72, 33);
            this.btnPedido.TabIndex = 3;
            this.btnPedido.Text = "Pedidos";
            this.btnPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // btnProduto
            // 
            this.btnProduto.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnProduto.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnProduto.Location = new System.Drawing.Point(148, 125);
            this.btnProduto.Name = "btnProduto";
            this.btnProduto.Size = new System.Drawing.Size(72, 33);
            this.btnProduto.TabIndex = 5;
            this.btnProduto.Text = "Produtos";
            this.btnProduto.Click += new System.EventHandler(this.btnProduto_Click);
            // 
            // picConfiguracoesDeAcesso
            // 
            this.picConfiguracoesDeAcesso.BackColor = System.Drawing.Color.SkyBlue;
            this.picConfiguracoesDeAcesso.Image = ((System.Drawing.Image)(resources.GetObject("picConfiguracoesDeAcesso.Image")));
            this.picConfiguracoesDeAcesso.Location = new System.Drawing.Point(1, 235);
            this.picConfiguracoesDeAcesso.Name = "picConfiguracoesDeAcesso";
            this.picConfiguracoesDeAcesso.Size = new System.Drawing.Size(34, 32);
            this.picConfiguracoesDeAcesso.Click += new System.EventHandler(this.picConfiguracoesDeAcesso_Click);
            // 
            // lblPedidosAEnviar
            // 
            this.lblPedidosAEnviar.BackColor = System.Drawing.SystemColors.Control;
            this.lblPedidosAEnviar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPedidosAEnviar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.lblPedidosAEnviar.Location = new System.Drawing.Point(41, 235);
            this.lblPedidosAEnviar.Name = "lblPedidosAEnviar";
            this.lblPedidosAEnviar.Size = new System.Drawing.Size(196, 32);
            // 
            // lblBateria
            // 
            this.lblBateria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(152)))), ((int)(((byte)(214)))));
            this.lblBateria.Location = new System.Drawing.Point(4, 3);
            this.lblBateria.Name = "lblBateria";
            this.lblBateria.Size = new System.Drawing.Size(43, 17);
            this.lblBateria.Text = "Bateria";
            // 
            // batteryLife
            // 
            this.batteryLife.Location = new System.Drawing.Point(3, 21);
            this.batteryLife.Name = "batteryLife";
            this.batteryLife.Size = new System.Drawing.Size(108, 20);
            this.batteryLife.TabIndex = 11;
            // 
            // btnRota
            // 
            this.btnRota.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnRota.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnRota.Location = new System.Drawing.Point(30, 164);
            this.btnRota.Name = "btnRota";
            this.btnRota.Size = new System.Drawing.Size(72, 33);
            this.btnRota.TabIndex = 15;
            this.btnRota.Text = "Seguir rota";
            this.btnRota.Visible = false;
            this.btnRota.Click += new System.EventHandler(this.btnRota_Click);
            // 
            // btnNoticias
            // 
            this.btnNoticias.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnNoticias.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnNoticias.Location = new System.Drawing.Point(148, 164);
            this.btnNoticias.Name = "btnNoticias";
            this.btnNoticias.Size = new System.Drawing.Size(72, 33);
            this.btnNoticias.TabIndex = 20;
            this.btnNoticias.Text = "Notícias";
            this.btnNoticias.Click += new System.EventHandler(this.btnNoticias_Click);
            // 
            // btnSincronizar
            // 
            this.btnSincronizar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnSincronizar.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnSincronizar.Location = new System.Drawing.Point(148, 86);
            this.btnSincronizar.Name = "btnSincronizar";
            this.btnSincronizar.Size = new System.Drawing.Size(72, 33);
            this.btnSincronizar.TabIndex = 25;
            this.btnSincronizar.Text = " Sincronizar";
            this.btnSincronizar.Click += new System.EventHandler(this.btnSincronizar_Click);
            // 
            // lbVersion
            // 
            this.lbVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(170)))));
            this.lbVersion.Location = new System.Drawing.Point(145, 215);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(72, 20);
            this.lbVersion.Text = "xx.xx.xx.xx";
            // 
            // lblVersaoTexto
            // 
            this.lblVersaoTexto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(91)))), ((int)(((byte)(170)))));
            this.lblVersaoTexto.Location = new System.Drawing.Point(120, 215);
            this.lblVersaoTexto.Name = "lblVersaoTexto";
            this.lblVersaoTexto.Size = new System.Drawing.Size(26, 20);
            this.lblVersaoTexto.Text = "ver";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblVersaoTexto);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.btnSincronizar);
            this.Controls.Add(this.btnNoticias);
            this.Controls.Add(this.btnRota);
            this.Controls.Add(this.lblBateria);
            this.Controls.Add(this.batteryLife);
            this.Controls.Add(this.picConfiguracoesDeAcesso);
            this.Controls.Add(this.lblPedidosAEnviar);
            this.Controls.Add(this.btnProduto);
            this.Controls.Add(this.btnPedido);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.picTelaPrincipal);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipal";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.GotFocus += new System.EventHandler(this.FrmPrincipal_GotFocus);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmPrincipal_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTelaPrincipal;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnPedido;
        private System.Windows.Forms.Button btnProduto;
        private System.Windows.Forms.PictureBox picConfiguracoesDeAcesso;
        private System.Windows.Forms.Label lblPedidosAEnviar;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private System.Windows.Forms.Label lblBateria;
        private OpenNETCF.Windows.Forms.BatteryLife batteryLife;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemRelPedidos;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemRelClienteOrdCidade;
        private System.Windows.Forms.MenuItem menuItemSair;
        private System.Windows.Forms.MenuItem menuItemRelClienteSemPedido;
        private System.Windows.Forms.Button btnRota;
        private System.Windows.Forms.Button btnNoticias;
        private System.Windows.Forms.Button btnSincronizar;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lblVersaoTexto;

    }
}