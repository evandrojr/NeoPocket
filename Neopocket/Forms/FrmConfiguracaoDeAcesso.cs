using System;
using System.Windows.Forms;
using System.IO;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de configuração de acesso ao NeoPocket.
    /// </summary>
    public partial class FrmConfiguracaoDeAcesso : FormBase
    {
        #region [ Construtor ]

        public FrmConfiguracaoDeAcesso()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Load ]

        private void FrmConfiguracaoDeAcesso_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            this.CarregaCampos();
        }

        #endregion

        #region [ Verificação do código único de acesso ]

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtCodigoAcesso.Text != "veb")
            {
                MessageBox.Show("Senha incorreta!");
                return;
            }

            pnlAcesso.Visible = false;
            pnlConfiguracao.Visible = true;
        }

        #endregion

        #region [ Carrega os campos de configuração ]

        /// <summary>
        /// Método responsável por carregar as informações de configuração do NeoPocket para edição.
        /// </summary>
        private void CarregaCampos()
        {
            txtNome.Text = D.APP_USER_NAME;
            txtSenha.Text = D.APP_USER_PASS;
            txtCodigoVendedor.Text = D.Funcionario.Id.ToString();
            txtUsuarioFTP.Text = D.APP_FTP_USER;
            txtSenhaFTP.Text = D.APP_FTP_PASS;
            try
            {
                if (
                        (String.IsNullOrEmpty(D.APP_FTP_USER)) ||
                        (String.IsNullOrEmpty(D.APP_FTP_PASS)) ||
                        (String.IsNullOrEmpty(D.Funcionario.Nome)) ||
                        (String.IsNullOrEmpty(D.Funcionario.Id.ToString()))
                    )
                {
                    CriarConfig();
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                MessageBox.Show("O dispositivo ainda não foi configurado!", "Alerta");
            }
        }

        #endregion

        #region [ Fechamento das configurações ]

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region [ Salvar as configurações ]

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                D.APP_USER_NAME = txtNome.Text;
                D.APP_USER_PASS = txtSenha.Text;
                D.Funcionario.Id = Convert.ToInt32(txtCodigoVendedor.Text);
                D.APP_FTP_PASS = txtSenhaFTP.Text;
                D.APP_FTP_USER = txtUsuarioFTP.Text;

                try
                {
                    File.Delete(D.AplicacaoDiretorio + "usuario.dat");
                }
                catch { }
                try
                {
                    File.Delete(D.AplicacaoDiretorio + "senha.seg");
                }
                catch { }
                try
                {
                    File.Delete(D.AplicacaoDiretorio + "ftpusuario.dat");
                }catch { }
                try
                {
                    File.Delete(D.AplicacaoDiretorio + "ftpsenha.seg");
                }catch { }
                try
                {
                    File.Delete(D.AplicacaoDiretorio + "vendedornome.dat");
                }
                catch { }
                try
                {
                    File.Delete(D.AplicacaoDiretorio + "vendedorcodigo.seg");
                }
                catch { }

                TextWriter tw;
                tw = new StreamWriter(D.AplicacaoDiretorio + "usuario.dat", true);
                tw.WriteLine(D.APP_USER_NAME);
                tw.Close();
                tw = new StreamWriter(D.AplicacaoDiretorio + "senha.seg", true);
                tw.WriteLine(Crypt.Transform(D.APP_USER_PASS));
                tw.Close();
                tw = new StreamWriter(D.AplicacaoDiretorio + "ftpusuario.dat", true);
                tw.WriteLine(D.APP_FTP_USER);
                tw.Close();
                tw = new StreamWriter(D.AplicacaoDiretorio + "ftpsenha.seg", true);
                tw.WriteLine(Crypt.Transform(D.APP_FTP_PASS));
                tw.Close();
                tw = new StreamWriter(D.AplicacaoDiretorio + "vendedorcodigo.seg", true);
                tw.WriteLine(Crypt.Transform(D.Funcionario.Id.ToString()));
                tw.Close();
                this.DialogResult = DialogResult.OK;

                string usuario = null, senha = null, ftpusuario = null, ftpsenha = null, vendedorcodigo=null;

                //Abre novamente tudo para checar se foi salvo com sucesso
                using (TextReader reader = File.OpenText(D.AplicacaoDiretorio + "usuario.dat"))
                {
                    usuario = reader.ReadLine();
                }

                using (TextReader reader = File.OpenText(D.AplicacaoDiretorio + "senha.seg"))
                {
                    senha = Crypt.Transform(reader.ReadLine());
                }

                using (TextReader reader = File.OpenText(D.AplicacaoDiretorio + "ftpusuario.dat"))
                {
                    ftpusuario = reader.ReadLine();
                }

                using (TextReader reader = File.OpenText(D.AplicacaoDiretorio + "ftpsenha.seg"))
                {
                    ftpsenha = Crypt.Transform(reader.ReadLine());
                }

                using (TextReader reader = File.OpenText(D.AplicacaoDiretorio + "vendedorcodigo.seg"))
                {
                    vendedorcodigo = Crypt.Transform(reader.ReadLine());
                }

                MessageBox.Show("Configurações armazenadas para o usuário " + usuario + " senha " + senha + " codigo " + vendedorcodigo + " usuário ftp " + ftpusuario + " senha ftp " + ftpsenha + " caso algum estes dados esteja incorreto, favor desligar e religar o pocket e repetir este procedimento. Pois se algum dado estiver incorreto então não foi possível atualizar os dados da configuração.", "--- ATENÇÃO!!! ---");
                this.Close();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        private static void CriarConfig()
        {
            FileStream fs;

            fs = File.Create(D.AplicacaoDiretorio + "usuario.dat");
            fs.Close();
            fs = File.Create(D.AplicacaoDiretorio + "senha.seg");
            fs.Close();
            fs = File.Create(D.AplicacaoDiretorio + "ftpusuario.dat");
            fs.Close();
            fs = File.Create(D.AplicacaoDiretorio + "ftpsenha.seg");
            fs.Close();
            fs = File.Create(D.AplicacaoDiretorio + "vendedorcodigo.seg");
            fs.Close();
        }

        #endregion

        #region [ Limpar dados do NeoPocket ]

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza? Isso irá apagar todos os dados da base de dados utilizada pelo Neo Pocket", "Alerta!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                return;
            D.Bd.DadosApagarCompletamente();
        }

        #endregion

        private void btnForcarAtualizacao_Click(object sender, EventArgs e)
        {
            try{
                File.Delete(D.AplicacaoDiretorio + "neoupdate.ini");
            }catch{}

            try
            {
                File.Delete(D.AplicacaoDiretorio + "neoupdater.ini");
            }
            catch { }

            MessageBox.Show("Agora desligue o pocket, ligue e chame e neoupdater");

        }
    }
}