using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Web;
using NeoPocketUpdater;


namespace Config
{
    public class D
    {
        public static bool ModoTeste = false;
        public static string APP_VERSION = "1.0.0.3";
        public static Bd Bd;
        public static string AplicacaoDiretorio = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";
        public static string BancoArquivo = "pocketdb.sdf";
        public static Funcionario Funcionario = new Funcionario();
        public static string ConexaoParamentros = "data source=" + AplicacaoDiretorio + BancoArquivo + ";Max Buffer Size=4096;Password=j$dqapc10;";

        /// <summary>
        /// Culturas
        /// </summary>
        public static System.Globalization.CultureInfo CultureInfoBRA = new System.Globalization.CultureInfo("pt-br");
        public static System.Globalization.CultureInfo CultureInfoEUA = new System.Globalization.CultureInfo("en-us");

        public static string LogImportacaoArquivo = "LogImportacao.txt";
        public static string TabelasXmlDiretorio = @"Tabelas\";
        public static string TabelasSincronizacaoDiretorio = @"SincDado\";
        public static string FtpServidor;  // Nunca tem a barra ao final
        public static string FtpDiretorio; // Quando não vazio, sempre tem uma barra '/' ao final
        public static string FtpUsuario;   // Somente o nome do usuário FTP, sem barras
        public static string FtpSenha;     // Somente a senha do usuário FTP
        public static string UsuarioNome;  // Nome do usuário do sistema no Pocket
        public static string UsuarioSenha; // Senha do usuário do sistema no Pocket
        public static bool CliqueUnico = false;
        public static string LogDebug = "log.txt";

        /// <summary>
        /// O formulário que está em exibição no momento
        /// </summary>
        public static FrmMain FrmPrincipalRef = null;
        public static string ValidationSoapHeaderDevToken = "hd83hjd%ns";


        /// <summary>
        /// Caminho do diretório da aplicação
        /// </summary>
        public static String APP_PATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\";

        /// <summary>
        /// Nome do arquivo do banco de dados
        /// </summary>
        public static String APP_DBFILENAME = "pocketdb.sdf";

        /// <summary>
        /// Nome do arquivo de log
        /// {0} = Nome do arquivo (Exemplo: Sincronizacao)
        /// {1} = Data da geração no formato (D01M02A2009H10M2)
        /// </summary>
        public static String APP_LOGFILENAME = "NeoPocket-{0}-{1}.txt";

        public static String APP_LOG_EXCEPTIONFILENAME = "NeoPocket-Exception.txt";

        /// <summary>
        /// Número máximo do histórico de logs
        /// </summary>
        public static Int32 APP_LOG_MAX = 5;

        /// <summary>
        /// Diretorio do arquivo de log
        /// </summary>
        public static String APP_LOGDIRECTORY = APP_PATH + @"Log\";

        /// <summary>
        /// Nome do arquivo de log de debug
        /// </summary>
        public static String APP_DEBUGLOGFILENAME = "log.txt";

        /// <summary>
        /// String de conexão com o banco
        /// </summary>
        public static String APP_DBCONN = "data source=" + APP_PATH + APP_DBFILENAME + ";Max Buffer Size=4096;Password=j$dqapc10;";

        /// <summary>
        /// Se a aplicação está em modo de desenvolvimento
        /// </summary>
        public static Boolean APP_TESTEMODE = false;

        /// <summary>
        /// Servidor FTP, nunca tem a barra ao final
        /// </summary>
        public static String APP_FTP_SERVER;

        /// <summary>
        /// Caminho do servidor ftp, quando não vazio, sempre tem uma barra '/' ao final
        /// </summary>
        public static String APP_FTP_PATH;

        /// <summary>
        /// Nome do usuário FTP, sem barras
        /// </summary>
        public static String APP_FTP_USER;

        /// <summary>
        /// Senha do usuário FTP
        /// </summary>
        public static String APP_FTP_PASS;

        /// <summary>
        /// Nome do usuário do sistema no Pocket
        /// </summary>
        public static String APP_USER_NAME;

        /// <summary>
        /// Senha do usuário do sistema no Pocket
        /// </summary>
        public static String APP_USER_PASS;


        /// <summary>
        /// Configura a quantidade de casas decimas do preço do produto
        /// </summary>
        public static Int32 ProdutoPrecoCasasDecimais = 4;



        static D(){ 
        }

    }
}
