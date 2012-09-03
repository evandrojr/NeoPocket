namespace Sample
{
    #region Using

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    #endregion

    public partial class FrmMain : Form
    {
        #region Fields

        private DataTable source = new DataTable("Test");

        #endregion

        #region Ctor

        public FrmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Load Event

        private void FrmMain_Load(object sender, EventArgs e)
        {
            #region Preenche o DataTable

            source.Columns.Add("Data");
            source.Columns.Add("Valor");

            DateTime dtInicial = DateTime.Now;

            for (int i = 0; i < 10000; i++)
            {
                dtInicial = dtInicial.AddMonths(1);
                DataRow l = source.NewRow();
                l["Data"] = dtInicial.AddMonths(1);
                l["Valor"] = i.ToString();
                source.Rows.Add(l);
            }

            #endregion

            #region Cria as colunas da NeoGrid

            /* Seguir esta ordem para utilizar a NeoGrid 
             * 1) Cria-se e configura as colunas
             * 2) Adiciona a coluna ao TableStyle da NeoGrid
             * 3) Carrega o datasource com um datatable
             * 4) Se for utilizar paginação, associa o paginador a NeoGrid
             */
            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colData = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colData.HeaderText = "Data";
            colData.ReadOnly = true;
            colData.Owner = NeoGrid;
            colData.MappingName = "Data";
            colData.OnDrawColumnCell += new Neo.Pocket.Controls.NeoDataGridCustomColumnBase.NeoDataGridTextBoxColumnEventHandler(colData_OnDrawColumnCell);
            NeoTableStyle.GridColumnStyles.Add(colData);

            Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn colValor = new Neo.Pocket.Controls.NeoDataGridCustomTextBoxColumn();
            colValor.HeaderText = "Valor";
            colValor.Owner = NeoGrid;
            colValor.MappingName = "Valor";
            colValor.Alignment = HorizontalAlignment.Right;
            colValor.ReadOnly = true;
            //colValor.AlternatingColor = Color.LightBlue; // Linhas com cores alternadas
            NeoTableStyle.GridColumnStyles.Add(colValor);

            NeoTableStyle.MappingName = source.TableName;
            NeoGrid.SetBackupDataSource(source);
            NeoGrid.DataSource = source;
            NeoGrid.Pager = NeoPager;

            #endregion
        }

        #endregion

        #region Evento de pintura das células
        /* Utilizar este evento da coluna customizavel do datagrid para implementar
         * a logica para definir as cores da celular.
         */
        protected void colData_OnDrawColumnCell(object sender, Neo.Pocket.Controls.NeoDataGridCustomColumnEventArgs e)
        {
            if (DateTime.Parse(e.Column.Text) < DateTime.Now)
            {
                e.Column.ForeColor = Color.Red;
            }
            else
            {
                e.Column.ForeColor = Color.Black;
            }
        }

        #endregion

        #region Application Exit

        private void menuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #endregion

        #region Cache Filter

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            NeoGrid.Search(1, txtSearch.Text, Neo.Pocket.Controls.NeoDataGridSearchMode.Like);
        }

        #endregion
    }
}