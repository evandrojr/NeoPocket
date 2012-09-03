using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlServerCe;
using Microsoft.WindowsCE.Forms;
using Neopocket.Core;
using Neopocket.Utils;
using System.Text;
using System.Drawing;

namespace Neopocket.Forms
{
    public partial class FrmTitulos : Form
    {
        public FrmTitulos()
        {
            InitializeComponent();
        }


        private void UpdateView()
        {
            try
            {
                String qryTit; ;
                DataTable QueryResult;

                qryTit = @"
                select 
                       ta.a_receber as Devendo, 
                       ta.vencimento_data as Vencimento,
                       ef.descricao as Item, 
                       ta.valor as Valor, 
                       ta.pago as Pago,
                       ta.juros_dinheiro as Juros
                from
                        titulo_aberto ta, especie_financeira ef
                where
                        ta.id_especie_financeira=ef.id_especie_financeira and 
                        ta.id_cliente = " + D.Cliente.IdStore;

                QueryResult = D.Bd.DataTablePreenche(qryTit, "titulo_aberto");

                for (int i = 0; i < QueryResult.Rows.Count; ++i)
                {
                    QueryResult.Rows[i]["Devendo"] = Fcn.DinheiroFormata(Convert.ToDouble(QueryResult.Rows[i]["Devendo"]));
                    QueryResult.Rows[i]["Valor"] = Fcn.DinheiroFormata(Convert.ToDouble(QueryResult.Rows[i]["Valor"]));
                    QueryResult.Rows[i]["Pago"] = Fcn.DinheiroFormata(Convert.ToDouble(QueryResult.Rows[i]["Pago"]));
                    QueryResult.Rows[i]["Juros"] = Fcn.DinheiroFormata(Convert.ToDouble(QueryResult.Rows[i]["Juros"]));
                }

                NeoTableStyle.MappingName = "titulo_aberto";
                dgTitulos.SetBackupDataSource(QueryResult);
                dgTitulos.DataSource = QueryResult;
                dgTitulos.Pager = pagerTitulos;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }

        }

        private void FrmTitulos_Load(object sender, EventArgs e)
        {
            UpdateView();
        }

    }
}