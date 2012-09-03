namespace Neopocket.Forms
{
    partial class FrmRelatorioClienteSemPedido
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
            this.dtg = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.txtInicio = new System.Windows.Forms.DateTimePicker();
            this.txtFim = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dtg.Location = new System.Drawing.Point(0, 47);
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(240, 218);
            this.dtg.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(86, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.Text = "até";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.Text = "Período";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(194, 20);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(43, 20);
            this.btnFiltrar.TabIndex = 5;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // txtInicio
            // 
            this.txtInicio.CustomFormat = "dd/MM/yyyy";
            this.txtInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtInicio.Location = new System.Drawing.Point(3, 19);
            this.txtInicio.Name = "txtInicio";
            this.txtInicio.Size = new System.Drawing.Size(83, 22);
            this.txtInicio.TabIndex = 0;
            // 
            // txtFim
            // 
            this.txtFim.CustomFormat = "dd/MM/yyyy";
            this.txtFim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtFim.Location = new System.Drawing.Point(108, 19);
            this.txtFim.Name = "txtFim";
            this.txtFim.Size = new System.Drawing.Size(83, 22);
            this.txtFim.TabIndex = 1;
            // 
            // FrmRelatorioClienteSemPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.txtFim);
            this.Controls.Add(this.txtInicio);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "FrmRelatorioClienteSemPedido";
            this.Text = "NeoPocket";
            this.Activated += new System.EventHandler(this.FrmRelatorioClienteSemPedido_Activated);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dtg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.DateTimePicker txtInicio;
        private System.Windows.Forms.DateTimePicker txtFim;

    }
}