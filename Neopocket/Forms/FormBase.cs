using System;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário base para os formulários da aplicação herdarem
    /// </summary>
    public partial class FormBase : Form
    {
        #region [ Atributos ]

        private bool checkScreenOrientation = true;

        #endregion

        #region [ Construtor ]

        public FormBase()
        {
            InitializeComponent();
        }

        public FormBase(bool checkScreenOrientation)
        {
            InitializeComponent();
            this.checkScreenOrientation = checkScreenOrientation;
        }

        #endregion

        #region [ Verificação da orientação da tela ]

        private void FormBase_Activated(object sender, EventArgs e)
        {
            if (checkScreenOrientation)
                Util.ScreenOrientationChecar();
        }

        #endregion

        private void FormBase_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void FormBase_GotFocus(object sender, EventArgs e)
        {
            BringToFront();
        }
    }
}