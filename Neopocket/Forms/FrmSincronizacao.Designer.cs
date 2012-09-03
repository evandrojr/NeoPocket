namespace Neopocket.Forms
{
    partial class FrmSincronizacao
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
            this.mniRetornar = new System.Windows.Forms.MenuItem();
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.TxtMsg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mniRetornar
            // 
            this.mniRetornar.Enabled = false;
            this.mniRetornar.Text = "Retornar";
            this.mniRetornar.Click += new System.EventHandler(this.mniRetornar_Click);
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mniRetornar);
            // 
            // TxtMsg
            // 
            this.TxtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtMsg.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.TxtMsg.Location = new System.Drawing.Point(0, 0);
            this.TxtMsg.Multiline = true;
            this.TxtMsg.Name = "TxtMsg";
            this.TxtMsg.ReadOnly = true;
            this.TxtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtMsg.Size = new System.Drawing.Size(240, 268);
            this.TxtMsg.TabIndex = 0;
            // 
            // FrmSincronizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.TxtMsg);
            this.Menu = this.mainMenu;
            this.Name = "FrmSincronizacao";
            this.Text = "NeoPocket";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mniRetornar;
        private System.Windows.Forms.MainMenu mainMenu;
        private System.Windows.Forms.TextBox TxtMsg;
    }
}