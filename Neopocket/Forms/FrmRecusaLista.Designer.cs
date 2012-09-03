namespace Neopocket.Forms
{
    partial class FrmRecusaLista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRecusaLista));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.lblCliente = new System.Windows.Forms.Label();
            this.grdRecusa = new System.Windows.Forms.DataGrid();
            this.dgsRecusa = new System.Windows.Forms.DataGridTableStyle();
            this.gcsCodigo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsDecsricao = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsData = new System.Windows.Forms.DataGridTextBoxColumn();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.picBox1 = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
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
            // picBox
            // 
            this.picBox.Image = ((System.Drawing.Image)(resources.GetObject("picBox.Image")));
            this.picBox.Location = new System.Drawing.Point(-3, 3);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(243, 265);
            // 
            // lblCliente
            // 
            this.lblCliente.BackColor = System.Drawing.SystemColors.Control;
            this.lblCliente.Location = new System.Drawing.Point(39, 0);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(201, 37);
            // 
            // grdRecusa
            // 
            this.grdRecusa.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdRecusa.Location = new System.Drawing.Point(0, 37);
            this.grdRecusa.Name = "grdRecusa";
            this.grdRecusa.Size = new System.Drawing.Size(240, 190);
            this.grdRecusa.TabIndex = 2;
            this.grdRecusa.TableStyles.Add(this.dgsRecusa);
            // 
            // dgsRecusa
            // 
            this.dgsRecusa.GridColumnStyles.Add(this.gcsCodigo);
            this.dgsRecusa.GridColumnStyles.Add(this.gcsDecsricao);
            this.dgsRecusa.GridColumnStyles.Add(this.gcsData);
            // 
            // gcsCodigo
            // 
            this.gcsCodigo.Format = "";
            this.gcsCodigo.HeaderText = "Codigo";
            this.gcsCodigo.MappingName = "id_recusa";
            this.gcsCodigo.Width = 0;
            // 
            // gcsDecsricao
            // 
            this.gcsDecsricao.Format = "";
            this.gcsDecsricao.HeaderText = "Motivo";
            this.gcsDecsricao.MappingName = "descricao";
            this.gcsDecsricao.Width = 100;
            // 
            // gcsData
            // 
            this.gcsData.Format = "";
            this.gcsData.HeaderText = "Data";
            this.gcsData.MappingName = "data_visita";
            this.gcsData.Width = 60;
            // 
            // btnNovo
            // 
            this.btnNovo.Location = new System.Drawing.Point(4, 236);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(72, 20);
            this.btnNovo.TabIndex = 3;
            this.btnNovo.Text = "Novo";
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(82, 236);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 20);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(160, 236);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(72, 20);
            this.btnExcluir.TabIndex = 5;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // picBox1
            // 
            this.picBox1.BackColor = System.Drawing.SystemColors.Control;
            this.picBox1.Image = ((System.Drawing.Image)(resources.GetObject("picBox1.Image")));
            this.picBox1.Location = new System.Drawing.Point(1, 0);
            this.picBox1.Name = "picBox1";
            this.picBox1.Size = new System.Drawing.Size(39, 37);
            this.picBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBox1.Visible = false;
            // 
            // FrmRecusaLista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.picBox1);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.grdRecusa);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.picBox);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmRecusaLista";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmRecusaLista_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.DataGrid grdRecusa;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.DataGridTableStyle dgsRecusa;
        private System.Windows.Forms.DataGridTextBoxColumn gcsCodigo;
        private System.Windows.Forms.DataGridTextBoxColumn gcsDecsricao;
        private System.Windows.Forms.DataGridTextBoxColumn gcsData;
        private System.Windows.Forms.PictureBox picBox1;
    }
}