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

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de relatório de clientes por cidade
    /// </summary>
    public partial class FrmRelatorioClienteCidade : FormBase
    {
        #region [ Construtor ]

        public FrmRelatorioClienteCidade()
        {
            InitializeComponent();
            D.Acao = D.AcaoEnum.RelatorioVer;
            this.Carregar();
        }

        #endregion

        #region [ Carrega o relatório ]

        protected void Carregar()
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendLine("SELECT");
                query.AppendLine("cliente_nome, cliente_nome_reduzido, endereco, bairro, cidade, uf_cod,");
                query.AppendLine("CASE lista_negra WHEN 'False' THEN 'Não' WHEN 'True' THEN 'Sim' ELSE 'desconhecido' END AS ListaNegra");
                query.AppendLine("FROM cliente");
                query.AppendLine("ORDER BY cidade DESC");
                dtg.DataSource = D.Bd.DataTablePreenche(query.ToString());
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion
    }
}