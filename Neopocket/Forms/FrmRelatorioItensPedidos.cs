using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Neopocket.Forms
{
    public partial class FrmRelatorioItensPedidos : Form
    {
        public FrmRelatorioItensPedidos()
        {
            InitializeComponent();
            D.Acao = D.AcaoEnum.RelatorioVer;
        }
    }
}