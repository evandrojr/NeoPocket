﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Config;

namespace NeoPocketUpdater
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.TxtMsg.Text = "";
            Script.Start();
        }

        public void TxtMessageAddMessageFromOtherThread(string msg){
                
               Object[] arguments = new Object[] { msg };
               this.TxtMsg.Invoke(new EventHandler(WorkerUpdate), arguments);
               Thread.Sleep(200);
        }
        
        public void WorkerUpdate(object sender, EventArgs e)
        {
            this.TxtMsg.Text = Script.MessageAdd(this.TxtMsg.Text, sender.ToString());
            this.TxtMsg.Update();
        }

        public void ScriptEnd()
        {
            this.Invoke(new EventHandler(TheEnd));
        }

        public void TheEnd(object sender, EventArgs e)
        {
            exitProgramOpenningNeoPocket();
        }

        private void exitProgramOpenningNeoPocket()
        {
            menuItemSair.Enabled = true;
            try
            {
                System.Diagnostics.Process.Start(D.AplicacaoDiretorio + "NeoPocket.exe", "");
                Close();
            }
            catch { }
        }
    }
}