using System;
using System.Data.SqlServerCe;
using Neopocket.Forms;
using Neopocket.Utils;


namespace Neopocket.Core
{
    public class Produto : ICloneable
    {
        public int Id; // = id_produto
        private int quantidadeRequerida; // Número de itens pedidos
        private int quantidadeRequeridaNoPedidoFechado; // Número de itens pedidos quando foi feito pedido,
        //não altera durante a edição de um pedido que foi carregado, apenas é atualizado quando o pedido é carregado
        public Pedido Pedido;
        public Grade Grade;
        //Chamado puro porque é o preço sem desconto
        private double precoPuro; // Indica o valor do preço puro que pode ser qq EnumPrecoTipo
        private double precoUtilizado; // Preço puro adicionando do desconto
        private double descontoPerc; // Desconto representa o valor da porcentagem do desconto selecionado 
        private double? precoMax, precoMin; // O preço utilizado + acrescim / - desconto
        public string Nome, Referencia, UnidadeVenda;
        public EnumAcaoProduto AcaoProduto= EnumAcaoProduto.ItemAdicionar;
        public EnumPreco PrecoTipo = EnumPreco.Indefinido;
        private double unidadeFator;

        public double UnidadeFator
        {
            get {
                if (unidadeFator > 0)
                    return unidadeFator;
                else
                    return 1;
                 }
        }

        public enum EnumAcaoProduto : byte
        {
            ItemAdicionar,
            ItemAlterar,
            ItemExcluir,
        }
 
        public enum EnumPreco : byte 
        {
            Indefinido,
            ProdutoPrecoBase, 
            ProdutoPromocao,
            TabelaPrecoQtd1,
            TabelaPrecoQtd2,
            TabelaPrecoQtd3,
            TabelaPrecoQtd4,
        }

        //Campos que poder ter o valor = nulo!
        public IntN IdGrade, estoque, IdItemPedido;
        private DoubleN precoBase, precoPromocao; 
        public DoubleN DescontoPercMaximo, AcrescimoPercMaximo;
        public DateTimeN DtPromocaoInicio, DtPromocaoFinal;
        public BoolN VendaFracionada;
        public IntN[] ArQuantidadeMinima = new IntN[4];
        public DoubleN[] ArPreco = new DoubleN[4];

        public bool CarrregouPedidoGravado = false;
        public char DescontoTipoBd; // Pode ser tanto $ para um valor específico ou % para um porcentagem no desconto
        private bool produtoNaoConstaNaTabelaPrecoQuandoUsandoTabelaPreco = false;
     
        public Produto(Pedido pd)
        {
            PrecoTipo = EnumPreco.Indefinido;
            Pedido = pd;
        }
        
        //Usado na carga de pedidos
        public Produto(Pedido pd, int _quantidade, double _valorUnitario, double _desconto) : this(pd)
        {
            CarrregouPedidoGravado = true;
            quantidadeRequerida = _quantidade;
            quantidadeRequeridaNoPedidoFechado = _quantidade;
            precoUtilizado = _valorUnitario;
            descontoPerc = _desconto;
        }

        object ICloneable.Clone()
        {
            // make memberwise copy
            return this.MemberwiseClone();
        }

        public double PrecoPuro
        {
            get { return precoPuro; }
        }

        public void CarregarProduto(int id){
            Id = id;

            //Tem que colocar transação para não dar falta de memória
            SqlCeTransaction bdTrans = null;
            bdTrans = D.Bd.Con.BeginTransaction();
            string sqlCarga = @"Select * from produto where id_produto=" + Id;
            SqlCeDataReader rdCarga = D.Bd.Linha(sqlCarga, bdTrans);
            Nome = Convert.ToString(rdCarga["nome"]);
            Referencia = Convert.ToString(rdCarga["referencia"]);
            UnidadeVenda = Convert.ToString(rdCarga["id_unidade_venda"]);
            IdGrade= Bd.IntN(rdCarga, "id_grade");
            VendaFracionada = Bd.BoolN(rdCarga,"venda_fracionada");
            precoBase = Bd.DoubleN(rdCarga,"preco_venda");
            precoPromocao = Bd.DoubleN(rdCarga,"preco_promocao");
            DtPromocaoFinal = Bd.DateTimeN(rdCarga,"promocao_data_final");
            DtPromocaoInicio = Bd.DateTimeN(rdCarga, "promocao_data_inicio");
            estoque = Bd.IntN(rdCarga, "estoque");
            unidadeFator = Convert.ToDouble(rdCarga["unidade_fator"]);

            rdCarga.Close();
            bdTrans.Commit();

            string qry;
            try
            {
                bdTrans = null;
                bdTrans = D.Bd.Con.BeginTransaction();

                if (Pedido.IdTabelaPreco != 0)
                {
                    qry = @"select count(*) from 
                                item_tabela_preco
                             where 
                                id_tabela_preco=" + Pedido.IdTabelaPreco +
                                    " and id_produto=" + Id;
                    if (D.Bd.I(qry, bdTrans) > 0)
                    {
                        sqlCarga = @"Select * from
                                        item_tabela_preco
                                     where 
                                        id_tabela_preco=" + Pedido.IdTabelaPreco +
                                        " and id_produto=" + Id;
                        rdCarga = D.Bd.Linha(sqlCarga, bdTrans);
                        DescontoTipoBd = Convert.ToChar(rdCarga["tipo_valor"]);

                        for (int i = 1; i <= 4; ++i)
                        {
                            ArQuantidadeMinima[i - 1] = Bd.IntN(rdCarga, "qtd_minima" + i);
                            ArPreco[i - 1] = Bd.DoubleN(rdCarga, "valor" + i);
                        }
                        DescontoPercMaximo = Bd.DoubleN(rdCarga, "desconto_maximo");
                        AcrescimoPercMaximo = Bd.DoubleN(rdCarga, "acrescimo_maximo");
                    }
                    else //Não encontrou o produto na tabela de preço então pega o ajuste geral da tabela de preço
                    {
                        rdCarga = D.Bd.Linha(@"
                        SELECT 
                                AJUSTE_PERCENTUAL, TIPO 
                        FROM 
                                TABELA_PRECO
                        WHERE
                                ID_TABELA_PRECO=" + Pedido.IdTabelaPreco, bdTrans);
                        //Acrescentar valor no preço
                        double ajuste;
                        if (rdCarga[0] is DBNull)
                            ajuste = 0;
                        else
                            ajuste = Convert.ToDouble(rdCarga[0]);
                        if (rdCarga[1].ToString().ToUpper() == "A") // Acrescimo no preço
                            precoBase.V = precoBase.V + precoBase.V * ajuste / 100.00;
                        else //Dar desconto
                            precoBase.V = precoBase.V - precoBase.V * ajuste / 100.00;
                        produtoNaoConstaNaTabelaPrecoQuandoUsandoTabelaPreco = true;
                        DescontoPercMaximo.V = 0;
                        AcrescimoPercMaximo.V = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Pedido.IdTabelaPreco = 0;
                PrecosCalcularEGuardar();
                FE.Show("Não foi possível encontrar um preço para esse produto na tabela selecionada, irei ignorar a tabela de preço e usar o preço base", "Neo", ex.Message);
            }
            finally
            {
                rdCarga.Close();
                bdTrans.Commit();
            }

            if (Pedido.IdTabelaPreco == 0)
            {
                PrecoTipo = EnumPreco.ProdutoPrecoBase;
                DescontoPercMaximo.V = 0;
                AcrescimoPercMaximo.V = 0;
                if (DtPromocaoFinal.Iniciada && DtPromocaoInicio.Iniciada && precoPromocao.Iniciada)
                {
                    // Está na promoção
                    if (Pedido.Data >= DtPromocaoInicio.V && Pedido.Data <= DtPromocaoFinal.V)
                    {
                        PrecoTipo = EnumPreco.ProdutoPromocao;
                    }
                    else // Não está na promoção
                    {
                        PrecoTipo = EnumPreco.ProdutoPrecoBase;
                    }
                }
            }
            PrecosCalcularEGuardar();
        }

        //Calcula e armazena os preços puros e com desconto
        public void PrecosCalcularEGuardar()
        {
            precoPuroCalcularEGuardar();
            if(DescontoPercMaximo.Iniciada)
                precoMin = precoPuro - (precoPuro * DescontoPercMaximo.V / 100);
            if(AcrescimoPercMaximo.Iniciada)
                precoMax = precoPuro + (precoPuro * AcrescimoPercMaximo.V / 100);
            precoUtilizadoCalcularEGuardar();
            
        }

        /// <summary>
        /// Precisa ser chamada após gravar o preço para colocar o desconto
        /// </summary>
        private void precoUtilizadoCalcularEGuardar()
        {
            //Não dá desconto no preço se a quantidade for zero ou se o desconto for zero
            if (QuantidadeRequerida <= 0 || descontoPerc == 0)
                return;
            precoUtilizado *= (100 - descontoPerc) / 100;
        }

        // Irá depender da quantitade, da data e da dataPromocao, neste caso o preço é variavel dependente
        private void precoPuroCalcularEGuardar()
        {
            //Não tem tabela de preço
            if (Pedido.IdTabelaPreco == 0)
            {
                PrecoTipo = EnumPreco.ProdutoPrecoBase;

                //Checa promocao
                if (DtPromocaoFinal.Iniciada && DtPromocaoInicio.Iniciada && precoPromocao.Iniciada)
                {
                    // Tá na promoção
                    if (DateTime.Today >= DtPromocaoInicio.V && DateTime.Today <= DtPromocaoFinal.V)
                    {
                        PrecoTipo = EnumPreco.ProdutoPromocao;
                        precoUtilizado = precoPromocao.V;
                    }
                    else//Nao está na promoção
                    {
                        PrecoTipo = EnumPreco.ProdutoPrecoBase;
                        precoUtilizado = precoBase.V;
                    }
                }
                else//Não está na promoção
                {
                    PrecoTipo = EnumPreco.ProdutoPrecoBase;
                    precoUtilizado = precoBase.V;
                }
            }
            else // Tem tabela de preço
            {
                if (produtoNaoConstaNaTabelaPrecoQuandoUsandoTabelaPreco)
                {
                    precoUtilizado = precoBase.V;
                    return;
                }
                try
                {
                    bool achouFaixaDeQuantidade = false;
                    for (int i = 3; i >= 0; --i)
                    {
                        if (ArQuantidadeMinima[i].Iniciada && ArPreco[i].Iniciada)
                            if (quantidadeRequerida >= ArQuantidadeMinima[i].V && ArQuantidadeMinima[i].V > 0)
                            {
                                switch (i){
                                    case 0:
                                        PrecoTipo = EnumPreco.TabelaPrecoQtd1;
                                        break;
                                    case 1:
                                        PrecoTipo = EnumPreco.TabelaPrecoQtd2;
                                        break;
                                    case 2:
                                        PrecoTipo = EnumPreco.TabelaPrecoQtd3;
                                        break;
                                    case 3:
                                        PrecoTipo = EnumPreco.TabelaPrecoQtd4;
                                        break;
                                    }
                                precoUtilizado = ArPreco[i].V;
                                achouFaixaDeQuantidade = true;
                                break;
                            }
                    }
                    if (achouFaixaDeQuantidade == false)
                    {
                        PrecoTipo = EnumPreco.ProdutoPrecoBase;
                        precoUtilizado = precoBase.V;
                    }
                }
                catch
                {
                    //Pega o preço base ou promoção caso não encontre

                    //Checa promoção
                    if (DtPromocaoFinal.Iniciada && DtPromocaoInicio.Iniciada && precoPromocao.Iniciada)
                    {
                        // Tá na promoção
                        if (DateTime.Today >= DtPromocaoInicio.V && DateTime.Today <= DtPromocaoFinal.V)
                        {
                            PrecoTipo = EnumPreco.ProdutoPromocao;
                            precoUtilizado = precoPromocao.V;
                        }
                        else//Não está na promoção
                        {
                            PrecoTipo = EnumPreco.ProdutoPrecoBase;
                            precoUtilizado = precoBase.V;
                        }
                    }
                    else//Nao está na promoção
                    {
                        precoUtilizado = precoBase.V;
                    }
                    precoPuro = precoUtilizado;
                }
            }
            precoPuro = precoUtilizado;
        }

        public double PrecoVendaTotal{
            get{
                return (quantidadeRequerida * precoUtilizado); 
            }   
        }

        //Informa se um produto já está na lista de produtos carregados, se estiver marcado para deleção não vale
        private bool itemConstaLogicamente()
        {
            if (this.Pedido.LstItem.Contains(this) && AcaoProduto != EnumAcaoProduto.ItemExcluir)
                return true;
            else
                return false;
        }

        public double CreditoRestante
        {
            get
            {
                if (D.Acao != D.AcaoEnum.PedidoEdicao)
                    return D.Pedido.CreditoRestante - PrecoVendaTotal;
                else
                {
                    //Verifica se o item ja consta no pedido e estorna o crédito
                    if (itemConstaLogicamente())
                    {
                        foreach (Produto original in Pedido.ItemCarregadosLst)
                        {
                            if (original.Id == this.Id)
                            {
                                return D.Pedido.CreditoRestante + original.PrecoVendaTotal - PrecoVendaTotal;
                            }
                        }
                        //Não encontrou    
                        return D.Pedido.CreditoRestante - PrecoVendaTotal;
                    }else
                        return D.Pedido.CreditoRestante - PrecoVendaTotal;
                }
            }
        }

        public double PrecoUtilizado
        {
            get { return precoUtilizado; }
            set
            {
                if (precoUtilizado == value)
                    return;
                if(value < 0)
                    throw new Exception("Não é permitido preço negativo");
                //Caso o usuário tente digitar o preço manualmente, então deverá ver se é permitdo usar esse novo valor
                if (Pedido.IdTabelaPreco == 0)
                    throw new Exception("Não é permitido alterar um produto caso uma tabela de preço não seja escolhida");
                if (QuantidadeRequerida == 0 && value != 0)
                    throw new Exception("Favor selecionar a quantidade antes de definir o preço");

                descontoPerc = 0;
                //Reseta o preço caso ja tivesse dado algum desconto antes
                PrecosCalcularEGuardar();
                //Se tentar reduzir o preço
                if (value < precoUtilizado)
                {
                    if (DescontoPercMaximo.Iniciada == false)
                        throw new Exception("Não foi possível encontrar o desconto máximo desse produto");

                    if (value < precoMin)
                    {
                        //throw new Exception("Preço inferior ao preço mínimo, que é " + precoMinimo.ToString("C"));
                        precoUtilizado = precoMin.Value;
                        descontoPerc = DescontoPercMaximo.V;
                    }
                    else
                    {
                        descontoPerc = 100 * (precoPuro - value) / precoPuro;
                        precoUtilizado = value;
                    }
                }
                else //Se tentar aumentar o preço
                {
                    if (AcrescimoPercMaximo.Iniciada == false)
                        throw new Exception("Não foi possível encontrar o acréscimo máximo desse produto");

                    if (value > precoMax)
                    {
                        //throw new Exception("Preço superior ao preço máximo que é " + precoMaximo.ToString("C"));
                        precoUtilizado = precoMax.Value;
                        descontoPerc = -1 * AcrescimoPercMaximo.V;
                    }
                    else
                    {
                        descontoPerc = -100 * (precoPuro - value) / precoPuro;
                        precoUtilizado = value;
                    }
                }
                //Remove o desconto do pedido para evitar que seja dado um desconto superior ao desconto máximo
                if (Pedido.Desconto > 0)
                    Pedido.desconto = 0;
                 descontoPerc = Math.Round(descontoPerc, 2);
            }
        }

        public int Estoque {
            get
            {
                if (estoque.Iniciada)
                {
                    if (D.Acao == D.AcaoEnum.PedidoEdicao || D.Acao == D.AcaoEnum.RelatorioVer)
                    {
                        return estoque.V + quantidadeRequeridaNoPedidoFechado;
                    }
                    else
                    {
                        return estoque.V;
                    }
                }
                else
                    return 0;
            } 
        }

        /// <summary>
        /// O desconto sempre vem 5 = 5%, 80 = 80% etc... 
        /// </summary>
        public double Desconto
        {
            get{ return descontoPerc; }
            set
            {
                //Não faça nada!!!
                if (value == descontoPerc)
                         return;

                if (QuantidadeRequerida == 0 && value != 0)
                    throw new Exception("Favor selecionar a quantidade antes de aplicar o desconto");

                if (Pedido.IdTabelaPreco == 0 && value != 0)
                    throw new Exception("Você não tem permissão dar desconto caso não seja selecionada uma tabela de preço");

                if (DescontoPercMaximo.Iniciada == false && value > 0)
                    throw new Exception("Não posso dar desconto nesse produto com essa tabela de preço");

                if (AcrescimoPercMaximo.Iniciada == false && value < 0)
                    throw new Exception("Não posso dar acréscimo nesse produto com essa tabela de preço");

                if (value >= 100 && Pedido.IdTabelaPreco != 0)
                        throw new Exception("Desconto não pode ser igual ou superior a 100%");

                //Desconto
                if (value >= 0)
                {
                    //Desconto superior ao máximo
                    if (value > DescontoPercMaximo.V)
                        descontoPerc = DescontoPercMaximo.V;
                    else
                        descontoPerc = value;
                }
                else// Acréscimo
                {
                    if (-value > AcrescimoPercMaximo.V)
                        descontoPerc = -AcrescimoPercMaximo.V;
                    else
                        descontoPerc = value;
                }
                //Reseta o desconto do pedido para evitar que seja dado um desconto superior ao desconto máximo
                descontoPerc = Math.Round(descontoPerc, 2);
                Pedido.desconto = 0;
                PrecosCalcularEGuardar();
            }
        }

        public int QuantidadeRequerida
        {
            get { return quantidadeRequerida; }
            set
            {
                if (quantidadeRequerida == value)
                    return;
                int quantidadeRequeridaOriginal = quantidadeRequerida;
                if (value < 0)
                    throw new Exception("Quantidade deve ser maior ou igual a zero");
                else
                    if (!Parametro.VenderSemEstoque && Estoque < value)
                        throw new Exception("Quantidade requerida maior que estoque");
                quantidadeRequerida = value;
                PrecosCalcularEGuardar();
                if (value > 0)
                {
                    try
                    {
                        VerificaItemPermitido();
                    }
                    catch (Exception ex)
                    {
                        quantidadeRequerida = quantidadeRequeridaOriginal;
                        throw new Exception(ex.Message);
                    }
                }
                //Reseta o desconto do pedido para evitar que seja dado um desconto superior ao desconto máximo
                Pedido.desconto = 0;
            }
        }


        /// <summary>
        /// Verifica sem tem crédito levando em conta até mesmo o parâmetro Parametro.VerificarCreditoVendaAPrazo
        /// </summary>
        /// <returns></returns>
        public bool CreditoDisponivelBool
        {
            get
            {
                if (!Parametro.VerificarCreditoVendaAPrazo)
                    return true;
                if ((D.Cliente.ListaNegra || CreditoRestante <= 0) && D.Pedido.EspecieFinanceiraVerificaCredito)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// Verifica se pode comprar a prazo, se estver na lista negra ja devolve false
        /// Retorna true se tiver extrapolado o limite de credito para venda a prazo, falta checar se a venda é a prazo
        /// </summary>
        /// <returns></returns>
        private void VerificaItemPermitido()
        {
            if (D.Cliente.ListaNegra && !CreditoDisponivelBool)
                throw new Exception("Cliente está na lista negra e essa compra requer crédito");
            if (CreditoRestante <= 0 && !CreditoDisponivelBool)
                throw new Exception("Valor extrapola o limite de crédito");
        }
   }

}
