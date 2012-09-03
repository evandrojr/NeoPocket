using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Config;
using Utils;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace NeoPocketUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            //Verificar se tem um neo pocket rodando
            using (AppExecutionManager execMgrNeoPocket = new AppExecutionManager("NeoPocket"))
            {
                string lpszParentWindowNeoPocket = "NeoPocket";
                if (execMgrNeoPocket.IsFirstInstance)
                {
                    //Nao tem neo pocket rodando, vou verificar se tem neo pocket updater
                    using (AppExecutionManager execMgrNeoPocketUpdater = new AppExecutionManager("NeoPocketUpdater"))
                    {
                        string lpszParentWindowNeoPocketUpdater = "NeoPocketUpdater";
                        if (execMgrNeoPocket.IsFirstInstance)
                        {
//                           Nao tem NeoPocketUpdater rodando, vou chamar um novo updater
                            InitAppVariables();
                            Application.Run(D.FrmPrincipalRef);
                        }
                        else
                        {
//                          Ja tinha, vou abrir o que estava rodando
                            BringWindowToTop(lpszParentWindowNeoPocketUpdater);
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    // Chama o Neo pocket que estava em execucao
                    BringWindowToTop(lpszParentWindowNeoPocket);
                    //Mesmo que já tenha um em execução aguarda 3 segundos e tenta chamar outro, pois a tela "Hoje" causa um problema de não 
                    //permitir que o aplicativo fique na frente
                    System.Threading.Thread.Sleep(3000);
                    System.Diagnostics.Process.Start(D.AplicacaoDiretorio + "NeoPocket.exe", "");
                    Application.Exit();
                }
            }
        }


        public static void InitAppVariables()
        {
            D.Bd = new Bd();
            D.Bd.ConStr = D.ConexaoParamentros;
            D.Bd.Connect();
            D.FrmPrincipalRef = new FrmMain();
        }

        #region Check App Is Run
        /// <summary>
        /// Checa se a applicação NeoUpdater já esta sendo rodada.
        /// </summray>
        private static void CheckNeoUpdaterIsRun()
        {
            string lpszParentClass = "NeoPocketUpdater";
            lpszParentClass = null;
            string lpszParentWindow = "NeoPocketUpdater";

            IntPtr NeoPocketUpdaterWnd = new IntPtr(0);
            NeoPocketUpdaterWnd = FindWindow(lpszParentClass, lpszParentWindow);
            if (NeoPocketUpdaterWnd.Equals(IntPtr.Zero))
            {
            }
            else
            {
                if (BringWindowToTop(lpszParentWindow))
                {
                    Application.Exit();
                }
            }
        }
        #endregion


        #region Check App Is Run
        /// <summary>
        /// Checa se a applicação NeoPocket já esta sendo rodada.
        /// </summary>
        private static void CheckNeoPocketIsRun()
        {
            string lpszParentClass = "NeoPocket";
            lpszParentClass = null;
            string lpszParentWindow = "NeoPocket";

            IntPtr NeoPocketUpdaterWnd = new IntPtr(0);
            NeoPocketUpdaterWnd = FindWindow(lpszParentClass, lpszParentWindow);
            if (NeoPocketUpdaterWnd.Equals(IntPtr.Zero))
            {
            }
            else
            {
                if (BringWindowToTop(lpszParentWindow))
                {
                    Application.Exit();
                }
            }
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
            while (wait && hWnd == IntPtr.Zero && count <=5)
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