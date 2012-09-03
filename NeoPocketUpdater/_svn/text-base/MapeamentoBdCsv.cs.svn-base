using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows.Forms; // Only because of the progress bar
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using System.Xml;
using Config;
using NeoCsv;

namespace NeoPocketUpdater
{

    public struct LigacaoCsv
    {
        public string BdCampo;
        public string CsvCampo;
    }

    
    public class MapeamentoBdCsv
    {
        List<LigacaoCsv> LstLigacao = new List<LigacaoCsv>();
        public string TabelaBdNome, CaminhoCsv, ArquivoCsvNome;
        public static string caminhoCsv;
        public NeoCsv.Csv csvArquivo ;
        public List<string> LstCamposData = new List<string>();
        public List<string> LstCamposBit = new List<string>();
        public List<string> LstCamposReal = new List<string>();
        public static string CampoBusca = null;
        public string TabelaCsvNomeExato;
        private static TextWriter log = null;
        public static int InsercaoQtd=0, ErrosQtd=0;
        private Bd bd; // O banco de dados usado para a carga de dados

        public MapeamentoBdCsv(string tabela, string arquivoCsv, Bd banco)
        {
            bd = banco;
            TabelaBdNome = tabela;
            csvArquivo = new NeoCsv.Csv(arquivoCsv);
            csvArquivo.ColumnsRead();
        }

        /// <summary>
        /// Ligacao adicionar texto
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="CsvCampo"></param>
        public void laT(string dbCampo, string CsvCampo)
        {
            LigacaoCsv lig = new LigacaoCsv();
            lig.BdCampo = dbCampo;
            lig.CsvCampo = CsvCampo;
            LstLigacao.Add(lig);
        }

        /// <summary>
        /// Ligacao adicionar data
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="CsvCampo"></param>
        public void laD(string dbCampo, string CsvCampo)
        {
            LigacaoCsv lig = new LigacaoCsv();
            lig.BdCampo = dbCampo;
            lig.CsvCampo = CsvCampo;
            LstLigacao.Add(lig);
            LstCamposData.Add(dbCampo);
        }

        /// <summary>
        /// Ligacao adicionar bit
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="CsvCampo"></param>
        public void laB(string dbCampo, string CsvCampo)
        {
            LigacaoCsv lig = new LigacaoCsv();
            lig.BdCampo = dbCampo;
            lig.CsvCampo = CsvCampo;
            LstLigacao.Add(lig);
            LstCamposBit.Add(dbCampo);
        }

        /// <summary>
        /// Ligacao adicionar Real
        /// </summary>
        /// <param name="dbCampo"></param>
        /// <param name="CsvCampo"></param>
        public void laR(string dbCampo, string CsvCampo)
        {
            LigacaoCsv lig = new LigacaoCsv();
            lig.BdCampo = dbCampo;
            lig.CsvCampo = CsvCampo;
            LstLigacao.Add(lig);
            LstCamposReal.Add(dbCampo);
        }


        public static void LogCria()
        {
            try
            {
                File.Delete(D.AplicacaoDiretorio + D.LogImportacaoArquivo);
            }
            catch { }
            log = new StreamWriter(D.AplicacaoDiretorio + D.LogImportacaoArquivo);
            log.Close();
        }

        public static void LogGrava(string mensagem)
        {
            log = new StreamWriter(D.AplicacaoDiretorio + D.LogImportacaoArquivo, true);
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


        private string  buscaValorCampo(string CsvCampo, NeoCsv.Csv csvArquivo){
            for (int i = 0; i < csvArquivo.Linha.LstCsvColuna.Count; ++i)
            {
                if (CsvCampo == csvArquivo.Linha.LstCsvColuna[i].Nome)
                    return csvArquivo.Linha.LstCsvColuna[i].Valor;
            }
            throw new Exception("Não foi possível encontrar o campo " + CsvCampo + " no arquivo " + csvArquivo.ArquivoNome);
        }

        //public void ExecutarQueryPreparada(bool LimparTabela)
        //{
        //    StringBuilder sb1, sb2;

        //    SqlCeCommand cmd = null;
        //    string valor, sql, sdata, debugValor;
        //    DateTime testeData;
        //    ErrosQtd = 0;
        //    int IdClientePocket;

        //    try
        //    {
        //        IdClientePocket=D.Bd.I("Select max(id_cliente_pocket) + 1 from cliente", true);
        //    }
        //    catch
        //    {
        //        IdClientePocket = 1;
        //    }

        //    LogGrava("===== " + TabelaBdNome + " ======");
        //    if (LimparTabela)
        //    {
        //        sql = "Delete from " + TabelaBdNome;
        //        LogGrava(sql);
        //        bd.ExecuteNonQuery(sql);
        //    }
        //    LogGrava("Memória usada antes da coleta " + GC.GetTotalMemory(false));
        //    GC.Collect();
        //    LogGrava("Memória usada após a coleta " + GC.GetTotalMemory(true));

        //    SqlCeTransaction dbTrans = null;
        //    while (csvArquivo.LerLinha())
        //    {
        //        dbTrans = bd.Con.BeginTransaction();
        //        cmd = new SqlCeCommand();
        //        sb1 = new StringBuilder("insert into ", 500);
        //        sb2 = new StringBuilder(" values (", 500);
        //        sb1.Append(TabelaBdNome);
        //        sb1.Append(" (");
        //        debugValor = "";

        //        for (int i = 0; i < LstLigacao.Count; ++i)
        //        {
        //            sb1.Append(LstLigacao[i].BdCampo);
        //            sb2.Append("@");
        //            sb2.Append(LstLigacao[i].BdCampo);

        //            valor = buscaValorCampo(LstLigacao[i].CsvCampo, csvArquivo);
        //            valor = valor.Replace("'", @"''");
        //            valor = valor.Trim();
        //            debugValor += "(" + LstLigacao[i].BdCampo + " = " + valor + ") ";
        //            //Para a funcao igual usada na busca das listas de campo data e Bit
        //            CampoBusca = LstLigacao[i].BdCampo;
        //            //Define códigos cliente_pocket para valores que vierem vazios ou brancos
        //            if (LstLigacao[i].BdCampo == "id_cliente_pocket" && (valor == "" || valor == "0"))
        //            {
        //                valor = IdClientePocket.ToString();
        //                ++IdClientePocket;
        //            }
        //            //Verifica se é nulo ou vazio
        //            if (valor == "" || valor.ToLower() == "null")
        //            {
        //                cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, DBNull.Value);
        //            }
        //            else
        //            {
        //                //Verifica se é um campo data
        //                if (LstCamposData.Exists(igual))
        //                {
        //                    sdata = DataPtParaBd(valor);
        //                    try
        //                    {
        //                        testeData = Convert.ToDateTime(sdata, D.CultureInfo.DateTimeFormat);
        //                        cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, testeData);
        //                    }
        //                    catch
        //                    {
        //                        cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, "null");
        //                    }
        //                }
        //                else
        //                    //Verifica se é um campo bit
        //                    if (LstCamposBit.Exists(igual))
        //                    {
        //                        if (valor == "1")
        //                            cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, true);
        //                        else
        //                            cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, false);
        //                    }
        //                    else
        //                        //Verifica se é um campo Real
        //                        if (LstCamposReal.Exists(igual))
        //                        {
        //                            valor = valor.Replace(",", ".");
        //                            cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, Convert.ToDouble(valor)); ;
        //                        }
        //                        else
        //                            //Caso normal, dado nao é data e nao é nulo
        //                            cmd.Parameters.Add('@' + LstLigacao[i].BdCampo, valor); ;
        //            }
        //            if (i < LstLigacao.Count - 1)
        //            {
        //                sb1.Append(",");
        //                sb2.Append(",");
        //            }
                    
        //        }
        //        sb1.Append(")");
        //        sb2.Append(")");
        //        sb1.Append(sb2.ToString());
        //        cmd.Connection = D.Bd.Con;
        //        cmd.CommandText=sb1.ToString();
        //        cmd.Transaction = dbTrans;
        //        foreach (SqlCeParameter Parameter in cmd.Parameters)
        //        {
        //            Parameter.IsNullable = true;
        //            if (Parameter.Value == null || Parameter.Value.ToString() == "null")
        //            {
        //                Parameter.Value = DBNull.Value;
        //            }
        //        }
        //        cmd.ExecuteNonQuery();
        //        dbTrans.Commit();
        //        //if (l % 50 == 0)
        //        //    GC.Collect();
        //    }
        //    csvArquivo.Fechar();
        //    LogGrava("Erros que tentaram ser reinseridos " + ErrosQtd);

        //}

        public void Executar(bool limparTabela)
        {
            StringBuilder sb1, sb2, data;
            int contadorLinha = 1;

            string valor, sql, sdata;
            DateTime testeData;
            ErrosQtd = 0;
            int IdClientePocket = 1000;

            LogGrava("===== " + TabelaBdNome + " ======");
            if (limparTabela)
            {
                sql = "Delete from " + TabelaBdNome;
                LogGrava(sql);
                bd.ExecuteNonQuery(sql);
            }
            //LogGrava("Memória usada antes da coleta " + GC.GetTotalMemory(false));
            //GC.Collect();
            //LogGrava("Memória usada após a coleta " + GC.GetTotalMemory(true));

//            GC.WaitForPendingFinalizers();

            SqlCeTransaction dbTrans = null;
            while(csvArquivo.LerLinha())
            {

                dbTrans = bd.Con.BeginTransaction();
                sb1 = new StringBuilder();
                sb2 = new StringBuilder();
                sb1.Append("insert into ");
                sb1.Append(TabelaBdNome);
                sb1.Append(" (");
                sb2.Append(" values (");
                for (int i = 0; i < LstLigacao.Count; ++i)
                {
                    sb1.Append(LstLigacao[i].BdCampo);
                    valor = buscaValorCampo(LstLigacao[i].CsvCampo, csvArquivo);
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
                            sdata = DataPtParaBd(valor);
                            try {
                                testeData = Convert.ToDateTime(sdata, D.CultureInfoBRA.DateTimeFormat);
                                data = new StringBuilder("'");
                                data.Append(sdata);
                                data.Append("'");
                            } catch {
                                data = new StringBuilder("null");
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
                bd.ImportaLinha(sb1.ToString() + sb2.ToString(), dbTrans);
                dbTrans.Commit();
                //if (l % 50 == 0)
                //    GC.Collect();
                contadorLinha++;
            }
            csvArquivo.Fechar();
            LogGrava("Erros que tentaram ser reinseridos " + ErrosQtd);
        }

    }

}
