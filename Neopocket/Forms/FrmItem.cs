using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;

namespace Neopocket.Forms
{
    public partial class FrmItem : FormBase
    {
        private Produto produto;
        public DataTable dtGrade;
        public static double DiferencaLimiteCredito;
        private int quantidadeDefinida;
        private double descontoDefinido, valorPedidoMenosProdutoSendoEditado;
      
       
        public FrmItem()
        {
            InitializeComponent();
        }

        public FrmItem(Produto p) : this()
        {
            produto = p;
            valorPedidoMenosProdutoSendoEditado = D.Pedido.Valor - p.PrecoVendaTotal;
        }

        private bool iuHabilitada
        {
            set
            {
                btnQtdMais.Enabled = value;
                btnQtdMenos.Enabled = value;
                btnDescontoMais.Enabled = value;
                btnDescontoMenos.Enabled = value;
                mnuIncluir.Enabled = value;
                mnuVoltar.Enabled = value;
                txtPercentualAjuste.Enabled = value;
                txtQtd.Enabled = value;
                txtPreco.Enabled = value;
            }
        }

        private void FrmItem_Load(object sender, EventArgs e)
        {

            foreach (Produto prod in D.Pedido.LstItem)
            {
                if (produto.Id ==  prod.Id && prod.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                {
                    MessageBox.Show("Item já presente no pedido, utilize a opção de editar o item para modificar sua quantidade");
                    Close();
                    return;
                }
            }

            if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                produto.QuantidadeRequerida = 0;
            txtCodProduto.Text = produto.Id.ToString();
            txtNomeProduto.Text = produto.Nome.ToString();
            txtEstoque.Text = produto.Estoque.ToString();
            txtReferencia.Text = produto.Referencia.ToString();
            if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAlterar)
            {
                quantidadeDefinida = produto.QuantidadeRequerida;
                descontoDefinido = produto.Desconto;
            }
            telaAtualizar();
        }

        private void telaAtualizar()
        {
            produto.PrecosCalcularEGuardar();
            txtPreco.Text = Fcn.DinheiroFormataSemCifrao(produto.PrecoUtilizado);
            txtPrecoUnitario.Text = Fcn.DinheiroFormata(produto.PrecoUtilizado / produto.UnidadeFator);
            txtQtd.Text = produto.QuantidadeRequerida.ToString();
            txtPercentualAjuste.Text = produto.Desconto.ToString();
            txtTotal.Text = Fcn.DinheiroFormata(produto.PrecoVendaTotal);
            //Atualiza o total o pedido para facilitar a vida do vendedor
            if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                txtPedidoTotal.Text = Fcn.DinheiroFormata(produto.PrecoVendaTotal + D.Pedido.Valor);
            else
                txtPedidoTotal.Text = Fcn.DinheiroFormata(valorPedidoMenosProdutoSendoEditado + produto.PrecoVendaTotal);
            if (produto.CreditoRestante <= 0 && D.Pedido.EspecieFinanceiraVerificaCredito && Parametro.VerificarCreditoVendaAPrazo)
            {
                MessageBox.Show("Excedeu limite de crédito!", "Neo");
                btnQtdMais.Enabled = false;
            }
            if (!Parametro.VerificarCreditoVendaAPrazo || ! D.Pedido.EspecieFinanceiraVerificaCredito)
                txtCreditoRestante.Text = "ilimitado";
            else
                txtCreditoRestante.Text = Fcn.DinheiroFormata(produto.CreditoRestante);
        }

        private void btnDescontoMais_Click(object sender, EventArgs e)
        {
            try
            {
                iuHabilitada = false;
                produto.Desconto++;
                telaAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neo");
            }
            finally
            {
                iuHabilitada = true;
            }
        }

        private void btnDescontoMenos_Click(object sender, EventArgs e)
        {
            try
            {
                iuHabilitada = false;
                produto.Desconto--;
                telaAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neo");
            }
            finally
            {
                iuHabilitada = true;
            }
        }

        private void btnQtdMais_Click(object sender, EventArgs e)
        {
            try
            {
                iuHabilitada = false;
                produto.QuantidadeRequerida++;                               

                telaAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neo");
            }
            finally
            {
                iuHabilitada = true;
            }
        }

        private void btnQtdMenos_Click(object sender, EventArgs e)
        {
            try
            {
                iuHabilitada = false;
                produto.QuantidadeRequerida--;
               
                telaAtualizar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Neo");
            }
            finally
            {
                iuHabilitada = true;
            }
        }

        private void mnuVoltar_Click(object sender, EventArgs e)
        {
           
            if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAlterar)
            {
                produto.QuantidadeRequerida = quantidadeDefinida;
                produto.Desconto = descontoDefinido;
            }

                FrmPedido f = new FrmPedido();
                Close();
            
        }
        
        private void txtPercentualAjuste_Validating(object sender, CancelEventArgs e)
        {
            double p = 0;

            try
            {
                p = Double.Parse(((TextBox)sender).Text, D.CultureInfoBRA);
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("Desconto inválido", "Neo");
            }
            try
            {
                produto.Desconto = p;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message, "Neo");
            }
            telaAtualizar();
        }

        private void txtQtd_Validating(object sender, CancelEventArgs e)
        {
            int q=0;

            try
            {
                q = Int32.Parse(((TextBox)sender).Text, D.CultureInfoBRA);
            }catch{
                    e.Cancel = true;
                    MessageBox.Show("Quantidade inválida", "Neo");
            }
            try
            {
                produto.QuantidadeRequerida = q;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message, "Neo");
            }
            telaAtualizar();
        }

        private void mnuIncluir_Click(object sender, EventArgs e)
        {
            foreach (Produto prod in D.Pedido.LstItem)
            {
                if (produto.Id == prod.Id && prod.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                {
                    MessageBox.Show("Item já presente no pedido, utilize a opção de editar o item para modificar sua quantidade");
                    Close();
                    return;
                }
            }

            //Apenas para forçar a chamada do invalidate do desconto e da quantidade
            txtCodProduto.Focus();

            if (produto.PrecoVendaTotal == 0)
            {
                MessageBox.Show("Item não pode ter preço 0");
                return;
            }


            if (produto.QuantidadeRequerida < 1)
            {
                MessageBox.Show("É necessário que se peça pelo menos um item no pedido.", "Neo");
                return;
            }
            if (produto.IdGrade.Iniciada)
            {

                string sqlSelGradeDado = @"
                SELECT        
                        saldo_grade.estoque,
                        item_grade.descricao AS item_grade_descricao,
                        item_atributo.descricao AS item_atributo_descricao, 
                        atributo.descricao AS atributo_descricao,
                        saldo_grade.id_produto,
                        saldo_grade.id_grade,
                        item_atributo.id_atributo,
                        grade.descricao AS grade_descricao,
                        item_atributo.id_item_atributo,
                        saldo_grade.id_item_grade AS item_grade,
                        saldo_grade.id_atributo AS atributo, 
                        saldo_grade.id_item_atributo AS item_atributo
                FROM
                    saldo_grade INNER JOIN
                         item_grade ON saldo_grade.id_item_grade = item_grade.id_item_grade AND saldo_grade.id_grade = item_grade.id_grade INNER JOIN
                         atributo ON saldo_grade.id_atributo = atributo.id_atributo INNER JOIN
                         item_atributo ON saldo_grade.id_item_atributo = item_atributo.id_item_atributo AND saldo_grade.id_atributo = item_atributo.id_atributo INNER JOIN
                         grade ON saldo_grade.id_grade = grade.id_grade
                WHERE (saldo_grade.id_produto = " + produto.Id + ")";
                dtGrade = D.Bd.DataTablePreenche(sqlSelGradeDado);
                if (dtGrade.Rows.Count < 1)
                {                    
                    MessageBox.Show("Não encontrei nenhum item em estoque para esta grade", "Neo");
                    Close();
                    return;
                }
                else// Pedido com grade
                {
                    FrmGrade f = new FrmGrade(produto, dtGrade);
                    f.ShowDialog();
                    FrmPedido.IsProductAdded = true;
                    Close();
                }
            }
            else//Pedido Sem grade
            {
                if (produto.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar)
                    D.Pedido.LstItem.Add(produto);
                FrmPedido.IsProductAdded = true;
                Close();
            }
        }

        private void txtPrecoUnitario_Validating(object sender, CancelEventArgs e)
        {
            double q = 0;

            iuHabilitada = false;
            try
            {
                q = Double.Parse(((TextBox)sender).Text, D.CultureInfoBRA);
            }
            catch
            {
                e.Cancel = true;
                MessageBox.Show("preço inválido", "Neo");
            }

            try
            {
                produto.PrecoUtilizado = q;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show(ex.Message, "Neo");
            }
            telaAtualizar();
            iuHabilitada = true;

        }
    }
}
