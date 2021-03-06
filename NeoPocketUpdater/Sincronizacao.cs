﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms; // Just because of the progress bar
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Config;
using NeoZip;

namespace NeoPocketUpdater
{
    public static class Sincronizacao
    {
        static MapeamentoBdCsv d = null;
        public static string ArquivoEnviarNome="", ArquivoClientesReceberNomeBase="";
        public static string ArquivoComumReceberNome = "STORE_RM.zip";
        public static Funcionario Funcionario = new Funcionario();
        static List<string> lstArquivosParaCompactar = new List<string>();
        public static string ClienteEnviarNome, PedidoEnviarNome, ItensPedidoEnviarNome, ItensPedidoGradeEnviarNome, RecusaEnviarNome;
        private static bool pedidosNaoForamFeitos = false;
        private static bool clientesNaoForamCadastrados = false;
        private static bool recusasNaoForamCadastradas = false;
        public static List<Guid> LstClienteNovoId = new List<Guid>();
        public static List<Guid> LstPedidoNovoId = new List<Guid>();
        public static List<int> LstRecusaNovoId = new List<int>(); 

        static public void Iniciar()
        {
            lstArquivosParaCompactar.Clear();
            DadoImportacao.LogCria();
        }

        ///*
        // * Método que percorre tds as conexoes abertas e as fecha
        // */
        //public static void FechaConexao()
        //{
        //    RasLibrary.RasConn[] rasConnexao = RasLibrary.Ras.EnumConnections();

        //    if (rasConnexao != null)
        //    {
        //        foreach (RasLibrary.RasConn rc in rasConnexao)
        //            RasLibrary.Ras.HangUp(rc.ConnectionHandle);
        //    }
        //}

        public static string ArquivosDescompactar()
        {
            string sucesso = "";
            try
            {
              
                Zip.UnzipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoComumReceberNome, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
                Zip.UnzipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoClientesReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
                sucesso = ex.Message;
                return sucesso;
            }
            return sucesso;
        }

        public static void CarregaProdutoCsv(){

            d = new MapeamentoBdCsv("produto", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "PRODUTO.csv", D.Bd);

            d.laT("id_produto", "CODIGO");
            d.laT("nome", "DESCRICAO");
            d.laT("referencia", "REFERENCIA");
            d.laT("id_unidade_venda", "COD_UNIDADE_VENDA");
            d.laT("id_grade", "COD_GRADE");
            d.laR("preco_venda", "PRECOVENDA");
            d.laR("preco_promocao", "PRECOPROMOCAO");
            d.laT("estoque", "QUANTIDADEESTOQUE");
            d.laT("venda_fracionada", "FRACIONADA");
            d.laR("unidade_fator", "UNIDADE_FATOR");
            d.laD("promocao_data_inicio", "DATAINICIOPROMOCAO");
            d.laD("promocao_data_final", "DATAFIMPROMOCAO");
            d.Executar(true);
        }

        public static void CarregaVendedorCsv()
        {
            d = new MapeamentoBdCsv("funcionario", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "FUNCIONARIO.csv", D.Bd);

            d.laT("id", "CODIGO");
            d.laT("nome", "NOME");
            d.laT("desconto_maximo", "DESCONTO_MAXIMO");
            d.laT("acrescimo_maximo", "ACRESCIMO_MAXIMO");
            d.Executar(true);
        }





        public static void CarregaCidadeCsv()
        {
            d = new MapeamentoBdCsv("cidade", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "CIDADE.csv", D.Bd);
            d.laT("id_cidade", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laT("uf_cod", "COD_UF");
            d.Executar(true);
        }

        public static void CarregaEspecieFinanceiraCsv()
        {
            d = new MapeamentoBdCsv("especie_financeira", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ESPECIEFINANCEIRA.csv", D.Bd);
            d.laT("id_especie_financeira", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laB("verifica_credito", "VERIFICA_CREDITO");
            d.Executar(true);
        }

        public static void CarregaFormaPagamentoCsv()
        {
            d = new MapeamentoBdCsv("forma_pagamento", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "FORMADEPAGAMENTO.csv", D.Bd);
            d.laT("id_forma_pagamento", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laT("no_parcelas", "PARCELAS");
            d.laT("prazo_medio", "PRAZO_MEDIO");
            d.Executar(true);
        }
        public static void CarregaGradeCsv()
        {
            d = new MapeamentoBdCsv("grade", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "GRADE.csv", D.Bd);
            d.laT("id_grade", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar(true);
        }

        public static bool ArquivosCompactar()
        {
            if (pedidosNaoForamFeitos && clientesNaoForamCadastrados && recusasNaoForamCadastradas)
                return false;

            StringBuilder sb = new StringBuilder();

            DateTime agora = new DateTime();
            agora = DateTime.Now;
            sb.Append("POCKETRT_" + agora.Year + "-" + agora.Month.ToString().PadLeft(2, '0') + "-" + agora.Day.ToString().PadLeft(2, '0') + "_");
            sb.Append(agora.Hour.ToString().PadLeft(2, '0') + "h" + agora.Minute.ToString().PadLeft(2, '0') + "m" + agora.Second.ToString().PadLeft(2, '0') + "s_Vendedor" + D.Funcionario.Id + ".zip");
            ArquivoEnviarNome = sb.ToString();
            NeoZip.Zip.ZipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoEnviarNome, lstArquivosParaCompactar);
            return true;
        }

        public static void ClientesSelecionarParaEnvioCSV()
        {
            int n;
            clientesNaoForamCadastrados = false;
            n = -1;
            n = D.Bd.I("select count(*) from cliente where status = 'N'", false);
            if (n > 0)
            {
                string qry = "select * from cliente where status = 'N'";
                
                DataTable dtClientesNovos = D.Bd.DataTablePreenche(qry);
                ClienteEnviarNome = montarNomeArquivo("CLT") + ".csv";
                NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
                csv.EscreveCsv(dtClientesNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
            }
            else
                clientesNaoForamCadastrados = true;
        }

        //public static void Desabilitado___ClientesSelecionarParaEnvioXML()
        //{
        //    int n;
        //    n = -1;
        //    n = D.Bd.I("select count(id_cliente) from cliente where status = 'N'", false);
        //    string qry = "select * from cliente where status = 'N'";
        //    if (n > 0)
        //    {
        //        DataTable dtClientesNovos = D.Bd.DataTablePreenche(qry);
        //        dtClientesNovos.TableName = "CLIENTE";
        //        ClienteEnviarNome = montarNomeArquivo("CLT") + ".xml";
        //        dtClientesNovos.WriteXml(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
        //        lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
        //    }
        //}


        public static void PedidosSelecionarParaEnvioCSV()
        {
            int n;
            n=-1;
            pedidosNaoForamFeitos = false;
            n = D.Bd.I("select count(id_pedido) from pedido where status = 'N'", false);
            if (n > 0)
            {
                string qry = "select * from pedido where status = 'N'";
                DataTable dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                PedidoEnviarNome = montarNomeArquivo("PEDIDOS") + ".csv";
                NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);
                csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);
                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);

                n = -1;
                n = D.Bd.I(@"
                    SELECT
                            count(pedido.id_pedido)
                    FROM
                            item_pedido INNER JOIN
                            pedido ON item_pedido.id_pedido = pedido.id_pedido
                    WHERE     
                            (pedido.status = 'N')", false);

                if (n > 0)
                {
                    qry = @"
                        select 
                                item_pedido.*
                        from 
                                item_pedido, pedido
                        where
                                pedido.id_pedido=item_pedido.id_pedido
                             and
                                pedido.status = 'N'";
                    dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                    ItensPedidoEnviarNome = montarNomeArquivo("ITENSDOPEDIDO") + ".csv";
                    csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
                }

                n = -1;
                n = D.Bd.I(@"
                    SELECT
                            count(pedido.id_pedido)
                    FROM
                            item_pedido_grade INNER JOIN
                            pedido ON item_pedido_grade.id_pedido = pedido.id_pedido
                    WHERE     
                            (pedido.status = 'N')", false);
                if (n > 0)
                {
                    qry = @"
                        select 
                                item_pedido_grade.*
                        from 
                                item_pedido_grade, pedido
                        where
                                pedido.id_pedido=item_pedido_grade.id_pedido
                             and
                                pedido.status = 'N'";
                    dtPedidosNovos = D.Bd.DataTablePreenche(qry);
                    ItensPedidoGradeEnviarNome = montarNomeArquivo("ITEMDOPEDIDOGRADE") + ".csv";
                    csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
                }
            }
            else
                pedidosNaoForamFeitos = true;

        }

//        public static void Desabilitado___PedidosSelecionarParaEnvioXML()
//        {
//            int n;
//            n=-1;
//            n = D.Bd.I("select count(id_pedido) from pedido where status = 'N'", false);
//            if (n > 0)
//            {

//                string qry = "select * from pedido where status = 'N'";
//                DataTable dtPedidosNovos = D.Bd.DataTablePreenche(qry);
//                dtPedidosNovos.TableName = "PEDIDO";
//                PedidoEnviarNome = montarNomeArquivo("PEDIDOS") + ".xml";
//                dtPedidosNovos.WriteXml(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);
//                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);

//                n = -1;
//                n = D.Bd.I(@"
//                SELECT
//                        count(pedido.id_pedido)
//                FROM
//                        item_pedido INNER JOIN
//                        pedido ON item_pedido.id_pedido = pedido.id_pedido
//                WHERE     
//                        (pedido.status = 'N')", false);
//                if (n > 0)
//                {
//                    qry = @"
//                    select 
//                            item_pedido.*
//                    from 
//                            item_pedido, pedido
//                    where
//                            pedido.id_pedido=item_pedido.id_pedido
//                         and
//                            pedido.status = 'N'";
//                    dtPedidosNovos = D.Bd.DataTablePreenche(qry);
//                    dtPedidosNovos.TableName = "ITEMPEDIDOPRODUTO";
//                    ItensPedidoEnviarNome = montarNomeArquivo("ITENSDOPEDIDO") + ".xml";
//                    dtPedidosNovos.WriteXml(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
//                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
//                }

//                n = -1;
//                n = D.Bd.I(@"
//                SELECT
//                        count(pedido.id_pedido)
//                FROM
//                        item_pedido_grade INNER JOIN
//                        pedido ON item_pedido_grade.id_pedido = pedido.id_pedido
//                WHERE     
//                        (pedido.status = 'N')", false);
//                if (n > 0)
//                {
//                    qry = @"
//                    select 
//                            item_pedido_grade.*
//                    from 
//                            item_pedido_grade, pedido
//                    where
//                            pedido.id_pedido=item_pedido_grade.id_pedido
//                         and
//                            pedido.status = 'N'";
//                    dtPedidosNovos = D.Bd.DataTablePreenche(qry);
//                    dtPedidosNovos.TableName = "ITEMPEDIDOPRODUTOGRADE";
//                    ItensPedidoGradeEnviarNome = montarNomeArquivo("ITENSDOPEDIDOGRADE") + ".xml";
//                    dtPedidosNovos.WriteXml(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
//                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
//                }
//            }
//        }


        public static void CarregaItemAtributoCsv(){

            d = new MapeamentoBdCsv("item_atributo", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ITEMATRIBUTO.csv", D.Bd);
            d.laT("id_item_atributo", "COD_ATRIBUTO");
            d.laT("id_atributo", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar(true);
        }
        
        public static void CarregaItemGradeCsv()
        {
            d = new MapeamentoBdCsv("item_grade", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ITEMGRADE.csv", D.Bd);
            d.laT("id_item_grade", "CODIGO");
            d.laT("id_grade", "COD_GRADE");
            d.laT("descricao", "DESCRICAO");
            d.Executar(true);

        }
        public static void CarregaItemTabelaPrecoCsv()
        {
            d = new MapeamentoBdCsv("item_tabela_preco", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ITEMTABELAPRECO.csv", D.Bd);
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
            d.Executar(true);
        }
        public static void CarregaParametroCsv()
        {
            d = new MapeamentoBdCsv("parametro", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "PARAMETRO.csv", D.Bd);

            d.laT("nome", "NOME");
            d.laT("tipo", "TIPO");
            d.laT("valor", "VALOR");
            d.Executar(true);
        }
        public static void CarregaSaldoGradeCsv()
        {
            d = new MapeamentoBdCsv("saldo_grade", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "SALDOGRADE.csv", D.Bd);

            d.laT("id_produto", "COD_PRODUTO");
            d.laT("id_grade", "COD_GRADE");
            d.laT("id_item_grade", "COD_ITEMGRADE");
            d.laT("id_atributo", "COD_ATRIBUTO");
            d.laT("id_item_atributo", "COD_ITEMATRIBUTO");
            d.laT("estoque", "QUANTIDADEESTOQUE");
            d.Executar(true);
        }

        public static void CarregaAtributoCsv()
        {
            d = new MapeamentoBdCsv("atributo", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ATRIBUTO.csv", D.Bd);
            d.laT("id_atributo", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar(true);
        }

        private static StringBuilder montaCodigoVendedor(string codigoVendedor)
        {
            StringBuilder nomeDoArquivo = new StringBuilder();
            nomeDoArquivo.Append("Vend").Append(codigoVendedor);
            return nomeDoArquivo;
        }

        private static StringBuilder montarNomeArquivo(string nomeDaTabela)
        {
            StringBuilder s = new StringBuilder();
            s.Append(nomeDaTabela);
            s.Append("Ano" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy"));
            s.Append("Mes" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("MM"));
            s.Append("Dia" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("dd") + "_h");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("HH") + "m");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("mm") + "s");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("ss") + "_");
            s.Append(montaCodigoVendedor(D.Funcionario.Id.ToString()));
            return s;
        }

        public static void CarregaTabelaPrecoCsv()
        {
            d = new MapeamentoBdCsv("tabela_preco", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "TABELAPRECO.csv", D.Bd);
            d.laT("id_tabela_preco", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.laR("ajuste_percentual", "PERCENTUALAJUSTE");
            d.laT("tipo", "TIPOAJUSTE");
            d.Executar(true);
        }

        public static void CarregaTabelaMotivoCsv()
        {
            d = new MapeamentoBdCsv("motivo", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "MOTIVO.csv", D.Bd);
            d.laT("id_motivo", "CODIGO");
            d.laT("descricao", "DESCRICAO");
            d.Executar(true);

        }

        public static void RecusaSelecionarParaEnvioCSV()
        {
            int n;
            recusasNaoForamCadastradas = false;
            n = -1;
            n = D.Bd.I("select count(*) from recusa where status = 'N'", false);
            if (n > 0)
            {
                string qry = "select * from recusa where status = 'N'";

                DataTable dtRecusasNovas = D.Bd.DataTablePreenche(qry);
                RecusaEnviarNome = montarNomeArquivo("REC") + ".csv";
                NeoCsv.Csv csv = new NeoCsv.Csv(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + RecusaEnviarNome);
                csv.EscreveCsv(dtRecusasNovas, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + RecusaEnviarNome);
                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + RecusaEnviarNome);
            }
            else
                recusasNaoForamCadastradas = true;

        }

        public static void CarregaItemFormaPagamentoCsv()
        {
            d = new MapeamentoBdCsv("item_forma_pagamento", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "ITEMFORMAPAGAMENTO.csv", D.Bd);
            d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
            d.laT("id_especie_financeira", "COD_ESPECIEFINANCEIRA");
            d.laT("id_item_forma_pagamento", "CODIGO");
            d.laR("prazo_vencimento", "PRAZOVENCIMENTO");
            d.laT("percentual_pagamento", "PERCENTUALPAGAMENTO");
            d.Executar(true);
        }

        public static void CarregaFormaPagamentoTabelaPrecoCsv()
        {
            d = new MapeamentoBdCsv("forma_pagamento_tabela_preco", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "FORMAPAGAMENTOTABELAPRECO.csv", D.Bd);
            d.laT("id_tabela_preco", "COD_TABELAPRECO");
            d.laT("id_forma_pagamento", "COD_FORMAPAGAMENTO");
            d.Executar(true);
        }



        //public static void CarregaFuncionarioCsv()
        //{
        //    d = new MapeamentoBdCsv("funcionario", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + "funcionario.csv", D.Bd);
        //    d.laT("id_funcionario", "codigo");
        //    d.laT("funcionario_nome", "nome");
        //    d.laT("funcionario_senha", "");
        //    d.Executar();
        //}

        //Passa o nome do arquivo desejado na area FTP para baixar.
        public static string Download(string arquivoABaixar, string nomeArquivoACriar)
        {
            //string sucesso = "";
            //MyService.Service serv = new Neopocket2.MyService.Service();
            //try
            //{
            //    string strSubPasta = "";
            //    int intPosBarra =  arquivoABaixar.LastIndexOf("/");
            //    if (intPosBarra >= 0)
            //    {
            //        strSubPasta = arquivoABaixar.Substring(0, intPosBarra);
            //        if(strSubPasta.IndexOf(D.FtpDiretorio)>=0 )
            //             strSubPasta = strSubPasta.Substring(strSubPasta.IndexOf(D.FtpDiretorio)+D.FtpDiretorio.Length);
            //        arquivoABaixar = arquivoABaixar.Substring(intPosBarra + 1);
            //    }
            //    Cursor.Current = Cursors.WaitCursor; //c:\ftpneopocket\usuario\pasta
            //    string var = (string)serv.downloadFile(arquivoABaixar, @"c:\ftpneopocket\" + D.FtpUsuario + @"\"+ D.FtpDiretorio + strSubPasta + @"\", D.FtpUsuario, D.FtpSenha);
            //    //Objeto paleativo só para passar o TIPO no metodo Deserializar/
            //    Byte[] obj = new Byte[1024];
            //    Byte[] fileData = (Byte[])Serializador.Deserializar(var, obj.GetType());
            //    Cursor.Current = Cursors.Default;
            //    FileStream file = File.Create(nomeArquivoACriar);
            //    file.Write(fileData, 0, fileData.Length);
            //    file.Close();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    if (arquivoABaixar[0] != 'C')
            //    {
            //        Cursor.Current = Cursors.Default;
            //    }
            //    Cursor.Current = Cursors.Default;
            //    //MessageBox.Show(ex.Message);
            //    sucesso = ex.Message;
            //}
            return "Implementar!";
        }

        ////...........................................................................
        //public static string Upload(string arquivoLocal, string arquivoRemoto, NeoFileSystem neoFileSystem)
        //{
        //    string msg = string.Empty;

        //    try
        //    {

        //        neoFileSystem.
        //        MyService.Service WService = new Neopocket2.MyService.Service();
        //        //Carrego o arquivo desejado para um FileStream
        //        FileStream file = File.Open(caminhoArquivoENome, FileMode.Open);
        //        FileInfo fi = new FileInfo(caminhoArquivoENome);

        //        //pego o tamanho do FileInfo para ser o tamanho do Array ;
        //        long temp = fi.Length;
        //        int val = Convert.ToInt32(temp);
        //        Byte[] copy = new Byte[val];

        //        //Copio FileStream para o array de Bytes e dps o fecho;
        //        file.Read(copy, 0, val);
        //        file.Close();

        //        //Chamo minha classe serializadora e mando serializar meu Array;
        //        string copySerializado = Serializador.Serializar(copy);

        //        //parametros recebidos pelo Upload => nome do arquivo, xml, endereço, nome, senha
        //        msg = WService.Upload(nomeEExtensao, copySerializado, D.FtpServidor + "/" + D.FtpDiretorio,);

        //        if (!msg.Equals("sucesso!"))
        //        {
        //            Drop Arquivo = new Drop();

        //            Arquivo.Descricao = caminhoArquivoENome;
        //            Arquivo.DescricaoSecundaria = nomeEExtensao;
        //        }
        //        Cursor.Current = Cursors.Default;

        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        Cursor.Current = Cursors.Default;
        //        if (!msg.Equals("sucesso!"))
        //        {
        //            Drop Arquivo = new Drop();

        //            Arquivo.Descricao = caminhoArquivoENome;
        //            Arquivo.DescricaoSecundaria = nomeEExtensao;

        //        }
        //        return ex.Message;
        //    }
        //    return "Implementar!";
        //}

    }
}
