using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsCE.Forms;
using System.Drawing;
using Neopocket.Forms;

namespace Neopocket.Utils
{
    public class Util
    {
        //Prevents a form from being open more than once
        private static bool ThereIsAlreadyAnOpenFormB=false; 

        #region [ Controla a exibição dos formulários ]

        /// <summary>
        /// Não permite que outro formulário seja aberto se esse já estiver aberto (usado especiamente na sincronização)
        /// 
        /// </summary>
        /// <param name="form">Form</param>
        /// <returns>DialogResult</returns>
        public static DialogResult FormDialogExibirApenasUm(Form form)
        {
            if (Util.ThereIsAlreadyAnOpenFormB)
                return DialogResult.Abort;
            try
            {
                if (D.FrmPrincipalRef == null)
                    throw new Exception("Um formulário pai deve ser setado!");

                form.Owner = D.FrmPrincipalRef;
                Cursor.Current = Cursors.WaitCursor;
                DialogResult result = form.ShowDialog();
                ThereIsAlreadyAnOpenFormB = true;
                Cursor.Current = Cursors.Default;
                return result;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao exibir formulário", "Neo", ex);
                return DialogResult.Abort;
            }
            finally
            {
                ThereIsAlreadyAnOpenFormB = false;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Exibe um formulário dentro da aplicação. Trocando o estado do cursor do mouse e evitando o desperdício de 
        /// memória
        /// </summary>
        /// <param name="form">Form</param>
        /// <returns>DialogResult</returns>
        public static DialogResult FormExibirDialog(Form form)
        {
            try
            {
                if (D.FrmPrincipalRef == null)
                    throw new Exception("Um formulário pai deve ser setado!");

                form.Owner = D.FrmPrincipalRef;
                Cursor.Current = Cursors.WaitCursor;
                DialogResult result = form.ShowDialog();
                Cursor.Current = Cursors.Default;
                return result;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao exibir formulário", "Neo", ex);
                return DialogResult.Abort;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }



        public static void FormExibir(Form form)
        {
            try
            {
                if (D.FrmPrincipalRef == null)
                    throw new Exception("Um formulário pai deve ser setado!");

                form.Owner = D.FrmPrincipalRef;
                Cursor.Current = Cursors.WaitCursor;
                form.Show();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao exibir formulário", "Neo", ex);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

        }

        #endregion

        /// <summary>
        /// Checha a orientação da tela para ajustar a depender do dispositivo.
        /// </summary>
        public static void ScreenOrientationChecar()
        {
            if (SystemSettings.ScreenOrientation != ScreenOrientation.Angle0)
                SystemSettings.ScreenOrientation = ScreenOrientation.Angle0;
        }

        /// <summary>
        /// Válida os componentes dentro de um panel.
        /// </summary>
        /// <param name="panel">Panel a ser validado</param>
        /// <param name="notification">Controle de notificação</param>
        /// <returns>Boolean</returns>
        public static Boolean ValidarPanel(System.Windows.Forms.Panel panel, Microsoft.WindowsCE.Forms.Notification notification)
        {
            bool result = true;

            for (int j = 0; j < panel.Controls.Count; j++)
                if (panel.Controls[j] is TextBox)
                {
                    if (String.IsNullOrEmpty(panel.Controls[j].Text))
                    {
                        result = false;
                        ((TextBox)panel.Controls[j]).BackColor = Color.Red;
                    }
                    else
                        ((TextBox)panel.Controls[j]).BackColor = Color.White;
                }

            if (!result)
            {
                notification.Text = "Campos obrigatórios não preechidos estão em vermelho.";
                notification.Visible = true;
                return false;
            }
            
            return result;
        }
    }
}
