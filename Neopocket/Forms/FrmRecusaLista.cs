using System;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de listagem das recusas dos clientes
    /// </summary>
    public partial class FrmRecusaLista : FormBase
    {
        #region [ Construtor ]

        public FrmRecusaLista()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Voltar ]

        private void mnuVoltar_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.PedidoADefinir;
            Util.FormExibir(new FrmClienteLista());
            this.Close();
        }

        #endregion

        #region [ Load ]

        private void FrmRecusaLista_Load(object sender, EventArgs e)
        {
            if (D.Cliente.TipoPessoa == "F")
            {
                picBox1.Visible = true;
            }
            lblCliente.Text = " \n    " + D.Cliente.NomeFantasia;
            this.Refrescar();
        }

        #endregion

        #region [ Nova recusa ]

        private void btnNovo_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RecusaCadastro;
            Util.FormExibir(new FrmRecusa());
            this.Close();
        }

        #endregion

        #region [ Edição da recusa ]

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int i = grdRecusa.CurrentRowIndex;
            if (i == -1)
                return;

            D.Acao = D.AcaoEnum.RecusaEdicao;
            D.Recusa.Carregar(Convert.ToInt32(grdRecusa[i, 0]));

            if (Convert.ToString(grdRecusa[i, 3]) == "N")
            {
                Util.FormExibir(new FrmRecusa());
                this.Close();
            }
            else
                MessageBox.Show("Recusa sincronizada não pode ser alterada! ", "Neo");
        }

        #endregion

        #region [ Exclusão de uma recusa ]

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            int i = grdRecusa.CurrentRowIndex;
            if (i == -1)
                return;

            D.Recusa.Id = Convert.ToInt32(grdRecusa[i, 0]);

            if (Convert.ToString(grdRecusa[i, 3]) == "N")
            {
                if (D.Recusa.ExcluirRecusa(D.Recusa.Id))
                {
                    MessageBox.Show("Recusa excluída !", "Neo");
                }
                else
                {
                    MessageBox.Show("Erro excluindo recusa !", "Neo");
                }

                this.Refrescar();
                btnNovo.Enabled = true;
            }
            else
                MessageBox.Show("Recusa sincronizada não pode ser removida! ", "Neo");
        }

        #endregion

        #region [ Carrega a listagem ]

        private void Refrescar()
        {
            DataTable dtRecusa;
            string sql = @"SELECT  recusa.id_recusa as Codigo , motivo.descricao as Motivo , recusa.data_visita as Data , recusa.status as Status   
                                                FROM recusa LEFT JOIN motivo ON recusa.id_motivo = motivo.id_motivo  
                                                WHERE recusa.id_cliente = " + D.Cliente.IdStore + " order by recusa.status ";

            dtRecusa = D.Bd.DataTablePreenche(sql);
            grdRecusa.DataSource = dtRecusa;


            if (grdRecusa.VisibleRowCount == 0)
            {
                btnEditar.Visible = false;
                btnExcluir.Visible = false;
            }
        }

        #endregion
    }
}