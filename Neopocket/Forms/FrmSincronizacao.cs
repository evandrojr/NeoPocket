using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Neopocket.Core;

namespace Neopocket.Forms
{
    public partial class FrmSincronizacao : Form
    {
        public FrmSincronizacao()
        {
            InitializeComponent();
            this.TxtMsg.Text = "";
            Cursor.Current = Cursors.WaitCursor;
            SincronizacaoScript.FrmSource = this;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            SincronizacaoScript.Start();
        }

        public void TxtMessageMensagemMontaP2(string msg)
        {
           Object[] arguments = new Object[] { msg };
           this.TxtMsg.Invoke(new EventHandler(WorkerMensagemMontaP2), arguments);
           //Thread.Sleep(200);
        }

        public void WorkerMensagemMontaP2(object sender, EventArgs e)
        {
            this.TxtMsg.Text = SincronizacaoScript.MensagemMontaP2(this.TxtMsg.Text, sender.ToString());
            this.TxtMsg.Update();
        }

        public void TxtMessageMontaP1(string msg)
        {
            Object[] arguments = new Object[] { msg };
            this.TxtMsg.Invoke(new EventHandler(WorkerMensagemMontaP1), arguments);
            Thread.Sleep(200);
        }

        public void WorkerMensagemMontaP1(object sender, EventArgs e)
        {
            this.TxtMsg.Text = SincronizacaoScript.MensagemMontaP1(this.TxtMsg.Text, sender.ToString());
            this.TxtMsg.Update();
        }

        public void TxtMessageShow(string msg)
        {
            Object[] arguments = new Object[] { msg };
            this.TxtMsg.Invoke(new EventHandler(WorkerMensagemShow), arguments);
            Thread.Sleep(200);
        }

        public void WorkerMensagemShow(object sender, EventArgs e)
        {
            this.TxtMsg.Text = SincronizacaoScript.MensageShow(this.TxtMsg.Text, sender.ToString());
            this.TxtMsg.Update();
        }


        public void ScriptEnd()
        {
            this.Invoke(new EventHandler(TheEnd));
        }

        //Chamar no final
        public void TheEnd(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
            mniRetornar.Enabled = true;
            TxtMsg.Text = SincronizacaoScript.MensageShow(TxtMsg.Text, "Tempo total decorrido " + Math.Round((DateTime.Now.Ticks - SincronizacaoScript.SincronizacaoInicioTick) / 10000000.0) + " segundos");
            TxtMsg.Text = SincronizacaoScript.MensageShow(TxtMsg.Text, "--- Sincronização finalizada ---");
        }

        private void mniRetornar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}