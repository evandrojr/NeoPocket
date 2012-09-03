using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;//provider do SGBD FireBird
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using Core;

namespace NeoSync
{

    class DadoImportacaoRoteiro {

        public static void RotinaImportacao() {
            DadoImportacao.caminhoXml = D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio;
            DadoImportacao d = null;
            DadoImportacao.LogCria();

            d = new DadoImportacao("produto", "TB_PRODUTOS.xml", "PRODUTO");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/PRODUTO");

            d.laT("id_produto", "CODIGO");
            d.laT("nome", "DESCRICAO");
            d.laT("referencia", "REFERENCIA");
            d.laT("id_unidade_venda", "COD_UNIDADE_VENDA");
            d.laT("id_grade", "COD_GRADE");
            d.laR("preco_venda", "PRECOVENDA");
            d.laR("preco_promocao", "PRECOPROMOCAO");
            d.laT("estoque", "QUANTIDADEESTOQUE");
            d.laT("venda_fracionada", "FRACIONADA");
            d.laD("promocao_data_inicio", "DATAINICIOPROMOCAO");
            d.laD("promocao_data_final", "DATAFIMPROMOCAO");
            d.Executar();


            ////......................Tabela Cliente ...............................

            d = new DadoImportacao("cliente", "TB_clientes.xml", "CLIENTE");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/CLIENTE");

            d.laT("id_cliente", "CODIGO");            
            d.laT("id_cliente_pocket", "COD_CLIENTE_POCKET");
            d.laT("cliente_nome", "NOME");
            d.laT("cliente_nome_reduzido", "NOMEREDUZIDO");
            d.laT("tipo_pessoa", "TIPOPESSOA");
            d.laT("telefone", "TELEFONE");
            d.laT("cgc_cpf", "CGC_CPF");
            d.laT("rg_inscricao", "RG_INSCRICAO");
            d.laT("endereco", "ENDERECO");
            d.laT("bairro", "BAIRRO");
            d.laT("cidade", "COD_CIDADE");
            d.laT("uf_cod", "COD_UF");
            d.laT("cep", "CEP");
            d.laT("comprador_nome", "NOME_COMPRADOR");
            d.laT("limite_credito", "LIMITECREDITO");
            d.laD("nascimento", "NASCIMENTO");
            d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
            d.laT("dia_visita", "DIA_VISITA");
            d.laT("id_funcionario", "COD_FUNCIONARIO");
            d.laT("intervalo", "INTERVALO");
            d.laT("banco_codigo", "COD_BANCO_REF");
            d.laT("agencia_codigo", "COD_AGENCIA_REF");
            d.laT("agencia_telefone", "TELEFONE_AGENCIA");
            d.laT("referencia_comercial1", "REFERENCIA_COMERCIAL1");
            d.laT("referencia_comercial1_telefone", "TELEFONE_REFERENCIA1");
            d.laT("referencia_comercial2", "REFERENCIA_COMERCIAL2");
            d.laT("referencia_comercial2_telefone", "TELEFONE_REFERENCIA2");
            d.laT("id_tabela_preco", "COD_TABELAPRECO");

            d.Executar();
            ////.................... Tabela Cidade ........................................

            d = new DadoImportacao("cidade", "TB_Cidades.xml", "CIDADE");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/CIDADE");

            d.laT("id_cidade", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laT("uf_cod", "COD_UF");
            d.Executar();

            //.....................Tabela Especie Financeira.............................

            d = new DadoImportacao("especie_financeira", "TB_EspecieFinanceira.xml", "ESPECIEFINANCEIRA");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/ESPECIEFINANCEIRA");

            d.laT("id_especie_financeira", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laB("verifica_credito", "VERIFICA_CREDITO");
            d.Executar();



            //.....................Tabela Forma Pagamento.................................

            d = new DadoImportacao("forma_pagamento", "TB_FormaDePagamento.xml", "FORMADEPAGAMENTO");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/FORMADEPAGAMENTO");

            d.laT("id_forma_pagamento", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laT("no_parcelas", "PARCELAS");
            d.Executar();

            ////.....................Tabela Funcionario.....................................

            //d = new DadoImportacao("funcionario", "TB_Vendedor.xml", "Vendedor");
            //d.XMLSchemaSemiAutoCarga(@"//DocumentElement/Vendedor");

            //d.laT("id_funcionario", "codigo");
            //d.laT("funcionario_nome", "nome");
            ////d.laT("funcionario_senha", "");
            //d.Executar();

            //....................Tabela Grade ............................................

            d = new DadoImportacao("grade", "TB_Grade.xml", "GRADE");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/GRADE");

            d.laT("id_grade", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar();

            //....................Tabela Item Atributo......................................

            d = new DadoImportacao("item_atributo", "TB_Itematributo.xml", "ITEMATRIBUTO");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/ITEMATRIBUTO");

            d.laT("id_item_atributo", "COD_ATRIBUTO");
            d.laT("id_atributo", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar();

            //....................Tabela Item Grade........................................

            d = new DadoImportacao("item_grade", "TB_Itemgrade.xml", "ITEMGRADE");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/ITEMGRADE");

            d.laT("id_item_grade", "COD_GRADE");
            d.laT("id_grade", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar();

            //......................Tabela Item tabela Preco.....................................

            d = new DadoImportacao("item_tabela_preco", "TB_ItensTabelaPreco.xml", "ITENSTABELAPRECO");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/ITENSTABELAPRECO");

            d.laT("id_tabela_preco", "COD_TABELAPRECO");
            d.laT("id_produto", "COD_PRODUTO");
            d.laT("tipo_valor", "TIPOVALOR");
            d.laR("valor1", "VALOR");
            d.laT("qtd_minima1", "QTD_MINIMA");
            d.laR("valor2", "VALOR1");
            d.laT("qtd_minima2", "QTD_MINIMA1");
            d.laR("valor3", "VALOR2");
            d.laT("qtd_minima3", "QTD_MINIMA2");
            d.laR("valor4", "VALOR3");
            d.laT("qtd_minima4", "QTD_MINIMA3");
            d.laR("desconto_maximo", "DESCONTO_MAXIMO");
            d.laR("acrescimo_maximo", "ACRESCIMO_MAXIMO");
            d.Executar();

            //.........................Tabela Parametro....................................

            d = new DadoImportacao("parametro", "TB_Parametros.xml", "PARAMETROS");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/PARAMETROS");

            d.laT("nome", "NOME");
            d.laT("tipo", "TIPO");
            d.laT("valor", "VALOR");
            d.Executar();


            //---------- Não descomente até ter a tabela certa com dados ------------------------
            //.........................Tabela Preco........................................

            //d = new DadoImportacao("tabela_preco", "TB_Preco.xml", "TABELAPRECO");
            //d.XMLSchemaSemiAutoCarga(@"//DocumentElement/TABELAPRECO");

            //d.laT("id_tabela_preco", "CODIGO");
            //d.laT("descricao", "DESCRICAO");
            //d.laR("ajuste_percentual", "PERCENTUALAJUSTE");
            //d.laT("tipo", "TIPOAJUSTE");
            //d.Executar();

            //.........................Tabela Saldo Grade.................................

            d = new DadoImportacao("saldo_grade", "TB_SaldoGrade.xml", "SALDOGRADE");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/SALDOGRADE");

            d.laT("id_produto", "COD_PRODUTO");
            d.laT("id_grade", "COD_GRADE");
            d.laT("id_item_grade", "COD_ITEMGRADE");
            d.laT("id_atributo", "COD_ATRIBUTO");
            d.laT("id_item_atributo", "COD_ITEMATRIBUTO");
            d.laT("estoque", "QUANTIDADEESTOQUE");
            d.Executar();

            //.........................Tabela Atributo.....................................

            d = new DadoImportacao("atributo", "TB_Atributo.xml", "ATRIBUTO");
            d.XMLSchemaSemiAutoCarga(@"//DocumentElement/ATRIBUTO");

            d.laT("id_atributo", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar();
        }
    }

    public struct LigacaoXml {
        public string BdCampo;
        public string XmlCampo;
    }


    public class DadoImportacao
    {
        List<LigacaoXml> LstLigacao = new List<LigacaoXml>();
        public DataTable DtTabelaXml = null;
        public DataSet Ds = null;
        public string TabelaBdNome, CaminhoXml, ArquivoXmlNome;
        public static string caminhoXml;
        public XmlDocument XmlDocument = null;
        public List<string> LstCamposData = new List<string>();
        public List<string> LstCamposBit = new List<string>();
        public List<string> LstCamposReal = new List<string>();
        public static string CampoBusca = null;
        public string TabelaXmlNomeExato;
        private static TextWriter log = null;
        public static int InsercaoQtd=0, ErrosQtd=0;


        public DadoImportacao(string tabela, string arquivoXml, string tabelaXmlNomeExato)
        {
            TabelaBdNome = tabela;
            ArquivoXmlNome = arquivoXml;
            TabelaXmlNomeExato = tabelaXmlNomeExato;
            DtTabelaXml = new DataTable(TabelaXmlNomeExato);
        }

        /// <summary>
        /// Ligacao adicionar texto
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="XmlCampo"></param>
        public void laT(string dbCampo, string XmlCampo)
        {
            LigacaoXml lig = new LigacaoXml();
            lig.BdCampo = dbCampo;
            lig.XmlCampo = XmlCampo;
            LstLigacao.Add(lig);
        }

        /// <summary>
        /// Ligacao adicionar data
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="XmlCampo"></param>
        public void laD(string dbCampo, string XmlCampo)
        {
            LigacaoXml lig = new LigacaoXml();
            lig.BdCampo = dbCampo;
            lig.XmlCampo = XmlCampo;
            LstLigacao.Add(lig);
            LstCamposData.Add(dbCampo);
        }

        /// <summary>
        /// Ligacao adicionar bit
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="XmlCampo"></param>
        public void laB(string dbCampo, string XmlCampo)
        {
            LigacaoXml lig = new LigacaoXml();
            lig.BdCampo = dbCampo;
            lig.XmlCampo = XmlCampo;
            LstLigacao.Add(lig);
            LstCamposBit.Add(dbCampo);
        }

        /// <summary>
        /// Ligacao adicionar Real
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="XmlCampo"></param>
        public void laR(string dbCampo, string XmlCampo)
        {
            LigacaoXml lig = new LigacaoXml();
            lig.BdCampo = dbCampo;
            lig.XmlCampo = XmlCampo;
            LstLigacao.Add(lig);
            LstCamposReal.Add(dbCampo);
        }


        public static void LogCria()
        {
            try
            {
                File.Delete(D.ApplicationDirectory + D.NeoDebug_LogFile);
            }
            catch { }
            log = new StreamWriter(D.ApplicationDirectory + D.NeoDebug_LogFile);
            log.Close();
        }

        public static void LogGrava(string mensagem)
        {
            log = new StreamWriter(D.ApplicationDirectory + D.NeoDebug_LogFile, true);
            log.WriteLine(mensagem);
            log.Close();
        }



        private static bool igual(String a)
        {
            if (a == CampoBusca)
                return true;
            else
                return false;
        }

        public static string DataPtParaBd(string data)
        {
            return data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);
        }

        public void Executar()
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string valor, data, sql;
            DateTime testeData;
            ErrosQtd = 0;
            int IdClientePocket = 1000;

            LogGrava("===== " + TabelaBdNome + " ======");
            sql="Delete from " + TabelaBdNome;
            LogGrava(sql);
            D.Bd.ExecuteNonQuery(sql);

            LogGrava("Tabela " + TabelaBdNome + " possue " + DtTabelaXml.Rows.Count + " registros");
            LogGrava("Memoria usada antes da coleta " + GC.GetTotalMemory(false));
            GC.Collect();
            LogGrava("Memoria usada após a coleta " + GC.GetTotalMemory(true));

//            GC.WaitForPendingFinalizers();

            //            OdbcTransaction dbTrans = null;
            //            dbTrans = con.BeginTransaction();

            FbTransaction dbTrans = null;
            sb1 = new StringBuilder();
            sb2 = new StringBuilder();
            for (int l = 0; l < DtTabelaXml.Rows.Count; ++l)
            {
                dbTrans = D.Bd.Con.BeginTransaction();
                sb1.Remove(0, sb1.Length);
                sb2.Remove(0, sb2.Length);
                sb1.Append("insert into " + TabelaBdNome + " (");
                sb2.Append(" values (");
                for (int i = 0; i < LstLigacao.Count; ++i)
                {
                    sb1.Append(LstLigacao[i].BdCampo);
                    valor = DtTabelaXml.Rows[l][LstLigacao[i].XmlCampo].ToString();
                    valor = valor.Replace("'", @"''");
                    valor = valor.Trim();
                    //Para a funcao igual usada na busca das listas de campo data e Bit
                    CampoBusca = LstLigacao[i].BdCampo;
                    //Define códigos cliente_pocket para valores que vierem vazios ou brancos
                    if (LstLigacao[i].BdCampo == "id_cliente_pocket" && (valor == "" || valor == "0"))
                    {
                        valor = IdClientePocket.ToString();
                        ++IdClientePocket;
                    }
                    //Verifica se é nulo ou vazio
                    if (valor == "" || valor.ToLower() == "null"){
                        valor = "null";
                        sb2.Append(valor);
                    }
                    else{
                        //Verifica se é um campo data
                        if (LstCamposData.Exists(igual))
                        {
                            data = DataPtParaBd(valor);
                            try {
                                testeData = Convert.ToDateTime(data, D.CultureInfoBRA.DateTimeFormat);
                                data = "'" + data + "'";
                            } catch {
                                data = "null";
                            }
                            sb2.Append(data);
                        }
                        else
                            //Verifica se é um campo bit
                            if (LstCamposBit.Exists(igual))
                            {
                                if (valor == "1")
                                    sb2.Append("'true'");
                                else
                                    sb2.Append("'false'");
                            }
                            else
                                //Verifica se é um campo Real
                                if (LstCamposReal.Exists(igual))
                                {
                                    valor = valor.Replace(",",".");
                                    sb2.Append(valor);
                                }
                            else                            
                            //Caso normal, dado nao é data e nao é nulo
                            sb2.Append("'" + valor + "'");
                    }
                    if (i < LstLigacao.Count - 1)
                    {
                        sb1.Append(",");
                        sb2.Append(",");
                    }
                }
                sb1.Append(")");
                sb2.Append(")");
                D.Bd.ImportaLinha(sb1.ToString() + sb2.ToString(), dbTrans);
                dbTrans.Commit();
                if (l % 50 == 0)
                    GC.Collect();
            }
            LogGrava("Erros que tentaram ser reinseridos " + ErrosQtd);
        }

        public void XMLSchemaSemiAutoCarga(string caminho)
        {

            XmlDocument = new XmlDocument();
            XmlDocument.Load(caminhoXml + ArquivoXmlNome);

            XmlNode node = XmlDocument.SelectSingleNode(caminho);
            XmlNodeList filhos = node.ChildNodes;

            foreach (XmlNode filho in filhos)
                DtTabelaXml.Columns.Add(filho.Name);
            DtTabelaXml.ReadXml(caminhoXml + ArquivoXmlNome);

        }


    }
}
