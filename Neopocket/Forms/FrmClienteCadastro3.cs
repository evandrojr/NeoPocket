using System;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;

namespace Neopocket.Forms
{
    /// <summary>
    /// Terceira tela de cadastro do cliente
    /// </summary>
    public partial class FrmClienteCadastro3 : FormBase
    {
        #region [ Construtor ]

        public FrmClienteCadastro3()
        {
            InitializeComponent();
            notification.Caption = "Neo";
        }

        #endregion

        #region [ Fase anterior ]

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            FrmClienteCadastro2 f = new FrmClienteCadastro2();
            this.Navegar(f);
            this.Close();
        }

        #endregion

        #region [ Validação ]

        /// <summary>
        /// Validação dos campos do formulário
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean ValidaCampos()
        {
            Boolean validacaoPanel = Util.ValidarPanel(this.pnlValidacao, this.notification);

            if (!Validator.IsReal(txtLimite.Text))
            {
                notification.Text = "Limite de crédito inválido.";
                notification.Visible = true;
                return false;
            }

            return validacaoPanel;
        }

        #endregion

        #region [ Cancelamento ]

        private void mnuCancelar_Click(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
            DialogResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
                this.Close();
        }

        #endregion

        #region [ Conclusão da fase ]



        private void Navegar(FormBase frm)
        {
            D.Cliente.Comprador = txtComprador.Text;
            D.Cliente.LimiteCreditoBd = Convert.ToDouble(txtLimite.Text);

            DataRowView drFormaPagamento = (DataRowView)cboPrazo.SelectedItem;
            D.Cliente.IdFormaPagamentoPadrao = (int)drFormaPagamento["id_forma_pagamento"];

            D.Cliente.DiaVisita = Convert.ToInt32(((cboDiaVisita.SelectedIndex + 1) % 7) + 1);
            D.Cliente.Intervalo = Convert.ToString(cboIntervaloVisita.SelectedItem);
            D.Cliente.Nascimento = dtpDataNascimento.Value;

            Util.FormExibir(frm);
            this.Close();
        }

        #endregion

        #region [ Load ]

        private void FrmClienteCadastro3_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtFormaPagamento;
                dtFormaPagamento = D.Bd.DataTablePreenche("Select id_forma_pagamento, descricao from forma_pagamento", "forma_pagamento");
                cboPrazo.DataSource = dtFormaPagamento;
                cboPrazo.DisplayMember = "descricao";
                cboPrazo.ValueMember = "descricao";

                this.Carregar();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Carrega as informações do cliente ]

        public void Carregar()
        {
            if (D.Cliente.IdFormaPagamentoPadrao != 0)
            {
                cboPrazo.SelectedText = D.Bd.T("Select descricao from forma_pagamento where id_forma_pagamento =" + D.Cliente.IdFormaPagamentoPadrao);
            }

            if (!String.IsNullOrEmpty(D.Cliente.Comprador))
            {
                txtComprador.Text = D.Cliente.Comprador;
            }

            if (D.Acao == D.AcaoEnum.ClienteCadastro)
            {
                txtLimite.Text = Convert.ToString(Parametro.LimiteDeCreditoPadrao);
            }
            else
            {
                txtLimite.Text = Convert.ToString(D.Cliente.LimiteCreditoBd);
            }

            if (D.Cliente.DiaVisita != 0)
            {
                cboDiaVisita.SelectedIndex = (D.Cliente.DiaVisita + 5) % 7;
            }

            if (D.Cliente.Intervalo != null)
            {
                if (!String.IsNullOrEmpty(D.Cliente.Intervalo))
                {
                    cboIntervaloVisita.SelectedItem = D.Cliente.Intervalo.Trim();
                }
            }
            try
            {
                if (D.Cliente.Nascimento.ToString() != "1/1/01 00:00:00")
                    dtpDataNascimento.Value = D.Cliente.Nascimento;
            }
            catch { }
        }

        #endregion

        #region [ Próxima fase ]

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            if (this.ValidaCampos())
            {
                FrmClienteCadastro4 f = new FrmClienteCadastro4();
                this.Navegar(f);
            }
        }

        #endregion
    }
}
