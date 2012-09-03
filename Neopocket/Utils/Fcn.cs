using System;
using Neopocket.Utils;
using System.Text.RegularExpressions;
using System.IO;


namespace Neopocket
{
    class Fcn
    {

        /// <summary>
        /// Clone of the desktop version
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadAllText(string path)
        {
            string text="";
            StreamReader s;
            s = File.OpenText(path);
            try
            {
                text = s.ReadToEnd();
            }
            catch
            {
                s.Close();
            }
            s.Close();
            return text;
        }

        /// <summary>
        /// Clone of the desktop version
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void WriteAllText(string path, string contents)
        {
            using( System.IO.StreamWriter writer = new System.IO.StreamWriter(path) )
            {
                writer.Write(contents);
                writer.Close();
            }
        }

        public static void WriteText(string path, string contents, int amountOfChars)
        {
            using (FileStream sFileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                if (contents.Length < amountOfChars)
                    amountOfChars = contents.Length;
                StreamWriter sWriter = new StreamWriter(sFileStream);
                sWriter.Write(contents.ToCharArray(),0, amountOfChars);
                sWriter.Close();
            }

        }
        public static string VersionGet()
        {
            string ver = "";

            try
            {
                // create reader & open file
                TextReader tr = new StreamReader("date.txt");

                // read a line of text
                ver = tr.ReadLine();

                // close the stream
                tr.Close();
            }
            catch
            {
            }
            return ver;
        }


        /// <summary>
        // removes character and leave numbers
        /// </summary>
        /// <param name="leaveOnlyNumbers"></param>
        /// <returns></returns>
        public static string RemoveAllExceptNumbers(string leaveOnlyNumbers)
        {
            // removing character and leave numbers
            return Regex.Replace(leaveOnlyNumbers, @"[\D]", "");
        }
        
        /// <summary>
        /// Informa se o objeto passado foi setadado com algum valor excluindo null 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool EhNulo(object o)
        {
            try
            {
                if (o == null)
                    return true;
            }
            catch
            {
                return true;
            }
            return false;
        }

        public static string DinheiroFormata(double valor)
        {
            //Não usa o R$ para economizar área da tela
            if(valor >= 0)
                return valor.ToString("C", D.CultureInfoBRA).Replace("R$", "").Replace(" ", "");
            else
                return "-" + valor.ToString("C", D.CultureInfoBRA).Replace("R$", "").Replace("-", "").Replace(" ", "").Replace("(", "").Replace(")", "");

            //if (valor >= 0)
            //    return valor.ToString("C", D.CultureInfoBRA);
            //else //Era assim que funcionava
            //    // return valor.ToString("C", D.CultureInfoBRA).Replace("(", "").Replace(")", "").Insert(2, "-");
            //    // Mas passou a funcionar assim:
            //    return valor.ToString("C", D.CultureInfoBRA).Replace("(", "").Replace(")", "").Replace("-", "").Insert(3, "-");
        }

        public static string DinheiroFormataSemCifrao(double valor)
        {
            //return String.Format("0.#0", valor);
            return valor.ToString("0.#0", D.CultureInfoBRA);
        }

        public static void DeEnderecoParaVariaveisFTP(String endereco)
        {
            if (!String.IsNullOrEmpty(endereco))
            {
                D.APP_FTP_PATH = endereco;
                if (D.APP_FTP_PATH.IndexOf("/", D.APP_FTP_PATH.IndexOf("//") + 2) >= 0)
                {
                    D.APP_FTP_SERVER = D.APP_FTP_PATH.Substring(0, D.APP_FTP_PATH.IndexOf("/", D.APP_FTP_PATH.IndexOf("//") + 2));
                    if ((D.APP_FTP_PATH.Length - D.APP_FTP_SERVER.Length) == 1)
                        D.APP_FTP_PATH = String.Empty;
                    else
                        D.APP_FTP_PATH = D.APP_FTP_PATH.Substring(D.APP_FTP_SERVER.Length + 1, D.APP_FTP_PATH.Length - D.APP_FTP_SERVER.Length - 1);
                }
                else
                {
                    D.APP_FTP_SERVER = D.APP_FTP_PATH;
                    D.APP_FTP_PATH = String.Empty;
                }
            }
        }

        /// <summary>
        /// Truncate a value keeping a number of decimal places
        /// </summary>
        /// <param name="value">Value to be truncated</param>
        /// <param name="decimals">Number of decimal places</param>
        /// <returns></returns>
        public static double TruncateWithDecimals(double value, int decimals)
        {
            double decimalsHolder = Math.Pow(10, decimals);

            value = Math.Floor(value * decimalsHolder);
            return value /= decimalsHolder;
        }

        /// <summary>
        /// Arredonda igualzinho ao do C# a unica diferenca eh que permite que no futuro seja trocado de volta para 
        /// outra funcao (antes usava a Truncate with decimals)
        /// </summary>
        /// <param name="value">Value to be truncated</param>
        /// <param name="decimals">Number of decimal places</param>
        /// <returns></returns>
        public static double RoundComputing(double value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        public static DateTime DomingoPassadoData()
        {
            DateTime hoje = DateTime.Today;
            DateTime domingoPassado = new DateTime();
            TimeSpan diasPassados;
            int diasPassadosI = 0;

            if (hoje.DayOfWeek == DayOfWeek.Monday)
                diasPassadosI = 1;
            else
                if (hoje.DayOfWeek == DayOfWeek.Tuesday)
                    diasPassadosI = 2;
                else
                    if (hoje.DayOfWeek == DayOfWeek.Wednesday)
                        diasPassadosI = 3;
                    else
                        if (hoje.DayOfWeek == DayOfWeek.Thursday)
                            diasPassadosI = 4;
                        else
                            if (hoje.DayOfWeek == DayOfWeek.Friday)
                                diasPassadosI = 5;
                            else
                                if (hoje.DayOfWeek == DayOfWeek.Saturday)
                                    diasPassadosI = 6;
            diasPassados = new TimeSpan(diasPassadosI, 0, 0, 0);
            domingoPassado = hoje - diasPassados;
            return domingoPassado.Date;
        }


    }

}
