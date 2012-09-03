using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Core;
using NeoDebug;
using ShowLib;

namespace NeoSync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    D.ArgLoja = args[0].PadLeft(6, '0');
                    D.ArgFtpUsuario = args[1];
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                FrmPrincipal frmPrincipal = new FrmPrincipal();
                if (!D.ModoTeste)
                    Application.ThreadException += new ThreadExceptionEventHandler(frmPrincipal.UnhandledThreadExceptionHandler);
                frmPrincipal.Start();
                Application.Run(frmPrincipal);
            }
            catch(Exception e)
            {
                if (D.ModoTeste)
                {
                    throw e;
                }
                else
                {
                    FE.Show(e);
                }
           }
        }
    }
}
