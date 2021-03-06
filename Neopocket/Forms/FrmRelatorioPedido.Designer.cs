﻿namespace Neopocket
{
    partial class FrmRelatorioPedido
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
            this.cboRelatorios = new System.Windows.Forms.ComboBox();
            this.grdRelatorios = new System.Windows.Forms.DataGrid();
            this.dgsRelatorio = new System.Windows.Forms.DataGridTableStyle();
            this.gcsRelatorioFuncionario = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsRelatorioData = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsRelatorioClienteNome = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsRelatorioValor = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsRelatorioClienteCodigo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.gcsRelatorioStatus = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblClientes = new System.Windows.Forms.Label();
            this.lblTotalClientes = new System.Windows.Forms.Label();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.lblTotalPedidos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboRelatorios
            // 
            this.cboRelatorios.Items.Add("Um mês");
            this.cboRelatorios.Items.Add("Dois meses");
            this.cboRelatorios.Items.Add("Três meses");
            this.cboRelatorios.Items.Add("Quatro meses");
            this.cboRelatorios.Items.Add("Todos");
            this.cboRelatorios.Location = new System.Drawing.Point(3, 5);
            this.cboRelatorios.Name = "cboRelatorios";
            this.cboRelatorios.Size = new System.Drawing.Size(234, 22);
            this.cboRelatorios.TabIndex = 0;
            this.cboRelatorios.SelectedIndexChanged += new System.EventHandler(this.cboRelatorios_SelectedIndexChanged);
            // 
            // grdRelatorios
            // 
            this.grdRelatorios.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.grdRelatorios.Location = new System.Drawing.Point(3, 28);
            this.grdRelatorios.Name = "grdRelatorios";
            this.grdRelatorios.Size = new System.Drawing.Size(234, 194);
            this.grdRelatorios.TabIndex = 1;
            this.grdRelatorios.TableStyles.Add(this.dgsRelatorio);
            this.grdRelatorios.DoubleClick += new System.EventHandler(this.grd_DoubleClick);
            // 
            // dgsRelatorio
            // 
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioFuncionario);
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioData);
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioClienteNome);
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioValor);
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioClienteCodigo);
            this.dgsRelatorio.GridColumnStyles.Add(this.gcsRelatorioStatus);
            this.dgsRelatorio.MappingName = "pedido";
            // 
            // gcsRelatorioFuncionario
            // 
            this.gcsRelatorioFuncionario.Format = "";
            this.gcsRelatorioFuncionario.HeaderText = "Funcionario";
            this.gcsRelatorioFuncionario.MappingName = "id_funcionario";
            this.gcsRelatorioFuncionario.Width = 70;
            // 
            // gcsRelatorioData
            // 
            this.gcsRelatorioData.Format = "";
            this.gcsRelatorioData.HeaderText = "Data Pedido";
            this.gcsRelatorioData.MappingName = "data";
            // 
            // gcsRelatorioClienteNome
            // 
            this.gcsRelatorioClienteNome.Format = "";
            this.gcsRelatorioClienteNome.HeaderText = "Cliente";
            this.gcsRelatorioClienteNome.MappingName = "cliente_nome_reduzido";
            this.gcsRelatorioClienteNome.Width = 80;
            // 
            // gcsRelatorioValor
            // 
            this.gcsRelatorioValor.Format = "c";
            this.gcsRelatorioValor.HeaderText = "Valor";
            this.gcsRelatorioValor.MappingName = "valor";
            // 
            // gcsRelatorioClienteCodigo
            // 
            this.gcsRelatorioClienteCodigo.Format = "";
            this.gcsRelatorioClienteCodigo.HeaderText = "Codigo";
            this.gcsRelatorioClienteCodigo.MappingName = "id_cliente_store";
            this.gcsRelatorioClienteCodigo.Width = 45;
            // 
            // gcsRelatorioStatus
            // 
            this.gcsRelatorioStatus.Format = "";
            this.gcsRelatorioStatus.HeaderText = "Status";
            this.gcsRelatorioStatus.MappingName = "status";
            this.gcsRelatorioStatus.Width = 45;
            // 
            // lblClientes
            // 
            this.lblClientes.Location = new System.Drawing.Point(10, 238);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(103, 31);
            this.lblClientes.Text = "Total de clientes:";
            // 
            // lblTotalClientes
            // 
            this.lblTotalClientes.Location = new System.Drawing.Point(113, 238);
            this.lblTotalClientes.Name = "lblTotalClientes";
            this.lblTotalClientes.Size = new System.Drawing.Size(55, 20);
            // 
            // lblPedidos
            // 
            this.lblPedidos.Location = new System.Drawing.Point(9, 262);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(104, 31);
            this.lblPedidos.Text = "Total de pedidos:";
            // 
            // lblTotalPedidos
            // 
            this.lblTotalPedidos.Location = new System.Drawing.Point(119, 262);
            this.lblTotalPedidos.Name = "lblTotalPedidos";
            this.lblTotalPedidos.Size = new System.Drawing.Size(79, 20);
            // 
            // FrmRelatorioPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(240, 294);
            this.Controls.Add(this.lblTotalPedidos);
            this.Controls.Add(this.lblPedidos);
            this.Controls.Add(this.lblTotalClientes);
            this.Controls.Add(this.lblClientes);
            this.Controls.Add(this.grdRelatorios);
            this.Controls.Add(this.cboRelatorios);
            this.MinimizeBox = false;
            this.Name = "FrmRelatorioPedido";
            this.Text = "NeoPocket";
            this.Load += new System.EventHandler(this.FrmRelatorios_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRelatorios;
        private System.Windows.Forms.DataGrid grdRelatorios;
        private System.Windows.Forms.DataGridTableStyle dgsRelatorio;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioFuncionario;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioData;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioClienteCodigo;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioValor;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioStatus;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.Label lblTotalClientes;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Label lblTotalPedidos;
        private System.Windows.Forms.DataGridTextBoxColumn gcsRelatorioClienteNome;

    }
}