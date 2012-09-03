 using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using NeoSync;

namespace Core
{
    public static class D
    {

        #region argumentos via linha de comando

        public static string ArgLoja = "";
        public static string ArgFtpUsuario = "";

        #endregion

        public static bool ModoTeste = false;
        public static Bd Bd;
        public static Bd BdRel;
        public static string ApplicationName = "NeoSync";
//        public static string AplicacaoDiretorio = (System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\").ToString().Substring(6);
        public static string ApplicationDirectory = Environment.CurrentDirectory[Environment.CurrentDirectory.Length - 1] != '\\' ? Environment.CurrentDirectory + '\\' : Environment.CurrentDirectory;
        public static string BancoAlvo = ""; // caminho e arquivo
        public static System.Globalization.CultureInfo CultureInfoBRA = new System.Globalization.CultureInfo("pt-br");
        public static System.Globalization.CultureInfo CultureInfoEUA = new System.Globalization.CultureInfo("en-us");
        public static string TabelasSincronizacaoDiretorio = @"SincDado\";
        public static string BackupDiretorio = @"Backup\";
        public static string Loja;
        public static string arquivoEnvioNome = "STORE_RM.zip";
        public static Roteiro Roteiro;

        #region Configuracao da Lib

        //Debug config
        public static string NeoDebug_LogFile = "NeoSyncLog.txt";
        public static bool NeoDebug_ReportSend = true;
        public static bool NeoDebug_ReportSave = true;
        public static string NeoDebug_From = "log@neotecnologia.net";
        public static string NeoDebug_To =   "log@neotecnologia.net";
        public static string NeoDebug_Subject = "NeoSync erro";
        public static string NeoDebug_Body = "";
        public static string NeoDebug_Host = "smtp.gmail.com";
        public static int NeoDebug_Port = 25;
        public static string NeoDebug_User = "log@neotecnologia.net";
        public static string NeoDebug_Password = "luavelha";
        public static string NeoDebug_LogFilePath = ApplicationDirectory;

        #endregion 

        static D(){
            if (System.Environment.MachineName.ToLower() == "venus")
                D.ModoTeste = true;
            else
                D.ModoTeste = false;
        }

        public static string ConexaoParamentros()
        {
            return "User=ABDMBA2001MXQ;Password=aqbxdmm1b0a02;Dialect=3;" + D.BancoAlvo;
        }

        //static D()
        //{
        //}
    }
}
