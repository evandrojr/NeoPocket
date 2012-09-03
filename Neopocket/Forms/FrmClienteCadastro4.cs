using System;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Quarta tela de cadastro do cliente
    /// </summary>
    public partial class FrmClienteCadastro4 : FormBase
    {
        #region [ Construtor ]

        public FrmClienteCadastro4()
        {
            InitializeComponent();
            notification.Caption = "Neo";
        }

        #endregion

        #region [ Fase anterior ]

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (!ValidaCampos())
                return;
            dataSave();
            Util.FormExibir(new FrmClienteCadastro3());
            this.Close();
        }

        #endregion

        #region [ Salvar ]

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
            Concluir();
        }

        #endregion

        #region [ Validação ]

        /// <summary>
        /// Validação dos campos do formulário
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean ValidaCampos()
        {
            Boolean result = true;

            if (!Validator.IsInteger(txtAgencia.Text) && !String.IsNullOrEmpty(txtAgencia.Text))
            {
                notification.Text = "Agência do banco inválida, digite apenas números";
                notification.Visible = true;
                return false;
            }

            if (!Validator.IsInteger(txtRefBanco.Text) && !String.IsNullOrEmpty(txtRefBanco.Text))
            {
                notification.Text = "Referência do banco inválida, digite apenas números";
                notification.Visible = true;
                return false;
            }

            return result;
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

        #region [ Load ]

        private void FrmClienteCadastro4_Load(object sender, EventArgs e)
        {
            if (D.Acao == D.AcaoEnum.ClienteCadastro)
            {
                btnSalvar.Text = "Inserir";
            }
            else if (D.Acao == D.AcaoEnum.ClienteEdicao)
            {
                btnSalvar.Text = "Atualizar";
            }

            this.Carregar();
        }

        #endregion

        #region [ Carrega as informações do cliente ]

        public void Carregar()
        {
            if (D.Cliente.RefBanco != "")
            {
                txtRefBanco.Text = Convert.ToString(D.Cliente.RefBanco);
            }
            if (D.Cliente.Agencia != "")
            {
                txtAgencia.Text = Convert.ToString(D.Cliente.Agencia);
            }
            if (!String.IsNullOrEmpty(D.Cliente.TelefoneAgencia))
            {
                txtTelefone.Text = D.Cliente.TelefoneAgencia;
            }
            if (!String.IsNullOrEmpty(D.Cliente.RefComercial1))
            {
                txtRefComercial1.Text = D.Cliente.RefComercial1;
            }
            if (!String.IsNullOrEmpty(D.Cliente.TelefoneComercial1))
            {
                txtTelefone1.Text = D.Cliente.TelefoneComercial1;
            }
            if (!String.IsNullOrEmpty(D.Cliente.RefComercial2))
            {
                txtRefComercial2.Text = D.Cliente.RefComercial2;
            }
            if (!String.IsNullOrEmpty(D.Cliente.TelefoneComercial2))
            {
                txtTelefone.Text = D.Cliente.TelefoneComercial2;
            }
        }

        #endregion

        #region [ Conclusão da fase ]

        private void Concluir()
        {
            try
            {
                if (!ValidaCampos())
                    return;
                dataSave();

                if (D.Acao == D.AcaoEnum.ClienteCadastro)
                {
                    if (D.Cliente.Inserir())
                    {
                        MessageBox.Show("Cadastro efetuado com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Dados não cadastrados!");
                    }
                }
                if (D.Acao == D.AcaoEnum.ClienteEdicao)
                {
                    if (D.Cliente.Atualizar())
                    {
                        MessageBox.Show("Atualização efetuada com sucesso!");
                    }
                    else
                    {
                        MessageBox.Show("Dados não atualizados!");
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        private void dataSave()
        {
            if (String.IsNullOrEmpty(txtRefBanco.Text))
            {
                D.Cliente.RefBanco = "";
            }
            else D.Cliente.RefBanco = txtRefBanco.Text;

            if (String.IsNullOrEmpty(txtAgencia.Text))
            {
                D.Cliente.Agencia = "";
            }
            else D.Cliente.Agencia = txtAgencia.Text;

            D.Cliente.TelefoneAgencia = txtTelefone.Text;
            D.Cliente.RefComercial1 = txtRefComercial1.Text;
            D.Cliente.TelefoneComercial1 = txtTelefone1.Text;
            D.Cliente.RefComercial2 = txtRefComercial2.Text;
            D.Cliente.TelefoneComercial2 = txtTelefone.Text;
        }

        #endregion
    }
}
