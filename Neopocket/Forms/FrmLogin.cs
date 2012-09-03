using System;
using System.Windows.Forms;
using System.IO;
using Neopocket.Utils;


namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de autenticação de um usuário no sistema NeoPocket
    /// </summary>
    public partial class FrmLogin : FormBase
    {
        #region [ Construtor ]

        //Unless the login button is pressed this is the default action
        public bool WishLeaveSystem = true;

        public FrmLogin()
        {
            InitializeComponent();

            if (!Directory.Exists(D.APP_LOGDIRECTORY))
                Directory.CreateDirectory(D.APP_LOGDIRECTORY);
            if (!this.ExisteArquivos())
                Util.FormExibir(new FrmConfiguracaoDeAcesso());

            this.CarregarArquivos();

            try
            {
                D.Funcionario.Carregar();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Sair ]

        private void picSair_Click(object sender, EventArgs e)
        {

        }

        private void mniSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region [ Entrar ]

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            this.Entrar();
        }

        private void pbEntrar_Click(object sender, EventArgs e)
        {
            this.Entrar();
        }

        /// <summary>
        /// Método responsável por realizar o login no NeoPocket
        /// </summary>
        protected void Entrar()
        {
            if(D.Funcionario.Valida(txtNome.Text,txtSenha.Text)){
                // Limpa os campos
                this.txtNome.Text = String.Empty;
                this.txtSenha.Text = String.Empty;
                WishLeaveSystem = false;
                Close();
            }
            else
            {
                WishLeaveSystem = true;
                MessageBox.Show("Usuário ou senha incorreto", "Neo");
            }
        }

        #endregion

        #region [ Configurações de acesso ]

        private void picConfiguracoesDeAcesso_Click(object sender, EventArgs e)
        {
            Util.FormExibir(new FrmConfiguracaoDeAcesso());
        }

        #endregion

        #region [ Focus dos campos ]

        private void FrmLogin_GotFocus(object sender, EventArgs e)
        {
            BringToFront();
            inputPanel.Enabled = false;
        }

        private void txtNome_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtSenha_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        #endregion

        #region [ Load ]

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lbVersion.Text = D.APP_VERSION;
            Cursor.Current = Cursors.Default;
            inputPanel.Enabled = false;

        }

        #endregion

        #region [ Checagem de arquivos básicos ]

        /// <summary>
        /// Método responsável por verificar a existência dos arquivos necessários para 
        /// o funcionamento do NeoPocket.
        /// </summary>
        /// <returns>Boolean - True se existem todos arquivo ou False se algum arquivo não existe</returns>
        protected Boolean ExisteArquivos()
        {
            Boolean arquivoNaoEncontrado = true;

            if (!File.Exists(D.AplicacaoDiretorio + "usuario.dat"))
                arquivoNaoEncontrado = false;

            if (!File.Exists(D.AplicacaoDiretorio + "senha.seg"))
                arquivoNaoEncontrado = false;

            if (!File.Exists(D.AplicacaoDiretorio + "ftpusuario.dat"))
                arquivoNaoEncontrado = false;

            if (!File.Exists(D.AplicacaoDiretorio + "ftpsenha.seg"))
                arquivoNaoEncontrado = false;

            if (!File.Exists(D.AplicacaoDiretorio + "vendedorcodigo.seg"))
                arquivoNaoEncontrado = false;

            return arquivoNaoEncontrado;
        }

        #endregion

        #region [ Carrega os arquivos ]

        /// <summary>
        /// Carrega os valores que estão contidos nos arquivos de configuração.
        /// </summary>
        protected void CarregarArquivos()
        {
            try
            {
                StreamReader sr = new StreamReader(D.AplicacaoDiretorio + "usuario.dat");
                D.APP_USER_NAME = sr.ReadLine();
                sr.Close();

                sr = new StreamReader(D.AplicacaoDiretorio + "senha.seg");
                D.APP_USER_PASS = Crypt.Transform(sr.ReadLine());
                sr.Close();

                sr = new StreamReader(D.AplicacaoDiretorio + "ftpusuario.dat");
                D.APP_FTP_USER = sr.ReadLine();
                sr.Close();

                sr = new StreamReader(D.AplicacaoDiretorio + "ftpsenha.seg");
                D.APP_FTP_PASS = Crypt.Transform(sr.ReadLine());
                sr.Close();

                sr = new StreamReader(D.AplicacaoDiretorio + "vendedorcodigo.seg");
                String codigoCriptografado = Crypt.Transform(sr.ReadLine());
                if (!String.IsNullOrEmpty(codigoCriptografado))
                    D.Funcionario.Id = Convert.ToInt32(codigoCriptografado);
                sr.Close();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        private void FrmLogin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WishLeaveSystem == true)
            {
                DialogResult dr = MessageBox.Show("Tem certeza que deseja sair do NeoPocket?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}