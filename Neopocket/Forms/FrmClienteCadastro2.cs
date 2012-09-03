using System;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Segunda tela de cadastro do cliente
    /// </summary>
    public partial class FrmClienteCadastro2 : FormBase
    {
        #region [ Construtor ]

        public FrmClienteCadastro2()
        {
            InitializeComponent();
            notification.Caption = "Neo";
        }

        #endregion

        #region [ Fase anterior ]

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (this.ValidaCampos())
            {
                FrmClienteCadastro1 f = new FrmClienteCadastro1();
                this.Navegar(f);
                this.Close();
            }


        }

        #endregion

        #region [ Próxima fase ]

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (this.ValidaCampos())
            {
                FrmClienteCadastro3 f = new FrmClienteCadastro3();
                this.Navegar(f);
                this.Close();
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
            Boolean validacaoPanel = Util.ValidarPanel(this.pnlValidacao, this.notification);

            if (!Validator.ValidaCEP(txtCEP.Text))
            {
                notification.Text = " CEP inválido !";
                notification.Visible = true;
                return false;
            }

            return validacaoPanel;
        }

        #endregion

        #region [ Conclusão da fase ]

        private void Navegar(FormBase frm)
        {
            D.Cliente.Endereco = txtEndereco.Text;
            D.Cliente.Bairro = txtBairro.Text;
            DataRowView drCidade = (DataRowView)cboCidade.SelectedItem;
            D.Cliente.Cidade = (int)drCidade["id_cidade"];
            D.Cliente.Telefone = txtTelefone.Text;
            D.Cliente.Cep = txtCEP.Text;

            Util.FormExibir(frm);
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

        #region [ Load ]

        private void FrmClienteCadastro2_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCidades;
                dtCidades = D.Bd.DataTablePreenche("Select id_cidade, descricao from cidade order by descricao", "cidade");
                cboCidade.DataSource = dtCidades;
                cboCidade.DisplayMember = "descricao";
                cboCidade.ValueMember = "id_cidade";

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
            if (!String.IsNullOrEmpty(D.Cliente.Endereco))
            {
                txtEndereco.Text = D.Cliente.Endereco;
            }
            if (!String.IsNullOrEmpty(D.Cliente.Bairro))
            {
                txtBairro.Text = D.Cliente.Bairro;
            }
            if (!String.IsNullOrEmpty(D.Cliente.Telefone))
            {
                txtTelefone.Text = D.Cliente.Telefone;
            }
            if (!String.IsNullOrEmpty(D.Cliente.Cep))
            {
                txtCEP.Text = D.Cliente.Cep;
            }
            if (D.Cliente.Cidade != 0)
            {
                cboCidade.SelectedValue = D.Cliente.Cidade;
            }
            else
            {
                cboCidade.SelectedText = "Salvador";
            }
        }

        #endregion
    }
}