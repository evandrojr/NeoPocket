//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.Text;
//using System.Configuration;
//using System.Data;
//using System.IO;
//using System.Data.Common;
//using FirebirdSql.Data.FirebirdClient;//provider do SGBD FireBird
//using System.Text.RegularExpressions;
//using System.Xml;
//using System.Xml.Serialization;
//using System.Runtime.Serialization;

//namespace NeoSync
//{
//    public static class Sincronizacao
//    {
//        static MapeamentoBdCsv d = null;
//        public static string ArquivoEnviarNome="", ArquivoClientesReceberNomeBase="";
        
//        static List<string> lstArquivosParaCompactar = new List<string>();
//        public static string ClienteEnviarNome, PedidoEnviarNome, ItensPedidoEnviarNome, ItensPedidoGradeEnviarNome;
//        private static string pClienteArqNome = "np_cliente.xml";
//        private static string pPedidoArqNome = "np_pedido.xml";
//        private static string pItemPedidoArqNome = "np_item_pedido.xml";        
//        private static string pItemPedidoGradeArqNome = "np_item_pedido_grade.xml";


//        static public void Iniciar()
//        {
//            lstArquivosParaCompactar.Clear();
//            DadoImportacao.LogCria();
//        }

//        /*
//         * Método que percorre  tds as conexoes abertas e as fecha
//         */
//        public static bool ArquivosDescompactar()
//        {

//            bool sucesso = true;
//            try
//            {
//                NeoZip.Zip.UnzipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoComumReceberNome, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
//                NeoZip.Zip.UnzipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoClientesReceberNomeBase + ".zip", D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio);
//            }
//            catch(Exception ex) 
//            {
//                sucesso = false;
//                return sucesso;
//            }
//            return sucesso;
//        }


//        //public static void LimparTabelasTempBd(){
//        //    string sql = "";
//        //    sql = "Delete from pedido";
//        //    BdTemp.ExecuteNonQuery(sql);
//        //    sql = "Delete from produto";
//        //    D.Bd.ExecuteNonQuery(sql);
//        //    sql = "Delete from cliente";
//        //    BdTemp.ExecuteNonQuery(sql);
//        //    sql = "Delete from cidade";
//        //    D.Bd.ExecuteNonQuery(sql);
//        //    sql = "Delete from especie_financeira";
//        //    D.Bd.ExecuteNonQuery(sql);
//        //    sql = "Delete from forma_pagamento";
//        //    D.Bd.ExecuteNonQuery(sql);
//        //}

//        /// <summary>
//        /// Carrega no banco definitivo sobre os novos
//        /// </summary>
//        public static void CarregaClienteCsv(string arquivo)
//        {
            
//            d = new MapeamentoBdCsv("cliente", arquivo, D.Bd);
//            d.ClienteCarregar();
//        }

//        public static void CarregaPedidoCsv(string arquivo)
//        {

//            d = new MapeamentoBdCsv("pedido", arquivo, D.Bd);
//            d.PedidoCarregar();
//        }

//        public static void CarregaItensDoPedidoCsv(string arquivo)
//        {

//            d = new MapeamentoBdCsv("item_pedido", arquivo, D.Bd);
//            d.ItensPedidoCarregar();
//        }


//        public static void RemoveClientesAntigos()
//        {
//            D.Bd.ExecuteNonQuery("Delete from cliente where status <> 'N'");
//        }

 


//        public static void ArquivosCompactar()
//        {
//            StringBuilder sb = new StringBuilder();

//            DateTime agora = new DateTime();
//            agora = DateTime.Now;
//            ArquivoEnviarNome = "STORERM";
//            NeoZip.Zip.ZipFiles(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ArquivoEnviarNome, lstArquivosParaCompactar);
//        }

//        public static void ClientesSelecionarParaEnvioCSV()
//        {
//            //int n;
//            //n = -1;
//            //n = D.Bd.I("select count(id_cliente) from cliente where status = 'N'", false);
//            //if (n > 0)
//            //{
//            //    string qry = "select * from cliente where status = 'N'";
//            //    DataTable dtClientesNovos = D.Bd.DataTablePreenche(qry);
//            //    //foreach (DataRow row in dtClientesNovos.Rows)
//            //    //{
//            //    //    if(row["fldName"] = "")


//            //    //}



//            //    NeoCsv.Csv csv = new NeoCsv.Csv();
//            //    ClienteEnviarNome = montarNomeArquivo("CLT") + ".zip";
//            //    csv.EscreveCsv(dtClientesNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
//            //    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ClienteEnviarNome);
//            //}
//        }



//        public static void PedidosSelecionarParaEnvioCSV()
//        {
////            int n;
////            n=-1;
////            n = D.Bd.I("select count(id_pedido) from pedido where status = 'N'", false);
////            if (n > 0)
////            {
////                    string qry = "select * from pedido where status = 'N'";
////                    DataTable dtPedidosNovos = D.Bd.DataTablePreenche(qry);
////                    PedidoEnviarNome = montarNomeArquivo("PEDIDOS") + ".csv";
////                    NeoCsv.Csv csv = new NeoCsv.Csv();
////                    csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);
////                    lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + PedidoEnviarNome);

////                    n = -1;
////                    n = D.Bd.I(@"
////                    SELECT
////                            count(pedido.id_pedido)
////                    FROM
////                            item_pedido INNER JOIN
////                            pedido ON item_pedido.id_pedido = pedido.id_pedido
////                    WHERE     
////                            (pedido.status = 'N')", false);

////                    if (n > 0)
////                    {
////                        qry = @"
////                        select 
////                                item_pedido.*
////                        from 
////                                item_pedido, pedido
////                        where
////                                pedido.id_pedido=item_pedido.id_pedido
////                             and
////                                pedido.status = 'N'";
////                        dtPedidosNovos = D.Bd.DataTablePreenche(qry);
////                        csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
////                        ItensPedidoEnviarNome = montarNomeArquivo("ITENSDOPEDIDO") + ".csv";
////                        lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoEnviarNome);
////                    }

////                    n = -1;
////                    n = D.Bd.I(@"
////                    SELECT
////                            count(pedido.id_pedido)
////                    FROM
////                            item_pedido_grade INNER JOIN
////                            pedido ON item_pedido_grade.id_pedido = pedido.id_pedido
////                    WHERE     
////                            (pedido.status = 'N')", false);
////                    if (n > 0)
////                    {
////                        qry = @"
////                        select 
////                                item_pedido_grade.*
////                        from 
////                                item_pedido_grade, pedido
////                        where
////                                pedido.id_pedido=item_pedido_grade.id_pedido
////                             and
////                                pedido.status = 'N'";
////                        dtPedidosNovos = D.Bd.DataTablePreenche(qry);
////                        ItensPedidoGradeEnviarNome = montarNomeArquivo("ITENSDOPEDIDOGRADE") + ".csv";
////                        csv.EscreveCsv(dtPedidosNovos, D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
////                        lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + ItensPedidoGradeEnviarNome);
////                    }
////            }
//        }



//        //Se conseguir ler duas linhas é porque tem conteúdo
//        public static void SelecionaArquivosParaCompactarCSV()
//        {
//            TextReader tr = new StreamReader(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pClienteArqNome, System.Text.Encoding.Default);
//            tr.ReadLine();
//            if(tr.ReadLine() != null){
//                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pClienteArqNome);
//            }
//            tr.Close();

//            tr = new StreamReader(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pPedidoArqNome, System.Text.Encoding.Default);
//            tr.ReadLine();
//            if (tr.ReadLine() != null)
//            {
//                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pPedidoArqNome);
//            }
//            tr.Close();

//            tr = new StreamReader(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pItemPedidoArqNome, System.Text.Encoding.Default);
//            tr.ReadLine();
//            if (tr.ReadLine() != null)
//            {
//                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pItemPedidoArqNome);
//            }
//            tr.Close();

//            tr = new StreamReader(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pItemPedidoGradeArqNome, System.Text.Encoding.Default);
//            tr.ReadLine();
//            if(tr.ReadLine() != null)
//            {
//                lstArquivosParaCompactar.Add(D.AplicacaoDiretorio + D.TabelasSincronizacaoDiretorio + pItemPedidoGradeArqNome);
//            }
//            tr.Close();
//        }

//        private static StringBuilder montaCodigoVendedor(string codigoVendedor)
//        {
//            StringBuilder nomeDoArquivo = new StringBuilder(6);
//            nomeDoArquivo.Append(codigoVendedor.PadLeft(6, '0'));
//            return nomeDoArquivo;
//        }

//        private static StringBuilder montarNomeArquivo(string nomeDaTabela)
//        {
//            StringBuilder s = new StringBuilder(15);
//            return s;
//        }

//    }
//}
