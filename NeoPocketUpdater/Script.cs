namespace NeoPocketUpdater
{
    #region using

    using System;
    using System.Text;
    using System.Threading;
    using Config;
    using System.IO;
    using NeoPocketUpdater.NeoFileSystemService;

    #endregion

    public class Script
    {
        #region data

        public static String directory = String.Empty;
        public static String password = String.Empty;
        public static String newVersion = String.Empty;
        public static String lastVersion = String.Empty;

        #endregion




        #region public methods

        public static string MessageAdd(string atual, string adicionar)
        {
            return adicionar + System.Environment.NewLine + atual;
        }

        public static void Start()
        {
            ThreadStart starter = new ThreadStart(Script.ScriptAlgorithm);
            Thread t = new Thread(starter);
            t.Start();
        }

        #endregion

        #region private methods

        private static void ConfigLoad()
        {
            StreamReader sr;
            if (!File.Exists(D.AplicacaoDiretorio + "ftpusuario.dat"))
                throw new Exception("Não consegui encontrar arquivo com nome do usuário do sistema de arquivo (diretório)");
            if (!File.Exists(D.AplicacaoDiretorio + "senha.seg"))
                throw new Exception("Não consegui encontrar arquivo com senha do diretório");
            if (!File.Exists(D.AplicacaoDiretorio + "vendedorcodigo.seg"))
                throw new Exception("Não consegui encontrar arquivo com código do vendedor");

            sr = new StreamReader(D.AplicacaoDiretorio + "senha.seg");
            password = Criptografia.Crypt.Transform(sr.ReadLine());
            sr.Close();
            sr = new StreamReader(D.AplicacaoDiretorio + "ftpusuario.dat");
            directory = sr.ReadLine();
            sr.Close();
            sr = new StreamReader(D.AplicacaoDiretorio + "ftpsenha.seg");
            password = Criptografia.Crypt.Transform(sr.ReadLine());
            sr.Close();
            D.Funcionario = new Funcionario();
            sr = new StreamReader(D.AplicacaoDiretorio + "vendedorcodigo.seg");
            D.Funcionario.Id = Convert.ToInt32(Criptografia.Crypt.Transform(sr.ReadLine()));
            sr.Close();


            try
            {
                Directory.CreateDirectory(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
            }
            catch { }

            //Apagar todos os arquivos no diretório
            DirectoryInfo di = new DirectoryInfo(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
            FileInfo[] rgFiles = di.GetFiles();
            foreach (FileInfo fi in rgFiles)
            {
                try
                {
                    fi.Delete();
                }
                catch { }
            }


            if (!File.Exists(D.AplicacaoDiretorio + "NeoPocketUpdater.ini"))
            {
                FileStream fs = File.Create(D.AplicacaoDiretorio + "NeoPocketUpdater.ini");
                fs.Close();
            }
        }

        private static void RollBackVersion()
        {
            D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Efetuando rollback na operação");
            StreamWriter tw = new StreamWriter(D.AplicacaoDiretorio + "NeoPocketUpdater.ini");

            if (!String.IsNullOrEmpty(lastVersion))
            {
                tw.Flush();
                tw.WriteLine(lastVersion);
            }
            else
            {
                tw.Flush();
                tw.WriteLine(String.Empty);
            }

            tw.Close();
        }

        private static void ScriptAlgorithm()
        {
            string remoteFile = "neopocket.zip";
            string localFile = "neopocket.zip";

            if (D.ModoTeste)
            {
                remoteFile = "neopocket_teste.zip";
                D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("### Executando o atualizador em modo de teste ###");
                D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Irá buscar arquivo " + remoteFile + " mesmo que não tenha sido lançada uma nova versão");
            }

            D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Iniciando o sistema de atualização automática versão " + D.APP_VERSION);
            //Dorme um segundo para poder ver a versão
            System.Threading.Thread.Sleep(1000);

            try
            {
                #region carrega as config

                try
                {
                    ConfigLoad();
                }
                catch
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Não foi possível carregar as configurações");
                    D.FrmPrincipalRef.ScriptEnd();
                    return;
                }

                #endregion

                #region referência para o web service / NeoFileSystemService

                ValidationSoapHeader header = new ValidationSoapHeader();
                header.Directory = directory;
                header.Password = password;
                NeoFileSystemService.NeoFileSystemService fileSystemService = new NeoFileSystemService.NeoFileSystemService();
                fileSystemService.ValidationSoapHeaderValue = header;

                try
                {
                    foreach (string diretorio in fileSystemService.DirList(""))
                    {
                        //Faz nada apenas testa se conectou
                    }
                }
                catch (Exception e)
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Não foi possível encontrar o servidor, tente novamente mais tarde " + e.Message);
                    D.FrmPrincipalRef.ScriptEnd();
                    return;
                }

                #endregion

                #region busca o número da versão atual do neopocket

                /* Variaveis utilizadas nessa região */
                String versionFileName = "neopocket-version.txt";
                Byte[] arlDataVersionFile = null;
                FileStream fsVersionFile = null;

                try
                {
                    /* Algoritmo */
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Checando versão do neopocket");
                    arlDataVersionFile = fileSystemService.FileGet(@"nova_versao\" + versionFileName);
                    fsVersionFile = File.Create(D.AplicacaoDiretorio + versionFileName);
                    fsVersionFile.Write(arlDataVersionFile, 0, arlDataVersionFile.Length);
                    fsVersionFile.Close();

                    if (File.Exists(D.AplicacaoDiretorio + "NeoPocketUpdater.ini"))
                    {
                        StreamReader sr = new StreamReader(D.AplicacaoDiretorio + "NeoPocketUpdater.ini");
                        lastVersion = sr.ReadLine();
                        sr.Close();

                        sr = new StreamReader(D.AplicacaoDiretorio + versionFileName);
                        newVersion = sr.ReadLine();
                        sr.Close();
                        File.Delete(D.AplicacaoDiretorio + versionFileName);

                        if (newVersion.Equals(lastVersion) && !D.ModoTeste)
                        {
                            D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Carregando sistema Neopocket");
                            D.FrmPrincipalRef.ScriptEnd();
                            return;
                        }
                        else
                        {
                            D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Guardando a versão desta atualização");
                            StreamWriter tw = new StreamWriter(D.AplicacaoDiretorio + "NeoPocketUpdater.ini");
                            tw.Flush();
                            tw.WriteLine(newVersion);
                            tw.Close();
                        }
                    }
                    else
                    {
                        D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Não foi possível verificar a versão atual do neopocket");
                    }
                }
                catch
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Não foi possível verificar a versão atual do neopocket");
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Carregando sistema Neopocket");
                    D.FrmPrincipalRef.ScriptEnd();
                    return;
                }

                #endregion

                /* Apartir daqui, qualquer exceção que ocorrer tem que da rollback no número
                 * da versão, para evitar que o neopocket fique com uma versão corrumpida. */

                #region sincronização

                try
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Verificando se existem pedidos, clientes, recusa para serem enviados");
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Selecionando novos clientes e pedidos");
                    Sincronizacao.Iniciar();
                    Sincronizacao.ClientesSelecionarParaEnvioCSV();
                    Sincronizacao.RecusaSelecionarParaEnvioCSV();
                    Sincronizacao.PedidosSelecionarParaEnvioCSV();
                    bool haviamArquivosParaEnviar = Sincronizacao.ArquivosCompactar();
                    if (haviamArquivosParaEnviar)
                    {
                        D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Enviando novos clientes e pedidos ");
                        Upload(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + Sincronizacao.ArquivoEnviarNome, directory + @"\" + Sincronizacao.ArquivoEnviarNome, fileSystemService);
                        //              Sincronizacao.FechaConexao();

                        Sincronizacao.LstClienteNovoId = D.Bd.ListGuid(@"
                        select
                                id_cliente_pocket
                        from 
                                cliente
                        where
                                status='N'");
                        Sincronizacao.LstPedidoNovoId = D.Bd.ListGuid(@"
                        select
                                id_pedido
                        from 
                                pedido
                        where
                                status='N'");

                        //............Recusa....................................

                        Sincronizacao.LstRecusaNovoId = D.Bd.ListInt(@"
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
                        D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Não existem clientes ou pedidos novos ");
                    }

                    D.Bd.Con.Close();
                }
                catch (Exception ex)
                {
                    RollBackVersion();
                    throw ex;
                }

                #endregion

                #region baixa a nova versão

                try
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Recebendo a nova versão do sistema");
                    Download(@"nova_versao\" + remoteFile, D.AplicacaoDiretorio + localFile, fileSystemService);
                }
                catch
                {
                    RollBackVersion();
                    throw;
                }

                #endregion

                #region verifica a integridade do arquivo

                D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Verificando a integridade do arquivo");
                if (Md5(D.AplicacaoDiretorio + localFile) != fileSystemService.Md5(@"nova_versao\" + remoteFile))
                {
                    RollBackVersion();
                    throw new Exception("A checagem de integridade falhou, tente mais tarde.");
                }

                #endregion

                #region descompacta a versão

                try
                {
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Descompactando a nova versão do sistema");
                    NeoZip.Zip.UnzipFiles(D.AplicacaoDiretorio + localFile, D.AplicacaoDiretorio);
                    D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Atualização concluída!");
                }
                catch (Exception ex)
                {
                    RollBackVersion();
                    throw ex;
                }


                #endregion

                #region carrega o sistema

                D.FrmPrincipalRef.TxtMessageAddMessageFromOtherThread("Carregando sistema Neopocket");
                D.FrmPrincipalRef.ScriptEnd();

                #endregion
            }
            catch (Exception ex)
            {
                FE.Show(ex);
                D.FrmPrincipalRef.ScriptEnd();
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

        private static void Upload(string arquivoLocal, string arquivoRemoto, NeoFileSystemService.NeoFileSystemService fileSystemService)
        {
            System.IO.BinaryReader br = new
            System.IO.BinaryReader(System.IO.File.Open(arquivoLocal, System.IO.FileMode.Open,
            System.IO.FileAccess.Read));

            byte[] buffer = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
            br.Close();
            fileSystemService.FilePut(buffer, arquivoRemoto);
        }

        private static void Download(string remoteFile, string localFile, NeoFileSystemService.NeoFileSystemService fileSystemService)
        {
            Byte[] fileData = fileSystemService.FileGet(remoteFile);
            FileStream file = File.Create(localFile);
            file.Write(fileData, 0, fileData.Length);
            file.Close();
        }

        #endregion
    }
}
