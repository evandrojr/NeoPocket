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
using NeoDebug;

namespace NeoPocketUpdater
{
    public class Bd
    {

        private string conStr;
        private SqlCeConnection con = null;

        public Bd()
        {

        }

        /// <summary>
        /// Trata números reais em formato português para serem inseridos no BD
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static string RealPtParaBd(string valor)
        {
            valor = valor.Replace("R$", "");
            valor = valor.Replace(".", "");
            valor = valor.Replace(",", ".");
            return valor;
        }

        public List<Guid> ListGuid(string sql)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            List<Guid> lstGuid = new List<Guid>();

            try
            {
                cmd = new SqlCeCommand(sql, Con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstGuid.Add((Guid)(reader[0]));
                }
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui obter ListGuid " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return lstGuid;
        }

        public static string Numerico(double d)
        {
            return S(d.ToString(D.CultureInfoEUA));  
        }

        /// <summary>
        /// Trata a entrade de dados
        /// </summary>
        /// <param name="entradaASerTratada"></param>
        /// <returns></returns>
        public static string S(string entradaASerTratada)
        {
            entradaASerTratada = entradaASerTratada.Trim();
            entradaASerTratada = entradaASerTratada.Replace(System.Environment.NewLine, "");
            return entradaASerTratada.Replace("'", "''");
        }

        /// <summary>
        /// Sanitaniza e cola aspas simples ao redor
        /// </summary>
        /// <param name="entradaASerTratada"></param>
        /// <returns></returns>
        public static string SA(string entradaASerTratada)
        {
            entradaASerTratada = entradaASerTratada.Trim();
            entradaASerTratada = entradaASerTratada.Replace(System.Environment.NewLine, "");
            entradaASerTratada = entradaASerTratada.Replace("'", "''");
            return "'" + entradaASerTratada + "'";
        }


        public static string S(int entradaASerTradada)
        {
            return S(entradaASerTradada.ToString());
        }
        public static string S(double entradaASerTradada)
        {
            return S(entradaASerTradada.ToString());
        }
        public static string S(DateTime entradaASerTradada)
        {
            return S(entradaASerTradada.ToString());
        }

        /// <summary>
        /// Tratando a entrada de dados , eliminando aspas simples  e tratando
        /// valores nulos . 
        /// 
        /// </summary>
        /// <param name="entradaASerTradada"></param>
        /// <returns></returns>
        public static string SN(string entradaASerTradada)
        {
            if (entradaASerTradada == "")
                return "NULL";
            else
                return "'" + S(entradaASerTradada) + "'";
        }
        public static string SN(int entradaASerTradada)
        {
            return SN(entradaASerTradada.ToString());
        }
        public static string SN(double entradaASerTradada)
        {
            return SN(entradaASerTradada.ToString());

        }
        public static string SN(DateTime entradaASerTradada)
        {
            return SN(entradaASerTradada.ToString());
        }

        public static string SN(IntN entradaASerTradada)
        {
            if (entradaASerTradada.Iniciada)
                return entradaASerTradada.V.ToString();
            else
                return "NULL";
        }

        public static string SN(DoubleN entradaASerTradada)
        {
            if (entradaASerTradada.Iniciada)
                return entradaASerTradada.V.ToString();
            else
                return "NULL";
        }

        public static string SN(BoolN entradaASerTradada)
        {
            if (entradaASerTradada.Iniciada)
                return "'" + entradaASerTradada.V.ToString() + "'";
            else
                return "NULL";
        }

        public static string SN(DateTimeN entradaASerTradada)
        {
            if (entradaASerTradada.Iniciada)
                return "'" + entradaASerTradada.V.ToString() + "'";
            else
                return "NULL";
        }


        /// <summary>
        /// Tratando a entrada de dados , eliminando aspas simples  e tratando
        /// valores nulos(numericos) . 
        /// 
        /// </summary>
        /// <param name="entradaASerTradada"></param>
        /// <returns></returns>
        /// 
        public static string SZ(int entradaASerTradada)
        {
            if (entradaASerTradada == 0)
                return "NULL";
            else
                return "'" + S(entradaASerTradada) + "'";
        }

        /// <summary>
        /// Tratando a entrada de dados, valores null e ""  . 
        /// Recebe o valor correto ou zero se for nulo
        /// </summary>
        /// <param name="entradaASerTradada"></param>
        /// <returns></returns>
        /// 
        public static int IntZeroIfNull(object entrada)
        {
            if (entrada == null || entrada.ToString() == "")
                return 0;
            else
                return Convert.ToInt32(entrada);
        }

        /// <summary>
        /// Tratando a entrada de dados, valores null e ""  . 
        /// Recebe o valor correto ou zero se for nulo
        /// </summary>
        /// <param name="entradaASerTradada"></param>
        /// <returns></returns>
        /// 
        public static double DoubleZeroIfNull(object entrada)
        {
            if (entrada == null || entrada.ToString() == "")
                return 0;
            else
                return Convert.ToDouble(entrada);
        }

        public static string DataParaBd(DateTime dt)
        {
            return "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + "'";
        }

        public static string TimeStampParaBd(DateTime dt)
        {
            return "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second + "'";
        }


        public string ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }

        public SqlCeConnection Con
        {
            get { return con; }
        }

        public bool Connect()
        {
            con = new SqlCeConnection(conStr);		// get the connection
            try
            {
                con.Open();		// open the connection
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro conectando ao banco de dados");
                return false;
            }
            return true;
        }

        public int ImportaLinha(string sql, SqlCeTransaction trans)
        {
            SqlCeCommand cmd = null;
            int rowsAffected = -1;
            cmd = new SqlCeCommand(sql, Con, trans);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DadoImportacao.ErrosQtd++;
                DadoImportacao.LogGrava("Erro: " + sql + Environment.NewLine + ex.Message);
                FE f = new FE("Erro importando do store: ", sql + Environment.NewLine + ex.Message);
                f.ShowDialog();
                throw new Exception("Erro: " + sql + Environment.NewLine + ex.Message);
            }
            return rowsAffected;
        }

        public int ImportaLinha(string sql)
        {
            SqlCeCommand cmd = null;
            int rowsAffected = -1;
            cmd = new SqlCeCommand(sql, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DadoImportacao.ErrosQtd++;
                DadoImportacao.LogGrava("Erro: " + sql + Environment.NewLine + ex.Message);
                FE f = new FE("Erro importando do store: ", sql + Environment.NewLine + ex.Message);
                f.ShowDialog();
                throw new Exception("Erro: " + sql + Environment.NewLine + ex.Message);
            }
            return rowsAffected;
        }
        /// <summary>
        /// Usar para uma tabela so 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tabela"></param>
        /// <returns></returns>
        public DataTable DataTablePreenche(string sql, string tabela)
        {

            SqlCeCommand cmd = new SqlCeCommand(sql, Con);
            SqlCeDataAdapter da = new SqlCeDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tabela);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// Cria e preenche um DataTable com o resultado do sql,
        /// pode usar com mais de uma tabela
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        public DataTable DataTablePreenche(string sql)
        {
            SqlCeCommand cmd = new SqlCeCommand(sql, Con);

            SqlCeDataReader reader = null;
            DataTable dt = new DataTable();
            reader = cmd.ExecuteReader();
            DataSet ds = convertDataReaderToDataSet(reader);
            return ds.Tables[0];
        }

        ///    <summary>
        /// Converts a SqlCeDataReader to a DataSet
        ///    <param name='reader'>
        /// SqlDataReader to convert.</param>
        ///    <returns>
        /// DataSet filled with the contents of the reader.</returns>
        ///    </summary>
        public static DataSet convertDataReaderToDataSet(SqlCeDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                // Create new data table

                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                        // Add the column definition to the data table
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }

        public int ExecuteNonQuery(string sql)
        {
            SqlCeCommand cmd = null;
            int rowsAffected = -1;

            cmd = new SqlCeCommand(sql, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                FE.Show(ex);
            }
            return rowsAffected;
        }

        public int ExecuteNonQuery(string sql, SqlCeTransaction trans)
        {
            SqlCeCommand cmd = null;
            int rowsAffected = -1;

            cmd = new SqlCeCommand(sql, Con, trans);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                FE.Show(ex);
            }
            return rowsAffected;
        }

        public double N(string qry)
        {  // Get Number
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            double r = -1;

            try
            {
                cmd = new SqlCeCommand(qry, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToDouble(reader[0]);
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message, "Erro executando Bd.N sql: " + qry, ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public double N(string qry, SqlCeTransaction bdTrans)
        {  // Get Number
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            double r = -1;

            try
            {
                cmd = new SqlCeCommand(qry, Con, bdTrans);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToDouble(reader[0]);
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message, "Erro executando Bd.N sql: " + qry, ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public int I(string qry)
        {  // Get Int32
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            int r = -1;

            try
            {
                cmd = new SqlCeCommand(qry, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message + " sql: " + qry, "Erro executando Bd.I", ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public int I(string qry, SqlCeTransaction dbTrans)
        {  // Get Int32
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            int r = -1;

            try
            {
                cmd = new SqlCeCommand(qry, Con, dbTrans);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToInt32(reader[0]);
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message + " sql: " + qry, "Erro executando Bd.I", ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }


        public int I(string qry, bool GeraExcecao)
        {  // Get Int32

            if (!GeraExcecao)
                return I(qry);

            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            int r = -1;

            cmd = new SqlCeCommand(qry, Con);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToInt32(reader[0]);
            reader.Close();
            return r;
        }

        public string T(string qry)
        {  // Le uma célula de texto
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            string r = "";

            try
            {
                cmd = new SqlCeCommand(qry, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToString(reader[0]);
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message + " sql: " + qry, "Erro executando Bd.T", ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public string T(string qry, bool modoSilencioso)
        {
            if (!modoSilencioso)
                return T(qry);

            // Le uma célula de texto
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            string r = "";

            cmd = new SqlCeCommand(qry, Con);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToString(reader[0]);
            reader.Close();
            return r;
        }


        public double N(string qry, bool silencioso)
        {  // Get Number
            if (silencioso == false)
            {
                return N(qry);
            }
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            double r = -1;

            try
            {
                cmd = new SqlCeCommand(qry, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToDouble(reader[0]);
            }
            catch
            {
                return Double.NaN;
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public List<IT> LstIT(string sql)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            List<IT> lstIT = new List<IT>();

            try
            {
                cmd = new SqlCeCommand(sql, Con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstIT.Add(new IT(Convert.ToInt32(reader[0]), Convert.ToString(reader[1])));
                }
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui obter lstIT " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return lstIT;
        }

        public List<int> ListInt(string sql)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            List<int> lstInt = new List<int>();

            try
            {
                cmd = new SqlCeCommand(sql, Con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstInt.Add(Convert.ToInt32((reader[0])));
                }
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui obter ListInt " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return lstInt;
        }

        public List<int> ListInt(string sql, SqlCeTransaction bdTrans)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            List<int> lstInt = new List<int>();

            try
            {
                cmd = new SqlCeCommand(sql, Con, bdTrans);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstInt.Add(Convert.ToInt32((reader[0])));
                }
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui obter ListInt " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return lstInt;
        }


        /// <summary>
        /// Retorna uma linha do banco de dados
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlCeDataReader Linha(string sql)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;

            try
            {
                cmd = new SqlCeCommand(sql, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                return reader;
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui executar Bd.Linha " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return reader;
        }


        /// <summary>
        /// Retorna uma linha do banco de dados
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlCeDataReader Linha(string sql, SqlCeTransaction bdTrans)
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;

            try
            {
                cmd = new SqlCeCommand(sql, Con, bdTrans);
                reader = cmd.ExecuteReader();
                reader.Read();
                return reader;
            }
            catch (Exception Ex)
            {
                FE.Show("Não consegui executar Bd.Linha " + Ex.Message + " SQL = " + sql, "Erro", Ex.StackTrace);
            }
            return reader;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="campo"></param>
        /// <returns></returns>
        public IntN IntN(string qry, SqlCeTransaction dbTrans)
        {  // Get Int32
            SqlCeCommand cmd = null;
            IntN r = new IntN();

            try
            {
                cmd = new SqlCeCommand(qry, Con, dbTrans);
                r.V = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                FE.Show(ex.Message + " sql: " + qry, "Erro executando Bd.IntN", ex.StackTrace);
            }
            return r;
        }

        public static IntN IntN(SqlCeDataReader dr, string campo)
        {
            string v;
            try
            {
                v = dr[campo].ToString();
            }
            catch
            {
                return new IntN();
            }
            if (v != "")
                return new IntN(Convert.ToInt32(v));
            else
                return new IntN();
        }

        public static BoolN BoolN(SqlCeDataReader dr, string campo)
        {
            string v = dr[campo].ToString();
            if (v != "")
                return new BoolN(Convert.ToBoolean(v));
            else
                return new BoolN();
        }

        public static DoubleN DoubleN(SqlCeDataReader dr, string campo)
        {
            string v = dr[campo].ToString();
            if (v != "")
                return new DoubleN(Convert.ToDouble(v));
            else
                return new DoubleN();
        }

        public static DateTimeN DateTimeN(SqlCeDataReader dr, string campo)
        {
            string v = dr[campo].ToString();
            if (v != "")
                return new DateTimeN(Convert.ToDateTime(v));
            else
                return new DateTimeN();
        }

        public void DadosApagarCompletamente()
        {
            try
            {
                SqlCeTransaction t = con.BeginTransaction();
                ExecuteNonQuery("Delete from atributo", t);
                ExecuteNonQuery("Delete from cidade", t);
                ExecuteNonQuery("Delete from cliente", t);
                ExecuteNonQuery("Delete from especie_financeira", t);
                ExecuteNonQuery("Delete from forma_pagamento", t);
                ExecuteNonQuery("Delete from item_forma_pagamento", t);
                ExecuteNonQuery("Delete from funcionario", t);
                ExecuteNonQuery("Delete from grade", t);
                ExecuteNonQuery("Delete from item_atributo", t);
                ExecuteNonQuery("Delete from item_pedido", t);
                ExecuteNonQuery("Delete from item_pedido_grade", t);
                ExecuteNonQuery("Delete from item_tabela_preco", t);
                ExecuteNonQuery("Delete from parametro", t);
                ExecuteNonQuery("Delete from pedido", t);
                ExecuteNonQuery("Delete from produto", t);
                ExecuteNonQuery("Delete from produto_grade", t);
                ExecuteNonQuery("Delete from saldo_grade", t);
                ExecuteNonQuery("Delete from tabela_preco", t);
                t.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Debug.LogGrava(ex.Message);


            }
        }


    }
}




//        public List<string> BaseTableList() {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<string> tables = new List<string>();

//            string sql = "select tablename from pg_tables where tablename SIMILAR TO '%$" + Def.DbBaseTableSufix + "' ESCAPE '$' order by tablename"; 
////            string sql = "select tablename from pg_tables where tablename NOT SIMILAR TO 'pg_%$" + Def.DbBaseTableSufix + "' ESCAPE '$'"; 

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    tables.Add((string)reader[0]);
//                }
//            } catch(Exception Ex) {
//                FE.Show(Ex.Message, "Error Could not read the base tables", Ex.StackTrace);
//            }
//            return tables;
//        }

//        public List<string> BaseTableMvList() {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<string> tables = new List<string>();

//            string sql = "select tablename from pg_tables where tablename SIMILAR TO '%$" + Def.DbBaseTableMvSufix + "' ESCAPE '$' order by tablename";
//            //            string sql = "select tablename from pg_tables where tablename NOT SIMILAR TO 'pg_%$" + Def.DbBaseTableSufix + "' ESCAPE '$'"; 

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    tables.Add((string)reader[0]);
//                }
//            } catch (Exception Ex) {
//                FE.Show(Ex.Message, "Error Could not read the multivariate base tables", Ex.StackTrace);
//            }
//            return tables;
//        }


//        public List<double> GetNumberLst(string sql) { // Return a column
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<double> doubleLst = new List<double>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    doubleLst.Add(Convert.ToDouble(reader[0]));
//                }
//            } catch (Exception Ex) {
//                FE.Show("Could not GetDoubleLst " + Ex.Message + " SQL = " + sql, "Error", Ex.StackTrace);
//            }
//            return doubleLst;
//        }

//        public List<double> GetNumberLst(string sql, OdbcTransaction dbTrans) { // Return a column
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<double> doubleLst = new List<double>();

//            try {
//                cmd = new OdbcCommand(sql, con, dbTrans);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    doubleLst.Add(Convert.ToDouble(reader[0]));
//                }
//            } catch (Exception Ex) {
//                FE.Show("Could not GetNumberLst " + Ex.Message + " SQL = " + sql, "Error", Ex.StackTrace);
//            }
//            return doubleLst;
//        }

//        public List<double> GetNumberRowLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<double> doubleLst = new List<double>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                reader.Read();
//                for(int i=0; i < reader.FieldCount; ++i)
//                    doubleLst.Add(Convert.ToDouble(reader[i]));
//            } catch (Exception Ex) {
//                FE.Show("Could not GetDoubleRowLst " + Ex.Message + " SQL = " + sql, "Error", Ex.StackTrace);
//            }
//            return doubleLst;
//        }

//        public List<double> GetNumberRowLst(string sql, OdbcTransaction dbTrans) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<double> doubleLst = new List<double>();

//            try {
//                cmd = new OdbcCommand(sql, con, dbTrans);
//                reader = cmd.ExecuteReader();
//                reader.Read();
//                for (int i = 0; i < reader.FieldCount; ++i)
//                    doubleLst.Add(Convert.ToDouble(reader[i]));
//            } catch (Exception Ex) {
//                FE.Show("Could not GetDoubleRowLst " + Ex.Message + " SQL = " + sql, "Error", Ex.StackTrace);
//            }
//            return doubleLst;
//        }

//        public int ExecuteNonQuery(string sql) {
//            OdbcCommand cmd = null;
//            int rowsAffected = -1;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return -1;
//            }
//            cmd = new OdbcCommand(sql, con);
//            rowsAffected = cmd.ExecuteNonQuery();
//            return rowsAffected;
//        }




           

//        public int ExecuteNonQuery(string sql, bool silent) {
//            OdbcCommand cmd = null;
//            int rowsAffected = -1;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return -1;
//            }

//            if (silent) {
//                try {
//                    cmd = new OdbcCommand(sql, con);
//                    rowsAffected = cmd.ExecuteNonQuery();
//                } catch { }
//            } else {
//                try {
//                    cmd = new OdbcCommand(sql, con);
//                    rowsAffected = cmd.ExecuteNonQuery();
//                } catch(Exception Ex) {
//                    FE.Show(Ex.Message, "Error Could not execute the query: " + sql, Ex.StackTrace);
//                }
//            }
//            return rowsAffected;
//        }

//        public double[] GetNN(string qry) {  // Get Number
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            double[] r = new double[2];

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return r;
//            }
//            try {
//                cmd = new OdbcCommand(qry, con);
//                reader = cmd.ExecuteReader();
//                reader.Read();
//                r[0] = Convert.ToDouble(reader[0]);
//                r[1] = Convert.ToDouble(reader[1]);
//            } catch (Exception ex) {
//                FE.Show("Could not execute GetNN " + ex.Message + " SQL = " + qry, "Error", ex.StackTrace);
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }

//        public List<N4> GetN4Lst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<N4> N4Lst = new List<N4>();
   
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    N4Lst.Add(new N4(Convert.ToDouble(reader[0]), Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2]), Convert.ToDouble(reader[3])));
//                }
//            } catch (Exception Ex) {
//                FE.Show("Could not GetN4Lst " + Ex.Message + " SQL = " + sql, "Error", Ex.StackTrace);
//            }
//            return N4Lst;
//        }

//        public List<N3T> GetN3TLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<N3T> N3TLst = new List<N3T>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    N3TLst.Add(new N3T(Convert.ToDouble(reader[0]), Convert.ToDouble(reader[1]), Convert.ToDouble(reader[2]), Convert.ToString(reader[3])));
//                }
//            } catch (Exception Ex) {
//                MessageBox.Show("Could not GetN3TLst(string sql) " + Ex.Message + " SQL = " + sql, "Error");
//            }
//            return N3TLst;
//        }

//        public List<NTT> GetNTTLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<NTT> NTTLst = new List<NTT>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    NTTLst.Add(new NTT(Convert.ToDouble(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2])));
//                }
//            } catch (Exception Ex) {
//                MessageBox.Show("Could not GetNTTLst(string sql) " + Ex.Message + " SQL = " + sql, "Error");
//            }
//            return NTTLst;
//        }


//        public List<NT> GetNTLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<NT> NTLst = new List<NT>();
//            NT nt;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    nt = new NT(Convert.ToDouble(reader[0]), Convert.ToString(reader[1]));
//                    NTLst.Add(nt);
//                }
//            } catch (Exception Ex) {
//                MessageBox.Show("Could not GetNTLst " + Ex.Message + " SQL = " + sql, "Error");
//            }
//            return NTLst;
//        }

        
//        public List<NNT> GetNNTLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<NNT> NNTLst = new List<NNT>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    NNTLst.Add(new NNT(Convert.ToDouble(reader[0]), Convert.ToDouble(reader[1]), Convert.ToString(reader[2])));
//                }
//            } catch (Exception Ex) {
//                MessageBox.Show("Could not GetNNTLst " + Ex.Message + " SQL = " + sql, "Error");
//            }
//            return NNTLst;
//        }

//        public List<string> TableFieldsLst(string sql) {
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<string> stringLst = new List<string>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    stringLst.Add((string)reader[0]);
//                }
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message, "Error Could not read the base tables");
//            }
//            return stringLst;
//        }

//        public bool SchemaFill(Schema sch) {

//            OdbcCommand cmd = null;
//            cmd = new OdbcCommand("select * from  " + Def.DbBsTb + " Limit 1", con);
//            SchemaVariable var;
//            OdbcDataReader reader = cmd.ExecuteReader();
//            bool r = false;
//            string typeName;
//            try {
        
//                reader.Read();
//                for (int x = 0; x < reader.FieldCount; ++x) {
//                    var = new SchemaVariable();
//                    var.Name = reader.GetName(x).ToLowerInvariant();
//                    if (var.Name == Def.DbTableIdName) {
//                        continue;
//                    }
//                    typeName = reader.GetDataTypeName(x).ToLowerInvariant();
//                    if (typeName == "float4" || typeName == "float8" || typeName == "real" || typeName == "dbtype_i2" || typeName == "dbtype_i4" || typeName == "dbtype_ui1" || typeName == "dbtype_r8" || typeName == "dbtype_r4" || typeName == "dbtype_numeric" || typeName == "integer" || typeName == "float" || typeName == "numeric" || typeName == "number") {
//                        var.DataType = SchemaVariable.DataTypeEnum.Number;
//                        var.VariableTypeDetected = SchemaVariable.VariableTypeEnum.Continuous;
//                        var.VariableTypeUserSet = SchemaVariable.VariableTypeEnum.Continuous;
//                    } else {
//                        //if (typeName == "date")
//                        //    continue;
//                        var.DataType = SchemaVariable.DataTypeEnum.Text;
//                        var.VariableTypeDetected = SchemaVariable.VariableTypeEnum.Categorical;
//                        var.VariableTypeUserSet = SchemaVariable.VariableTypeEnum.Categorical;
//                    }
//                    Def.Schema.VariableLst.Add(var);
//                    r = true;
//                }
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message, "Error! in SchemaFill() Database.cs");
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }

//        public bool RefereceTableCreate(int nodeId) {
//            OdbcCommand cmd = null;
//            string sql = "Empty";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                sql = "drop table " + Def.DbTrTb + nodeId;
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
//            } catch { }
//            try{
//                sql = "Create table " + Def.DbTrTb + nodeId + " (" + Def.DbTableIdName + " integer PRIMARY KEY)";
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message + " SQL = " + sql, "Error!");
//                return false;
//            }
//            return true;
//        }

//        //public bool RefereceTableMvCreate(int nodeId) {
//        //    OdbcCommand cmd = null;
//        //    string sql = "Empty";
//        //    if (con.State != ConnectionState.Open)
//        //        con.Open();
//        //    if (con.State != ConnectionState.Open) {
//        //        MessageBox.Show("Could not open the connection", "Error");
//        //    }
//        //    try {
//        //        sql = "drop table " + Def.DbTrMvTb + nodeId;
//        //        cmd = new OdbcCommand(sql, con);
//        //        cmd.ExecuteNonQuery();
//        //    } catch { }
//        //    try {
//        //        sql = "Create table " + Def.DbTrMvTb + nodeId + " (" + Def.DbTableIdName + " integer PRIMARY KEY)";
//        //        cmd = new OdbcCommand(sql, con);
//        //        cmd.ExecuteNonQuery();
//        //    } catch (Exception ex) {
//        //        MessageBox.Show(ex.Message + " SQL = " + sql, "Error!");
//        //        return false;
//        //    }
//        //    return true;
//        //}


//        public bool SelectIntoTable(string table, string select) {
//            OdbcCommand cmd = null;
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("drop table " + table, con);
//                cmd.ExecuteNonQuery();
//            } catch { }
//            try {
//                cmd = new OdbcCommand(select + " into " + table, con);
//                cmd.ExecuteNonQuery();
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message, "Error!");
//                return false;
//            }
//            return true;
//        }




//        public double GetNumber(string qry, OdbcTransaction dbTrans) {  // Get Number
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            double r = -1;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return r;
//            }
//            try {
//                cmd = new OdbcCommand(qry, con, dbTrans);
//                reader = cmd.ExecuteReader();
//                reader.Read();
//                r = Convert.ToDouble(reader[0]);
//            } catch (Exception ex) {
//                FE.Show(ex.Message, "Error could not execute GetNumber sql: " + qry, ex.StackTrace);
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }


//        public string GetText(string qry) {  // Get Number
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            string r = "Error";

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return r;
//            }
//            try {
//                cmd = new OdbcCommand(qry, con);
//                reader = cmd.ExecuteReader();
//                reader.Read();
//                r = Convert.ToString(reader[0]);
//            } catch (Exception ex) {
//                FE.Show(ex.Message, "Error could not execute GetText sql: " + qry, ex.StackTrace);
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }


//        public List<string> GetTextLst(string sql) { // Return a column
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            List<string> stringLst = new List<string>();

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return null;
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    stringLst.Add(Convert.ToString(reader[0]));
//                }
//            } catch (Exception ex) {
//                FE.Show(ex.Message, "Error could not execute GetTextLst sql: " + sql, ex.StackTrace);
//            }
//            return stringLst;
//        }

//        public void Sample() {
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return;
//            }
//            OdbcCommand cmd=null;
//            List<double> tr;
//            tr = GetNumberLst("Select " + Def.DbTableIdName + " FROM " + Def.DbBsTb + " WHERE " + Def.Schema.Target.Name + " IS NOT NULL");
//            int removed;
//            RNG r = null;
//            if (Experiment.SampleSeed == 0) {
//                if (Def.SampleUsingTheSameSeed)
//                    r = new RNG(1);
//                else
//                    r = new RNG();
//            } else {
//                r = new RNG(Experiment.SampleSeed);
//            }
//            OdbcTransaction dbTrans = null;
//            dbTrans = con.BeginTransaction();
//            for (int i = 0; i < Def.TestingSetRowCount; ++i) {
//                removed=(int)RNG.GetUniform(0, tr.Count - 1);
//                cmd = new OdbcCommand("insert into " + Def.DbTsTb + "0 values (" + tr[removed] + ")", con, dbTrans);
//                cmd.ExecuteNonQuery();
//                //if (Def.Multivariate) {
//                //    cmd = new OdbcCommand("insert into " + Def.DbTsMvTb + "0 values (" + tr[removed] + ")", con, dbTrans);
//                //    cmd.ExecuteNonQuery();
//                //}
//                tr.RemoveAt(removed);
                
//            }
//            for (int i = 0; i < tr.Count; ++i) {
//                cmd = new OdbcCommand("insert into " + Def.DbTrTb + "0 values (" + tr[i] +")", con, dbTrans);
//                //if (Def.Multivariate) {
//                //    cmd = new OdbcCommand("insert into " + Def.DbTrMvTb + "0 values (" + tr[i] + ")", con, dbTrans);
//                //}
//                cmd.ExecuteNonQuery();
//           }
//           dbTrans.Commit();
//       }
               

//        public void Close() {
//            con.Close();		// close the connection
//            con=null;
//        }


//        public bool TargetCategoricalClassFill(Node node, SortedList<string, int> ClassSd) {
//            OdbcCommand cmd = null;
//            string qry="";
//            OdbcDataReader reader = null;
//            bool r = false;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                @qry = "SELECT " + Def.Schema.Target.Name + ", count(*)  " +
//                        "FROM " + Def.DbTableInUse + Def.DbBaseTableSufix + ", " + Def.DbTableInUse + Def.DbTrainingTableSufix + node.Id + " " +
//                        "WHERE " + Def.DbTableInUse + Def.DbBaseTableSufix + "." + Def.DbTableIdName + "=" + Def.DbTableInUse + Def.DbTrainingTableSufix + node.Id + "." + Def.DbTableIdName + " " + 
//                        "GROUP BY " + Def.Schema.Target.Name;
//                cmd = new OdbcCommand(qry, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    ClassSd.Add((string)reader[0], Convert.ToInt32(reader[1]));
//                }
//                r = true;
//            } catch (Exception ex) {
//                FE.Show(ex.Message, "Error could not execute TargetCategoricalClassFill", ex.StackTrace);
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }

//        public bool NodeGridFill(Node node, DataGridView grid, int page, string orderBy) {
            
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            string qry = "";
//            bool r = false;
//            int x=0,y=0;
//            int offset = Def.NodeDataViewRowsMax * (page - 1);
            
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                qry = 
//                @"SELECT * " +
//                "FROM " + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                "where " + Def.DbBsTb + "." + Def.DbTableIdName + "=" + Def.DbTrTb + node.Id + "." + Def.DbTableIdName +
//                " ORDER BY " + Def.DbBsTb + "." + orderBy + " Limit " + Def.NodeDataViewRowsMax + " OFFSET " + offset;
//                cmd = new OdbcCommand(qry, con);
//                reader = cmd.ExecuteReader();                
//                while (reader.Read()) {
//                    grid.Rows[y].HeaderCell.Value = Convert.ToString((y + 1) + ((page - 1) * Def.NodeDataViewRowsMax));
//                    for (x = 1; x < reader.FieldCount -1; ++x)
//                        grid.Rows[y].Cells[x - 1].Value = Convert.ToString(reader[x]);
//                    ++y;
//                }
//                r = true;
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message + " SQL= " + qry, "Error on NodeGridFill(Node node, DataGridView grid, int page)");
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }


//        public bool ReferenceTableGridFill(string table, DataGridView grid, int page, string orderBy) {

//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            string qry = "";
//            bool r = false;
//            int x = 0, y = 0;
//            int offset = Def.NodeDataViewRowsMax * (page - 1);

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                qry =
//                @"SELECT * " +
//                "FROM " + Def.DbBsTb + ", " + table + " " +
//                "WHERE " + Def.DbBsTb + "." + Def.DbTableIdName + "=" + table + "." + Def.DbTableIdName +
//                " ORDER BY " + Def.DbBsTb + "." + orderBy + " Limit " + Def.NodeDataViewRowsMax + " OFFSET " + offset;
//                cmd = new OdbcCommand(qry, con);
//                reader = cmd.ExecuteReader();
//                while (reader.Read()) {
//                    grid.Rows[y].HeaderCell.Value = Convert.ToString((y + 1) + ((page - 1) * Def.NodeDataViewRowsMax));
//                    for (x = 1; x < reader.FieldCount - 1; ++x)
//                        grid.Rows[y].Cells[x - 1].Value = Convert.ToString(reader[x]);
//                    ++y;
//                }
//                r = true;
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message + " SQL= " + qry, "Error on ReferenceTableGridFill");
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }


//        public bool PredictorsFill(Node node) {

//            Predictor pred = null;
//            OdbcCommand cmd = null;
//            OdbcDataReader reader = null;
//            string qry = "";
//            bool r = false;
 
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                foreach (SchemaVariable predVar in Def.Schema.PredictorLst) {
//                    pred = new Predictor(predVar, node, node.PredictorLst.Count);
//                    if (predVar.VariableTypeUserSet == SchemaVariable.VariableTypeEnum.Categorical) {
//                        qry = @"SELECT " 
//                                    + predVar.Name + ", count(" + predVar.Name + ") " +
//                              "FROM " 
//                                    + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                              "WHERE " 
//                                    + Def.DbBsTb + "." + Def.DbTableIdName + "=" + 
//                                    Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " " +
//                                    " and " + predVar.Name + " IS NOT NULL " + 
//                              "GROUP BY "
//                                    + predVar.Name;
                        
////                       qry = @"SELECT " + predVar.Name + ", count(" + predVar.Name + ") " +
////                            "FROM dataset ds " +
////                            "where Exists (select ref" + node.Id + "." + Def.DbTableIdName + " from ref" + node.Id + ", dataset " +
////                            "where ds." + Def.DbTableIdName + "=ref" + node.Id + "." + Def.DbTableIdName + ") " +
////                            "GROUP BY " + predVar.Name;
//                        cmd = new OdbcCommand(qry, con);
//                        reader = cmd.ExecuteReader();                        
//                        while (reader.Read()) {
//                            pred.ValueSd.Add(Convert.ToString(reader[0]), Convert.ToInt32(reader[1]));
//                        }
//                        qry = 
//                            @"SELECT COUNT(*)" +
//                              "FROM "
//                                    + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                              "where " + Def.DbBsTb + "." + Def.DbTableIdName + "="
//                                       + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " "
//                                       + " and " + predVar.Name + " IS NULL";
//                        cmd = new OdbcCommand(qry, con);
//                        reader = cmd.ExecuteReader();
//                        reader.Read();
//                        pred.NullCount = Convert.ToInt32(reader[0]);
//                        node.PredictorLst.Add(pred);
//                        node.PredCatLst.Add(pred);
//                    } else {

//////////////////////////////////////////////
///// CAN BE REDUCED TO ONLY ONE DATABASE PASS, JUST SELECT ALL VARIABLES AT THE SAME TIME (BELOW)
//////////////////////////////////////////////
//                        qry =
//                              @"SELECT 
//                                    COALESCE(MIN(" + predVar.Name + "),0),  COALESCE(MAX(" + predVar.Name + "), 0) " +
//                              "FROM "
//                                    + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                              "WHERE " + Def.DbBsTb + "." + Def.DbTableIdName + "="
//                                       + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and "
//                                       + predVar.Name + " IS NOT NULL ";
//                        /* +
//                              "ORDER BY " + predVar.Name + " " +
//                              "LIMIT 1";
//                          */
//                        cmd = new OdbcCommand(qry, con);
//                        reader = cmd.ExecuteReader();
//                        reader.Read();
//                        pred.SetLowerAndHigher(Convert.ToDouble(reader[0]), Convert.ToDouble(reader[1]));
//                        qry = 
//                            @"SELECT COUNT(DISTINCT " + predVar.Name + ")" +
//                              "FROM "
//                                    + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                              "WHERE " + Def.DbBsTb + "." + Def.DbTableIdName + "="
//                                       + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " and "
//                                       + predVar.Name + " IS NOT NULL";
//                        cmd = new OdbcCommand(qry, con);
//                        reader = cmd.ExecuteReader();
//                        reader.Read();
//                        pred.DistinctValuesCount = Convert.ToInt32(reader[0]);
//                        qry = 
//                              @"SELECT COUNT(*)" +
//                              "FROM "
//                                    + Def.DbBsTb + ", " + Def.DbTrTb + node.Id + " " +
//                              "where " + Def.DbBsTb + "." + Def.DbTableIdName + "="
//                                       + Def.DbTrTb + node.Id + "." + Def.DbTableIdName + " " 
//                                       + " and " + predVar.Name + " IS NULL";
//                        cmd = new OdbcCommand(qry, con);
//                        reader = cmd.ExecuteReader();
//                        reader.Read();
//                        pred.NullCount = Convert.ToInt32(reader[0]);
//                        node.PredictorLst.Add(pred);
//                        node.PredNumLst.Add(pred);
//                    }
//                }
//                r = true;
//            } catch (Exception ex) {
//                FE.Show(ex.Message, "Error could not execute PredictorsFill(Node node)", ex.StackTrace);
//            } finally {
//                reader.Close();
//            }
//            return r;
//        }


////        public bool LoadData(ProgressBar progressBar)
////        {
////            bool ret = false;
////
////            if (driver == DriverEnum.CSV) {
////
////                using (StreamReader sr = new StreamReader(Def.DataSourceFilePath)) {
////                    while (sr.Peek() >= 0) {
////                  //      ++lineCounter;
////                        sr.ReadLine();
////                    }
////                }
////                variableCount = reader.FieldCount;
////                Def.Schema.SetDimensions(variableCount, instanceCount);
////                FillTableCct(progressBar);
////                Close();
////            }
////
////            if (CommandSetUp() == false)
////                return false;
////            try
////            {
////                instanceCount = (int)command.ExecuteScalar();
////            }   
////            catch (System.Exception ex)
////            {
////                MessageBox.Show(ex.Message, "Error:");
////            }
////            command = new OdbcCommand(qryReader, Con);
////            try
////            {
////                reader = command.ExecuteReader();
////            }
////            catch (System.Exception ex)
////            {
////                MessageBox.Show(ex.Message, "Error:");
////            }
////            variableCount = reader.FieldCount;
////            Def.Schema.SetDimensions(variableCount, instanceCount);
////            FillTableCct(progressBar);
////            Close();
////
////            return ret;
////        }


//        public void ImportedTableDrop() {

//            OdbcCommand cmd = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection ImportedTableDrop()", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("DROP TABLE " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix, con);
//                cmd.ExecuteNonQuery();
//              } catch { }
//        }

//        public void ImportedTableMvDrop() {

//            OdbcCommand cmd = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection ImportedTableDrop()", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("DROP TABLE " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix, con);
//                cmd.ExecuteNonQuery();
//            } catch { }
//        }

//        public void TableDropIfExists(string table) { //DROPs anyway
//            OdbcCommand cmd = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection ImportedTableDrop()", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("DROP TABLE " + table, con);
//                cmd.ExecuteNonQuery();
//              } catch { }
//        }

//        public void ImportedSequenceDrop() {
//            OdbcCommand cmd = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("DROP SEQUENCE " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_seq", con);
//                cmd.ExecuteNonQuery();
//            } catch {}
//        }

        
//        public void ImportedSequenceMvDrop() {
//            OdbcCommand cmd = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("DROP SEQUENCE " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_seq", con);
//                cmd.ExecuteNonQuery();
//            } catch { }
//        }


//        public bool ImportedSequenceCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("CREATE SEQUENCE " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_seq START 1", con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch(Exception Ex) {
//                MessageBox.Show(Ex.Message, "Error in ImportedSequenceCreate Database.cs");
//            }
//            return r;
//        }

//        public bool ImportedSequenceMvCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand("CREATE SEQUENCE " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_seq START 1", con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message, "Error in ImportedSequenceCreate Database.cs");
//            }
//            return r;
//        }

//        public bool ImportedTableCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;
//            int x, i;
//            string variableName;
//            string createTable = "Create table " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + " (" + Def.DbTableIdName + " INT4 DEFAULT nextval('" + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_seq'), PRIMARY KEY(" + Def.DbTableIdName + "), ";

//            for (x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
//                if (Def.DataImportDefs.VariableLst[x].Import == false)
//                    continue;
//                variableName = Def.DataImportDefs.VariableLst[x].Name;
//                if (Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.Number) {
//                    createTable += variableName + " " + Def.DbNumericFormat + " ";
//                } else {
//                    createTable += variableName + " " + Def.DbTextFormat + " ";
//                }
//                if (x < Def.DataImportDefs.VariableLst.Count - 1) {
//                    for (i = x + 1; i < Def.DataImportDefs.VariableLst.Count; ++i) {
//                        if (Def.DataImportDefs.VariableLst[i].Import == true) {
//                            createTable += " , ";
//                            break;
//                        }
//                    }
//                }
//            }
//            createTable += " ) ";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection ImportedTableCreate()", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(createTable, con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message, "Error in ImportedTableCreate() Database.cs");
//            }
//            return r;
//        }


//        public bool ImportedTableMvCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;
//            int x, i, vidx;
//            string variableName;
//            string createTable = "Create table " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + " (" + Def.DbTableIdName + " INT4 DEFAULT nextval('" + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_seq'), PRIMARY KEY(" + Def.DbTableIdName + "), ";

//            for (x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
//                if (Def.DataImportDefs.VariableLst[x].Import == false)
//                    continue;
//                variableName = Def.DataImportDefs.VariableLst[x].Name;
//                if (Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.Number || Def.DataImportDefs.DependentVariableIdx==x) {
//                    if(Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.Number)
//                        createTable += variableName + " " + Def.DbNumericFormat + " ";
//                    else
//                        createTable += variableName + " " + Def.DbTextFormat + " ";
//                    if (x < Def.DataImportDefs.VariableLst.Count - 1) {
//                        for (i = x + 1; i < Def.DataImportDefs.VariableLst.Count; ++i) {
//                            if (Def.DataImportDefs.VariableLst[i].Import == true) {
//                                createTable += " , ";
//                                break;
//                            }
//                        }
//                    }
//                } else {
//                    for (vidx = 0; vidx < Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count; ++vidx) {
//                        createTable += variableName + "_" + vidx + " " + Def.DbNumericFormat + " ";
//                        if (vidx < Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count - 1) {
//                            createTable += " , ";
//                        }
//                    }
//                    if (x < Def.DataImportDefs.VariableLst.Count - 1) {
//                        for (i = x + 1; i < Def.DataImportDefs.VariableLst.Count; ++i) {
//                            if (Def.DataImportDefs.VariableLst[i].Import == true) {
//                                createTable += " , ";
//                                break;
//                            }
//                        }
//                    }
//                }
//            }
//            createTable += " ) ";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection ImportedTableMvCreate()", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(createTable, con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message, "Error in ImportedTableMvCreate() Database.cs");
//            }
//            return r;
//        }

//        public void ImportedIndexesDrop() {

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            for (int x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
//                if (Def.DataImportDefs.VariableLst[x].Import == false)
//                    continue;
//                IndexDrop(Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_" + Def.DataImportDefs.VariableLst[x].Name + "_idx");
//            }
//        }

//        public void ImportedIndexesMvDrop() {

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            for (int x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
//                    if (Def.DataImportDefs.VariableLst[x].Import == false)
//                        continue;
//                    if (Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.Number)
//                        IndexDrop(Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_" + Def.DataImportDefs.VariableLst[x].Name + "_idx");
//                    else {
//                        for (int i = 0; i < Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count; ++i)
//                            IndexDrop(Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_" + Def.DataImportDefs.VariableLst[x].Name + "_" + i + "_idx");
//                    }


//            }
//        }

//         public void ReferenceTableIndexDropIfExists(int nodeId) {
//            OdbcCommand cmd = null;

//            string sql =
//            @"DROP INDEX " + Def.DbTrTb + nodeId + "_" + Def.DbTableIdName + "_idx";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
 
//            } catch  {
//            }
//        }

//        public void ValidationTableIndexDropIfExists(int nodeId) {
//            OdbcCommand cmd = null;

//            string sql =
//            @"DROP INDEX " + Def.DbTsTb + nodeId + "_" + Def.DbTableIdName + "_idx";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
//            } catch {
//            }
//        }

//        public bool ReferenceTableIndexCreate(int nodeId) {
//            OdbcCommand cmd = null;
//            bool r = false;

//            string sql =
//            @"CREATE UNIQUE INDEX " +
//                    Def.DbTrTb + nodeId + "_" + Def.DbTableIdName + "_idx on " +
//                    Def.DbTrTb + nodeId + " (" + Def.DbTableIdName + ")";
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message + " SQL=" + sql, "Error in ReferenceTableIndexCreate(Node n) Database.cs ");
//            }
//            return r;
//        }

//        public bool ValidationTableIndexCreate(int nodeId) {
//            OdbcCommand cmd = null;
//            bool r = false;

//            string sql = 
//            @"CREATE UNIQUE INDEX " +
//                    Def.DbTsTb + nodeId + "_" + Def.DbTableIdName + "_idx on " +
//                    Def.DbTsTb + nodeId + " (" + Def.DbTableIdName + ")";    
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                cmd = new OdbcCommand(sql, con);
//                cmd.ExecuteNonQuery();
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message + " SQL=" + sql, "Error in ValidationTableIndexCreate(Node n) Database.cs ");
//            }
//            return r;
//        }


//        public void IndexDrop(string index) {
        
//            OdbcCommand cmd = null;
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                    cmd = new OdbcCommand("DROP INDEX " + index, con);
//                    cmd.ExecuteNonQuery();
//            } catch { }
//        }


//        public bool ImportedIndexesCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;
//            string sql = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                for (int x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
//                    if (Def.DataImportDefs.VariableLst[x].Import == false)
//                        continue;
//                    sql = "CREATE INDEX " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_" + Def.DataImportDefs.VariableLst[x].Name + "_idx on " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + " (" + Def.DataImportDefs.VariableLst[x].Name + ")";
//                    cmd = new OdbcCommand(sql, con);
//                    cmd.ExecuteNonQuery();
//                }
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message + " SQL=" + sql, "Error in ImportedIndexesCreate() Database.cs");
//            }
//            return r;
//        }


//        public bool ImportedIndexesMvCreate() {

//            OdbcCommand cmd = null;
//            bool r = false;
//            string sql = null;

//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//            }
//            try {
//                for (int x = 0; x < Def.DataImportDefs.VariableMvLst.Count; ++x) {
//                    sql = "CREATE INDEX " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_" + Def.DataImportDefs.VariableMvLst[x].Name + "_idx on " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + " (" + Def.DataImportDefs.VariableMvLst[x].Name + ")";
//                    cmd = new OdbcCommand(sql, con);
//                    cmd.ExecuteNonQuery();
//                }
//                r = true;
//            } catch (Exception Ex) {
//                MessageBox.Show(Ex.Message + " SQL=" + sql, "Error in ImportedIndexesMvCreate() Database.cs");
//            }
//            return r;
//        }


//        public void ImportedTableFill() {
//            string path = Def.DataImportDefs.FilenameOriginal;
//            string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
//            string doubleQuotes = "\"";
//            Regex r = new Regex(pattern);
//            int x, i, lineCounter=0;
//            string[] strArr;
//            string fieldValue;
//            string line;
//            string insert = ""; 
//            bool skipRow = false;
//            OdbcCommand cmd = new OdbcCommand();
//            cmd.Connection = con;
//            OdbcTransaction dbTrans;
//            dbTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
//            cmd.Transaction = dbTrans;
//            try{
//                using (StreamReader sr = new StreamReader(path)) {
//                    while (sr.Peek() >= 0) {
//                        ++lineCounter;
//                        line = sr.ReadLine();
//                        if(lineCounter==1)
//                            continue;
//                        strArr = r.Split(line);
//                        if (Def.ImportedDataRemoveRowsWithBlanks) { //Don't insert blank values if told not to
//                            for (x = 0; x < strArr.Length; ++x) {
//                            if (Def.DataImportDefs.VariableLst[x].Import == false)
//                                continue;
//                            fieldValue = strArr[x].Replace(doubleQuotes, "");
//                            fieldValue = fieldValue.Replace("'", "");
//                                if (fieldValue == "") { 
//                                    skipRow = true;
//                                    break;
//                                }
//                            }
//                        }
//                        if (skipRow) {
//                            skipRow = false;
//                            continue;
//                        }
//                        insert = "insert into " + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + " values (nextval('" + Def.DataImportDefs.TableName + Def.DbBaseTableSufix + "_seq'), ";
//                        for(x = 0; x < strArr.Length; ++x) {
//                            if (Def.DataImportDefs.VariableLst[x].Import == false)
//                                continue;
//                            fieldValue = strArr[x].Replace(doubleQuotes, "");
//                            fieldValue = fieldValue.Replace("'", "");
//                            if(fieldValue=="")
//                                insert += "NULL";
//                            else
//                                insert += "'" + fieldValue + "'";
//                            if (x < Def.DataImportDefs.VariableLst.Count - 1) {
//                                for (i = x + 1; i < Def.DataImportDefs.VariableLst.Count; ++i) {
//                                    if (Def.DataImportDefs.VariableLst[i].Import == true) {
//                                        insert += " , ";
//                                        break;
//                                    }
//                                }
//                            }
//                        }
//                        insert += ")";
//                        cmd.CommandText = insert;
//                        cmd.ExecuteNonQuery();
//                }
//            }
//            dbTrans.Commit();
//        } catch (Exception ex) {
//               MessageBox.Show(ex.Message + " SQL = " + insert, "Error in FillImportedTable() Database.cs");
//           }
//        }

  
//        public void ImportedTableMvFill() {
//            string path = Def.DataImportDefs.FilenameOriginal;
//            string pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
//            string doubleQuotes = "\"";
//            Regex r = new Regex(pattern);
//            double varValue;
//            int x, i, lineCounter = 0, fieldOffset=0, valuePos;
//            bool lastVar;
//            string[] strArr;
//            string fieldValue;
//            string line;
//            string insert = "";
            
//            OdbcCommand cmd = new OdbcCommand();
//            cmd.Connection = con;
//            OdbcTransaction dbTrans;
//            dbTrans = con.BeginTransaction(IsolationLevel.ReadCommitted);
//            cmd.Transaction = dbTrans;
//            try {
//                using (StreamReader sr = new StreamReader(path)) {
//                    while (sr.Peek() >= 0) {
//                        ++lineCounter;
//                        line = sr.ReadLine();
//                        if (lineCounter == 1)
//                            continue;
//                        strArr = r.Split(line);
//                        insert = "insert into " + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + " values (nextval('" + Def.DataImportDefs.TableName + Def.DbBaseTableMvSufix + "_seq'), ";
//                        for (x = 0; x < strArr.Length; ++x) {
//                            if (Def.DataImportDefs.VariableLst[x].Import == false)
//                                continue;
//                            lastVar = true;
//                            for (i = x + 1; i < Def.DataImportDefs.VariableLst.Count; ++i) {
//                                if (Def.DataImportDefs.VariableLst[i].Import == true) {
//                                    lastVar=false;
//                                    break;
//                                }
//                            }
//                            fieldValue = strArr[x].Replace(doubleQuotes, "");
//                            fieldValue = fieldValue.Replace("'", "");
//                            //Puts data in multiple columns
//                            if (Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.String)
//                                fieldValue = fieldValue.ToLowerInvariant();
//                            if (Def.DataImportDefs.VariableLst[x].DataType != DataTypeEnum.Number && x != Def.DataImportDefs.DependentVariableIdx) {
//                                valuePos=Def.DataImportDefs.VariableLst[x].ValueGroupLst.IndexOfKey(fieldValue);
//                                for (int valIdx = 0; valIdx < Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count; ++valIdx) {
//                                    if (valuePos != valIdx) {
//                                        insert += "0";
//                                    } else {
//                                        insert += "1";
//                                    }
//                                    if (!lastVar || valIdx < Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count - 1)
//                                        insert += ", ";
//                                }
//                            } else {
//                                //Numeric values requires nomarlisation
//                                if (x != Def.DataImportDefs.DependentVariableIdx) {
//                                    varValue = Convert.ToDouble(fieldValue);
//                                    varValue = ((varValue - Def.DataImportDefs.VariableLst[x].MinimumD) / (Def.DataImportDefs.VariableLst[x].MaximumD - Def.DataImportDefs.VariableLst[x].MinimumD)) + 1;
//                                    if (fieldValue == "")
//                                        insert += "NULL";
//                                    else
//                                        insert += "'" + varValue + "'";
//                                    if (!lastVar)
//                                        insert += ", ";
//                                } else {//Dep var
//                                    if (fieldValue == "")
//                                        insert += "NULL";
//                                    else
//                                        insert += "'" + fieldValue + "'";
//                                    if (!lastVar)
//                                        insert += ", ";
//                                }

//                            }
//                            if (Def.DataImportDefs.VariableLst[x].DataType != DataTypeEnum.Number && x != Def.DataImportDefs.DependentVariableIdx) {
//                                fieldOffset = Def.DataImportDefs.VariableLst[x].ValueGroupLst.Count;
//                            }
//                        }
//                        insert += ")";
//                        cmd.CommandText = insert;
//                        cmd.ExecuteNonQuery();
//                    }
//                }
//                dbTrans.Commit();
//            } catch (Exception ex) {
//                MessageBox.Show(ex.Message + " SQL = " + insert, "Error in ImportedTableMvFill() Database.cs");
//            }
//        }

//        public void NonQueryTransaction(List<string> sql) {
//            if (con.State != ConnectionState.Open)
//                con.Open();
//            if (con.State != ConnectionState.Open) {
//                MessageBox.Show("Could not open the connection", "Error");
//                return;
//            }
//            OdbcCommand cmd = null;
//            OdbcTransaction dbTrans = null;
//            dbTrans = con.BeginTransaction();
//            for (int i = 0; i < sql.Count; ++i) {
//                cmd = new OdbcCommand(sql[i], con, dbTrans);
//                cmd.ExecuteNonQuery();
//            }
//            dbTrans.Commit();
//        }

////        public void FillTableCct() {
////            int x = 0, y = 0;
////            double progressBarUpdate;
////            string varibleName, typeName;
////            string createTable = "Create table " + Def.DataImportDefs.TableName + " (" + Def.DbTableIdName + " integer primary key autoincrement, ";

////            string insert = "";


////            try {
////                for (x = 0; x < Def.DataImportDefs.VariableLst.Count; ++x) {
////                    varibleName = Def.DataImportDefs.VariableLst[x].Name;
////                    //typeName = Def.DataImportDefs.VariableLst[x].DataType;
////                    //MessageBox.Show(reader[x].GetType().ToString() + "\r\n" + typeName);

////                    if (Def.DataImportDefs.VariableLst[x].DataType == DataTypeEnum.Number) {
////                        createTable += varibleName + " float4 ";
//////                        Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Number);
////                        //                            var = new Variable(colName, Variable.TypeEnum.Continuous, PhysicalColumn.DataTypeEnum.Number);
////                    } else {
////                        createTable += varibleName + " text ";
//////                        Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Text);
////                        //                            var = new Variable(colName, Variable.TypeEnum.Categorical, PhysicalColumn.DataTypeEnum.Text);
////                    }
////                    if (x < Def.DataImportDefs.VariableLst.Count - 1)
////                        createTable += " , ";
////                }
////                createTable += " ) ";

////                SQLiteConnection cnn = new SQLiteConnection();
////                cnn.ConnectionString = "Data Source=" + Def.APPLICATION_DIR + "/tmp.db";
////                cnn.Open();

////                SQLiteCommand cmd = new SQLiteCommand(cnn);

////                cmd.CommandText = createTable;
////                cmd.ExecuteNonQuery();

////                DbTransaction dbTrans = cnn.BeginTransaction();
////                while (reader.Read()) {
////                    insert = "insert into dataset values (NULL, ";
////                    for (x = 0; x < reader.FieldCount; ++x) {
////                        if (y == 0) {
////                            varibleName = reader.GetName(x).ToLowerInvariant();
////                            typeName = reader.GetDataTypeName(x).ToLowerInvariant();
////                            //MessageBox.Show(reader[x].GetType().ToString() + "\r\n" + typeName);

////                            if (typeName == "integer" || typeName == "float" || typeName == "numeric" || typeName == "number") {
////                                Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Number);
////                                //                            var = new Variable(colName, Variable.TypeEnum.Continuous, PhysicalColumn.DataTypeEnum.Number);
////                            } else {
////                                Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Text);
////                                //                            var = new Variable(colName, Variable.TypeEnum.Categorical, PhysicalColumn.DataTypeEnum.Text);
////                            }
////                        }

////                        if (Def.Schema.VariableLst[x].DataType == VariableCct.DataTypeEnum.Text) {
////                            //Def.Schema[x, y] = reader[x].ToString().ToLowerInvariant();
////                            insert += "'" + reader[x].ToString().ToLowerInvariant() + "'";
////                        } else {
////                            //Def.Schema[x, y] = reader[x];
////                            insert += "'" + reader[x].ToString() + "'";
////                        }
////                        if (x < reader.FieldCount - 1)
////                            insert += " , ";
////                    }
////                    insert += ")";
////                    cmd.CommandText = insert;

////                    cmd.ExecuteNonQuery();

////                    if (y > 0 && (y % progressBarUpdate == 0))
////                        progressBar.PerformStep();
////                    ++y;
////                }
////                dbTrans.Commit();
////                cnn.Close();
////            } catch (Exception ex) {
////                MessageBox.Show(ex.Message, "Error");
////            }

////        }



////        public void FillTableCct(ProgressBar progressBar)
////        {
////            int x = 0, y = 0;
////            double progressBarUpdate;
////            string varibleName, typeName;
////            string createTable = "Create table " + Def.DataImportDefs.TableName + " ("  + Def.DbTableIdName +  " integer primary key autoincrement, ";

////            string insert = "";


////            try {

////                System.IO.File.Delete(Def.APPLICATION_DIR + "/tmp.db");


////                progressBarUpdate = (double)(instanceCount * progressBar.Step);
////                progressBarUpdate = (int)progressBarUpdate / 100;

////                for (x = 0; x < reader.FieldCount; ++x) {
////                    varibleName = reader.GetName(x).ToLowerInvariant();
////                    typeName = reader.GetDataTypeName(x).ToLowerInvariant();
////                    //MessageBox.Show(reader[x].GetType().ToString() + "\r\n" + typeName);

////                    if (typeName == "integer" || typeName == "float" || typeName == "numeric" || typeName == "number") {
////                        createTable += varibleName + " REAL ";
////                        Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Number);
//////                            var = new Variable(colName, Variable.TypeEnum.Continuous, PhysicalColumn.DataTypeEnum.Number);
////                    } else {
////                        createTable += varibleName + " TEXT ";
////                        Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Text);
//////                            var = new Variable(colName, Variable.TypeEnum.Categorical, PhysicalColumn.DataTypeEnum.Text);
////                    }
////                    if (x < reader.FieldCount - 1)
////                        createTable += " , ";
////                }
////                createTable += " ) ";

////                SQLiteConnection cnn = new SQLiteConnection();
////                cnn.ConnectionString = "Data Source=" + Def.APPLICATION_DIR + "/tmp.db";
////                cnn.Open();

////                SQLiteCommand cmd = new SQLiteCommand(cnn);

////                cmd.CommandText = createTable;
////                cmd.ExecuteNonQuery();

////                DbTransaction dbTrans = cnn.BeginTransaction();
////                while (reader.Read()) {
////                    insert = "insert into dataset values (NULL, ";
////                    for (x = 0; x < reader.FieldCount; ++x) {
////                        if (y == 0) {
////                            varibleName = reader.GetName(x).ToLowerInvariant();
////                            typeName = reader.GetDataTypeName(x).ToLowerInvariant();
////                            //MessageBox.Show(reader[x].GetType().ToString() + "\r\n" + typeName);

////                            if (typeName == "integer" || typeName == "float" || typeName == "numeric" || typeName == "number") {
////                                Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Number);
//////                            var = new Variable(colName, Variable.TypeEnum.Continuous, PhysicalColumn.DataTypeEnum.Number);
////                            } else {
////                                Def.Schema.AddVariable(varibleName, VariableCct.DataTypeEnum.Text);
//////                            var = new Variable(colName, Variable.TypeEnum.Categorical, PhysicalColumn.DataTypeEnum.Text);
////                            }
////                        }

////                        if (Def.Schema.VariableLst[x].DataType == VariableCct.DataTypeEnum.Text) {
////                            //Def.Schema[x, y] = reader[x].ToString().ToLowerInvariant();
////                            insert += "'" + reader[x].ToString().ToLowerInvariant() + "'";
////                        } else {
////                            //Def.Schema[x, y] = reader[x];
////                            insert += "'" + reader[x].ToString() + "'";
////                        }
////                        if (x < reader.FieldCount - 1)
////                            insert += " , ";
////                    }
////                    insert += ")";
////                    cmd.CommandText = insert;

////                    cmd.ExecuteNonQuery();

////                    if (y > 0 && (y % progressBarUpdate == 0))
////                        progressBar.PerformStep();
////                    ++y;
////                }
////                dbTrans.Commit();
////                cnn.Close();
////            } catch(Exception ex) {
////                MessageBox.Show(ex.Message, "Error");
////            }

////        }

//    }
//}
