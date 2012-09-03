using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NeoPocketUpdater {
    public partial class FE : Form {

        public FE() {
            InitializeComponent();
        }

        public FE(string title, string message) {
            InitializeComponent();
            Text = title;
            txtMensagem.Text = message;
            txtDetalhes.Visible = false;
        }

        public FE(string title, string message, string details) {
            InitializeComponent();
            Text = title;
            txtMensagem.Text = message;
            txtDetalhes.Text = details;
        }

        public static void Show(string message, string title) {
            FE f = new FE(title, message);
            f.ShowDialog();
        }

        public static void Show(string message, string title, string details) {
            FE f = new FE(title, message, details);
            f.ShowDialog();
        }

        public static void Show(string message, string title, Exception ex)
        {
            FE f = new FE(title, message, ex.Message + " StackTrace " + ex.StackTrace);
            f.ShowDialog();
        }


        public static void Show(Exception ex) {
            FE f = new FE("Aviso", ex.Message, ex.StackTrace);
            f.ShowDialog();
        }

        private void btClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void txtDetalhes_Click(object sender, EventArgs e) {
            txtDetalhes.Visible = true;
        }

        private void FE_Load(object sender, EventArgs e) {

        }




    }
}