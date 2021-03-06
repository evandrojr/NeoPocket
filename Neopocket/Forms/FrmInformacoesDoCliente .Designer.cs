﻿namespace Neopocket.Forms
{
    partial class FrmInformacoesDoCliente
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInformacoesDoCliente));
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.mnuFuncoes = new System.Windows.Forms.MenuItem();
            this.mnuVoltar = new System.Windows.Forms.MenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.lblBairro = new System.Windows.Forms.Label();
            this.lblEndereco = new System.Windows.Forms.Label();
            this.lblTelefone = new System.Windows.Forms.Label();
            this.lblComprador = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtBairro = new System.Windows.Forms.TextBox();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.txtComprador = new System.Windows.Forms.TextBox();
            this.btnPedidos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.Add(this.mnuFuncoes);
            this.mainMenu.MenuItems.Add(this.mnuVoltar);
            // 
            // mnuFuncoes
            // 
            this.mnuFuncoes.Text = "Funções";
            // 
            // mnuVoltar
            // 
            this.mnuVoltar.Text = "Voltar";
            this.mnuVoltar.Click += new System.EventHandler(this.mnuVoltar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 268);
            // 
            // lblNome
            // 
            this.lblNome.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblNome.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblNome.Location = new System.Drawing.Point(41, 26);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(44, 20);
            this.lblNome.Text = "Nome";
            // 
            // lblBairro
            // 
            this.lblBairro.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblBairro.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblBairro.Location = new System.Drawing.Point(41, 58);
            this.lblBairro.Name = "lblBairro";
            this.lblBairro.Size = new System.Drawing.Size(44, 20);
            this.lblBairro.Text = "Bairro";
            // 
            // lblEndereco
            // 
            this.lblEndereco.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblEndereco.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblEndereco.Location = new System.Drawing.Point(24, 94);
            this.lblEndereco.Name = "lblEndereco";
            this.lblEndereco.Size = new System.Drawing.Size(63, 20);
            this.lblEndereco.Text = "Endereço";
            // 
            // lblTelefone
            // 
            this.lblTelefone.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblTelefone.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblTelefone.Location = new System.Drawing.Point(23, 131);
            this.lblTelefone.Name = "lblTelefone";
            this.lblTelefone.Size = new System.Drawing.Size(63, 20);
            this.lblTelefone.Text = "Telefone";
            // 
            // lblComprador
            // 
            this.lblComprador.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblComprador.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblComprador.Location = new System.Drawing.Point(16, 166);
            this.lblComprador.Name = "lblComprador";
            this.lblComprador.Size = new System.Drawing.Size(75, 20);
            this.lblComprador.Text = "Comprador";
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(85, 25);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(131, 21);
            this.txtNome.TabIndex = 6;
            // 
            // txtBairro
            // 
            this.txtBairro.Location = new System.Drawing.Point(85, 57);
            this.txtBairro.Name = "txtBairro";
            this.txtBairro.Size = new System.Drawing.Size(131, 21);
            this.txtBairro.TabIndex = 7;
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(85, 93);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(131, 21);
            this.txtEndereco.TabIndex = 8;
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(85, 130);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(131, 21);
            this.txtTelefone.TabIndex = 9;
            // 
            // txtComprador
            // 
            this.txtComprador.Location = new System.Drawing.Point(87, 166);
            this.txtComprador.Name = "txtComprador";
            this.txtComprador.Size = new System.Drawing.Size(129, 21);
            this.txtComprador.TabIndex = 10;
            // 
            // btnPedidos
            // 
            this.btnPedidos.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnPedidos.Location = new System.Drawing.Point(144, 201);
            this.btnPedidos.Name = "btnPedidos";
            this.btnPedidos.Size = new System.Drawing.Size(72, 29);
            this.btnPedidos.TabIndex = 11;
            this.btnPedidos.Text = "Pedidos";
            // 
            // FrmInformacoesDoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnPedidos);
            this.Controls.Add(this.txtComprador);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.txtEndereco);
            this.Controls.Add(this.txtBairro);
            this.Controls.Add(this.txtNome);
            this.Controls.Add(this.lblComprador);
            this.Controls.Add(this.lblTelefone);
            this.Controls.Add(this.lblEndereco);
            this.Controls.Add(this.lblBairro);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.pictureBox1);
            this.Menu = this.mainMenu;
            this.MinimizeBox = false;
            this.Name = "FrmInformacoesDoCliente";
            this.Text = "NeoPocket";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuItem mnuFuncoes;
        private System.Windows.Forms.MenuItem mnuVoltar;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label lblBairro;
        private System.Windows.Forms.Label lblEndereco;
        private System.Windows.Forms.Label lblTelefone;
        private System.Windows.Forms.Label lblComprador;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.TextBox txtBairro;
        private System.Windows.Forms.TextBox txtEndereco;
        private System.Windows.Forms.TextBox txtTelefone;
        private System.Windows.Forms.TextBox txtComprador;
        private System.Windows.Forms.Button btnPedidos;
    }
}