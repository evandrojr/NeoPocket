namespace Neopocket.Forms
{
    partial class FrmOpcao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOpcao));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.mnuSalvar = new System.Windows.Forms.MenuItem();
            this.picOpcao = new System.Windows.Forms.PictureBox();
            this.radDuploClique = new System.Windows.Forms.RadioButton();
            this.radClique = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            this.mainMenu.MenuItems.Add(this.mnuSalvar);
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            // 
            // mnuSalvar
            // 
            this.mnuSalvar.Text = "Salvar";
            // 
            // picOpcao
            // 
            this.picOpcao.Image = ((System.Drawing.Image)(resources.GetObject("picOpcao.Image")));
            this.picOpcao.Location = new System.Drawing.Point(0, 0);
            this.picOpcao.Name = "picOpcao";
            this.picOpcao.Size = new System.Drawing.Size(240, 268);
            // 
            // radDuploClique
            // 
            this.radDuploClique.BackColor = System.Drawing.SystemColors.Control;
            this.radDuploClique.Location = new System.Drawing.Point(133, 127);
            this.radDuploClique.Name = "radDuploClique";
            this.radDuploClique.Size = new System.Drawing.Size(94, 20);
            this.radDuploClique.TabIndex = 1;
            this.radDuploClique.Text = "Duplo Clique";
            // 
            // radClique
            // 
            this.radClique.BackColor = System.Drawing.SystemColors.Control;
            this.radClique.Location = new System.Drawing.Point(133, 90);
            this.radClique.Name = "radClique";
            this.radClique.Size = new System.Drawing.Size(94, 20);
            this.radClique.TabIndex = 2;
            this.radClique.Text = "Um Clique";
            // 
            // FrmOpcao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.radDuploClique);
            this.Controls.Add(this.radClique);
            this.Controls.Add(this.picOpcao);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmOpcao";
            this.Text = "NeoPocket";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picOpcao;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.MenuItem mnuSalvar;
        private System.Windows.Forms.RadioButton radDuploClique;
        private System.Windows.Forms.RadioButton radClique;
    }
}