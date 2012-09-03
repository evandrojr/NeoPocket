namespace Sample
{
    partial class FrmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.MainMenu = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.NeoPager = new Neo.Pocket.Controls.NeoDataGridPager();
            this.NeoGrid = new Neo.Pocket.Controls.NeoDataGrid();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            ((System.ComponentModel.ISupportInitialize)(this.NeoGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 20);
            this.label1.Text = "Neo Data Grid Sample";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(165, 40);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 20);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "search";
            this.btnSearch.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(4, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.Text = "Cache filter";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(5, 40);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(154, 21);
            this.txtSearch.TabIndex = 0;
            // 
            // MainMenu
            // 
            this.MainMenu.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.Text = "Arquivo";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Sair";
            // 
            // NeoPager
            // 
            this.NeoPager.Location = new System.Drawing.Point(5, 246);
            this.NeoPager.Name = "NeoPager";
            this.NeoPager.Owner = this.NeoGrid;
            this.NeoPager.PageIndex = 0;
            this.NeoPager.PageSize = 50;
            this.NeoPager.Size = new System.Drawing.Size(200, 19);
            this.NeoPager.TabIndex = 3;
            this.NeoPager.Text = "neoDataGridPager1";
            // 
            // NeoGrid
            // 
            this.NeoGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.NeoGrid.Location = new System.Drawing.Point(5, 66);
            this.NeoGrid.Name = "NeoGrid";
            this.NeoGrid.Pager = this.NeoPager;
            this.NeoGrid.Size = new System.Drawing.Size(232, 174);
            this.NeoGrid.TabIndex = 2;
            this.NeoGrid.TableStyles.Add(this.NeoTableStyle);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NeoPager);
            this.Controls.Add(this.NeoGrid);
            this.Menu = this.MainMenu;
            this.Name = "FrmMain";
            this.Text = "Sample";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NeoGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Neo.Pocket.Controls.NeoDataGrid NeoGrid;
        private Neo.Pocket.Controls.NeoDataGridPager NeoPager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.MainMenu MainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
    }
}