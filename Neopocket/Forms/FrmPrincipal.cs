using System;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using System.IO;


namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário principal do NeoPocket, provê as opções de menu para o usuário.
    /// </summary>
    public partial class FrmPrincipal : FormBase
    {
        //Usado apenas para começar o thread que faz a busca por notícias
        private Neopocket.Core.News news;
        private bool fecheSemPedirConfirmacao = false;
        
        #region [ Construtor ]
        public FrmPrincipal()
        {
            InitializeComponent();
            
            //Código para atualizar o atualizador
            if(File.Exists(D.AplicacaoDiretorio + "NeoPocketUpdaterNovo.exe")){
                if (File.Exists(D.AplicacaoDiretorio + "NeoPocketUpdaterAntigo.ex_"))
                    File.Delete(D.AplicacaoDiretorio + "NeoPocketUpdaterAntigo.ex_");
                try
                {
                    File.Move(D.AplicacaoDiretorio + "NeoPocketUpdater.exe", D.AplicacaoDiretorio + "NeoPocketUpdaterAntigo.ex_");
                }
                catch(Exception ex)
                {
                    FE.Show("Não foi possível desabilitar o arquivo antigo do atualizador","Falha na mudança do atualizador", ex);
                    return;
                }
 
                try
                {
                    File.Move(D.AplicacaoDiretorio + "NeoPocketUpdaterNovo.exe", D.AplicacaoDiretorio + "NeoPocketUpdater.exe");
                }
                catch(Exception ex)
                {
                    File.Move(D.AplicacaoDiretorio + "NeoPocketUpdaterAntigo.ex_", D.AplicacaoDiretorio + "NeoPocketUpdater.exe");
                    FE.Show("Não foi possível trocar o arquivo do coloquei a versão antiga de volta","Falha na mudança do atualizador", ex);
                    return;
                }
                //MessageBox.Show("O atualizador do NeoPocket foi substituido pela versão mais recente", "Aviso");
                LogBuilder.LogWrite("O atualizador do NeoPocket foi substituido pela versão mais recente");
            }
        }

        #endregion

        #region [ Menu ]

        #region [ Relatorios ]

        #region [ Clientes ]

        /* Ordenados por cidade */
        private void menuItemRelClienteOrdCidade_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RelatorioVer;
            Util.FormExibirDialog(new FrmRelatorioClienteCidade());
        }

        #endregion

        #endregion

        #endregion

        #region [ Botões ]

        /* Clientes */
        private void btnCliente_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.ClienteADefinir;
            Util.FormExibirDialog(new FrmClienteLista());
        }

        /* Pedido */
        private void btnPedido_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.PedidoADefinir;
            Util.FormExibirDialog(new FrmClienteLista());
        }


        /* Produto */
        private void btnProduto_Click(object sender, EventArgs e)
        {
            Util.FormExibirDialog(new FrmProdutoLista());
        }

        /* Configurações */
        private void picConfiguracoesDeAcesso_Click(object sender, EventArgs e)
        {
            Util.FormExibirDialog(new FrmConfiguracaoDeAcesso());
        }

        #endregion

        #region [ Load ]

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            do{
                FrmLogin f = new FrmLogin();
                Util.FormExibirDialog(f);
                if (f.WishLeaveSystem)
                {
                    fecheSemPedirConfirmacao = true;
                    Close();
                    return;
                }
            }while(!D.Funcionario.Validado);
            lbVersion.Text = D.APP_VERSION;

            //Oculta o botão de notícias caso não seja a sanog
            if (D.APP_FTP_USER.ToLower() != "sanog")
                btnNoticias.Visible = false;

            if (D.AppVersion.ToString() != "")
                this.Text += " - " + D.AppVersion.ToString();
            Cursor.Current = Cursors.Default;
            inputPanel.Enabled = false;
            this.MostraPedidosRestantes();
            //Inicia o thread que faz a busca por notícias
            news = new Neopocket.Core.News();
        }
        #endregion

        #region [ Pedidos restantes ]

        /// <summary>
        /// Verifica a existência de pedidos restantes e exibe a quantidade de pedidos
        /// restantes, caso a cota de pedidos esteja terminada exibe uma mensagem avisando
        /// ao usuário.
        /// </summary>
        private void MostraPedidosRestantes()
        {
            try
            {
                if (Pedido.PedidosAEnviar())
                {
                    lblPedidosAEnviar.Visible = true;

                    if (Pedido.PedidosRestantes == 1)
                    {
                        lblPedidosAEnviar.Text = Pedido.PedidosRestantes + " pedido restante";
                    }
                    else if (Pedido.PedidosRestantes == 0)
                    {
                        lblPedidosAEnviar.Text = "Nenhum pedido poderá ser feito até a próxima sincronização.";
                    }
                    else
                        lblPedidosAEnviar.Text = Pedido.PedidosRestantes + " pedidos restantes";
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion   


        #region [ Focus do formulário ]

        private void FrmPrincipal_GotFocus(object sender, EventArgs e)
        {
            this.BringToFront();
            this.MostraPedidosRestantes();

        }

        #endregion

        private void menuItemRelPedidos_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RelatorioVer;
            Util.FormExibirDialog(new FrmRelatorioPedido());
        }

        private void menuItemRelClienteSemPedido_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RelatorioVer;
            Util.FormExibirDialog(new FrmRelatorioClienteSemPedido());
        }

        private void btnRota_Click(object sender, EventArgs e)
        {
            D.Acao = D.AcaoEnum.RotaSeguir;
            Util.FormExibirDialog(new FrmRotaProcessa());
        }

        private void NoticiasVerificar()
        {
            //Não checar notícias se não for sanog
            if (D.APP_FTP_USER.ToLower() != "sanog")
                return;
            news = null;
            GC.Collect();
            //Inicia o thread que faz a busca por notícias
            news = new Neopocket.Core.News();
        }

        private void btnNoticias_Click(object sender, EventArgs e)
        {
            NoticiasVerificar();
        }

        private void FrmPrincipal_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!fecheSemPedirConfirmacao)
            {
                DialogResult dr = MessageBox.Show("Tem certeza que deseja sair do NeoPocket?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void menuItemSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSincronizar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Tem certeza que deseja sincronizar?","Confirmação", MessageBoxButtons.YesNo,  MessageBoxIcon.Question ,MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No)
                return;
            Util.FormDialogExibirApenasUm(new FrmSincronizacao());
        }
    }
}
