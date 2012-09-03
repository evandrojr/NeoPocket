using System.IO;
using System;
using System.Text;
using System.Windows.Forms;


namespace Neopocket.Utils
{

    /// <summary>
    /// Provê um ponto único do controle dos logs do NeoPocket
    /// </summary>
    public class LogBuilder
    {
        
        /// <summary>
        /// As mensagens mais recentes ficam no topo do logo e o que exceder o D.LogCharsMax será
        /// descartado
        /// </summary>
        /// <param name="Message"></param>
        public static void LogWrite(string Message)
        {
            try
            {
                DateTime now = DateTime.Now;
                string log = "";
                String strData = "D{0}M{1}A{2}h{3}m{4}s{5}    ";
                StringBuilder sb = new StringBuilder(
                    String.Format(
                            strData, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second));
                sb.Append(Message);
                if (File.Exists(D.APP_LOGDIRECTORY + D.LogFileName))
                    log = Fcn.ReadAllText(D.APP_LOGDIRECTORY + D.LogFileName);
                else
                    log = "";
                sb.Append(log);
                sb.Append("\r\n");
                Fcn.WriteText(D.APP_LOGDIRECTORY + D.LogFileName, sb.ToString(), D.LogCharsMax);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro tentando escrever o log: " + ex.Message + " " + ex.StackTrace);
            }

            //#region referência para o web service / NeoFileSystemService

            //ValidationSoapHeader header = new ValidationSoapHeader();
            //header.Directory = directory;
            //header.Password = password;
            //NeoFileSystemService.NeoFileSystemService fileSystemService = new NeoFileSystemService.NeoFileSystemService();
            //fileSystemService.ValidationSoapHeaderValue = header;


        }

        
        /// <summary>
        /// Depreciada
        /// Método responsável por escrever no log, só deixa escrever no log
        /// se a mensagem passada não for vazia.
        /// </summary>
        /// <param name="caminhoLog">Caminho do arquivo de log</param>
        /// <param name="mensagem">Mensagem a ser gravada</param>
        public static void DEPRECIADO_Append(String caminhoLog, String mensagem, Boolean usarFormatacao)
        {
            try
            {
                if (String.IsNullOrEmpty(mensagem))
                    return;

                if (usarFormatacao)
                {
                    String formatoMensagem = "[" + DateTime.Now.ToString() + "] {0}";
                    mensagem = String.Format(formatoMensagem, mensagem);
                }

                using (FileStream sFileStream = File.Open(caminhoLog, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    StreamWriter sWriter = new StreamWriter(sFileStream);
                    sWriter.WriteLine(mensagem);
                    sWriter.Close();
                }
            }
            catch (Exception ex)
            {
                // Se ocorrer um erro na hora de salvar o log, não trava a aplicação!
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Apaga o arquivo de log
        /// </summary>
        /// <param name="caminhoLog">Caminho do log</param>
        public static void DEPRECIADO_Clear(String caminhoLog)
        {
            if (File.Exists(caminhoLog))
                File.Delete(caminhoLog);
        }
    }
}
