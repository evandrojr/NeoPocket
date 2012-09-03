using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Neopocket
{
    public class NeoGrade
    {
        public Panel Panel = new Panel();
        public TextBox[,] Tabela;
        public List<string> LstColunaNome;
        private int colunaLargura, linhaAltura = 18;
        private int largura;
        int linhas, colunas;
        public Color CorCabecalho, CorLinhaImpar, CorLinhaPar;
        public Font Font = new Font("Arial", 8F, System.Drawing.FontStyle.Regular);


        public NeoGrade(List<string> NomeColunas, int _linhasQtq, int _largura, int _altura)
        {
            largura = _largura;
            Width = largura;
            Height = _altura;
            linhas = _linhasQtq;
            colunas = NomeColunas.Count;
            Tabela = new TextBox[colunas, linhas + 1];
            for (int y = 0; y <= linhas; ++y)
                for (int x = 0; x < colunas; ++x)
                {
                    Tabela[x, y] = new TextBox();
                }
            Panel.AutoScroll = true;
            LstColunaNome = NomeColunas;
            colunaLargura = Width / colunas;
            CorCabecalho = Color.LightBlue;
            CorLinhaImpar = Color.AliceBlue;
            CorLinhaPar = Color.LightGray;
            Mostrar();
        }

        public void Mostrar()
        {
            for (int x = 0; x < colunas; ++x) // Gera os Títulos
            {
                Tabela[x, 0].Text = LstColunaNome[x];
                Tabela[x, 0].Top = 0;
                Tabela[x, 0].Left = colunaLargura * x;
                Tabela[x, 0].Width = colunaLargura;
                Tabela[x, 0].Height = linhaAltura;
                Tabela[x, 0].ReadOnly = true;
                Tabela[x, 0].BackColor = CorCabecalho;
                Tabela[x, 0].Font = Font;
                Panel.Controls.Add(Tabela[x, 0]);
            }
            for (int y = 1; y <= linhas; ++y)
            {
                for (int x = 0; x < colunas; ++x)
                {
                    Tabela[x, y].Top = linhaAltura * y;
                    Tabela[x, y].Left = colunaLargura * x;
                    Tabela[x, y].Width = colunaLargura;
                    Tabela[x, y].Height = linhaAltura;
                    if (y % 2 == 0)
                        Tabela[x, y].BackColor = CorLinhaPar;
                    else
                        Tabela[x, y].BackColor = CorLinhaImpar;
                    Tabela[x, 0].Font = Font;
                    Panel.Controls.Add(Tabela[x, y]);
                }
            }
        }

        /// <summary>
        /// Lê e escreve na tabela
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public string this[int x, int y]
        {
            get { return Tabela[x, y + 1].Text; }
            set { Tabela[x, y + 1].Text = value; }
        }


        //Marca coluna como sendo apenas para leitura
        public void ApenasLer(int y)
        {
            for (int i = 0; i <= linhas; ++i)
            {
                Tabela[y, i].ReadOnly = true;
            }
        }


        public int Top
        {
            set { Panel.Top = value; }
            get { return Panel.Top; }
        }

        public int Left
        {
            set { Panel.Left = value; }
            get { return Panel.Left; }
        }

        public int Width
        {
            set { Panel.Width = value; }
            get { return Panel.Width; }
        }

        public int Height
        {
            set { Panel.Height = value; }
            get { return Panel.Height; }
        }


    }



}
