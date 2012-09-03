using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using Neopocket.Forms;
using Neopocket.Utils;
using System.Text;

namespace Neopocket.Core
{


    /// <summary>
    /// 
    /// </summary>
    public class Pedido
    {
        public Guid Id; // = IdPedidoPocket
        public List<Produto> LstItem = new List<Produto>();
        // Não serão modificados, usando apenas para recalcular o limite de crédito
        // caso da edição do pedido
        public List<Produto> ItemCarregadosLst = new List<Produto>();
        public static Guid IdClientePocket;
        public static int IdClienteStore;
        public int IdFuncionario;
        //Status do pedido pode ser (N)ovo, ou (S)incronizado
        public string Status;
        private string observacao = "";
        private double valorBd; //Usado quando se carrega um pedido que já foi editado, nunca é alterado em outros locais  

        // desconto É public mas pela interface se deve usar o pedido.Desconto para que ocorra as
        // validações e elas devem tratar exceções
        public double desconto;
        public DateTime Data;
        public static int PedidosRestantes;
        public static double ValorTotalPedidos;
        private double bdi;
        private bool somenteParaLeitura = false;


        private int idFormaPagamento, idEspecieFinanceira, idTabelaPreco; // Se for 0 não está usando tabela preço

        public Pedido()
        {
            Id = Guid.NewGuid();
            Data = new DateTime();
            Data = DateTime.Now;
            bdi = D.Cliente.Bdi;
        }

        public Pedido(bool somenteParaLeitura)
        {
            //Implementado para carregar um pedido usando o relatorio
            this.somenteParaLeitura = somenteParaLeitura;
        }

        public bool SomenteParaLeitura
        {
            get { return somenteParaLeitura; }
        }

        /*
         * Informação passada por Humberto:
         * 
            alterei no SP_PKT_IMPORTA_PEDIDO
            se vier com 9.99 a SP coloca zero
            mas no Pocket, se o cliente estiver no cadastro com o BDI 9,99
            significa que o vendedor não poderá mudar
            Humberto diz (11:56):
            se estiver 0.00 ele podera alterar no intervalo de 0,01 a 9,98
            Humberto diz (11:56):
            e se já estiver preenchido no cadastro do cliente, ele tambem não poderá alterar para zero ou algo diferente do que esteja no cadastro...
            Humberto diz (11:57):
            resumo:
            Evandro diz (11:57):
            vc testou aquele negócio q eu fiz da forma de pagamento padrão?
            Humberto diz (11:57):
            cliente com BDI no cadastro de 9,99 o pedido gravara um BDI = 0.00
        */
        public double Bdi
        {
            get { return bdi; }
            set
            {
                if (D.Cliente.Bdi > 0 && value != D.Cliente.Bdi)
                    throw new Exception("O valor do BDI não pode ser alterado para este cliente");
                else
                    if (value < 0 || value > 9.98)
                        throw new Exception("O BDI só pode variar entre 0 e 9,98%");
                bdi = value;
            }
        }

        public double ParcelaMinima
        {
            get {
                if (idFormaPagamento == 0)
                    return 0;
                else
                    return D.Bd.N(@"
                    Select 
                            parcela_minima
                    from
                            forma_pagamento
                    where
                            id_forma_pagamento=" + idFormaPagamento);   
                }
        }

        public int ParcelaQuantidade
        {
            get
            {
                if (idFormaPagamento == 0)
                    return 0;
                else
                    return D.Bd.I(@"
                    Select 
                            no_parcelas
                    from
                            forma_pagamento
                    where
                            id_forma_pagamento=" + idFormaPagamento);
            }
        }

        /// <summary>
        /// Retorna string vazia se estiver OK ou o erro caso tenha algum
        /// </summary>
        public string ValidadePedidoChecar()
        {
            //Verificar se a parcela mínima é aprovada
            double parcelaMinima = ParcelaMinima;
            double parcelaQuantidade = ParcelaQuantidade;

            if (parcelaQuantidade < 1)
            {
                LogBuilder.DEPRECIADO_Append(D.APP_LOGDIRECTORY + D.DEPRECIADO_APP_LOGFILENAME, "Carregando registro de todos os vendedores", true);
                return @"Quantidade de parcelas não pode ser inferior a uma";
            }

            if (parcelaMinima > 0)
            {
                if((Valor / parcelaQuantidade) < parcelaMinima)
                    return @"Parcela mínima menor que o permitido, para esta condição de pagamento. O valor do pedido deve ser de, no mínimo, " + Fcn.DinheiroFormata(parcelaMinima * parcelaQuantidade);
            }

            return String.Empty;
        }

        //Inicialmente não é permitido dar desconto negativo
        public double Desconto
        {
            get { return desconto; }
            set
            {
                if (value == 0)
                {
                    desconto = 0;
                    return;
                }
                if (D.Funcionario.DescontoMaximo <= 0)
                {
                    desconto = 0;
                    throw new Exception("Você não pode dar desconto");
                }
                if (value < 0)
                {
                    desconto = 0;
                    throw new Exception("Desconto não pode ser negativo");
                }

                if (ItensQt == 0 && value > 0) // 
                {
                    desconto = 0;
                    throw new Exception("Adicione itens antes de dar desconto");
                }

                if (ItensQt == 0 && value == 0)
                    return;

                if (value < 0)
                    throw new Exception("Valor do desconto não pode ser negativo");

                if (value >= 100)
                    throw new Exception("Valor do desconto deve ser inferior a 100%");

                double descontoPedidoMax = 0, descontoItensPedido = 0;
                double valorSemDesconto = 0, valorComDesconto = 0;
                double valorComDescontoMax = 0;

                foreach (Produto p in LstItem)
                {
                    if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                    {
                        valorSemDesconto += p.PrecoPuro * p.QuantidadeRequerida;
                        valorComDescontoMax += p.PrecoPuro * (100 - p.DescontoPercMaximo.V) * p.QuantidadeRequerida;
                     }
                }
                double DescontoMaximoDaTabela = (valorSemDesconto - valorComDescontoMax) / valorSemDesconto;

                //Necessário para evitar divisão por zero
                if (valorSemDesconto == 0)
                {
                    throw new Exception("Adicione itens ao pedido antes de dar desconto");
                }
                foreach (Produto p in LstItem)
                {
                    if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                        valorComDesconto += p.PrecoVendaTotal;
                }

                descontoItensPedido = (valorSemDesconto - valorComDesconto) / valorSemDesconto;

                //Calcular o desconto maximo permitido
                descontoPedidoMax = Math.Max(D.Funcionario.DescontoMaximo, DescontoMaximoDaTabela) - descontoItensPedido;

                if (value > descontoPedidoMax)
                {
                    desconto = descontoPedidoMax;
                    throw new Exception("Desconto final máximo para esse pedido é " + Fcn.TruncateWithDecimals(descontoPedidoMax, Parametro.ProdutoPrecoCasasDecimais) + "%, desta forma este valor será aplicado ao pedido.");
                }
                else
                    desconto = value;
            }
        }

        public int ItensQt
        {
            get
            {
                int qt = 0;
                foreach (Produto p in LstItem)
                {
                    if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                        ++qt;
                }
                return qt;
            }
        }

        public string Observacao
        {
            get { return observacao; }
            set { observacao = value.Replace(System.Environment.NewLine, ""); }
        }

        public double ValorBd
        {
            get { return valorBd; }
        }

        public int IdFormaPagamento
        {
            get { return idFormaPagamento; }
            set
            {
                //Se o valor for o mesmo, não haverá nenhuma modificação
                if (value == idFormaPagamento)
                    return;
                int idFormaPagamentoOriginal = idFormaPagamento;
                idFormaPagamento = value;
                try
                {
                    pedidoAdaptarDianteNovasCircunstancias();
                }
                catch (Exception ex)
                {
                    idFormaPagamento = idFormaPagamentoOriginal;
                    throw new Exception("Não foi possível modificar a forma de pagamento, pois " + ex.Message);
                }
            }
        }

        public int IdEspecieFinanceira
        {
            get { return idEspecieFinanceira; }
            set
            {
                if (value == idEspecieFinanceira)
                    return;
                int idEspecieFinanceiraOriginal = idEspecieFinanceira;
                idEspecieFinanceira = value;
                try
                {
                    pedidoAdaptarDianteNovasCircunstancias();
                }
                catch (Exception ex)
                {
                    idEspecieFinanceira = idEspecieFinanceiraOriginal;
                    throw new Exception("Não foi possível modificar espécie financeira, pois " + ex.Message + " ");
                }

            }
        }

        public int IdTabelaPreco
        {
            get { return idTabelaPreco; }
            set
            {
                if (idTabelaPreco == value)
                    return;
                int idTabelaPrecoOriginal = idTabelaPreco;
                idTabelaPreco = value;
                try
                {
                    pedidoAdaptarDianteNovasCircunstancias();
                }
                catch (Exception ex)
                {
                    idTabelaPreco = idTabelaPrecoOriginal;
                    throw new Exception("Não foi possível modificar tabela de preço, pois " + ex.Message + " ");
                }
            }
        }

        /// <summary>
        /// Executado depois que é feita alguma moficação no pedido, tipo outra tabela de preço
        /// Tenta manter quantidades e descontos originais, se não conseguir vai zerando a quantidade ou o desconto
        /// do produto
        /// </summary>
        private void pedidoAdaptarDianteNovasCircunstancias()
        {
            Produto p, clone;
            int c = LstItem.Count;
            //Valores desejados mas que talvez não sejam possíveis dado as novas condições 
            int quantidadeRequeridaDesejada;
            double descontoDesejado;
            bool pedidoNaoPodeSerAlterado = false;
            List<Produto> itemBackupLst = new List<Produto>();

            //Fazer backup de todos os itens caso nao seja possivel manter a quantidade zerando o desconto

            foreach (Produto it in LstItem)
            {
                clone = new Produto(D.Pedido);
                clone = (Produto)((ICloneable)it).Clone();
                itemBackupLst.Add(clone);
            }

            List<string> ExceptionMessageLst = new List<string>();
            for (int i = c - 1; i >= 0; --i)
            {
                p = LstItem[i];
                LstItem.RemoveAt(i);
                quantidadeRequeridaDesejada = p.QuantidadeRequerida;
                descontoDesejado = p.Desconto;
                p.QuantidadeRequerida = 0;
                p.Desconto = 0;
                p.CarregarProduto(p.Id);

                //Tenta aplicar tudo desejado
                try
                {
                    p.QuantidadeRequerida = quantidadeRequeridaDesejada;
                    p.Desconto = descontoDesejado;
                    D.Pedido.LstItem.Add(p);
                }
                catch
                {
                    //Tenta zerar o desconto mantendo a quantidade
                    try
                    {
                        p.QuantidadeRequerida = quantidadeRequeridaDesejada;
                        p.Desconto = 0;
                        D.Pedido.LstItem.Add(p);
                    }
                    //Desiste e guarda e msg da exceção
                    catch (Exception ex)
                    {
                        p.QuantidadeRequerida = 0;
                        p.Desconto = 0;
                        pedidoNaoPodeSerAlterado = true;
                        ExceptionMessageLst.Add("item " + p.Id + ex.Message + ", ");
                    }
                }
            }
            if (pedidoNaoPodeSerAlterado)
            {
                string Msg = "Não foi possível modificar algum ou alguns itens do pedido ";
                foreach (string s in ExceptionMessageLst)
                    Msg += s;
                Msg = Msg.Substring(0, Msg.Length - 2);
                
                //Restaurar backup
                LstItem.Clear();
                foreach (Produto it in itemBackupLst)
                {
                    LstItem.Add(it);
                }
                //Lança exceção para desfazer a atividade que estava tentando ser feita
                throw new Exception(Msg);
            }
            clone = null;
        }

        /// <summary>
        /// Inclue na lstItem e ou decrementa o estoque
        /// </summary>
        /// <param name="pt"></param>
        public void ListaInserirProduto(Produto pt)
        {
            LstItem.Add(pt);
            pt.AcaoProduto = Produto.EnumAcaoProduto.ItemAdicionar;
            pt.estoque.V -= pt.QuantidadeRequerida;
        }

        public void ListaEditarProduto(Produto pt)
        {
            pt.AcaoProduto = Produto.EnumAcaoProduto.ItemAlterar;
            pt.estoque.V -= pt.QuantidadeRequerida;
        }


        public double ValorAntesBDIeDescontoPedido
        {
            get
            {
                double total = 0;
                foreach (Produto p in LstItem)
                {
                    if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                        total += p.PrecoVendaTotal;
                }
                return total;
            }
        }

        public double Valor
        {
            get
            {
                double total = ValorAntesBDIeDescontoPedido;
                total -= total * desconto / 100;

                if (D.Cliente.Bdi == 9.99 || D.Pedido.Bdi == 9.99)
                    return total;
                else
                    total += total * bdi / 100;
                //Colocar o calculo da substituicao do ICMS aqui
                return total;
            }
        }

        //Somente para o pedido, não deve ser usado para verificar o crédito do cliente
        public double CreditoUtilizado
        {
            get
            {
                if (!EspecieFinanceiraVerificaCredito || !Parametro.VerificarCreditoVendaAPrazo)
                    return 0;
                if (D.Acao != D.AcaoEnum.PedidoEdicao)
                    return D.Cliente.CreditoUtilizado + Valor;
                else
                    return D.Cliente.CreditoUtilizado - ValorBd;
            }
        }

        public double CreditoRestante
        {
            get
            {
                if (D.Acao != D.AcaoEnum.PedidoEdicao)
                    return D.Cliente.CreditoRestante - Valor;
                else
                {
                    return D.Cliente.CreditoRestante + ValorBd - Valor;
                }
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
                if ((D.Cliente.ListaNegra || D.Pedido.CreditoRestante <= 0) && D.Pedido.EspecieFinanceiraVerificaCredito)
                    return false;
                else
                    return true;
            }
        }


        public bool EspecieFinanceiraVerificaCredito
        {
            get
            {
                if (IdEspecieFinanceira == 0)
                {
                    throw new Exception("Nenhuma especie financeira foi selecionada!");
                }

                string sql = @"SELECT  verifica_credito FROM  especie_financeira 
                                           WHERE id_especie_financeira = " + IdEspecieFinanceira;

                string verificaCredito = D.Bd.T(sql);

                if (verificaCredito == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ExecutarTarefasNoBd()//da
        {
            //Iniciar transação
            SqlCeTransaction dbTrans = null;
            dbTrans = D.Bd.Con.BeginTransaction();

            if (D.Acao == D.AcaoEnum.PedidoCadastro)
            {
                if (!inserirTabelaPedido(dbTrans))
                    return false;

                foreach (Produto p in LstItem)
                {
                    if (p.QuantidadeRequerida > 0)
                    {
                        if (p.AcaoProduto == Produto.EnumAcaoProduto.ItemAdicionar || p.AcaoProduto == Produto.EnumAcaoProduto.ItemAlterar)
                            if (!inserirItemPedido(dbTrans, p))
                                return false;
                    }
                }
            }
            else
            {
                if (D.Acao == D.AcaoEnum.PedidoEdicao)
                {
                    if (!atualizaTabelaPedido(dbTrans))
                        return false;

                    foreach (Produto p in LstItem)
                    {
                        string sqlChecaItemNoBd = @"
                        SELECT
                                pedido.id_pedido, item_pedido.id_produto
                        FROM    
                                pedido INNER JOIN
                                item_pedido ON pedido.id_pedido = item_pedido.id_pedido
                        WHERE
                                (pedido.id_pedido = '" + D.Pedido.Id + "') AND (item_pedido.id_produto = " + p.Id + ")";
                        IntN testeExistencia;

                        SqlCeDataReader rdCarga = D.Bd.Linha(sqlChecaItemNoBd);
                        testeExistencia = Bd.IntN(rdCarga, "id_produto");

                        if (testeExistencia.Iniciada)
                        {
                            if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                                p.AcaoProduto = Produto.EnumAcaoProduto.ItemAlterar;
                            else // Else Não faz nada mas deixa isso bem claro que é para Excluir
                                p.AcaoProduto = Produto.EnumAcaoProduto.ItemExcluir;
                        }
                        else
                        {
                            if (p.AcaoProduto != Produto.EnumAcaoProduto.ItemExcluir)
                                p.AcaoProduto = Produto.EnumAcaoProduto.ItemAdicionar;
                            //Caso contrário um item foi deletado da lista antes de ser inserido no 
                            //banco de dados, então nada precisa ser feito
                        }

                        switch (p.AcaoProduto)
                        {
                            case Produto.EnumAcaoProduto.ItemAdicionar:
                                if (!inserirItemPedido(dbTrans, p))
                                    return false;
                                break;
                            case Produto.EnumAcaoProduto.ItemAlterar:
                                if (!atualizaTabelaItemPedido(dbTrans, p))
                                    return false;
                                break;
                            case Produto.EnumAcaoProduto.ItemExcluir:
                                if (!ItensExcluir(dbTrans, p))
                                    return false;
                                break;
                            default:
                                FE.Show("Nenhuma ação foi definida para esse produto executar no banco de dados.", "Neo");
                                break;
                        }
                    }
                }
            }

            Rota r = new Rota();
            Int32 Status = (int)Rota.StatusEnum.Visitado;

            // Cria uma visita para o cliente nao exista
            using (SqlCeCommand cmd = new SqlCeCommand(String.Empty, D.Bd.Con))
            {
                // Verificar se existe alguma visita agendada para aquele cliente no periodo de validade e que já não esteja pendente!
                cmd.CommandText = "SELECT COUNT(*) as Total FROM visita WHERE id_cliente=@id_cliente and (data_visita >= @data_visita_ini and data_visita <= @data_visita_end)";
                cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = D.Cliente.IdStore;
                cmd.Parameters.Add("@data_visita_ini", SqlDbType.DateTime).Value = r.ValidadeInicio;
                cmd.Parameters.Add("@data_visita_end", SqlDbType.DateTime).Value = r.ValidadeFim;

                Int32 total = 0;

                try
                {
                    total = Int32.Parse(cmd.ExecuteScalar().ToString());
                }
                catch { }

                cmd.Parameters.Clear();

                StringBuilder builder = new StringBuilder();

                if (total == 0)
                {
                    builder.AppendLine("INSERT INTO visita (id_cliente, id_funcionario, status, data_visita)");
                    builder.AppendLine("VALUES");
                    builder.AppendLine("(@id_cliente, @id_funcionario, @status, @data_visita)");
                    cmd.Parameters.Add("@id_funcionario", SqlDbType.Int).Value = D.Funcionario.Id;
                    cmd.Parameters.Add("@data_visita", SqlDbType.DateTime).Value = DateTime.Now;
                }
                else
                {
                    // Partindo do pre suposto que so deixe cadastrar uma unica visita para o cliente no determinado periodo
                    builder.AppendLine("UPDATE visita SET status=@status WHERE id_cliente=@id_cliente and (data_visita >= @data_visita_ini and data_visita <= @data_visita_end)");
                    cmd.Parameters.Add("@data_visita_ini", SqlDbType.DateTime).Value = r.ValidadeInicio;
                    cmd.Parameters.Add("@data_visita_end", SqlDbType.DateTime).Value = r.ValidadeFim;
                }

                cmd.CommandText = builder.ToString();
                cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = D.Cliente.IdStore;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = Status;

                // Executa o comando
                cmd.ExecuteNonQuery();
            }

            dbTrans.Commit();
            return true;
        }

        public void Carregar(Guid id)    //da
        {
            Id = id;

            Produto clone;
            string sqlPedido = @"select * from pedido where id_pedido='" + Id + "'";
            SqlCeDataReader rdCarga = D.Bd.Linha(sqlPedido);
            IdClientePocket = (Guid)(rdCarga["id_cliente_pocket"]);
            IdClienteStore = Bd.IntZeroIfNull(rdCarga["id_cliente_store"]);
            Data = Convert.ToDateTime(rdCarga["data"]);

            try
            {
                Bdi = Convert.ToDouble(rdCarga["bdi"]);
            }
            catch { }


            IdFuncionario = Convert.ToInt32(rdCarga["id_funcionario"]);
            idTabelaPreco = Bd.IntZeroIfNull(rdCarga["id_tabela_preco"]);
            idFormaPagamento = Bd.IntZeroIfNull(rdCarga["id_forma_pagamento"]);
            string sqlEspecieFinanceira = @"SELECT     id_especie_financeira
                FROM         item_forma_pagamento
                    WHERE    id_forma_pagamento = " + IdFormaPagamento;
            idEspecieFinanceira = D.Bd.I(sqlEspecieFinanceira);

            Status = Convert.ToString(rdCarga["status"]);
            valorBd = Bd.IntZeroIfNull(rdCarga["valor"]);
            Observacao = Convert.ToString(rdCarga["observacao"]);

            int quantidade, idProduto;
            double valorUnitario, desconto;

            DataTable dtItemPedido = D.Bd.DataTablePreenche("select * from item_pedido where id_pedido ='" + Id + "'", "item_pedido");
            for (int y = 0; y < dtItemPedido.Rows.Count; ++y)
            {
                quantidade = Bd.IntZeroIfNull(dtItemPedido.Rows[y]["quantidade"]);
                valorUnitario = Bd.DoubleZeroIfNull(dtItemPedido.Rows[y]["valor_unitario"]);
                desconto = Bd.DoubleZeroIfNull(dtItemPedido.Rows[y]["desconto"]);
                idProduto = Convert.ToInt32(dtItemPedido.Rows[y]["id_produto"]);

                Produto p = new Produto(this, quantidade, valorUnitario, desconto);
                p.CarregarProduto(idProduto);
                p.PrecosCalcularEGuardar();
                //Incrementa a quantidade do item caso esteja editando
                //Isto é repoe o estoque na memória para edição
                p.estoque.V += quantidade;

                //Carregar grade
                if (p.IdGrade.Iniciada)
                {
                    p.Grade = new Grade(p);
                    p.Grade.Carregar();
                }
                LstItem.Add(p);
                clone = new Produto(D.Pedido);
                clone = (Produto)((ICloneable)p).Clone();
                ItemCarregadosLst.Add(clone);
            }
            rdCarga.Close();
            return;
        }

        private bool inserirTabelaPedido(SqlCeTransaction dbTrans)//da
        {
            int rf;

            string sqlInsertPedido = @"
            insert into
                        pedido
            (id_pedido,
             id_cliente_pocket,
             id_cliente_store,
             data,
             id_funcionario,
             id_tabela_preco,
             id_forma_pagamento,
             status,
             valor,
             observacao,
             desconto,
             bdi)
                    values ('" +
            Id + "','" +
            IdClientePocket + "'," +
            Bd.SZ(IdClienteStore) + "," +
            Bd.TimeStampParaBd(Data) + "," +
            Bd.S(D.Funcionario.Id) + "," +
            Bd.S(IdTabelaPreco) + "," +
            Bd.S(IdFormaPagamento) + "," +
            "'N'," +
            Bd.RealPtParaBd(Bd.S(Valor)) + "," +
            Bd.SN(Observacao) + "," +
            Bd.Numerico(Fcn.RoundComputing(desconto, Parametro.ProdutoPrecoCasasDecimais)) + "," +
            Bd.Numerico(bdi) +
            ")";

            rf = D.Bd.ExecuteNonQuery(sqlInsertPedido, dbTrans);
            if (rf < 1)
            {
                dbTrans.Rollback();
                return false;
            }
            //No longer required code since after Guid deployment
            //            Globals.Pedido.Id = Globals.Bd.Guid("SELECT @@IDENTITY AS UltimaIdentity", dbTrans);
            return true;
        }

        private bool inserirItemPedido(SqlCeTransaction dbTrans, Produto p)
        {
            int rf;

            //Dá baixa no estoque:
            string sqlUpdateEstoque = @"
            update 
                    produto
            set
                    estoque=estoque - " + p.QuantidadeRequerida +
            @" where  
                    id_produto =  " + p.Id;
            rf = D.Bd.ExecuteNonQuery(sqlUpdateEstoque, dbTrans);
            if (rf < 1)
            {
                dbTrans.Rollback();
                return false;
            }

            string sqlInsertItem = "";
            sqlInsertItem = @"
            insert into
                item_pedido
            (id_pedido,
             id_funcionario, 
             id_produto,
             quantidade,
             valor_unitario,
             desconto)
                    values ('" +
            D.Pedido.Id + "'," +
            D.Funcionario.Id + "," +
            p.Id + "," +
            p.QuantidadeRequerida + "," +
            Bd.RealPtParaBd(p.PrecoUtilizado.ToString()) + "," +
            Bd.Numerico(Fcn.RoundComputing(p.Desconto, Parametro.ProdutoPrecoCasasDecimais)) + ")";

            rf = D.Bd.ExecuteNonQuery(sqlInsertItem, dbTrans);
            if (rf < 1)
            {
                dbTrans.Rollback();
                return false;
            }

            //Verifica se o pedido tem grade e insere
            p.IdItemPedido.V = D.Bd.I("SELECT @@IDENTITY AS UltimaIdentity", dbTrans);

            if (p.IdGrade.Iniciada)
            {
                rf = p.Grade.Inserir(dbTrans);
                if (rf < 1)
                {
                    try
                    {
                        dbTrans.Rollback();
                    }
                    catch { }
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Não é permitido editar pedidos com status S
        /// </summary>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        private bool atualizaTabelaPedido(SqlCeTransaction dbTrans) //da
        {
            //Iniciar transação
            int rf;

            string sqlInsertPedido = @"
            update pedido
            set
            id_cliente_pocket='" + IdClientePocket + "'," +
            "id_cliente_store=" + Bd.SN(IdClienteStore) + "," +
            "data=" + Bd.TimeStampParaBd(Data) + "," +
            "id_funcionario=" + Bd.S(D.Funcionario.Id) + "," +
            "id_tabela_preco=" + Bd.S(IdTabelaPreco) + "," +
            "id_forma_pagamento=" + Bd.S(IdFormaPagamento) + "," +
            "status='N'" + "," +
            "valor=" + Bd.RealPtParaBd(Bd.S(Valor)) + "," +
            "observacao=" + Bd.SN(Observacao) + "," +
            "desconto=" + Bd.Numerico(desconto) + "," +
            "bdi=" + Bd.Numerico(bdi) +
            " where id_pedido='" + Id + "'";

            rf = D.Bd.ExecuteNonQuery(sqlInsertPedido, dbTrans);
            if (rf < 1)
            {
                dbTrans.Rollback();
                return false;
            }
            return true;
        }


        private bool atualizaTabelaItemPedido(SqlCeTransaction dbTrans, Produto p)
        {

            int rf;

            //Atualiza o estoque
            string sqlPedidoOriginalQuantidade = @"
            select
                    quantidade
            from
                    item_pedido
            where id_pedido='" + D.Pedido.Id + "' and id_produto=" + p.Id;

            int quantidadeOriginal = D.Bd.I(sqlPedidoOriginalQuantidade, dbTrans);
            string sqlUpdateEstoque = @"
            update
                    produto
            set
                    estoque=estoque + " + quantidadeOriginal + " - " + p.QuantidadeRequerida + @" 
            where 
                    id_produto = " + p.Id;
            D.Bd.ExecuteNonQuery(sqlUpdateEstoque, dbTrans);


            string sqlUpdateItem = "";
            sqlUpdateItem = @"
            update item_pedido
            set 
            quantidade=" + p.QuantidadeRequerida + "," +
            "id_funcionario=" + D.Funcionario.Id + "," +
            "valor_unitario=" + Bd.RealPtParaBd(p.PrecoUtilizado.ToString()) + "," +
            "desconto=" + p.Desconto +
            " where id_pedido='" + D.Pedido.Id + "' and id_produto=" + p.Id;

            rf = D.Bd.ExecuteNonQuery(sqlUpdateItem, dbTrans);
            if (rf < 1)
            {
                dbTrans.Rollback();
                return false;
            }
            //Verifica se o pedido tem grade e atualiza
            if (p.IdGrade.Iniciada)
            {
                rf = p.Grade.Atualizar(dbTrans);
                if (rf < 1)
                {

                    dbTrans.Rollback();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Exclui o pedido por completo e implicitamente repõe o estoque
        /// </summary>
        /// <returns></returns>
        public bool PedidoExcluir()
        {
            //Iniciar transação
            SqlCeTransaction dbTrans = null;
            dbTrans = D.Bd.Con.BeginTransaction();
            int rf;

            foreach (Produto p in LstItem)
            {
                ItensExcluir(dbTrans, p);
            }

            string sqlDelPedido = @"Delete from pedido where id_pedido='" + Id + "'";

            rf = D.Bd.ExecuteNonQuery(sqlDelPedido, dbTrans);

            dbTrans.Commit();
            D.Pedido = new Pedido();
            return true;
        }

        /// <summary>
        /// Exclui os itens marcados para exclusão
        /// </summary>
        /// <returns></returns>
        public bool ItensExcluir(SqlCeTransaction dbTrans, Produto p)
        {
            int rf;


            //Incrementa estoque:
            string sqlUpdateEstoque = @"
            update 
                    produto
            set
                    estoque=estoque + " + p.QuantidadeRequerida +
            @" where  
                    id_produto =  " + p.Id;
            rf = D.Bd.ExecuteNonQuery(sqlUpdateEstoque, dbTrans);

            //Verificar se funcionou...

            string sqlDelItemPedido = @"
            Delete from
                    item_pedido
            where 
                    id_pedido='" + Id + "' and id_produto=" + p.Id;
            rf = D.Bd.ExecuteNonQuery(sqlDelItemPedido, dbTrans);
            //Esta trazendo zero mesmo que tenha tido sucesso na delecao
            //if (rf < 1)             
            //{
            //    dbTrans.Rollback();
            //    return false;
            //}

            if (p.IdGrade.Iniciada)
            {
                p.Grade.Excluir(dbTrans);
            }
            return true;
        }
        public static bool PedidosAEnviar()
        {
            DataTable dtPedido = D.Bd.DataTablePreenche("select * from pedido where status='N'", "pedido");
            if (dtPedido.Rows.Count <= Parametro.LimitePedidosEmAberto)
            {
                PedidosRestantes = Convert.ToInt32(Parametro.LimitePedidosEmAberto) - Convert.ToInt32(dtPedido.Rows.Count);
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
