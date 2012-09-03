using System;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Primeira tela de cadastro do cliente
    /// </summary>
    public partial class FrmClienteCadastro1 : FormBase
    {
        #region [ Construtor ]

        public FrmClienteCadastro1()
        {
            InitializeComponent();
            notification.Caption = "Neo";
            this.Carregar();
        }

        #endregion

        #region [ Carrega as informações do cliente ]

        public void Carregar()
        {
            //Signigica que é um cliente cadastradado pelo store
            if (D.Cliente.IdTipo == Neopocket.Core.Cliente.IdTipoEnum.IdStore)
            {
                if (!String.IsNullOrEmpty(D.Cliente.IdStore.ToString()))
                    txtCodigo.Text = D.Cliente.IdStore.ToString();
            }
            else
            {
                if (!String.IsNullOrEmpty(D.Cliente.Id.ToString()))
                    txtCodigo.Text = D.Cliente.Id.ToString();
            }
            if (!String.IsNullOrEmpty(D.Cliente.NomeFantasia))
            {
                txtNomeFantasia.Text = D.Cliente.NomeFantasia;
            }
            if (!String.IsNullOrEmpty(D.Cliente.RazaoSocial))
            {
                txtRazaoSocial.Text = D.Cliente.RazaoSocial;
            }
            if (!String.IsNullOrEmpty(D.Cliente.CnpjCpf))
            {
                txtCNPJCPF.Text = D.Cliente.CnpjCpf;
            }
            if (!String.IsNullOrEmpty(D.Cliente.RgIncricao))
            {
                txtRG.Text = D.Cliente.RgIncricao;
            }
            if (String.IsNullOrEmpty(D.Cliente.TipoPessoa))
            {
                cboTipoPessoa.SelectedIndex = 1;
            }
            else
            {
                if (D.Cliente.TipoPessoa == "F")
                    cboTipoPessoa.SelectedIndex = 0;
                else
                    cboTipoPessoa.SelectedIndex = 1;
            }
        }

        #endregion

        #region [ Validação ]

        /// <summary>
        /// Validação dos campos do formulário
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean ValidaCampos()
        {
            try
            {
                txtCNPJCPF.Text = Fcn.RemoveAllExceptNumbers(txtCNPJCPF.Text);
                txtRG.Text = Fcn.RemoveAllExceptNumbers(txtRG.Text);

                Boolean validacaoPanel = Util.ValidarPanel(this.pnlValidacao, this.notification);

                if (!D.APP_TEST_MODE)
                {
                    if (cboTipoPessoa.SelectedIndex == 1)
                    {
                        if (!Validator.IsCnpj(txtCNPJCPF.Text))
                        {
                            notification.Text = "CNPJ inválido";
                            notification.Visible = true;
                            return false;
                        }
                    }
                    else
                    {
                        if (!Validator.IsCpf(txtCNPJCPF.Text))
                        {
                            notification.Text = "CPF inválido";
                            notification.Visible = true;
                            return false;
                        }
                    }

                    
                }
                string excludeTheCustomerHimselfSql ="";
                //Permite edição usando o CPF/CNPJ do proprío cliente (senão não passa para a próxima tela!)
                if (D.Acao == D.AcaoEnum.ClienteEdicao)
                {
                    excludeTheCustomerHimselfSql = " and cnpj_cpf <> " + Bd.SA(D.Cliente.CnpjCpf);
                }
                
                if (0 < D.Bd.I(@"
                SELECT count(cnpj_cpf)
                FROM 
                               cliente_cadastrado
                WHERE
                               cnpj_cpf = '" + txtCNPJCPF.Text + "' " + excludeTheCustomerHimselfSql))
                {
                    notification.Text = "Cliente já cadastrado ou alteração de CNPJ/CPF não são permitidas pelo pocket";
                    notification.Visible = true;
                    return false;
                }

                return validacaoPanel;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                return false;
            }
        }

        #endregion

        #region [ Conclusão da fase ]

        private void Concluir()
        {
            D.Cliente.NomeFantasia = txtNomeFantasia.Text;
            D.Cliente.RazaoSocial = txtRazaoSocial.Text;
            D.Cliente.CnpjCpf = txtCNPJCPF.Text;
            D.Cliente.RgIncricao = txtRG.Text;

            if (cboTipoPessoa.SelectedIndex == 0)
            {
                D.Cliente.TipoPessoa = "F";
            }
            else
            {
                D.Cliente.TipoPessoa = "J";
            }

            Util.FormExibir(new FrmClienteCadastro2());
            this.Close();
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

        #region [ Trata o scroll ]

        private void inputPanel_EnabledChanged(object sender, EventArgs e)
        {
            pnlValidacao.AutoScroll = true;
        }

        #endregion

        #region [ Próxima fase ]

        private void btnProximo_Click_1(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                Concluir();
            }
        }

        #endregion

        #region [ Mudança do tipo de pessoa ]

        private void cboTipoPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPessoa.SelectedIndex != 0)
            {
                lblRazaoSocial.Text = "Razão Social:";
                lblNomeFantasia.Text = "Nome Fantasia:";
                lblCNPJCPF.Text = "CNPJ:";
                lblRG.Text = "Insc. Est:";
            }
            else
            {
                lblRazaoSocial.Text = "Nome Completo:";
                lblNomeFantasia.Text = "Apelido:";
                lblCNPJCPF.Text = "CPF:";
                lblRG.Text = "RG:";
            }
        }

        #endregion
    }
}