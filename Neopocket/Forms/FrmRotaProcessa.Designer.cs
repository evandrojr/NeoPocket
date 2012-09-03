namespace Neopocket.Forms
{
    partial class FrmRotaProcessa
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
            this.pnlPaginacao = new System.Windows.Forms.Panel();
            this.NeoPager = new Neo.Pocket.Controls.NeoDataGridPager();
            this.grdRota = new Neo.Pocket.Controls.NeoDataGrid();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.pnlPaginacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRota)).BeginInit();
            this.pnlGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaginacao
            // 
            this.pnlPaginacao.BackColor = System.Drawing.Color.Transparent;
            this.pnlPaginacao.Controls.Add(this.NeoPager);
            this.pnlPaginacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPaginacao.Location = new System.Drawing.Point(0, 245);
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
            this.NeoPager.Owner = this.grdRota;
            this.NeoPager.PageIndex = 0;
            this.NeoPager.PageSize = 100;
            this.NeoPager.Size = new System.Drawing.Size(240, 23);
            this.NeoPager.TabIndex = 12;
            // 
            // grdRota
            // 
            this.grdRota.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdRota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRota.Location = new System.Drawing.Point(0, 0);
            this.grdRota.Name = "grdRota";
            this.grdRota.Pager = this.NeoPager;
            this.grdRota.Size = new System.Drawing.Size(240, 245);
            this.grdRota.TabIndex = 11;
            this.grdRota.TableStyles.Add(this.NeoTableStyle);
            this.grdRota.DoubleClick += new System.EventHandler(this.grdRota_DoubleClick);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.Transparent;
            this.pnlGrid.Controls.Add(this.grdRota);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 0);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(240, 245);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            this.mnuVoltar.Click += new System.EventHandler(this.mnuVoltar_Click);
            // 
            // FrmRotaProcessa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlPaginacao);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmRotaProcessa";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.Frm_Load);
            this.pnlPaginacao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRota)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPaginacao;
        private Neo.Pocket.Controls.NeoDataGridPager NeoPager;
        private Neo.Pocket.Controls.NeoDataGrid grdRota;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.MenuItem mnuVoltar;

    }
}