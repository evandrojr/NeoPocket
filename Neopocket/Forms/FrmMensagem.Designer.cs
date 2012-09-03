namespace Neopocket
{
    partial class FrmMensagem
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.TxtMsg = new System.Windows.Forms.TextBox();
            this.TimerVerificaSeTopMost = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Fechar";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // TxtMsg
            // 
            this.TxtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtMsg.Location = new System.Drawing.Point(0, 0);
            this.TxtMsg.Multiline = true;
            this.TxtMsg.Name = "TxtMsg";
            this.TxtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TxtMsg.Size = new System.Drawing.Size(240, 268);
            this.TxtMsg.TabIndex = 0;
            // 
            // TimerVerificaSeTopMost
            // 
            this.TimerVerificaSeTopMost.Interval = 30000;
            this.TimerVerificaSeTopMost.Tick += new System.EventHandler(this.TimerVerificaSeTopMost_Tick);
            // 
            // FrmMensagem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.TxtMsg);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FrmMensagem";
            this.Text = "NeoPocket";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        public System.Windows.Forms.TextBox TxtMsg;
        public System.Windows.Forms.Timer TimerVerificaSeTopMost;
    }
}