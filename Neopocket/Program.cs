using System;
using System.Windows.Forms;
using Neopocket.Forms;
using Neopocket.Utils;
using Utils;
using Neopocket.Core;
using System.IO;
using System.Runtime.InteropServices;

//using System.Threading;
//using System.Globalization;

namespace Neopocket
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Nao permite a execucao de mais de uma instancia do programa
            using (AppExecutionManager execMgr = new AppExecutionManager(D.AplicacaoNome))
            {
                string lpszParentWindow = D.AplicacaoNome;
                if (execMgr.IsFirstInstance)
                {
                    InitAppVariables();
                    Application.Run(D.FrmPrincipalRef);
                }
                else
                {
                    //Deveria ter aqui apenas o BringWindowToTop(lpszParentWindow); entranto é necessário este workaround para consertar o 
                    //problema da janela hoje que não permitia o aplicativo aparecer na frente
                    BringWindowToTop(lpszParentWindow);
                    InitAppVariables();
                    Application.Run(D.FrmPrincipalRef);
                }
            }

        }

        #region Init App Variables
        /// <summary>
        /// Inicia as variaveis da aplicação.
        /// </summary>
        private static void InitAppVariables()
        {
            try
            {
                Directory.CreateDirectory(D.APP_LOGDIRECTORY);
            }
            catch { }

            try
            {
                Directory.CreateDirectory(D.AplicacaoDiretorio + D.APP_SYNC_PATH);
            }
            catch { }

            // Carrega a versão do neopocket, utilizado para comparar se existe atualizações
            // Mudar para esta no futuro
            //D.AppVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            D.AppVersion = Fcn.VersionGet();
            D.FrmPrincipalRef = new FrmPrincipal();
            D.Bd = new Bd();
            D.Bd.ConStr = D.APP_DBCONN;
            D.Bd.Connect();
            D.Cliente = new Cliente();
            D.Funcionario = new Funcionario();
            Parametro.Carregar();
        }
        #endregion

        #region Win API

        [DllImport("coredll.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("coredll.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static IntPtr FindWindow(string windowName, bool wait)
        {
            int count = 0;
            IntPtr hWnd = FindWindow(null, windowName);
            while (wait && hWnd == IntPtr.Zero && count <= 5)
            {
                System.Threading.Thread.Sleep(500);
                hWnd = FindWindow(null, windowName);
                ++count;
            }
            return hWnd;
        }

        public static bool BringWindowToTop(string windowName)
        {
            IntPtr hWnd = FindWindow(windowName, true);
            if (hWnd != IntPtr.Zero)
            {
                return SetForegroundWindow((IntPtr)hWnd);
            }
            return false;
        }
        #endregion
    }
}
