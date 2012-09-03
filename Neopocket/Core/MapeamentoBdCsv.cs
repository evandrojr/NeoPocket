using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
// Only because of the progress bar
using System.Data.SqlServerCe;
using Neopocket.Utils;
using Neopocket.Forms;
using System.Data;


namespace Neopocket
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
        public NeoCsv.Csv csvArquivo;
        public List<string> LstCamposData = new List<string>();
        public List<string> LstCamposBit = new List<string>();
        public List<string> LstCamposReal = new List<string>();
        public static string CampoBusca = "";
        public string TabelaCsvNomeExato;
        private static TextWriter log = null;
        public static int InsercaoQtd = 0, ErrosQtd = 0;
        private Bd bd; // O banco de dados usado para a carga de dados
        private static String caminhoLog = String.Empty;


        public MapeamentoBdCsv(string tabela, string arquivoCsv, Bd banco, String pCaminhoLog)
        {
            bd = banco;
            TabelaBdNome = tabela;
            caminhoLog = pCaminhoLog;
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

        public static void LogGrava(string mensagem)
        {
            LogBuilder.DEPRECIADO_Append(caminhoLog, mensagem, true);
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


        #region [ Busca o valor de um campo no arquivo csv ]

        private String BuscaValorCampo(String CsvCampo, NeoCsv.Csv csvArquivo)
        {
            for (int i = 0; i < csvArquivo.Linha.LstCsvColuna.Count; ++i)
            {
                if (CsvCampo == csvArquivo.Linha.LstCsvColuna[i].Nome)
                    return csvArquivo.Linha.LstCsvColuna[i].Valor;
            }
            throw new Exception("Não foi possível encontrar o campo " + CsvCampo + " no arquivo " + csvArquivo.ArquivoNome);
        }

        #endregion

        private void fieldValueSet(Dictionary<string, int> fieldIndexByFieldNameDic, SqlCeUpdatableRecord rec, 
                                    string DbField, object value){

            rec.SetValue(fieldIndexByFieldNameDic[DbField.ToLower()], (object) value);
        }

        #region [ Executar ]

        public void Executar(Boolean limparTabela)
        {
            int contadorLinha = 1;
            string valor="", sql, sdata;
            DateTime testeData;
            ErrosQtd = 0;
            Dictionary<string, int> fieldIndexByFieldNameDic;
            string errorMsg = "";


            fieldIndexByFieldNameDic = D.Bd.FieldIndexesFill(TabelaBdNome);

            SqlCeCommand cmd = new SqlCeCommand();
            SqlCeResultSet rs;
            SqlCeUpdatableRecord reg;

            cmd.Connection = D.Bd.Con;
            cmd.CommandText = TabelaBdNome;
            cmd.CommandType = CommandType.TableDirect;

            rs = cmd.ExecuteResultSet(ResultSetOptions.Updatable);
            reg = rs.CreateRecord();

            try
            {
                LogGrava("===== " + TabelaBdNome + " ======");
                if (limparTabela)
                {
                    sql = "Delete from " + TabelaBdNome;
                    LogGrava("[ " + sql + " ]");
                    bd.ExecuteNonQuery(sql);
                }
                while (csvArquivo.LerLinha())
                {
                    for (int i = 0; i < LstLigacao.Count; ++i)
                    {
                        valor = BuscaValorCampo(LstLigacao[i].CsvCampo, csvArquivo);
                        valor = valor.Trim();
                        //Para a funcao igual usada na busca das listas de campo data e Bit
                        CampoBusca = LstLigacao[i].BdCampo;
                        //Define códigos cliente_pocket para serem sempre = NULL pois deve-se usar o ID_Cliente que vier
                        if (LstLigacao[i].BdCampo == "id_cliente_pocket")
                        {
                            //valor = "null";
                            fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object)null);
                        }
                        //Verifica se é nulo ou vazio
                        if (valor == "" || valor.ToLower() == "null")
                        {
                            //valor = "null";
                            //sb2.Append(valor);
                            fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object)null);
                        }
                        else
                        {
                            //Verifica se é um campo data
                            if (LstCamposData.Exists(igual))
                            {
                                sdata = DataPtParaBd(valor);
                                testeData = Convert.ToDateTime(sdata, D.CultureInfoBRA.DateTimeFormat);
                                fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object)testeData);
                            }
                            else
                                //Verifica se é um campo bit
                                if (LstCamposBit.Exists(igual))
                                {
                                    if (valor == "1")
                                        fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object) true);
                                    else
                                        fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object) false);
                                }
                                else
                                    //Verifica se é um campo Real
                                    if (LstCamposReal.Exists(igual))
                                    {
                                        //valor = valor.Replace(",", ".");
                                        fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object)Convert.ToDouble(valor, D.CultureInfoBRA));
                                    }
                                    else
                                        //Caso normal, dado nao é data e nao é nulo
                                        fieldValueSet(fieldIndexByFieldNameDic, reg, CampoBusca, (object) valor);
                        }
                    }
                    rs.Insert(reg);
                    contadorLinha++;
                }
                LogGrava("Erros que tentaram ser reinseridos " + ErrosQtd);
            }
            catch (Exception ex)
            {
                errorMsg = "Erro importando do store: tabela " + TabelaBdNome + " campo '" + CampoBusca + "' valor '" + valor + "'" + Environment.NewLine + ex.Message + " " + ex.StackTrace;
                DadoImportacao.ErrosQtd++;
                DadoImportacao.LogGrava(errorMsg);
                FE f = new FE("Erro", errorMsg);
                f.ShowDialog();
            }
            finally
            {
                csvArquivo.Fechar();
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                if(errorMsg != "")
                    throw new Exception(errorMsg);
            }
        }
        #endregion
    }
}
