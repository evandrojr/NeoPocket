using System;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de cadastro de uma recusa
    /// </summary>
    public partial class FrmRecusa : FormBase
    {
        #region [ Construtor ]

        public FrmRecusa()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Cancelamento ]

        private void mnuCancelar_Click(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
            DialogResult result = MessageBox.Show("Deseja realmente cancelar o cadastro da recusa ?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                Util.FormExibir(new FrmRecusaLista());
                this.Close();
            }
        }

        #endregion

        #region [ Load ]

        private void FrmRecusa_Load(object sender, EventArgs e)
        {
            try
            {
                lblRecusa.Text = "\n " + "Motivo não compra";

                DataTable dtMotivo;
                dtMotivo = D.Bd.DataTablePreenche(@"
            SELECT 0 AS id_motivo, '(selecione)' AS descricao
            UNION
            SELECT id_motivo, descricao
            FROM motivo", "motivo");

                cboMotivo.DataSource = dtMotivo;

                cboMotivo.DisplayMember = "descricao";
                cboMotivo.ValueMember = "id_motivo";

                if (D.Recusa.IdMotivo != 0)
                {
                    cboMotivo.SelectedValue = (int)D.Recusa.IdMotivo;
                }

                if (D.Recusa.Observacao != "")
                {
                    txtObservação.Text = "";
                    txtObservação.Text = D.Recusa.Observacao;
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Conclusão ]

        private void Concluir()
        {
            //........grava motivo ................
            try
            {
                D.Recusa.IdMotivo = (int)cboMotivo.SelectedValue;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                MessageBox.Show(ex.Message);
                return;
            }
            //.......grava observação...............
            try
            {
                D.Recusa.Observacao = txtObservação.Text;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                MessageBox.Show(ex.Message);
                return;
            }
            //..................................................

            try
            {
                if (D.Acao == D.AcaoEnum.RecusaCadastro)
                {
                    if (D.Recusa.inserirTabelaRecusa())
                    {
                        MessageBox.Show("Cadastro efetuado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Dados não cadastrados!");
                    }
                }
                if (D.Acao == D.AcaoEnum.RecusaEdicao)
                {
                    if (D.Recusa.atualizaTabelaRecusa(D.Recusa.Id))
                    {
                        MessageBox.Show("Atualização efetuada com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Dados não atualizados!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region [ Salvar ]

        private void mnuSalvar_Click(object sender, EventArgs e)
        {
            if ((Int32)cboMotivo.SelectedValue == 0)
            {
                MessageBox.Show("Selecione motivo de não compra !", "Neo");
                return;
            }

            if (String.IsNullOrEmpty(txtObservação.Text))
            {
                MessageBox.Show("Campo observação vasio !", "Neo");
                return;
            }

            this.Concluir();
            Util.FormExibir(new FrmRecusaLista());
            this.Close();
        }

        #endregion

        #region [ Observação focus ]

        private void txtObservação_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtObservação_LostFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
        }

        #endregion
    }
}