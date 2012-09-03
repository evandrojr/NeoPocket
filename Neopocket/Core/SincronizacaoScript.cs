using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Text;
using System.Windows.Forms; 
using Neopocket.Utils;
using Neopocket.Forms;
using System.Threading;

namespace Neopocket.Core
{
    public static class SincronizacaoScript
    {
        #region [ Atributos ]

        public static FrmSincronizacao FrmSource = null; // O Formulário que chama essa thread
        static MapeamentoBdCsv d = null;
        public static string ArquivoEnviarNome = "", ArquivoClientesReceberNomeBase = "";
        public static string ArquivoTitulosEmAbertoReceberNomeBase = "TITULOS_ABERTOS";
        public static string ArquivoComumReceberNome = "STORE_RM.zip";
        static List<string> lstArquivosParaCompactar = new List<string>();
        public static string ClienteEnviarNome, PedidoEnviarNome, ItensPedidoEnviarNome, ItensPedidoGradeEnviarNome, RecusaEnviarNome;
        private static bool pedidosNaoForamFeitos = false;
        private static bool clientesNaoForamCadastrados = false;
        private static bool recusasNaoForamCadastradas = false;
        public static List<Guid> LstClienteNovoId = new List<Guid>();
        public static List<Guid> LstPedidoNovoId = new List<Guid>();
        public static List<int> LstRecusaNovoId = new List<int>();
        private static long processoInicioTick; // Conta tempo de uma tarefa
        public static long SincronizacaoInicioTick; // Contao tempo de toda a sincronização
        private static string msgSucesso;
        private static string processMensage = "";
        private static string clientesCadastradosArquivoEnviarNome = "CLIENTESCADASTRADOS.csv";

        /// <summary>
        /// Nome do arquivo de log em questão
        /// </summary>
        private static String logName = String.Empty;

        #endregion

        #region [ Inicia o processo de sincronização ]


        public static string MensageShow(string atual, string adicionar)
        {
            return adicionar + Environment.NewLine + atual;
        }

        public static string MensagemMontaP2(string atual, string adicionar)
        {
            return lastAddedMsgP1 + adicionar + Environment.NewLine + lastAtualMsgP1;
        }

        private static string lastAddedMsgP1 = "";
        private static string lastAtualMsgP1 = "";

        public static string MensagemMontaP1(string atual, string adicionar)
        {
            lastAddedMsgP1 = adicionar;
            lastAtualMsgP1 = atual;
            return lastAddedMsgP1 + Environment.NewLine + lastAtualMsgP1;
        }

        public static void Start()
        {
            ThreadStart starter = new ThreadStart(SincronizacaoScript.ScriptAlgorithm);
            Thread t = new Thread(starter);
            t.Start();
        }

        #endregion

        #region private methods

        private static void startNewProcessMsg(string msg)
        {
            processoInicioTick = DateTime.Now.Ticks;
            FrmSource.TxtMessageMontaP1(msg);
        }

        private static void resetSyncronizationTimeCounter()
        {
            SincronizacaoInicioTick = DateTime.Now.Ticks;
        }

        private static void endProcessMsg()
        {
            long duracao = (long)Math.Round((DateTime.Now.Ticks - processoInicioTick) / 10000000.0);
            FrmSource.TxtMessageMensagemMontaP2(" (" + duracao + "s) ");
            return;
        }

        private static long getSyncronizationTimeCounter()
        {
            return DateTime.Now.Ticks - SincronizacaoInicioTick;
        }

        /// <summary>
        /// Criar diretorio
        /// Selecionar clientes para envio
        /// Selecionar pedidos para envio
        /// Compactar arquivos
        /// Procurar a pasta mais recente
        /// Enviar arquivos
        /// Marcar pedidos como enviados
        /// Fechar conexões
        /// Receber arquivos clientes
        /// Fechar conexões
        /// Receber arquivos gerais
        /// Fechar conexões
        /// Descompactar arquivo de clientes
        /// Descompactar arquivos gerais
        /// Remover clientes antigos
        /// Marcar clientes e pedidos como sincronizados
        /// Carregar dados
        /// Deletar arquivos
        /// 
        /// </summary>
        /// 
        private static void ScriptAlgorithm()
        {
            lastAddedMsgP1 = "";
            lastAtualMsgP1 = "";
            resetSyncronizationTimeCounter();

            try{ //Try geral

            #region [ Procura o servidor FTP ]
            
            FrmSource.TxtMessageMontaP1("Iniciando a sincronização");
            startNewProcessMsg("Procurando o servidor");
            NeoFileSystemService.ValidationSoapHeader header = new NeoFileSystemService.ValidationSoapHeader();
            header.Directory = D.APP_FTP_USER;
            header.Password = D.APP_FTP_PASS;
            NeoFileSystemService.NeoFileSystemService fileSystemService = new NeoFileSystemService.NeoFileSystemService();
            fileSystemService.ValidationSoapHeaderValue = header;
            try
            {
                // Lista para testar se conectou OK
                foreach (String d in fileSystemService.DirList(D.APP_FTP_USER + @"\"))
                {
                    String strDiretorioNome = d;
                    break;
                }
            }
            catch (Exception e)
            {
                FrmSource.ScriptEnd(); 
                FE.Show("Não foi possível encontrar o servidor, tente novamente", "Aviso", e.Message);
                return;
            }
            endProcessMsg(); // Fim busca servidor
            
            #endregion

            #region [ Apagar todos os arquivos no diretório de sincronização ]

            FrmSource.TxtMessageMontaP1("Limpando os arquivos do diretório de sincronização");

            DirectoryInfo di;
            if (Directory.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH))
            {
                di = new DirectoryInfo(D.AplicacaoDiretorio + D.APP_SYNC_PATH);
                //FileInfo[] rgFiles = di.GetFiles();
                di.Delete(true);
            }

            #region [ Cria o diretório de Sincronização ]

            try
            {
                if(!Directory.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH))
                    Directory.CreateDirectory(D.AplicacaoDiretorio + D.APP_SYNC_PATH);
            }
            catch(Exception ex)
            {
                FrmSource.ScriptEnd();
                FE.Show("Não foi possível criar diretório de sincronização", "Aviso", ex);
                return;
            }

            #endregion

            //foreach (FileInfo fi in rgFiles)
            //{
            //    try
            //    {
            //        fi.Delete();
            //    }
            //    catch { }
            //}

            endProcessMsg();     

            #endregion

            #region [ 1° Parte de Sincronização ]

            //FrmSource.TxtMessageShow("Selecionando novos clientes e pedidos ");
            startNewProcessMsg("Selecionando novos clientes e pedidos ");
            Iniciar();
            ClientesSelecionarParaEnvioCSV();
            RecusaSelecionarParaEnvioCSV();
            PedidosSelecionarParaEnvioCSV();
            Boolean haviamArquivosParaEnviar = ArquivosCompactar();
            if (haviamArquivosParaEnviar)
            {
                try
                {
                    Upload(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoEnviarNome, D.APP_FTP_USER + @"\" + ArquivoEnviarNome, fileSystemService);
                }catch(Exception ex){
                    FrmSource.ScriptEnd();
                    FE.Show("Não foi possível enviar arquivo com clientes e pedidos para servidor FTP", "Neo", ex);
                    return;
                }

                LstClienteNovoId = D.Bd.ListGuid(@"
                select
                        id_cliente_pocket
                from 
                        cliente
                where
                        status='N'");
                LstPedidoNovoId = D.Bd.ListGuid(@"
                select
                        id_pedido
                from 
                        pedido
                where
                        status='N'");

                //............Recusa....................................

                LstRecusaNovoId = D.Bd.ListInt(@"
                select
                        id_recusa
                from 
                        recusa
                where
                        status='N'");
                D.Bd.ExecuteNonQuery("Update recusa set status = 'S'");
                D.Bd.ExecuteNonQuery("Update pedido set status = 'S'");
                D.Bd.ExecuteNonQuery("Update cliente set status = 'S'");
            }
            else
            {
                FrmSource.TxtMessageMontaP1("Não existem clientes ou pedidos novos");
            }
            #endregion

            endProcessMsg();     

            #region [ 2° Parte da Sincronização - Importação dos arquivos da loja ]

                List<String> lstPastas = new List<String>();
                foreach (String d in fileSystemService.DirList(D.APP_FTP_USER + @"\"))
                {
                    String strDiretorioNome = d.Substring(d.LastIndexOf(@"\", d.Length - 2) + 1,
                                                                                   d.Length -
                                                                                   d.LastIndexOf(@"\", d.Length - 2) - 1);
                    if (strDiretorioNome.ToUpper().Substring(0, Math.Min(3, strDiretorioNome.Length)) == "ANO")
                    {
                        lstPastas.Add(strDiretorioNome);
                    }
                }
                if (lstPastas.Count == 0)
                {
                    FrmSource.ScriptEnd();
                    FE.Show("Não foi encontrada a pasta contendo dados a receber", "Neo");
                    return;
                }

#region "Recebendo arquivo de clientes"

                startNewProcessMsg("Recebendo arquivo de clientes");
                lstPastas.Sort();
                String strSubpasta = lstPastas[lstPastas.Count - 1];
                ArquivoClientesReceberNomeBase = "CLR" + D.Funcionario.Id.ToString().PadLeft(6, '0');
                Download(D.APP_FTP_USER + @"\" + strSubpasta + @"\" + ArquivoClientesReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoClientesReceberNomeBase + ".zip", fileSystemService);
                D.Bd.ExecuteNonQuery("Update cliente set status ='S' where status is null");
                endProcessMsg();

#endregion "Recebendo arquivo de clientes"

#region [ Carrega títulos em aberto ]

                startNewProcessMsg("Recebendo arquivo de títulos em aberto");
                ArquivoTitulosEmAbertoReceberNomeBase += D.Funcionario.Id.ToString().PadLeft(6, '0');
                try
                {
                    Download(D.APP_FTP_USER + @"\" + strSubpasta + @"\" + ArquivoTitulosEmAbertoReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoTitulosEmAbertoReceberNomeBase + ".zip", fileSystemService);
                }catch{
                    startNewProcessMsg("Não foi encontrado arquivo com títulos em aberto");
                }
                endProcessMsg();
#endregion

                #region "Recebendo dados gerais de atualização"

                startNewProcessMsg("Recebendo dados gerais de atualização");
                try
                {
                    Download(D.APP_FTP_USER + @"\" + strSubpasta + @"\" + ArquivoComumReceberNome, D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoComumReceberNome, fileSystemService);
                }catch(Exception ex){
                    FrmSource.ScriptEnd();
                    FE.Show("Não foi encontrado arquivo com atualizações ou não foi possivel conectar com a Internet", "Neo", ex);
                    return;
                }
                endProcessMsg();

#endregion "Recebendo arquivos gerais"

                FrmSource.TxtMessageMontaP1("Agora você já pode desconectar da Internet");

#region "  Descompactando arquivos  "

                startNewProcessMsg("Descompactando arquivos ");
                msgSucesso = ArquivosDescompactar();
                if (msgSucesso != "")
                {
                    FrmSource.ScriptEnd();
                    FE.Show("Não foi possível descompactar o arquivo com atualizações. O Neo Pocket conseguiu enviar mas os dados locais não foram atualizados", "Neo", msgSucesso);
                    return;
                }
                endProcessMsg();

#endregion "---Descompactando arquivos---"

                startNewProcessMsg("Removendo clientes antigos");
                RemoveClientesAntigos();
                endProcessMsg();

                startNewProcessMsg("Carregando novos clientes");
                CarregaClienteCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando os títulos em aberto");
                CarregaTitulosEmAbertoCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando rotas para as cidades");
                RotaCidadeCarregar();
                endProcessMsg();

                //Se existir o arquivo verifica se já está na hora de atualizar
                if(File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + clientesCadastradosArquivoEnviarNome))
                {
                    if (verificaSePrecisaCarregarOsRegistrosDeTodosOsClientes())
                    {
                        startNewProcessMsg("Carregando dados para evitar duplicidades nos cadastros");
                        ClientesCastradosCarregar();
                        Parametro.Gravar("clientes_cadastrados_update_data", DateTime.Now.ToString(D.CultureInfoBRA));
                        endProcessMsg();
                    }
                }

                startNewProcessMsg("Carregando produtos");
                CarregaProdutoCsv();
                endProcessMsg(); 

                startNewProcessMsg("Carregando vendedores");
                CarregaVendedorCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando cidades");
                CarregaCidadeCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando espécies financeira");
                CarregaEspecieFinanceiraCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando os itens da tabela de preço");
                CarregaItemTabelaPrecoCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando formas de pagamento");
                CarregaFormaPagamentoCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando os itens das formas de pagamento");
                CarregaItemFormaPagamentoCsv();
                endProcessMsg();

                startNewProcessMsg("Carregando as formas de pagamento das tabelas de preço");
                CarregaFormaPagamentoTabelaPrecoCsv();
                endProcessMsg();

                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ATRIBUTO.csv"))
                {
                    startNewProcessMsg("Carregando atributos");
                    CarregaAtributoCsv();
                    endProcessMsg();
                }

                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "GRADE.csv"))
                {
                    startNewProcessMsg("Carregando grades");
                    CarregaGradeCsv();
                    endProcessMsg();
                }

                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMATRIBUTO.csv"))
                {
                    startNewProcessMsg("Carregando tabela item atributo");
                    CarregaItemAtributoCsv();
                    endProcessMsg();
                }

                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMGRADE.csv"))
                {
                    startNewProcessMsg("Carregando tabela item grade");
                    CarregaItemGradeCsv();
                    endProcessMsg();
                }
               
                startNewProcessMsg("Carregando parâmetros");
                //Lê clientes_cadastrados_update_data para que não seja perdido
                DateTime clientes_cadastrados_update_data = new DateTime(2000,1,1);;
                if(Parametro.Ler("clientes_cadastrados_update_data") != null)
                    clientes_cadastrados_update_data = DateTime.Parse(Parametro.Ler("clientes_cadastrados_update_data"), D.CultureInfoBRA);
                else // Coloca uma data bem passada para forçar a atualização 
                    clientes_cadastrados_update_data = new DateTime(2000, 1, 1); ;
                CarregaParametroCsv();
                //Grava novamente
                Parametro.Gravar("clientes_cadastrados_update_data",clientes_cadastrados_update_data.ToString(D.CultureInfoBRA));
                endProcessMsg();


                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "SALDOGRADE.csv"))
                {
                    startNewProcessMsg("Carregando saldo grade");
                    CarregaSaldoGradeCsv();
                    endProcessMsg();
                }

                startNewProcessMsg("Carregando tabelas de preço");
                CarregaTabelaPrecoCsv();
                endProcessMsg();

                if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + "MOTIVO.csv"))
                {
                    startNewProcessMsg("Carregando motivo");
                    CarregaTabelaMotivoCsv();
                    endProcessMsg();
                }

                #region [ Limpa arquivos temporários ]

                if (Directory.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH))
                {
                    startNewProcessMsg("Removendo arquivos temporários");
                    di = new DirectoryInfo(D.AplicacaoDiretorio + D.APP_SYNC_PATH);
                    if (di.Exists)
                        di.Delete(true);
                    di.Create();
                    endProcessMsg();
                }

                #endregion

                startNewProcessMsg("Carregando dados deste funcionário");
                //Carregar os dados do funcionário
                try
                {
                    D.Funcionario.Carregar();
                }
                catch (Exception ex)
                {
                    FE.Show(ex);
                    Application.Exit();
                }
                endProcessMsg();

                if (Parametro.Carregar())
                {
                    Cursor.Current = Cursors.Default;
                    FrmSource.ScriptEnd();
                    MessageBox.Show("Atualização ocorrida com sucesso pressione 'ok' e 'Retornar' para sair.", "Neo");
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    FrmSource.ScriptEnd();
                    MessageBox.Show("Atualização finalizada pressione 'ok' e 'Retornar' para sair.", "Neo");
                }
                #endregion
            }
            catch (Exception ex) // Fim do try geral
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FrmSource.ScriptEnd();
                FE.Show(ex);
                return;
            }
        }

        private static string Md5(string fileName)
        {
            // Primeiro passo, calcular o MD5 hash a partir da string
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            BinaryReader binReader = new BinaryReader(File.Open(fileName, FileMode.Open, FileAccess.Read));
            binReader.BaseStream.Position = 0;
            byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
            binReader.Close();

            byte[] hash = md5.ComputeHash(binFile);

            // Segundo passo, converter o array de bytes em uma string haxadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        //So busca se for mais velho que seis dias e o arquivo só será enviado pelo neoSync a cada duas semanas!
        private static bool verificaSePrecisaCarregarOsRegistrosDeTodosOsClientes()
        {
            //Se estiver vazio carrega tudo
            if (D.Bd.I("select count(*) from cliente_cadastrado") == 0)
                return true;
            else
                if (Parametro.Ler("clientes_cadastrados_update_data") == null)
                    return true;
                else
                {
                    DateTime dt = DateTime.Parse(Parametro.Ler("clientes_cadastrados_update_data"), D.CultureInfoBRA);
                    if (dt.AddDays(6) <= DateTime.Today)
                        return true;
                    else
                        return false;
                }
        }

        //private static void Upload(string arquivoLocal, string arquivoRemoto, NeoFileSystemService.NeoFileSystemService fileSystemService)
        //{
        //    System.IO.BinaryReader br = new
        //    System.IO.BinaryReader(System.IO.File.Open(arquivoLocal, System.IO.FileMode.Open,
        //    System.IO.FileAccess.Read));

        //    byte[] buffer = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
        //    br.Close();
        //    fileSystemService.FilePut(buffer, arquivoRemoto);
        //}

        //private static void Download(string remoteFile, string localFile, NeoFileSystemService.NeoFileSystemService fileSystemService)
        //{
        //    Byte[] fileData = fileSystemService.FileGet(remoteFile);
        //    FileStream file = File.Create(localFile);
        //    file.Write(fileData, 0, fileData.Length);
        //    file.Close();
        //}

        #endregion

        public static void Iniciar()
        {
            try
            {
                // Limpa a lista de arquivos para compactar
                    lstArquivosParaCompactar.Clear();

                // Cria a pasta de logs
                if (!Directory.Exists(D.APP_LOGDIRECTORY))
                {
                    Directory.CreateDirectory(D.APP_LOGDIRECTORY);

                    String strData = "D{0}M{1}A{2}H{3}MI{4}";
                    strData = String.Format(strData, DateTime.Now.Day,
                                                     DateTime.Now.Month,
                                                     DateTime.Now.Year,
                                                     DateTime.Now.Hour,
                                                     DateTime.Now.Minute);

                    logName = String.Format(D.DEPRECIADO_APP_LOGFILENAME, "Sincronizacao", strData);
                }
                else
                {
                    /* Deleta todos os logs antigos, pois os 
                     * logs serão armazenados por dia.
                     */
                    DirectoryInfo di = new DirectoryInfo(D.APP_LOGDIRECTORY);
                    FileInfo[] rgFiles = di.GetFiles();

                    if (rgFiles.Length > 0)
                    {
                        // Log mais antigo
                        DateTime? menorData = null;

                        foreach (FileInfo fi in rgFiles)
                        {
                            #region [ Recupera a string da hora no nome do arquivo ]

                            String strDataGerada = String.Empty;
                            Int32 dia = 0;
                            Int32 mes = 0;
                            Int32 ano = 0;
                            Int32 hora = 0;
                            Int32 minuto = 0;

                            try
                            {
                                strDataGerada = fi.FullName.Substring(fi.FullName.LastIndexOf("-") + 1);
                                dia = Int32.Parse(strDataGerada.Substring(1, strDataGerada.IndexOf("M") - 1));
                                strDataGerada = strDataGerada.Substring(strDataGerada.IndexOf("M"));
                                mes = Int32.Parse(strDataGerada.Substring(1, strDataGerada.IndexOf("A") - 1));
                                strDataGerada = strDataGerada.Substring(strDataGerada.IndexOf("A"));
                                ano = Int32.Parse(strDataGerada.Substring(1, strDataGerada.IndexOf("H") - 1));
                                strDataGerada = strDataGerada.Substring(strDataGerada.IndexOf("H"));
                                hora = Int32.Parse(strDataGerada.Substring(1, strDataGerada.IndexOf("MI") - 1));
                                strDataGerada = strDataGerada.Substring(strDataGerada.IndexOf("MI"));
                                minuto = Int32.Parse(strDataGerada.Replace(".txt", "").Substring(strDataGerada.IndexOf("MI") + 2));
                            }
                            catch { }

                            #endregion

                            #region [ Transforma a string da data do nome do arquivo em DATETIME ]

                            DateTime? dtGeracao = null;

                            try
                            {
                                dtGeracao = new DateTime(ano, mes, dia, hora, minuto, 0);
                            }
                            catch { }

                            #endregion

                            /* Caso o nome do arquivo não esteja corrompido */
                            if (dtGeracao != null)
                            {
                                /* Se a data de geração for menor que a de hoje, apaga o log*/
                                if (dtGeracao.Value.Date < DateTime.Now.Date)
                                {
                                    try
                                    {
                                        fi.Delete();
                                    }
                                    catch { }
                                }
                                else
                                {
                                    /* Gerar o nome do proximo log caso ainda não 
                                     * tenha gerado o limite logs naquele dia. */
                                    if (rgFiles.Length < D.APP_LOG_MAX)
                                    {
                                        String strData = "D{0}M{1}A{2}H{3}MI{4}";
                                        strData = String.Format(strData, DateTime.Now.Day,
                                                                         DateTime.Now.Month,
                                                                         DateTime.Now.Year,
                                                                         DateTime.Now.Hour,
                                                                         DateTime.Now.Minute);

                                        logName = String.Format(D.DEPRECIADO_APP_LOGFILENAME, "Sincronizacao", strData);
                                    }
                                    else // Já gerou o limte de logs, então o log em questão deve ser o mais antigo
                                    {
                                        if (menorData == null)
                                        {
                                            menorData = dtGeracao;
                                        }
                                        else
                                        {
                                            if (dtGeracao < menorData)
                                                menorData = dtGeracao;
                                        }
                                    }
                                }
                            }
                            else // Se o nome do arquivo estava corrumpido, deleta o arquivo
                            {
                                try
                                {
                                    fi.Delete();
                                }
                                catch { }
                            }
                        }

                        if (rgFiles.Length >= D.APP_LOG_MAX)
                        {
                            // Encontrou o log mais antigo para apagar
                            if (menorData != null)
                            {
                                String strData = "D{0}M{1}A{2}H{3}MI{4}";
                                strData = String.Format(strData, menorData.Value.Day,
                                                             menorData.Value.Month,
                                                             menorData.Value.Year,
                                                             menorData.Value.Hour,
                                                             menorData.Value.Minute);

                                String logApagar = String.Format(D.DEPRECIADO_APP_LOGFILENAME, "Sincronizacao", strData);
                                File.Delete(D.APP_LOGDIRECTORY + logApagar);

                                /* Cria o nome do novo log */
                                strData = "D{0}M{1}A{2}H{3}MI{4}";
                                strData = String.Format(strData, DateTime.Now.Day,
                                                                 DateTime.Now.Month,
                                                                 DateTime.Now.Year,
                                                                 DateTime.Now.Hour,
                                                                 DateTime.Now.Minute);

                                logName = String.Format(D.DEPRECIADO_APP_LOGFILENAME, "Sincronizacao", strData);
                            }
                        }
                    }
                    else
                    {
                        String strData = "D{0}M{1}A{2}H{3}MI{4}";
                        strData = String.Format(strData, DateTime.Now.Day,
                                                         DateTime.Now.Month,
                                                         DateTime.Now.Year,
                                                         DateTime.Now.Hour,
                                                         DateTime.Now.Minute);
                        logName = String.Format(D.DEPRECIADO_APP_LOGFILENAME, "Sincronizacao", strData);
                    }
                }

                // Inicia o processo de gravação do log da sincronização
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Iniciando sincronização", true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Falha ao iniciar processo de sincronização", true);
                throw ex;
            }
        }



        #region [ Seleciona clientes para envio ]

        /// <summary>
        /// Seleciona todos os clientes com Status = N ( Novo )
        /// </summary>
        public static void ClientesSelecionarParaEnvioCSV()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Selecionando clientes novos para envio", true);

                clientesNaoForamCadastrados = false;

                // Busca a quantidade de clientes novos
                Int32 numeroClientesNovos = D.Bd.I("SELECT COUNT(*) FROM cliente WHERE status = 'N'", false);
                if (numeroClientesNovos > 0)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Total de clientes(s) = " + numeroClientesNovos.ToString(), true);
                    String qry = "SELECT * FROM cliente WHERE status = 'N'";
                    DataTable dtClientesNovos = D.Bd.DataTablePreenche(qry);
                    ClienteEnviarNome = MontarNomeArquivo("CLT") + ".csv";
                    NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ClienteEnviarNome);
                    csv.EscreveCsv(dtClientesNovos, D.AplicacaoDiretorio + D.APP_SYNC_PATH + ClienteEnviarNome);
                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ClienteEnviarNome);
                }
                else
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Nenhum cliente foi cadastrado", true);
                    clientesNaoForamCadastrados = true;
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao selecionar clientes novos para envio", true);
                throw ex;
            }
        }

        #endregion

        #region [ Seleciona recusas para envio ]

        /// <summary>
        /// Seleciona todas as recusas com Status = N ( Nova )
        /// </summary>
        public static void RecusaSelecionarParaEnvioCSV()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Selecionando recusas novas para envio", true);
                recusasNaoForamCadastradas = false;
                Int32 numeroRecusasNovas = D.Bd.I("SELECT COUNT(*) FROM recusa WHERE status = 'N'", false);
                if (numeroRecusasNovas > 0)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Total de recusa(s) = " + numeroRecusasNovas.ToString(), true);
                    String qry = "SELECT * FROM recusa WHERE status = 'N'";

                    DataTable dtRecusasNovas = D.Bd.DataTablePreenche(qry);
                    RecusaEnviarNome = MontarNomeArquivo("REC") + ".csv";
                    NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.APP_SYNC_PATH + RecusaEnviarNome);
                    csv.EscreveCsv(dtRecusasNovas, D.AplicacaoDiretorio + D.APP_SYNC_PATH + RecusaEnviarNome);
                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.APP_SYNC_PATH + RecusaEnviarNome);
                }
                else
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Nenhuma recusa foi cadastrada", true);
                    recusasNaoForamCadastradas = true;
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao selecionar recusas novas para envio", true);
                throw ex;
            }
        }

        #endregion

        #region [ Seleciona pedidos para envio ]

        /// <summary>
        /// Seleciona todos os pedidos novos com Status = N ( Novo )
        /// </summary>
        public static void PedidosSelecionarParaEnvioCSV()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Selecionando pedidos novos para envio", true);

                pedidosNaoForamFeitos = false;

                Int32 numeroPedidosNovos = D.Bd.I("SELECT COUNT(id_pedido) FROM pedido WHERE status = 'N'", false);
                if (numeroPedidosNovos > 0)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Total de pedido(s) = " + numeroPedidosNovos.ToString(), true);

// Use esta se der erro quando o campo data é nulo
//                    String qry = @"
//                    SELECT
//                            *
//                    FROM
//                         pedido
//                    WHERE status = 'N'";

                    String qry = @"
                    SELECT
                         id_cliente_store, CONVERT(nvarchar(19), data, 120) AS data, id_funcionario,
                         id_tabela_preco, id_forma_pagamento, status, valor, observacao, desconto, bdi, 
                         id_cliente_pocket, id_pedido
                    FROM
                         pedido
                    WHERE status = 'N'";

                    DataTable dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                    PedidoEnviarNome = MontarNomeArquivo("PEDIDOS") + ".csv";
                    NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.APP_SYNC_PATH + PedidoEnviarNome);
                    csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.APP_SYNC_PATH + PedidoEnviarNome);
                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.APP_SYNC_PATH + PedidoEnviarNome);

                    numeroPedidosNovos = -1;
                    numeroPedidosNovos = D.Bd.I(@"
                    SELECT
                            count(pedido.id_pedido)
                    FROM
                            item_pedido INNER JOIN
                            pedido ON item_pedido.id_pedido = pedido.id_pedido
                    WHERE     
                            (pedido.status = 'N')", false);

                    if (numeroPedidosNovos > 0)
                    {
                        qry = @"
                        select 
                                item_pedido.*
                        from 
                                item_pedido, pedido
                        where
                                pedido.id_pedido=item_pedido.id_pedido
                             and
                                pedido.status = 'N'";
                        dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                        ItensPedidoEnviarNome = MontarNomeArquivo("ITENSDOPEDIDO") + ".csv";
                        csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.APP_SYNC_PATH + ItensPedidoEnviarNome);
                        lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ItensPedidoEnviarNome);
                    }

                    numeroPedidosNovos = -1;
                    numeroPedidosNovos = D.Bd.I(@"
                    SELECT
                            count(pedido.id_pedido)
                    FROM
                            item_pedido_grade INNER JOIN
                            pedido ON item_pedido_grade.id_pedido = pedido.id_pedido
                    WHERE     
                            (pedido.status = 'N')", false);
                    if (numeroPedidosNovos > 0)
                    {
                        qry = @"
                        select 
                                item_pedido_grade.*
                        from 
                                item_pedido_grade, pedido
                        where
                                pedido.id_pedido=item_pedido_grade.id_pedido
                             and
                                pedido.status = 'N'";
                        dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                        ItensPedidoGradeEnviarNome = MontarNomeArquivo("ITEMDOPEDIDOGRADE") + ".csv";
                        csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.APP_SYNC_PATH + ItensPedidoGradeEnviarNome);
                        lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ItensPedidoGradeEnviarNome);
                    }
                }
                else
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Nenhum pedido foi cadastrado", true);
                    pedidosNaoForamFeitos = true;
                }
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao se0lecionar pedidos novos para envio", true);
                throw ex;
            }
        }

        #endregion

        #region [ Compacta arquivos para envio pro ftp ]

        public static Boolean ArquivosCompactar()
        {
            try
            {
                if (pedidosNaoForamFeitos && clientesNaoForamCadastrados && recusasNaoForamCadastradas)
                    return false;

                StringBuilder sb = new StringBuilder();

                DateTime agora = new DateTime();
                agora = DateTime.Now;
                sb.Append("POCKETRT_" + agora.Year + "-" + agora.Month.ToString().PadLeft(2, '0') + "-" + agora.Day.ToString().PadLeft(2, '0') + "_");
                sb.Append(agora.Hour.ToString().PadLeft(2, '0') + "h" + agora.Minute.ToString().PadLeft(2, '0') + "m" + agora.Second.ToString().PadLeft(2, '0') + "s_Vendedor" + D.Funcionario.Id + ".zip");
                ArquivoEnviarNome = sb.ToString();

                Zip.ZipFiles(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoEnviarNome, lstArquivosParaCompactar);
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Compactando arquivos para envio", true);

                return true;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao compactar arquivos para envio", true);
                throw ex;
            }
        }

        #endregion

        #region [ Descompacta arquivos recebidos ]

        public static String ArquivosDescompactar()
        {
            try
            {
                String sucesso = String.Empty;
                try
                {
                    Zip.UnzipFiles(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoComumReceberNome, D.AplicacaoDiretorio + D.APP_SYNC_PATH);
                    Zip.UnzipFiles(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoClientesReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.APP_SYNC_PATH);
                    if(File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoTitulosEmAbertoReceberNomeBase + ".zip"))
                        Zip.UnzipFiles(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoTitulosEmAbertoReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.APP_SYNC_PATH);

                }
                catch (Exception ex)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao descompactar arquivos recebidos", true);
                    MessageBox.Show(ex.Message);
                    sucesso = ex.Message;
                    return sucesso;
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Descompactando arquivos recebidos", true);
                throw ex;
            }
        }

        #endregion

        #region [ Remove os clientes antigos ]

        public static void RemoveClientesAntigos()
        {
            try
            {
                //Vai remover tudo pois quando puxar já puxa correto
                D.Bd.ExecuteNonQuery("DELETE FROM cliente");
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Limpando base de clientes", true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao limpar base de clientes", true);
                throw ex;
            }

        }

        #endregion

        #region [ Unindo clientes antigos com os novos ]

        /// <summary>
        /// Unindo clientes antigos com os novos
        /// </summary>
        public static void CarregaClienteCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Unindo clientes antigos com os novos", true);
                String clienteArquivoNome = ("CLR") + D.Funcionario.Id.ToString().PadLeft(6, '0') + ".csv";
                d = new MapeamentoBdCsv("cliente", D.AplicacaoDiretorio + D.APP_SYNC_PATH + clienteArquivoNome, D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_cliente", "CODIGO");
                d.laT("id_cliente_pocket", "COD_CLIENTE_POCKET");
                d.laT("comprador_nome", "NOME_COMPRADOR");
                d.laT("id_tabela_preco", "COD_TABELAPRECO");
                d.laT("cliente_nome", "NOME");
                d.laT("cliente_nome_reduzido", "NOMEREDUZIDO");
                d.laT("tipo_pessoa", "TIPOPESSOA");
                d.laT("telefone", "TELEFONE");
                d.laT("cgc_cpf", "CGC_CPF");
                d.laT("rg_inscricao", "RG_INSCRICAO");
                d.laT("endereco", "ENDERECO");
                d.laT("bairro", "BAIRRO");
                d.laT("cidade", "COD_CIDADE");
                d.laT("uf_cod", "COD_UF");
                d.laT("cep", "CEP");
                d.laR("limite_credito", "LIMITECREDITO");
                d.laD("nascimento", "NASCIMENTO");
                d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
                d.laT("dia_visita", "DIA_VISITA");
                d.laT("id_funcionario", "COD_FUNCIONARIO");
                d.laT("intervalo", "INTERVALO");
                d.laT("banco_codigo", "COD_BANCO_REF");
                d.laT("agencia_codigo", "COD_AGENCIA_REF");
                d.laT("agencia_telefone", "TELEFONE_AGENCIA");
                d.laT("referencia_comercial1", "REFERENCIA_COMERCIAL1");
                d.laT("referencia_comercial1_telefone", "TELEFONE_REFERENCIA1");
                d.laT("referencia_comercial2", "REFERENCIA_COMERCIAL2");
                d.laT("referencia_comercial2_telefone", "TELEFONE_REFERENCIA2");
                d.laB("lista_negra", "LISTANEGRA");
                d.laR("bdi", "BDI");
                d.laR("montante_aberto", "MONTANTE_ABERTO");
                d.laR("montante_vencido", "MONTANTE_VENCIDO");
                d.laR("montante_a_vencer", "MONTANTE_A_VENCER");
                d.laT("maior_vencimento", "MAIOR_VENCIMENTO");
                d.laB("ativo", "ATIVO");
//              Desabilitado até a rota ser reprogramada                
//                d.laT("visitacao_ordem", "ORDEM_VISITA");
                d.laB("contribuinte_icms","CONTRIBUINTE_ICMS");
                d.Executar(false);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao unir clientes antigos com os novos", true);
                throw ex;
            }
        }

        #endregion
         
        #region [ Carrega títulos em aberto ]

        public static void CarregaTitulosEmAbertoCsv()
        {
            if (File.Exists(D.AplicacaoDiretorio + D.APP_SYNC_PATH + ArquivoTitulosEmAbertoReceberNomeBase + ".csv"))
            {
                try
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando os títulos em aberto", true);
                    String clienteArquivoNome = ("TITULOS_ABERTOS") + D.Funcionario.Id.ToString().PadLeft(6, '0') + ".csv";
                    d = new MapeamentoBdCsv("titulo_aberto", D.AplicacaoDiretorio + D.APP_SYNC_PATH + clienteArquivoNome, D.Bd, D.APP_LOGDIRECTORY + logName);
                    //ID_ESPECIE_FINANCEIRA;VALOR;VENCIMENTO_DATA;PAGO;JUROS_DINHEIRO;A_RECEBER
                    d.laT("ID_CLIENTE", "ID_CLIENTE");
                    d.laT("ID_ESPECIE_FINANCEIRA", "ID_ESPECIE_FINANCEIRA");
                    d.laR("VALOR", "VALOR");
                    d.laD("VENCIMENTO_DATA", "VENCIMENTO_DATA");
                    d.laR("PAGO", "PAGO");
                    d.laR("JUROS_DINHEIRO", "JUROS_DINHEIRO");
                    d.laR("A_RECEBER", "A_RECEBER");
                    d.Executar(true);
                }
                catch (Exception ex)
                {
                    LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao unir clientes antigos com os novos", true);
                    throw ex;
                }
            }
        }

        #endregion

        #region [ Carrega registro de todos os clientes ]

        public static void ClientesCastradosCarregar()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todos os clientes", true);
                d = new MapeamentoBdCsv("cliente_cadastrado", D.AplicacaoDiretorio + D.APP_SYNC_PATH + clientesCadastradosArquivoEnviarNome, D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("cnpj_cpf", "CPNJ_CPF");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todos os clientes", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega registro das rotas das cidades ]

        public static void RotaCidadeCarregar()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando rotas das cidades", true);
                String rotaCidadeArquivoEnviarNome = "ROTACIDADE.csv";
                d = new MapeamentoBdCsv("rota_cidade", D.AplicacaoDiretorio + D.APP_SYNC_PATH + rotaCidadeArquivoEnviarNome, D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_rota", "COD_PERCURSO");
                d.laT("id_funcionario","COD_VENDEDOR");
                d.laT("id_cidade","COD_CIDADE");
                d.laT("cidade","CIDADE");
                d.laT("id_uf","COD_UF");
// Desabilitado até ser programado
//                d.laT("visitacao_ordem", "ORDEM_VISITA");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todos os clientes", true);
                throw ex;
            }
        }

        #endregion


        #region [ Carrega registro de todos os produtos ]

        public static void CarregaProdutoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todos os produtos", true);
                d = new MapeamentoBdCsv("produto", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "PRODUTO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_produto", "CODIGO");
                d.laT("nome", "DESCRICAO");
                d.laT("referencia", "REFERENCIA");
                d.laT("id_unidade_venda", "COD_UNIDADE_VENDA");
                d.laT("id_grade", "COD_GRADE");
                d.laR("preco_venda", "PRECOVENDA");
                d.laR("preco_promocao", "PRECOPROMOCAO");
                d.laR("estoque", "QUANTIDADEESTOQUE");
                d.laB("venda_fracionada", "FRACIONADA");
                d.laR("unidade_fator", "UNIDADE_FATOR");
                d.laD("promocao_data_inicio", "DATAINICIOPROMOCAO");
                d.laD("promocao_data_final", "DATAFIMPROMOCAO");
                d.laB("permitir_venda_nao_contribuinte", "PERMITIR_VENDA_NAO_CONTRIBUINTE");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todos os produtos", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega registro de todos os vendedores ]

        public static void CarregaVendedorCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todos os vendedores", true);
                d = new MapeamentoBdCsv("funcionario", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "FUNCIONARIO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id", "CODIGO");
                d.laT("nome", "NOME");
                d.laT("desconto_maximo", "DESCONTO_MAXIMO");
                d.laT("acrescimo_maximo", "ACRESCIMO_MAXIMO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todos os vendedores", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega registro de todas as cidades ]

        public static void CarregaCidadeCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todas as cidades", true);
                d = new MapeamentoBdCsv("cidade", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "CIDADE.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_cidade", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.laT("uf_cod", "COD_UF");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todas as cidades", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todas as especies financeiras ]

        public static void CarregaEspecieFinanceiraCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todas as espécies financeiras", true);
                d = new MapeamentoBdCsv("especie_financeira", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ESPECIEFINANCEIRA.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_especie_financeira", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.laB("verifica_credito", "VERIFICA_CREDITO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todas as espécies financeiras", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega os registro de todos ítens da tabela de preço ]

        public static void CarregaItemTabelaPrecoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando registro de todos os itens da tabela de preço", true);
                d = new MapeamentoBdCsv("item_tabela_preco", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMTABELAPRECO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_tabela_preco", "COD_TABELAPRECO");
                d.laT("id_produto", "COD_PRODUTO");
                d.laT("tipo_valor", "TIPOVALOR");
                d.laR("valor1", "VALOR");
                d.laT("qtd_minima1", "QTD_MINIMA");
                d.laR("valor2", "VALOR1");
                d.laT("qtd_minima2", "QTD_MINIMA1");
                d.laR("valor3", "VALOR2");
                d.laT("qtd_minima3", "QTD_MINIMA2");
                d.laR("valor4", "VALOR3");
                d.laT("qtd_minima4", "QTD_MINIMA3");
                d.laR("desconto_maximo", "DESCONTO_MAXIMO");
                d.laR("acrescimo_maximo", "ACRESCIMO_MAXIMO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar registro de todos os itens da tabela de preço", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todas as formas de pagamento ]

        public static void CarregaFormaPagamentoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando formas de pagamento", true);
                d = new MapeamentoBdCsv("forma_pagamento", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "FORMADEPAGAMENTO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_forma_pagamento", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.laT("no_parcelas", "PARCELAS");
                d.laT("prazo_medio", "PRAZO_MEDIO");
                d.laR("parcela_minima", "PARCELA_MINIMA");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar formas de pagamento", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todos os itens de forma de pagamento ]

        public static void CarregaItemFormaPagamentoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando os ítems das formas de pagamento", true);
                d = new MapeamentoBdCsv("item_forma_pagamento", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMFORMAPAGAMENTO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
                d.laT("id_especie_financeira", "COD_ESPECIEFINANCEIRA");
                d.laT("id_item_forma_pagamento", "CODIGO");
                d.laR("prazo_vencimento", "PRAZOVENCIMENTO");
                d.laT("percentual_pagamento", "PERCENTUALPAGAMENTO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar os ítems das formas de pagamento", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todas as formas de pagamento das tabelas de preço ]

        public static void CarregaFormaPagamentoTabelaPrecoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando as formas de pagamento das tabelas de preço", true);
                d = new MapeamentoBdCsv("forma_pagamento_tabela_preco", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "FORMAPAGAMENTOTABELAPRECO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_tabela_preco", "COD_TABELAPRECO");
                d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar as formas de pagamento das tabelas de preço", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todos os atributos ]

        public static void CarregaAtributoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando atributos", true);
                d = new MapeamentoBdCsv("atributo", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ATRIBUTO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_atributo", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar atributos", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todas as grades ]

        public static void CarregaGradeCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando as grades", true);
                d = new MapeamentoBdCsv("grade", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "GRADE.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_grade", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar as grades", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todos os itens do atributo ]

        public static void CarregaItemAtributoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando o registro de todos os itens do atributo", true);
                d = new MapeamentoBdCsv("item_atributo", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMATRIBUTO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_item_atributo", "COD_ATRIBUTO");
                d.laT("id_atributo", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar o registro de todos os itens do atributo", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todos os itens da grade ]

        public static void CarregaItemGradeCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando tabela ítem grade", true);
                d = new MapeamentoBdCsv("item_grade", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "ITEMGRADE.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_item_grade", "CODIGO");
                d.laT("id_grade", "COD_GRADE");
                d.laT("descricao", "DESCRICAO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar tabela ítem grade", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro de todos os parametros ]

        public static void CarregaParametroCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando parâmetros", true);
                d = new MapeamentoBdCsv("parametro", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "PARAMETRO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("nome", "NOME");
                d.laT("tipo", "TIPO");
                d.laT("valor", "VALOR");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, " Erro ao carregar parâmetros", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega o registro do saldo da grade ]

        public static void CarregaSaldoGradeCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando saldo grade", true);
                d = new MapeamentoBdCsv("saldo_grade", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "SALDOGRADE.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_produto", "COD_PRODUTO");
                d.laT("id_grade", "COD_GRADE");
                d.laT("id_item_grade", "COD_ITEMGRADE");
                d.laT("id_atributo", "COD_ATRIBUTO");
                d.laT("id_item_atributo", "COD_ITEMATRIBUTO");
                d.laT("estoque", "QUANTIDADEESTOQUE");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Eror ao carregar saldo grade", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega todos os registros de tabela de preço ]

        public static void CarregaTabelaPrecoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando tabelas de preço", true);
                d = new MapeamentoBdCsv("tabela_preco", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "TABELAPRECO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_tabela_preco", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.laR("ajuste_percentual", "PERCENTUALAJUSTE");
                d.laT("tipo", "TIPOAJUSTE");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar tabelas de preço", true);
                throw ex;
            }
        }

        #endregion

        #region [ Carrega todos os registros de motivos ]

        public static void CarregaTabelaMotivoCsv()
        {
            try
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Carregando motivos", true);
                d = new MapeamentoBdCsv("motivo", D.AplicacaoDiretorio + D.APP_SYNC_PATH + "MOTIVO.csv", D.Bd, D.APP_LOGDIRECTORY + logName);
                d.laT("id_motivo", "CODIGO");
                d.laT("descricao", "DESCRICAO");
                d.Executar(true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao carregar motivos", true);
                throw ex;
            }
        }

        #endregion

        #region [ Upload e Download ]

        public static void Upload(string arquivoLocal, string arquivoRemoto, NeoFileSystemService.NeoFileSystemService fileSystemService)
        {
            try
            {
                System.IO.BinaryReader br = new
                            System.IO.BinaryReader(System.IO.File.Open(arquivoLocal, System.IO.FileMode.Open,
                            System.IO.FileAccess.Read));

                byte[] buffer = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
                br.Close();
                fileSystemService.FilePut(buffer, arquivoRemoto);

                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, arquivoLocal + " enviado para " + arquivoRemoto, true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao enviar " + arquivoLocal + " para " + arquivoRemoto, true);
                throw ex;
            }
        }

        public static void Download(string remoteFile, string localFile, NeoFileSystemService.NeoFileSystemService fileSystemService)
        {
            try
            {
                Byte[] fileData = fileSystemService.FileGet(remoteFile);
                FileStream file = File.Create(localFile);
                file.Write(fileData, 0, fileData.Length);
                file.Close();

                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, remoteFile + " baixado para " + localFile, true);
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + logName, "Erro ao baixar " + remoteFile + " para " + localFile, true);
                throw ex;
            }
        }

        #endregion

        #region [ Utils ]

        private static StringBuilder MontaCodigoVendedor(String codigoVendedor)
        {
            StringBuilder nomeDoArquivo = new StringBuilder();
            nomeDoArquivo.Append("Vend").Append(codigoVendedor);
            return nomeDoArquivo;
        }

        private static StringBuilder MontarNomeArquivo(String nomeDaTabela)
        {
            StringBuilder s = new StringBuilder();
            s.Append(nomeDaTabela);
            s.Append("Ano" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy"));
            s.Append("Mes" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("MM"));
            s.Append("Dia" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("dd") + "_h");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("HH") + "m");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("mm") + "s");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("ss") + "_");
            s.Append(MontaCodigoVendedor(D.Funcionario.Id.ToString()));
            return s;
        }

        #endregion
    }
}
