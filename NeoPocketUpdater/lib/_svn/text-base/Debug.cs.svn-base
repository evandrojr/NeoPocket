using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Xml;
using Config;

namespace NeoDebug
{

    public static class Debug
    {
        static StreamWriter log;

        public static void LogCria()
        {
            try
            {
                File.Delete(D.AplicacaoDiretorio + D.LogDebug);
            }
            catch { }
            log = new StreamWriter(D.AplicacaoDiretorio + D.LogDebug);
            log.Close();
        }

        public static void LogGrava(string mensagem)
        {
            log = new StreamWriter(D.AplicacaoDiretorio + D.LogDebug, true);
            log.WriteLine(DateTime.Now + " " + mensagem);
            log.Close();
        }

    }
}
