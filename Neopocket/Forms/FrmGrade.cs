using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de exibição de dados
    /// </summary>
    public partial class FrmGrade : FormBase
    {
        #region [ Atributos ]

        private Produto produto;
        private DataTable dtGrade;
        private NeoGrade gradeItem;

        #endregion

        #region [ Construtor ]

        public FrmGrade(Produto _produto, DataTable _dtGrade)
        {
            InitializeComponent();
            produto = _produto;
            dtGrade = _dtGrade;
        }

        #endregion

        #region [ Load ]

        private void FrmGrade_Load(object sender, EventArgs e)
        {
            CarregarGradeItem();
            lblQtd.Text = "Quantidade total do pedido: " + produto.QuantidadeRequerida;
        }

        #endregion

        #region [ Cancelamento ]

        private void mnuCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja realmente cancelar o cadastro?", "Neo", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
                this.Close();
        }

        #endregion

        #region [ Carregamento das informações ]

        protected void CarregarGradeItem()
        {
            try
            {
                if (dtGrade.Rows.Count < 1)
                {
                    MessageBox.Show("Não encontrei nenhum item em estoque para esta grade!");
                    Close();
                    return;
                }
                int quantidadeOriginal;
                List<string> nomeColunas = new List<string>();
                nomeColunas.Add(((string)dtGrade.Rows[0]["atributo_descricao"]).ToLower());
                nomeColunas.Add("tam");
                nomeColunas.Add("estoque");
                nomeColunas.Add("qtd");
                gradeItem = new NeoGrade(nomeColunas, dtGrade.Rows.Count, 220, 185);
                gradeItem.Panel.BackColor = Color.AliceBlue;
                gradeItem.Top = 80;
                gradeItem.Left = 10;
                gradeItem.ApenasLer(0);
                gradeItem.ApenasLer(1);
                gradeItem.ApenasLer(2);
                Controls.Add(gradeItem.Panel);

                for (int y = 0; y < dtGrade.Rows.Count; ++y)
                {
                    gradeItem[0, y] = Convert.ToString(dtGrade.Rows[y]["item_atributo_descricao"]);
                    gradeItem[1, y] = Convert.ToString(dtGrade.Rows[y]["item_grade_descricao"]);
                    gradeItem[2, y] = Convert.ToString((int)dtGrade.Rows[y]["estoque"]);
                    if (D.Acao == D.AcaoEnum.PedidoEdicao && produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAlterar)
                    {
                        //Recupera a quantidade do estoque
                        quantidadeOriginal = 0;
                        foreach (Grade.GradeItem gi in produto.Grade.LstGradeItem)
                        {
                            if (gi.IdItemGrade == (int)dtGrade.Rows[y]["item_grade"])
                            {
                                quantidadeOriginal = gi.Quantidade;
                            }
                        }
                        //               
                        //                        string sqlPedidoOriginalQuantidade = @"
                        //                        select
                        //                                quantidade
                        //                        from
                        //                                item_pedido_grade
                        //                        where 
                        //                                id_pedido=" + Globals.Pedido.Id + " and id_produto=" + produto.Id +
                        //                                                @" and id_grade = " + produto.IdGrade.V +
                        //                                                @" and id_item_grade = " + dtGrade.Rows[y]["item_grade"] +
                        //                                                @" and id_atributo = " + dtGrade.Rows[y]["atributo"] +
                        //                                                @" and id_item_atributo = " + dtGrade.Rows[y]["item_atributo"];

                        //                       rdCarga = Globals.Bd.Linha(sqlPedidoOriginalQuantidade);
                        //                       quantidadeOriginal = Bd.IntN(rdCarga, "id_produto");
                        //                       if (quantidadeOriginal>0){

                        // gradeItem[3, y] = produto.Grade.LstGradeItem[y].Quantidade.ToString();
                        gradeItem[3, y] = quantidadeOriginal.ToString();
                        gradeItem[2, y] = Convert.ToString((int)dtGrade.Rows[y]["estoque"] + quantidadeOriginal);
                    }
                    else
                        gradeItem[3, y] = "";
                }

            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion

        #region [ Focus do campo de quantidade ]

        private void txtQtd_GotFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = true;
        }

        private void txtQtd_LostFocus(object sender, EventArgs e)
        {
            inputPanel.Enabled = false;
        }

        #endregion

        #region [ Salvar ]

        private void mnuSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                inputPanel.Enabled = false;

                int ItemGradeQuantidade = 0;
                int y = 0;

                try
                {
                    for (; y < dtGrade.Rows.Count; ++y)
                    {
                        if (gradeItem[3, y] != "")
                            ItemGradeQuantidade += Int32.Parse(gradeItem[3, y], D.CultureInfoBRA);
                    }
                }
                catch
                {
                    MessageBox.Show("Quantidade inválida na linha " + (y + 1));
                }

                if (ItemGradeQuantidade < produto.QuantidadeRequerida)
                {
                    if ((produto.QuantidadeRequerida - ItemGradeQuantidade) == 1)
                    {
                        MessageBox.Show("Falta 1 item no pedido.", "Neo");
                    }
                    else
                    {
                        MessageBox.Show("Faltam " + (produto.QuantidadeRequerida - ItemGradeQuantidade) + " itens a serem pedidos.", "Neo");
                    }
                    return;
                }
                if (ItemGradeQuantidade > produto.QuantidadeRequerida)
                {
                    if ((ItemGradeQuantidade - produto.QuantidadeRequerida) == 1)
                    {

                        MessageBox.Show("Você excedeu 1 item no pedido.", "Neo");

                    }
                    else
                    {
                        MessageBox.Show("Você excedeu " + (ItemGradeQuantidade - produto.QuantidadeRequerida) + " ítens no pedidos.", "Neo");
                    }
                    return;
                }
                Grade grd = new Grade(produto);
                produto.Grade = grd;
                if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                    D.Pedido.LstItem.Add(produto);
                if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAlterar)
                    grd.LstGradeItem.Clear();
                for (y = 0; y < dtGrade.Rows.Count; ++y)
                {
                    if (gradeItem[3, y] != "")
                        grd.GuardaLista((int)dtGrade.Rows[y]["item_atributo"], (int)dtGrade.Rows[y]["item_grade"], (int)dtGrade.Rows[y]["atributo"], (int)dtGrade.Rows[y]["id_grade"], Convert.ToInt32(gradeItem[3, y]));
                }

                Close();
            }
            catch (Exception ex)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.APP_LOG_EXCEPTIONFILENAME, ex.Message, true);
                FE.Show(ex);
            }
        }

        #endregion
    }
}
