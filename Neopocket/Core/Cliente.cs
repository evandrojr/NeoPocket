using System;
using System.Data.SqlServerCe;
using System.Data;
using Neopocket.Forms;
using Neopocket.Utils;


namespace Neopocket.Core
{
    public class Cliente
    {
        public Guid Id; // é igual ao Id Pocket 
        public int IdStore=0;
        public int FormaPagamentoPrazoMedio, MaiorVencimento;
        public bool ListaNegra = false;
        public bool ClienteSemIdStore;
        public double MontanteAberto, MontanteVencido, MontanteAVencer;
        private string cidadeNome = "";
        public DataTable DtFormaPagamento = null; //Usado para o combo de tabela de formas de pagamento
        public DataTable DtTabelaPreco = null; // Usado para o combo de tabela de preço 
        private bool ativo;
        //Só é setado no Carregar(****) e em nenhuma outro lugar pode ser modificado
        private IdTipoEnum idTipo;
        private bool contribuinteIcms;

        public bool Ativo
        {
            get { return ativo; }
        }

        public bool ContribuinteIcms
        {
            get { return contribuinteIcms; }
        }

        public enum EnumComboAtualizar : byte
        {
            TabelaPreco,
            FormaPagamento,
            Ambos,
        }

        public IdTipoEnum IdTipo
        {
            get
            {
                return idTipo;
            }
        }

        public enum IdTipoEnum : byte
        {
            IdPocket, // GuiD
            IdStore,  // Int
        }

        public string CidadeNome
        {
            get { return cidadeNome; }
        }

        //cad1
        public string NomeFantasia = "", RazaoSocial = "", TipoPessoa = "", CnpjCpf = "", RgIncricao = "";

        //Cad2
        public string Endereco = "", Bairro = "", Telefone = "", Cep = "";

        public int Cidade;

        //Cad3
        public string Comprador = "", Intervalo = "";
        public int IdFormaPagamentoPadrao, IdTabelaPrecoFixa, DiaVisita;
        public double LimiteCreditoBd;
        private double bdi = 0;
        public DateTime Nascimento;

        //Cad4
        public string RefBanco, Agencia;
        public string TelefoneAgencia = "", RefComercial1 = "", TelefoneComercial1 = "";
        public string RefComercial2 = "", TelefoneComercial2 = "";
        public string Status = ""; // (N)ovo ou (S)incronizado

        public Cliente()
        {
            if (D.Acao == D.AcaoEnum.ClienteCadastro)
                Id = Guid.NewGuid();
        }

        // Não é o ideal mas tem que ser assim pois se usarmos um auto-incremento pode chocar
        // o IdPocket após uma importação de dados
        public static Guid IdPocketProximo()
        {
            try
            {
                return D.Bd.Guid("Select max(id_cliente_pocket) + 1 from cliente", true);
            }
            catch
            {
                return new Guid();
            }
        }

        public bool Inserir()
        {
            string sdata;


            if (TipoPessoa == "F")
                sdata = Bd.DataParaBd(Nascimento);
            else
                sdata = "NULL";
            Cep = Cep.Replace(".", "");
            Cep = Cep.Replace("-", "");
            Cep = Cep.Replace(" ", "");
            String sql = @"
            Insert 
                into cliente
                    (id_cliente_pocket,
                    cliente_nome_reduzido,
                    cliente_nome,
                    tipo_pessoa,
                    cgc_cpf,
                    rg_inscricao,
                    endereco,
                    bairro,
                    telefone,
                    cep,
                    cidade,
                    comprador_nome,
                    intervalo,
                    id_forma_pagamento,
                    dia_visita,
                    limite_credito,
                    nascimento,
                    banco_codigo,
                    agencia_codigo,
                    agencia_telefone,
                    referencia_comercial1,
                    referencia_comercial1_telefone,
                    referencia_comercial2,
                    referencia_comercial2_telefone,
                    id_funcionario,
                    status)
            values ('" +
                     Id + "'," +
                     Bd.SN(NomeFantasia) + "," +
                     Bd.SN(RazaoSocial) + "," +
                     Bd.SN(TipoPessoa) + "," +
                     Bd.SN(CnpjCpf) + "," +
                     Bd.SN(RgIncricao) + "," +
                     Bd.SN(Endereco) + "," +
                     Bd.SN(Bairro) + "," +
                     Bd.SN(Telefone) + "," +
                     Bd.SN(Cep) + "," +
                     Bd.SZ(Cidade) + "," +
                     Bd.SN(Comprador) + "," +
                     Bd.SN(Intervalo) + "," +
                     Bd.SZ(IdFormaPagamentoPadrao) + "," +
                     Bd.SN(DiaVisita) + "," +
                     Bd.DoubleZeroIfNull(Parametro.LimiteDeCreditoPadrao) + "," +   // O limite de crédido padrão para os novos clientes está como 0!
                     sdata + "," +
                     Bd.SN(RefBanco) + "," +
                     Bd.SN(Agencia) + "," +
                     Bd.SN(TelefoneAgencia) + "," +
                     Bd.SN(RefComercial1) + "," +
                     Bd.SN(TelefoneComercial1) + "," +
                     Bd.SN(RefComercial2) + "," +
                     Bd.SN(TelefoneComercial2) + "," +
                     D.Funcionario.Id +
                     ",'N')";
            try
            {
                D.Bd.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao inserir o cliente", "Neo", "Sql=" + sql + ex.Message);
                return false;
            }
        }

        public double Bdi
        {
            get { return bdi; }
            set { bdi = value; }
        }

        public bool Carregar(Guid idGuid, int idInt, IdTipoEnum _idTipo)
        {
            D.Cliente = new Cliente();
            String sql = "";
            idTipo = _idTipo;
            if (_idTipo == IdTipoEnum.IdPocket)
            {
                sql = @"
                SELECT
                        cliente.*, cidade.descricao as cidade_nome
                FROM
                        cidade  JOIN
                        cliente ON cidade.id_cidade = cliente.cidade
                where
                        id_cliente_pocket='" + idGuid + "'";
                D.Cliente.Id = idGuid;
                Id = idGuid;
                IdStore = 0;
            }
            else
            {
                sql = @"
                SELECT
                        cliente.*, cidade.descricao as cidade_nome
                FROM
                        cidade  JOIN
                        cliente ON cidade.id_cidade = cliente.cidade
                where
                        id_cliente=" + idInt;
                D.Cliente.IdStore = idInt;
                Id = Guid.Empty;
                IdStore = idInt;
            }
            SqlCeDataReader reader;
            SqlCeTransaction bdTrans = null;
            bdTrans = D.Bd.Con.BeginTransaction();

            SqlCeCommand cmd = new SqlCeCommand(sql, D.Bd.Con, bdTrans);
            reader = cmd.ExecuteReader();
            reader.Read();
            Id = idGuid;
            try
            {
                ativo = Convert.ToBoolean(reader["ativo"]);
            }
            catch
            {
                ativo = true;
            }
            TipoPessoa = Convert.ToString(reader["tipo_pessoa"]);
            if (reader["id_cliente"] == System.DBNull.Value)
            {
                IdStore = 0;
                D.Cliente.ClienteSemIdStore = true;
                // FE.Show("Este cliente ainda não tem código no sistema de retaguarda", "Aviso");
            }
            else
            {
                IdStore = Convert.ToInt32(reader["id_cliente"]);
                D.Cliente.ClienteSemIdStore = false;
            }

            try
            {
                contribuinteIcms = Convert.ToBoolean(reader["contribuinte_icms"]);
            }
            catch
            {
                contribuinteIcms = true;
            }

            NomeFantasia = Convert.ToString(reader["cliente_nome_reduzido"]).Trim();
            RazaoSocial = Convert.ToString(reader["cliente_nome"]).Trim();
            TipoPessoa = Convert.ToString(reader["tipo_pessoa"]).Trim();
            CnpjCpf = Convert.ToString(reader["cgc_cpf"]).Trim();
            RgIncricao = Convert.ToString(reader["rg_inscricao"]).Trim();
            Endereco = Convert.ToString(reader["endereco"]).Trim();
            Bairro = Convert.ToString(reader["bairro"]).Trim();
            Telefone = Convert.ToString(reader["telefone"]).Trim();
            Cep = Convert.ToString(reader["cep"]).Trim();
            Cidade = Convert.ToInt32(reader["cidade"]);
            cidadeNome = Convert.ToString(reader["cidade_nome"]).Trim();
            Comprador = Convert.ToString(reader["comprador_nome"]).Trim();
            Intervalo = Convert.ToString(reader["intervalo"]).Trim();
            if (Convert.ToString(reader["id_forma_pagamento"]) != "")
                IdFormaPagamentoPadrao = Convert.ToInt32(reader["id_forma_pagamento"]);
            else
                IdFormaPagamentoPadrao = 0;
            if (Convert.ToString(reader["dia_visita"]) != "")
                DiaVisita = Convert.ToInt32(reader["dia_visita"]);
            else
                DiaVisita = 0;
            LimiteCreditoBd = Convert.ToDouble(reader["limite_credito"]);
            try
            {
                Nascimento = Convert.ToDateTime(reader["nascimento"]);
            }
            catch
            {
                Nascimento = Convert.ToDateTime("01/01/2000");
            }
            //ListaNegra
            try
            {
                if (Convert.ToBoolean(reader["lista_negra"]))
                    ListaNegra = true;
            }
            catch
            {
                ListaNegra = false;
            }
            RefBanco = Convert.ToString(reader["banco_codigo"]);
            Agencia = Convert.ToString(reader["agencia_codigo"]);

            TelefoneAgencia = Convert.ToString(reader["agencia_telefone"]).Trim();
            RefComercial1 = Convert.ToString(reader["referencia_comercial1"]).Trim();
            TelefoneComercial1 = Convert.ToString(reader["referencia_comercial1_telefone"]).Trim();
            RefComercial2 = Convert.ToString(reader["referencia_comercial2"]).Trim();
            TelefoneComercial2 = Convert.ToString(reader["referencia_comercial2_telefone"]).Trim();
            Status = Convert.ToString(reader["status"]).Trim();
            if (IdFormaPagamentoPadrao > 0)
            {
                FormaPagamentoPrazoMedio = (int)D.Bd.N(@"
                select 
                        prazo_medio
                from
                        forma_pagamento
                where
                        id_forma_pagamento=" + IdFormaPagamentoPadrao);
            }
            else
            {
                FormaPagamentoPrazoMedio = -1; // Sem prazo médio
            }

            try
            {
                IdTabelaPrecoFixa = Convert.ToInt32(reader["id_tabela_preco"]);
            }
            catch
            {
                //Debug.LogGrava("Não encontrei o valor do IdTabelaPrecoFixa ao carregar o cliente");
                IdTabelaPrecoFixa = 0; ;
            }

            bdi = Bd.DoubleZeroIfNull(reader["bdi"]);
            MontanteAberto = Bd.DoubleZeroIfNull(reader["montante_aberto"]);
            MontanteVencido = Bd.DoubleZeroIfNull(reader["montante_vencido"]);
            MontanteAVencer = Bd.DoubleZeroIfNull(reader["montante_a_vencer"]);
            MaiorVencimento = Bd.IntZeroIfNull(reader["maior_vencimento"]);

            reader.Close();
            bdTrans.Commit();
            D.Cliente = this;
            return true;
        }


        public bool Atualizar()
        {
            string IdVariavel = "";

            if (idTipo == IdTipoEnum.IdStore)
                IdVariavel = " id_cliente=" + Bd.SA(IdStore.ToString());
            else
                IdVariavel = " id_cliente_pocket=" + Bd.SA(Id.ToString());

            SqlCeCommand cmd = new SqlCeCommand();

            Cep = Cep.Replace(".", "");
            Cep = Cep.Replace("-", "");
            Cep = Cep.Replace(" ", "");

            cmd.CommandText = @"
            UPDATE 
                 Cliente SET
                 cliente_nome_reduzido = @NomeFantasia,
                 cliente_nome = @RazaoSocial,
                 tipo_pessoa = @TipoPessoa,
                 cgc_cpf = @CnpjCpf,
                 rg_inscricao = @RgIncricao,
                 endereco = @Endereco,
                 bairro = @Bairro,
                 telefone = @Telefone,
                 cep = @Cep,
                 cidade = @Cidade,
                 comprador_nome = @Comprador,
                 intervalo = @Intervalo,
                 id_forma_pagamento = @FormaPagamento,
                 dia_visita = @DiaVisita,
                 limite_credito = @LimiteCredito,
                 nascimento = @Nascimento,    
                 banco_codigo = @RefBanco,
                 agencia_codigo = @Agencia,
                 agencia_telefone = @TelefoneAgencia,
                 referencia_comercial1 = @RefComercial1,
                 referencia_comercial1_telefone = @TelefoneComercial1,
                 referencia_comercial2 = @RefComercial2,
                 referencia_comercial2_telefone = @TelefoneComercial2,
                 id_funcionario = @Id_funcionario,
                 status = 'N'
             where " + IdVariavel;
                  

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@NomeFantasia", NomeFantasia);
            cmd.Parameters.AddWithValue("@RazaoSocial", RazaoSocial);
            cmd.Parameters.AddWithValue("@TipoPessoa", TipoPessoa);
            cmd.Parameters.AddWithValue("@CnpjCpf", CnpjCpf);
            cmd.Parameters.AddWithValue("@RgIncricao", RgIncricao);
            cmd.Parameters.AddWithValue("@Endereco", Endereco);
            cmd.Parameters.AddWithValue("@Bairro", Bairro);
            cmd.Parameters.AddWithValue("@Telefone", Telefone);
            cmd.Parameters.AddWithValue("@Cep", Cep);
            cmd.Parameters.AddWithValue("@Cidade", Cidade);
            cmd.Parameters.AddWithValue("@Comprador", Comprador);
            cmd.Parameters.AddWithValue("@Intervalo", Intervalo);
            cmd.Parameters.AddWithValue("@FormaPagamento", IdFormaPagamentoPadrao);
            cmd.Parameters.AddWithValue("@DiaVisita", DiaVisita);
            cmd.Parameters.AddWithValue("@LimiteCredito", LimiteCreditoBd);
            cmd.Parameters.AddWithValue("@Nascimento", Nascimento);
            cmd.Parameters.AddWithValue("@RefBanco", RefBanco);
            cmd.Parameters.AddWithValue("@Agencia", Agencia);
            cmd.Parameters.AddWithValue("@TelefoneAgencia", TelefoneAgencia);
            cmd.Parameters.AddWithValue("@RefComercial1", RefComercial1);
            cmd.Parameters.AddWithValue("@TelefoneComercial1", TelefoneComercial1);
            cmd.Parameters.AddWithValue("@RefComercial2", RefComercial2);
            cmd.Parameters.AddWithValue("@TelefoneComercial2", TelefoneComercial2);
            cmd.Parameters.AddWithValue("@Id_funcionario", D.Funcionario.Id);

            cmd.Connection = D.Bd.Con;
            cmd.ExecuteNonQuery();

            return true;
        }

        public bool Excluir(Guid idPocket)
        {
            Id = idPocket;
            String sql = @"Delete from cliente where id_cliente_pocket='" + idPocket + "'";

            try
            {
                D.Bd.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao excluir o cliente", "Neo", "Sql=" + sql + ex.Message);
                return false;
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
                if ((D.Cliente.ListaNegra || D.Cliente.CreditoRestante <= 0) && D.Pedido.EspecieFinanceiraVerificaCredito)
                    return false;
                else
                    return true;
            }
        }


        /// <summary>
        /// Verifica sem pode PermitirListarEspeciesFinanceirasQueVerificamCreditoBool
        /// </summary>
        /// <returns></returns>
        public bool PermitirListarEspeciesFinanceirasQueVerificamCreditoBool
        {
            get
            {
                if (!Parametro.VerificarCreditoVendaAPrazo)
                    return true;
                if ((D.Cliente.ListaNegra || D.Cliente.CreditoRestante <= 0))
                    return false;
                else
                    return true;
            }
        }


        private string sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoFormaPagamentoPadrao()
        {
            return @"Select  
                            forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                      FROM
                              forma_pagamento INNER JOIN
                              forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                              tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                              item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                              especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                      WHERE
                              tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco + @" and
                              forma_pagamento.id_forma_pagamento = " + D.Cliente.IdFormaPagamentoPadrao + @"   
                      Union 
                      SELECT     
                            forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                      FROM
                            forma_pagamento INNER JOIN
                              forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                              tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                              item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                              especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                       WHERE
                              tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco;
        }

        private string sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoFormaPagamentoPadraoClienteSemCredito()
        {
            return @"Select  
                            forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                      FROM
                              forma_pagamento INNER JOIN
                              forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                              tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                              item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                              especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                      WHERE
                              (especie_financeira.verifica_credito = 'false') and   
                              tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco + @" and
                              forma_pagamento.id_forma_pagamento = " + D.Cliente.IdFormaPagamentoPadrao + @"   
                      Union 
                      SELECT     
                            forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                      FROM
                            forma_pagamento INNER JOIN
                              forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                              tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                              item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                              especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                      WHERE
                              (especie_financeira.verifica_credito = 'false') and 
                               tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco; ;
        }

        private string sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoClienteSemCredito()
        {
            return @"Select  '(selecione)' as descricao, 0 as id_forma_pagamento
                                    Union 
                              SELECT     
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                              FROM
                                    forma_pagamento INNER JOIN
                                      forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                                      tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                                      item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                                      especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                              WHERE
                                      (especie_financeira.verifica_credito = 'false') and 
                                       tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco; ;
        }

        private string sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPreco()
        {
            return @"Select  '(selecione)' as descricao, 0 as id_forma_pagamento
                            Union 
                      SELECT     
                            forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                      FROM
                            forma_pagamento INNER JOIN
                              forma_pagamento_tabela_preco ON forma_pagamento.id_forma_pagamento = forma_pagamento_tabela_preco.id_forma_pagamento INNER JOIN
                              tabela_preco ON forma_pagamento_tabela_preco.id_tabela_preco = tabela_preco.id_tabela_preco INNER JOIN
                              item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                              especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                       WHERE
                              tabela_preco.id_tabela_preco = " + D.Pedido.IdTabelaPreco;
        }

        private string sqlFormaPagamentoTabelaPrecoPredefinida()
        {
            return @"Select  
                                    '(selecione)' as descricao, 0 as id_forma_pagamento
                             FROM
                                    forma_pagamento
                             Union 

                             SELECT
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento INNER JOIN
                                    item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                                    especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira";
        }

        private string sqlFormaPagamentoTabelaPrecoPredefinidaClienteSemCredito()
        {
            return @"Select  
                                    '(selecione)' as descricao, 0 as id_forma_pagamento
                             FROM
                                    forma_pagamento
                             Union 

                             SELECT
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento INNER JOIN
                                    item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                                    especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira and
                                    (especie_financeira.verifica_credito = 'false')";
        }


        private string sqlFormaPagamentoFormaPagamentoPredefinidasQueNaoNecessitemDeCredito()
        {
            return @"Select  
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                   item_forma_pagamento INNER JOIN
                                   forma_pagamento ON item_forma_pagamento.id_forma_pagamento = forma_pagamento.id_forma_pagamento INNER JOIN
                                   especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                             WHERE
                                    forma_pagamento.id_forma_pagamento = " + D.Cliente.IdFormaPagamentoPadrao + @"
                                    and forma_pagamento.prazo_medio <= " + D.Cliente.FormaPagamentoPrazoMedio + @" and
                                    (especie_financeira.verifica_credito = 'false')
                             Union 
                             SELECT     
                                   forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                   item_forma_pagamento INNER JOIN
                                   forma_pagamento ON item_forma_pagamento.id_forma_pagamento = forma_pagamento.id_forma_pagamento INNER JOIN
                                   especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                             WHERE
                                    (especie_financeira.verifica_credito = 'false') and 
                                    forma_pagamento.prazo_medio <= " + D.Cliente.FormaPagamentoPrazoMedio;
        }

        private string sqlFormaPagamentoFormaPagamentoPredefinida()
        {
            return @"Select  
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento
                             WHERE
                                    forma_pagamento.id_forma_pagamento = " + D.Cliente.IdFormaPagamentoPadrao + @"
                                    and forma_pagamento.prazo_medio <= " + D.Cliente.FormaPagamentoPrazoMedio + @" 
                             Union 
                             SELECT     
                                   forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                   item_forma_pagamento INNER JOIN
                                   forma_pagamento ON item_forma_pagamento.id_forma_pagamento = forma_pagamento.id_forma_pagamento INNER JOIN
                                   especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                             WHERE
                                    forma_pagamento.prazo_medio <= " + D.Cliente.FormaPagamentoPrazoMedio;
        }

        private string sqlFormaPagamentoFormaPagamentoPredefinidaClienteSemCredito()
        {
            return @"Select  
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento
                             WHERE
                                    forma_pagamento.id_forma_pagamento = " + D.Cliente.IdFormaPagamentoPadrao + @"
                                    and (especie_financeira.verifica_credito = 'false')
                             Union 
                             SELECT     
                                   forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                   item_forma_pagamento INNER JOIN
                                   forma_pagamento ON item_forma_pagamento.id_forma_pagamento = forma_pagamento.id_forma_pagamento INNER JOIN
                                   especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                             WHERE
                                    forma_pagamento.prazo_medio <= " + D.Cliente.FormaPagamentoPrazoMedio + @" and
                                    (especie_financeira.verifica_credito = 'false')";
        }


        private string sqlFormaPagamento()
        {
            return @"Select  
                                    '(selecione)' as descricao, 0 as id_forma_pagamento
                             FROM
                                    forma_pagamento
                             Union 
                             SELECT
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento INNER JOIN
                                    item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                                    especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira";
        }

        private string sqlFormaPagamentoClienteSemCredito()
        {
            return @"Select  
                                    '(selecione)' as descricao, 0 as id_forma_pagamento
                             FROM
                                    forma_pagamento
                             Union 
                             SELECT
                                    forma_pagamento.descricao, forma_pagamento.id_forma_pagamento
                             FROM
                                    forma_pagamento INNER JOIN
                                    item_forma_pagamento ON forma_pagamento.id_forma_pagamento = item_forma_pagamento.id_forma_pagamento INNER JOIN
                                    especie_financeira ON item_forma_pagamento.id_especie_financeira = especie_financeira.id_especie_financeira
                             WHERE
                                     (especie_financeira.verifica_credito = 'false')";
        }

        /// <summary>
        /// A depender do contexto irá fornecer dados para popular os combos da
        /// tabela de preço e forma de pagamento do formulário do pedido
        /// 
        /// Também tentará definir a espécie financeira caso a forma de pagamento seja > 0
        /// </summary>
        /// <returns></returns>
        public void TabPrecoEFrmPagCbosPopularDefEspFinanc(EnumComboAtualizar combos)
        {
            string sqlTabPreco = "";
            string sqlFormPag = "";
            string sqlFormaPagamentoRelacionadasComTabelaPreco = "";
            bool existeRelacionamentoComTabelaDePreco = false;

            //Não será permitido alterar a tabela de preço caso ela venha pré definida
            if (D.Cliente.IdTabelaPrecoFixa > 0)
            {
                D.Pedido.IdTabelaPreco = D.Cliente.IdTabelaPrecoFixa;
                sqlTabPreco = @"
                SELECT 
                        descricao, id_tabela_preco
                FROM 
                        tabela_preco
                WHERE
                        id_tabela_preco = " + D.Cliente.IdTabelaPrecoFixa;

                sqlFormaPagamentoRelacionadasComTabelaPreco =
                @"SELECT     
                            count(*)
                    FROM    
                            tabela_preco INNER JOIN
                            forma_pagamento_tabela_preco ON tabela_preco.id_tabela_preco = forma_pagamento_tabela_preco.id_tabela_preco INNER JOIN
                            forma_pagamento ON forma_pagamento_tabela_preco.id_forma_pagamento = forma_pagamento.id_forma_pagamento
                    WHERE     
                            tabela_preco.id_tabela_preco =" + D.Cliente.IdTabelaPrecoFixa;

                //Testa se existe tabela de preço relacionada
                existeRelacionamentoComTabelaDePreco = D.Bd.I(sqlFormaPagamentoRelacionadasComTabelaPreco) > 0;

                //Mostrar só as formas de pagamento que estão relacionadas com aquela tabela de preço
                if (existeRelacionamentoComTabelaDePreco)
                {
                    if (D.Cliente.IdFormaPagamentoPadrao > 0)
                    {
                        //Não pode ser selecionado nenhuma opção de forma de pagamento que tenha verifica_credito 
                        if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoFormaPagamentoPadrao();
                        else
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoFormaPagamentoPadraoClienteSemCredito();
                    }
                    else
                    {
                        //Não pode ser selecionado nenhuma opção de forma de pagamento que tenha verifica_credito 
                        if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPreco();
                        else
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinidaRelacinadoTabelaPrecoClienteSemCredito();
                    }
                }
                else
                {    //Não existe relacionamento com as tabelas de preço, MAS ainda existe com a tabela pré-definida!!!!
                    //Escolhe apenas aquelas forma de pagamento que tem o prazo médio igual ou inferior ao prazo médio
                    if (D.Cliente.IdFormaPagamentoPadrao > 0)
                    {
                        //Não pode ser selecionado nenhuma opção de forma de pagamento que tenha verifica_credito 
                        if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                           sqlFormPag = sqlFormaPagamentoFormaPagamentoPredefinida();
                        else
                           sqlFormPag = sqlFormaPagamentoFormaPagamentoPredefinidasQueNaoNecessitemDeCredito();
                    }
                    else// Nao existe forma de pagamento pré-definida para o cliente
                    {
                        //Não existe relacionamento com as tabelas de preço MAS ainda existe com a tabela pré-definida!!!!
                        if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinida();
                        else
                            sqlFormPag = sqlFormaPagamentoTabelaPrecoPredefinidaClienteSemCredito();
                    } // FIM else Nao existe forma de pagamento pré-definida para o cliente
                }//Fim testa relacionamento da tabela de preço com a forma de pagamento
            }// Fim Tabela de preço vem pré-definida
            else
            {// Não existe tabela de preço pré-definida: Tabela de preço pode ser escolhida pelo vendedor
                sqlTabPreco = @"
                SELECT
                        0 AS id_tabela_preco, '(nenhuma)' AS descricao
                UNION
                SELECT 
                        id_tabela_preco, descricao
                FROM 
                        tabela_preco";

                //Escolhe apenas aquelas forma de pagamento que tem o prazo medio igual ou inferior ao 
                if (D.Cliente.IdFormaPagamentoPadrao > 0)
                {
                    if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                        sqlFormPag = sqlFormaPagamentoFormaPagamentoPredefinida();
                    else
                        sqlFormPag = sqlFormaPagamentoFormaPagamentoPredefinidaClienteSemCredito();
                }
                else//Não existe forma de pagamento pré-definida
                {
                    if (PermitirListarEspeciesFinanceirasQueVerificamCreditoBool)
                        sqlFormPag = sqlFormaPagamento();
                    else
                        sqlFormPag = sqlFormaPagamentoClienteSemCredito();
                }
            }

            if (combos == EnumComboAtualizar.Ambos)
            {
                DtTabelaPreco = D.Bd.DataTablePreenche(sqlTabPreco);
                DtFormaPagamento = D.Bd.DataTablePreenche(sqlFormPag);
            }
            else
                if (combos == EnumComboAtualizar.TabelaPreco)
                    DtTabelaPreco = D.Bd.DataTablePreenche(sqlTabPreco);
                else
                    if (combos == EnumComboAtualizar.FormaPagamento)
                        DtFormaPagamento = D.Bd.DataTablePreenche(sqlFormPag);
        }

        /// <summary>
        /// Após a sincronização deveria vir o limite de crédito atualizado, mas isso não vai 
        /// aconter pois o processo não é on-line
        /// </summary>
        public double CreditoUtilizado
        {
            get
            {
                //Iniciar transação
                SqlCeTransaction bdTrans = null;
                bdTrans = D.Bd.Con.BeginTransaction();

                double totalDosOutroPedidosAPrazo = 0;

                //Cliente que não foi cadastrado agora
                if (D.Cliente.Id == Guid.Empty)
                {
                    totalDosOutroPedidosAPrazo = D.Bd.N(@"
                    SELECT COALESCE (SUM(pedido.valor), 0) AS Expr1
                    FROM pedido LEFT OUTER JOIN
                          (SELECT DISTINCT id_forma_pagamento, id_especie_financeira
                            FROM          item_forma_pagamento) AS ifp ON ifp.id_forma_pagamento = pedido.id_forma_pagamento LEFT OUTER JOIN
                      especie_financeira ON ifp.id_especie_financeira = especie_financeira.id_especie_financeira
                    WHERE (pedido.status = 'N')  AND (especie_financeira.verifica_credito = 'True')AND(pedido.id_cliente_store = '" + D.Cliente.IdStore + "')", bdTrans);
                }
                else
                {
                    totalDosOutroPedidosAPrazo = D.Bd.N(@"
                    SELECT COALESCE (SUM(pedido.valor), 0) AS Expr1
                    FROM pedido LEFT OUTER JOIN
                          (SELECT DISTINCT id_forma_pagamento, id_especie_financeira
                            FROM          item_forma_pagamento) AS ifp ON ifp.id_forma_pagamento = pedido.id_forma_pagamento LEFT OUTER JOIN
                      especie_financeira ON ifp.id_especie_financeira = especie_financeira.id_especie_financeira
                    WHERE (pedido.status = 'N')  AND (especie_financeira.verifica_credito = 'True')AND(pedido.id_cliente = '" + D.Cliente.Id + "')", bdTrans);
                }

                bdTrans.Commit();
                return totalDosOutroPedidosAPrazo + D.Cliente.MontanteAberto;
            }
        }

        public double CreditoRestante
        {
            get
            {
                return LimiteCreditoBd - CreditoUtilizado;
            }
        }

    }
}
