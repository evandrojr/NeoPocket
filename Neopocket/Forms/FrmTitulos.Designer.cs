namespace Neopocket.Forms
{
    partial class FrmTitulos
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.dgTitulos = new Neo.Pocket.Controls.NeoDataGrid();
            this.pagerTitulos = new Neo.Pocket.Controls.NeoDataGridPager();
            this.NeoTableStyle = new System.Windows.Forms.DataGridTableStyle();
            ((System.ComponentModel.ISupportInitialize)(this.dgTitulos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgTitulos
            // 
            this.dgTitulos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgTitulos.Location = new System.Drawing.Point(0, 0);
            this.dgTitulos.Name = "dgTitulos";
            this.dgTitulos.Pager = null;
            this.dgTitulos.Size = new System.Drawing.Size(240, 231);
            this.dgTitulos.TabIndex = 0;
            this.dgTitulos.TableStyles.Add(this.NeoTableStyle);
            // 
            // pagerTitulos
            // 
            this.pagerTitulos.Location = new System.Drawing.Point(41, 237);
            this.pagerTitulos.Name = "pagerTitulos";
            this.pagerTitulos.Owner = null;
            this.pagerTitulos.PageIndex = 0;
            this.pagerTitulos.PageSize = 50;
            this.pagerTitulos.Size = new System.Drawing.Size(176, 31);
            this.pagerTitulos.TabIndex = 1;
            this.pagerTitulos.Text = "neoDataGridPager1";
            // 
            // FrmTitulos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.pagerTitulos);
            this.Controls.Add(this.dgTitulos);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FrmTitulos";
            this.Text = "Títulos em aberto";
            this.Load += new System.EventHandler(this.FrmTitulos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTitulos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Neo.Pocket.Controls.NeoDataGrid dgTitulos;
        private Neo.Pocket.Controls.NeoDataGridPager pagerTitulos;
        private System.Windows.Forms.DataGridTableStyle NeoTableStyle;
    }
}