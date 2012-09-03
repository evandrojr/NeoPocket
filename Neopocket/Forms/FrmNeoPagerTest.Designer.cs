namespace Neopocket.Forms
{
    partial class FrmNeoPagerTest
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.notification = new Microsoft.WindowsCE.Forms.Notification();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.mnuRecusa = new System.Windows.Forms.MenuItem();
            this.inputPanel = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.grdProduto = new Neo.Pocket.Controls.NeoDataGrid();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.NeoPager = new Neo.Pocket.Controls.NeoDataGridPager();
            ((System.ComponentModel.ISupportInitialize)(this.grdProduto)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlPaginacao
            // 
            this.pnlPaginacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlPaginacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaginacao.Location = new System.Drawing.Point(0, 245);
            this.pnlPaginacao.Name = "pnlPaginacao";
            this.pnlPaginacao.Size = new System.Drawing.Size(240, 23);
            // 
            // notification
            // 
            this.notification.Caption = "Detalhes do cliente:";
            this.notification.InitialDuration = 2;
            this.notification.Text = "";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            this.mainMenu.MenuItems.Add(this.mnuRecusa);
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            // 
            // mnuRecusa
            // 
            this.mnuRecusa.Text = "Recusa";
            // 
            // grdProduto
            // 
            this.grdProduto.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdProduto.Location = new System.Drawing.Point(0, 0);
            this.grdProduto.Name = "grdProduto";
            this.grdProduto.Pager = null;
            this.grdProduto.Size = new System.Drawing.Size(240, 200);
            this.grdProduto.TabIndex = 12;
            this.grdProduto.TableStyles.Add(this.NeoTableStyle);
            // 
            // NeoPager
            // 
            this.NeoPager.BackColor = System.Drawing.SystemColors.Control;
            this.NeoPager.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.NeoPager.Location = new System.Drawing.Point(3, 92);
            this.NeoPager.Name = "NeoPager";
            this.NeoPager.Owner = this.grdProduto;
            this.NeoPager.PageIndex = 0;
            this.NeoPager.PageSize = 100;
            this.NeoPager.Size = new System.Drawing.Size(221, 23);
            this.NeoPager.TabIndex = 13;
            // 
            // FrmNeoPagerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.NeoPager);
            this.Controls.Add(this.grdProduto);
            this.Controls.Add(this.pnlPaginacao);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FrmNeoPagerTest";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmNeoPagerTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdProduto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaginacao;
        private Microsoft.WindowsCE.Forms.Notification notification;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.MenuItem mnuRecusa;
        private Microsoft.WindowsCE.Forms.InputPanel inputPanel;
        private Neo.Pocket.Controls.NeoDataGrid grdProduto;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
        private Neo.Pocket.Controls.NeoDataGridPager NeoPager;


    }
}