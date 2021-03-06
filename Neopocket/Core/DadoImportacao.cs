﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.SqlServerCe;
using System.Xml;
using Neopocket.Utils;

namespace Neopocket
{
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
                File.Delete(D.AplicacaoDiretorio + D.DEPRECIADO_APP_LOGFILENAME);
            }
            catch { }
            log = new StreamWriter(D.AplicacaoDiretorio + D.DEPRECIADO_APP_LOGFILENAME);
            log.Close();
        }

        public static void LogGrava(string mensagem)
        {
            log = new StreamWriter(D.AplicacaoDiretorio + D.DEPRECIADO_APP_LOGFILENAME, true);
            log.WriteLine(mensagem);
            log.Close();
        }



        //private static bool igual(String a)
        //{
        //    if (a == CampoBusca)
        //        return true;
        //    else
        //        return false;
        //}

        //public static string DataPtParaBd(string data)
        //{
        //    return data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);
        //}

        //public void Executar()
        //{
        //    StringBuilder sb1 = new StringBuilder();
        //    StringBuilder sb2 = new StringBuilder();
        //    string valor, data, sql;
        //    DateTime testeData;
        //    ErrosQtd = 0;
        //    int IdClientePocket = 1000;

        //    LogGrava("===== " + TabelaBdNome + " ======");
        //    sql="Delete from " + TabelaBdNome;
        //    LogGrava(sql);
        //    Globals.Bd.ExecuteNonQuery(sql);

        //    LogGrava("Tabela " + TabelaBdNome + " possue " + DtTabelaXml.Rows.Count + " registros");
        //    LogGrava("Memoria usada antes da coleta " + GC.GetTotalMemory(false));
        //    GC.Collect();
        //    LogGrava("Memoria usada após a coleta " + GC.GetTotalMemory(true));

        //    SqlCeTransaction dbTrans = null;
        //    sb1 = new StringBuilder();
        //    sb2 = new StringBuilder();
        //    for (int l = 0; l < DtTabelaXml.Rows.Count; ++l)
        //    {
        //        dbTrans = Globals.Bd.Con.BeginTransaction();
        //        sb1.Remove(0, sb1.Length);
        //        sb2.Remove(0, sb2.Length);
        //        sb1.DEPRECIADO_Append("insert into " + TabelaBdNome + " (");
        //        sb2.DEPRECIADO_Append(" values (");
        //        for (int i = 0; i < LstLigacao.Count; ++i)
        //        {
        //            sb1.DEPRECIADO_Append(LstLigacao[i].BdCampo);
        //            valor = DtTabelaXml.Rows[l][LstLigacao[i].XmlCampo].ToString();
        //            valor = valor.Replace("'", @"''");
        //            valor = valor.Trim();
        //            //Para a funcao igual usada na busca das listas de campo data e Bit
        //            CampoBusca = LstLigacao[i].BdCampo;
        //            //Define códigos cliente_pocket para valores que vierem vazios ou brancos
        //            if (LstLigacao[i].BdCampo == "id_cliente" && (valor == "" || valor == "0"))
        //            {
        //                valor = IdClientePocket.ToString();
        //                ++IdClientePocket;
        //            }
        //            //Verifica se é nulo ou vazio
        //            if (valor == "" || valor.ToLower() == "null"){
        //                valor = "null";
        //                sb2.DEPRECIADO_Append(valor);
        //            }
        //            else{
        //                //Verifica se é um campo data
        //                if (LstCamposData.Exists(igual))
        //                {
        //                    data = DataPtParaBd(valor);
        //                    try {
        //                        testeData = Convert.ToDateTime(data, Globals.CultureInfoBRA.DateTimeFormat);
        //                        data = "'" + data + "'";
        //                    } catch {
        //                        data = "null";
        //                    }
        //                    sb2.DEPRECIADO_Append(data);
        //                }
        //                else
        //                    //Verifica se é um campo bit
        //                    if (LstCamposBit.Exists(igual))
        //                    {
        //                        if (valor == "1")
        //                            sb2.DEPRECIADO_Append("'true'");
        //                        else
        //                            sb2.DEPRECIADO_Append("'false'");
        //                    }
        //                    else
        //                        //Verifica se é um campo Real
        //                        if (LstCamposReal.Exists(igual))
        //                        {
        //                            valor = valor.Replace(",",".");
        //                            sb2.DEPRECIADO_Append(valor);
        //                        }
        //                    else                            
        //                    //Caso normal, dado nao é data e nao é nulo
        //                    sb2.DEPRECIADO_Append("'" + valor + "'");
        //            }
        //            if (i < LstLigacao.Count - 1)
        //            {
        //                sb1.DEPRECIADO_Append(",");
        //                sb2.DEPRECIADO_Append(",");
        //            }
        //        }
        //        sb1.DEPRECIADO_Append(")");
        //        sb2.DEPRECIADO_Append(")");
        //        Globals.Bd.ImportaLinha(sb1.ToString() + sb2.ToString(), dbTrans);
        //        dbTrans.Commit();
        //        if (l % 50 == 0)
        //            GC.Collect();
        //    }
        //    LogGrava("Erros que tentaram ser reinseridos " + ErrosQtd);
        //}

        //public void XMLSchemaSemiAutoCarga(string caminho)
        //{

        //    XmlDocument = new XmlDocument();
        //    XmlDocument.Load(caminhoXml + ArquivoXmlNome);

        //    XmlNode node = XmlDocument.SelectSingleNode(caminho);
        //    XmlNodeList filhos = node.ChildNodes;

        //    foreach (XmlNode filho in filhos)
        //        DtTabelaXml.Columns.Add(filho.Name);
        //    DtTabelaXml.ReadXml(caminhoXml + ArquivoXmlNome);

        //}


    }
}
