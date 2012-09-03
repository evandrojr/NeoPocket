using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Neopocket.Forms;
using Neopocket.Utils;
using Microsoft.WindowsCE.Forms;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de relatório de clientes por cidade
    /// </summary>
    public partial class FrmRelatorioClienteSemPedido : FormBase
    {
        #region [ Construtor ]

        public FrmRelatorioClienteSemPedido()
            : base(false)
        {
            InitializeComponent();
            D.Acao = D.AcaoEnum.RelatorioVer;
            this.Filtrar();
        }

        #endregion

        #region [ Carrega o relatório ]

        protected void Filtrar()
        {
            this.Carregar(txtInicio.Value, txtFim.Value);
        }

        protected void Carregar(DateTime? dtInicio, DateTime? dtFim)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("cliente_nome, cliente_nome_reduzido, endereco, cidade");
                query.AppendLine("FROM cliente");
                query.AppendLine("WHERE id_cliente_pocket not in (");
                query.AppendLine("SELECT id_cliente_pocket FROM pedido p");
                if ((dtInicio != null) && (dtFim != null))
                    query.AppendLine("WHERE p.data BETWEEN " + dtInicio.Value.Date.ToString("dd/MM/yyyy") + " AND " + dtFim.Value.Date.ToString("dd/MM/yyyy"));
                query.AppendLine(")");
                query.AppendLine("ORDER BY cliente_nome");
                dtg.DataSource = D.Bd.DataTablePreenche(query.ToString());
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Click do filtrar ]

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtrar();
        }

        #endregion

        #region [ Form Activated ]

        private void FrmRelatorioClienteSemPedido_Activated(object sender, EventArgs e)
        {
            if (SystemSettings.ScreenOrientation != ScreenOrientation.Angle90)
                SystemSettings.ScreenOrientation = ScreenOrientation.Angle90;
        }

        #endregion
    }
}