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
    /// <summary>
    /// Formulário de exibição dos dados do cliente
    /// </summary>
    public partial class FrmInformacoesDoCliente : FormBase
    {
        #region [ Construtor ]

        public FrmInformacoesDoCliente()
        {
            InitializeComponent();
        }

        #endregion

        #region [ Voltar ]

        private void mnuVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion  
    }
}