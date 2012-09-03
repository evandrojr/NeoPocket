using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Neopocket
{
    public partial class FrmMensagem : Form
    {
        public bool DeveEstarNoTopo = false;

        public FrmMensagem(string msg)
        {
            InitializeComponent();
            TxtMsg.Text = msg.Replace("\n", Environment.NewLine);
        }

        public void MensagemAtualizar(string msg){
            TxtMsg.Text = msg.Replace("\n", Environment.NewLine);
        }
        
        private void menuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TimerVerificaSeTopMost_Tick(object sender, EventArgs e)
        {
            if (DeveEstarNoTopo)
                this.ShowDialog();
        }
    }
}