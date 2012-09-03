using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NeoDebug;
using ShowLib;
using Core;

namespace NeoSync
{
    public partial class FrmPrincipal : Form
    {
        Roteiro r = null;
        Boolean cancelled; //Informa que a sincronização foi cancelada

        private DateTime proximaExecucao;
        int minutosEntreExecucoes = 5;


        public FrmPrincipal()
        {
            InitializeComponent();
        }

        public void Start()
        {
            r = new Roteiro(this);
            string msg = r.Iniciar();
            if (msg != "")
            {
                MessageBox.Show(msg);
                Close();
            }
            else
            {
                lblStatus.Text = "Conectado ao banco de dados: " + D.BancoAlvo;
            }

            Op.AtualizationInProgressCheck();

            tmrSincronizacao.Interval = minutosEntreExecucoes * 60 * 1000;
            btnChegouCadastro.Visible = false;
            tmrSincronizacao.Enabled = true;
            tmrPiscar.Enabled = true;
        }

        private void someEventHandler(Object sender, EventArgs args)
        {
            //if you want this event handler executed for just once
            //DispatcherTimer thisTimer = (DispatcherTimer)sender;
            //thisTimer.Stop();
            proximaExecucao = DateTime.Now;
            proximaExecucao = proximaExecucao.AddMilliseconds(minutosEntreExecucoes * 60 * 1000);
            lblProximaExecucao.Text = "Próxima sincronização automática em: " + proximaExecucao.ToString();
            SincronizacaoIniciar();
        }



        void DoLengthyOperation(Object param)
        {
            MsgAppend("Iniciando a sincronização");

            if (!D.ModoTeste)
            {
                try
                {
                    Roteiro r = new Roteiro(this);
                    string msg = r.Sincronizar();
                    if (msg != "")
                        MsgAppend(msg);
                }
                catch (Exception ex)
                {
                    HandleUnhandledException(ex);
                }
            }
            else
            {
                Roteiro r = new Roteiro(this);
                string msg = r.Sincronizar();
                if (msg != "")
                    MsgAppend(msg);
            }
            // Re-enable UI
            this.SetDoingLengthyOperation(false);
        }

        // Enables and disables UI, also makes sure it runs on UI thread
        void SetDoingLengthyOperation(Boolean working)
        {
            if (this.InvokeRequired)
            { // Make sure we run on UI thread
                // Create a delegate to self
                HelperDelegate setDoingLengthyOperation =
                   new HelperDelegate(this.SetDoingLengthyOperation);
                // Roll arguments in an Object array
                Object[] arguments = new Object[] { working };
                // "Recurse once, onto another thread"
                    this.Invoke(setDoingLengthyOperation, arguments);
                return; // return;
            }

            // If this is executing then the call occured on the UI thread
            // so we can freely access controls
            if (working)
            {
                TabControl.SelectedTab = TabPagePrincipal;
                //TabPagePrincipal.Select();
                //TabPagePrincipal.Focus();

                salvar();
                BtnSincronizar.Enabled = false;
                //btnCancelar.Enabled = true;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                BtnSincronizar.Enabled = true;
                this.Cursor = Cursors.Arrow;
                tmrSincronizacao.Enabled = true;
            }
        }

        delegate void HelperDelegate(Boolean working);

        public void UnhandledThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            if(!D.ModoTeste)
                this.HandleUnhandledException(e.Exception);
        }

        public void HandleUnhandledException(Exception e)
        {
            if (D.ModoTeste)
            {
                throw e;
            }

            MsgAppend("Erro" + e.Message);
            //Já faz o log da exceção aqui, via email e FS
            FE.Show(e);

            //Chama o programa novamente
            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo.WorkingDirectory = D.AplicacaoDiretorio;
            //proc.StartInfo.FileName = "NeoSync.exe";
            //proc.StartInfo.Arguments = D.AplicacaoDiretorio + " " + D.ApplicationName;
            //proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.RedirectStandardOutput = false;
            //proc.StartInfo.RedirectStandardError = false;
            //proc.Start();
            //proc.WaitForExit();
            //proc.Close();
            //Application.Exit();
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            SincronizacaoIniciar();
        }

        private void SincronizacaoIniciar()
        {
            tmrSincronizacao.Enabled = false;
            SetDoingLengthyOperation(true);

            // Call asynchronously method that does work that takes time
            WaitCallback doWork =
               new WaitCallback(this.DoLengthyOperation);
            cancelled = false;
            ThreadPool.QueueUserWorkItem(doWork, 500);
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {


            lblVersion.Text = "Ver " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            SetDoingLengthyOperation(false);
            Roteiro r = new Roteiro(this);
            camposCarregar();
            BtnSincronizar.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelled = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            salvar();
        }

        private void btnReverter_Click(object sender, EventArgs e)
        {
            camposCarregar();
        }


        public void MsgAppend(string msg)
        {
            TxtMsg.Invoke(new TxtMsgAppendCallback(this.TxtMsgAppend),
                    new object[] { msg });
        }

        private void TxtMsgAppend(string text)
        {
            // Set the textbox text.
            TxtMsg.Text = text + Environment.NewLine + TxtMsg.Text;
        }

        public delegate void TxtMsgAppendCallback(string text);

        public void AlertaAtivo(bool visivel)
        {
            TxtMsg.Invoke(new BtnChegouCadastroVisivelCallback(this.btnChegouCadastroVisivel),
                    new object[] { visivel });
        }

        private void btnChegouCadastroVisivel(bool visivel)
        {
            btnChegouCadastro.Visible = visivel;
        }

        public delegate void BtnChegouCadastroVisivelCallback(bool visivel);

        private void salvar()
        {
            D.Loja = lstLoja.SelectedItem.ToString().Substring(0, 6);
            if(txtDiretorio.Text.Length > 0)
                if (txtDiretorio.Text[(txtDiretorio.Text.Length - 1)] != '/')
                    txtDiretorio.Text += "/";
            if (txtDiretorio.Text.Length > 1 && txtDiretorio.Text[0] != '/')
                txtDiretorio.Text = "/" + txtDiretorio.Text;

            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtServidor.Text + "' where NOME='POCKET_FTP_SERVIDOR'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtUsuario.Text + "' where NOME='POCKET_FTP_USUARIO'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtSenha.Text + "' where NOME='POCKET_FTP_SENHA'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtSenha.Text + "' where NOME='POCKET_FTP_SENHA'");
//            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtPorta.Text + "' where NOME='POCKET_FTP_PORTA'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='' where NOME='POCKET_FTP_DIRETORIO'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtPedidoQtdMaxAntesSincronizacao.Text + "' where NOME='POCKET_LIMITE_PEDIDOS_EM_ABERTO'");
            D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtPedidoQtdMaxAntesSincronizacao.Text + "' where NOME='POCKET_LIMITE_PEDIDOS_EM_ABERTO'");

            //D.Bd.ExecuteNonQuery("Update parametro set valor='" + tx + "' where NOME='POCKET_LIMITE_PEDIDOS_EM_ABERTO'");

            //........................................Outras configuracoes....................................................
            if (chkVerificarCreditosParaVendaAPrazo.Checked == true)
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 1 + "' where NOME='POCKET_VERIFICAR_CREDITO'");
            }
            else
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 0 + "' where NOME='POCKET_VERIFICAR_CREDITO'");
            }
            //...........................................................

            if (chkPermitirPedidoComEstoqueZerado.Checked == true)
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 1 + "' where NOME='POCKET_VENDER_SEM_ESTOQUE'");
            }
            else
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 0 + "' where NOME='POCKET_VENDER_SEM_ESTOQUE'");
            }

            //...........................................................

            if (chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Checked == true)
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 1 + "' where NOME='POCKET_PERIODICAMENTE_ENVIAR_ID_CLIENTES'");
            }
            else
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 0 + "' where NOME='POCKET_PERIODICAMENTE_ENVIAR_ID_CLIENTES'");
            }
            
            //...........................................................

            if (chkEnviarTodosOsClientesParaTodosOsVendedores.Checked == true)
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 1 + "' where NOME='POCKET_ENVIAR_TODOS_CLIENTES_PARA_TODOS_VENDEDORES'");
            }
            else
            {
                D.Bd.ExecuteNonQuery("Update parametro set valor='" + 0 + "' where NOME='POCKET_ENVIAR_TODOS_CLIENTES_PARA_TODOS_VENDEDORES'");
            }
            
            //...........................................................
            //if (chkPermitirEscolherTabelaDePreco.Checked == true)
            //{
            //    D.Bd.ExecuteNonQuery("Update parametro set valor='" + 1 + "' where NOME='POCKET_ESCOLHER_TABELA'");
            //}
            //else
            //{
            //    D.Bd.ExecuteNonQuery("Update parametro set valor='" + 0 + "' where NOME='POCKET_ESCOLHER_TABELA'");
            //}
            //...........................................................

            //D.Bd.ExecuteNonQuery("Update parametro set valor='" + txtDias.Text + "' where NOME='POCKET_DIAS_PERMANENCIA'");

            lblStatus.Text = "Dados armazenados";
        }

        private void camposCarregar()
        {
            DataTable dsLoja;
            
            //Carregar valor dos parametros antes de mostrá-los
            Parametro.Carregar();

            dsLoja = D.Bd.DataTablePreenche(@"
                SELECT
                        codigo, nome_fantasia
                FROM    
                        loja", "loja");

            lstLoja.Items.Clear();
            for (int i = 0; i < dsLoja.Rows.Count; ++i)
            {
                lstLoja.Items.Add(dsLoja.Rows[i][0].ToString() + " - " + dsLoja.Rows[i][1].ToString());
            }

            string lojaDefault=D.Bd.T("Select valor from Parametro where nome ='LOJA_DEFAULT'");
            lstLoja.SelectedIndex = 0;

            if (D.ArgLoja == "")
            {
                for (int i = 0; i < dsLoja.Rows.Count; ++i)
                {
                    if (dsLoja.Rows[i][0].ToString() == lojaDefault)
                    {
                        lstLoja.SelectedIndex = i;
                        break;
                    }
                }
            }else{
                for (int i = 0; i < dsLoja.Rows.Count; ++i)
                {
                    if (dsLoja.Rows[i][0].ToString() == D.ArgLoja)
                    {
                        lstLoja.SelectedIndex = i;
                        break;
                    }
                }
            }

            txtServidor.Text = D.Bd.T("Select VALOR from PARAMETRO where NOME='POCKET_FTP_SERVIDOR'");
            if (D.ArgFtpUsuario == "")
                txtUsuario.Text = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_USUARIO'");
            else
                txtUsuario.Text = D.ArgFtpUsuario;
            txtSenha.Text = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_SENHA'");
//            txtPorta.Text = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_PORTA'");
            txtPedidoQtdMaxAntesSincronizacao.Text = Parametro.LimitePedidosEmAberto.ToString();
            chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Checked = Parametro.PeriodicamenteEnviarIdClientes;
            chkVerificarCreditosParaVendaAPrazo.Checked = Parametro.VerificarCreditoVendaAPrazo;
            chkPermitirPedidoComEstoqueZerado.Checked = Parametro.VenderSemEstoque;
            chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Checked = Parametro.PeriodicamenteEnviarIdClientes;
            chkEnviarTodosOsClientesParaTodosOsVendedores.Checked = Parametro.EnviarTodosClientesParaTodosOsVendedores;
//          txtDias.Text = D.Bd.T("Select valor from parametro where nome='POCKET_DIAS_PERMANENCIA'");
//          lblStatus.Content = "Conectado ao banco de dados: " + D.BancoAlvo;
        }

        private void tmrSincronização_Tick(object sender, EventArgs e)
        {
            //Lá dentro ele para o timer           
            SincronizacaoIniciar();
        }

        private void tmrPiscar_Tick(object sender, EventArgs e)
        {
            if (btnChegouCadastro.ForeColor == Color.Red)
            {
                btnChegouCadastro.ForeColor = Color.Black;
                btnChegouCadastro.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                btnChegouCadastro.ForeColor = Color.Red;
                btnChegouCadastro.FlatStyle = FlatStyle.Standard;
            }
        }

        private void btnChegouCadastro_Click(object sender, EventArgs e)
        {
            btnChegouCadastro.Visible = false;
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            FrmRelPedido f = new FrmRelPedido();
            f.Show();
        }

        private void chkEnviarTodosOsClientesParaTodosOsVendedores_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnviarTodosOsClientesParaTodosOsVendedores.Checked)
                chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Checked = false;
        }

        private void chkPeriodicamenteEnviarCNPJDeTodosOsClientes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPeriodicamenteEnviarCNPJDeTodosOsClientes.Checked)
                chkEnviarTodosOsClientesParaTodosOsVendedores.Checked = false;
        }





    }
}
