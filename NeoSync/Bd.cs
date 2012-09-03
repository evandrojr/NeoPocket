using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using FirebirdSql.Data.FirebirdClient;//provider do SGBD FireBird
using Core;
using NeoDebug;

namespace NeoSync
{
    public class Bd
    {

        private string conStr;
        private FbConnection con = null;

        public DataTable dt;

        public Bd()
        {

        }

        public string ConStr
        {
            get { return conStr; }
            set { conStr = value; }
        }

        public FbConnection Con
        {
            get { return con; }
        }

        public bool Connect()
        {
            con = new FbConnection(conStr);		// get the connection
            con.Open();		// open the connection
            return true;
        }



        /// <summary>
        /// Cria e preenche um DataTable com o resultado do sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        public DataTable DataTablePreenche(string sql)
        {
            FbCommand cmd = new FbCommand(sql, Con);
            FbDataReader reader = null;
            DataTable dt = new DataTable();
                reader = cmd.ExecuteReader();
            DataSet ds = convertDataReaderToDataSet(reader);
            return ds.Tables[0];
        }

        public DataTable DataTablePreenche(string sql, FbTransaction bdTrans)
        {
            FbCommand cmd = new FbCommand(sql, Con, bdTrans);
            FbDataReader reader = null;
            DataTable dt = new DataTable();
            reader = cmd.ExecuteReader();
            DataSet ds = convertDataReaderToDataSet(reader);
            return ds.Tables[0];
        }

        ///    <summary>
        /// Converts a FbDataReader to a DataSet
        ///    <param name='reader'>
        /// SqlDataReader to convert.</param>
        ///    <returns>
        /// DataSet filled with the contents of the reader.</returns>
        ///    </summary>
        public static DataSet convertDataReaderToDataSet(FbDataReader reader)
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

        public string T(string qry)
        {  // Le uma célula de texto
            FbCommand cmd = null;
            FbDataReader reader = null;
            string r = "";

            cmd = new FbCommand(qry, Con);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToString(reader[0]);
            reader.Close();
            return r;
        }

        public string T(string qry, FbTransaction trans)
        {  // Le uma célula de texto
            FbCommand cmd = null;
            FbDataReader reader = null;
            string r = "";

            cmd = new FbCommand(qry, Con, trans);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToString(reader[0]);
            reader.Close();
            return r;
        }

        public int I(string qry)
        {  // Le uma célula de numero
            FbCommand cmd = null;
            FbDataReader reader = null;
            int r = 0;

            cmd = new FbCommand(qry, Con);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToInt32(reader[0]);
            reader.Close();
            return r;
        }
        
        public int? Inull(string qry, FbTransaction trans)
        {  // Le uma célula de numero
            FbCommand cmd = null;
            FbDataReader reader = null;
            int r = 0;

            cmd = new FbCommand(qry, Con, trans);
            reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;
            r = Convert.ToInt32(reader[0]);
            reader.Close();
            return r;
        }

        public int I(string qry, FbTransaction bdTrans)
        {  // Le uma célula de numero
            FbCommand cmd = null;
            FbDataReader reader = null;
            int r = 0;

            cmd = new FbCommand(qry, Con, bdTrans);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToInt32(reader[0]);
            reader.Close();
            return r;
        }


        public int ExecuteNonQuery(string sql)
        {
            FbCommand cmd = null;
            int rowsAffected = -1;
            cmd = new FbCommand(sql, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Error reading data from the connection.")
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.WorkingDirectory = D.ApplicationDirectory;
                    proc.StartInfo.FileName = D.ApplicationName;
                    proc.StartInfo.Arguments = "";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = false;
                    proc.StartInfo.RedirectStandardError = false;
                    proc.Start();
                    //proc.WaitForExit();
                    //proc.Close();
                    Environment.FailFast(ex.Message);
                }
            }
            return rowsAffected;
        }

        public int ExecuteNonQuery(string sql, FbTransaction bdTrans)
        {
            FbCommand cmd = null;
            int rowsAffected = -1;
            cmd = new FbCommand(sql, Con, bdTrans);
            rowsAffected = cmd.ExecuteNonQuery();
            return rowsAffected;
        }

        public int ImportaLinha(string sql, FbTransaction trans)
        {
            FbCommand cmd = null;
            int rowsAffected = -1;
            cmd = new FbCommand(sql, Con, trans);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DadoImportacao.ErrosQtd++;
                DadoImportacao.LogGrava("Erro: " + sql + Environment.NewLine + ex.Message);
            }
            return rowsAffected;
        }

        public static string DataParaBd(DateTime dt)
        {
            return "'" + dt.Year + "-" + dt.Month + "-" + dt.Day + "'";
        }

        public int ImportaLinha(string sql)
        {
            FbCommand cmd = null;
            int rowsAffected = -1;
            cmd = new FbCommand(sql, Con);
            try
            {
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DadoImportacao.ErrosQtd++;
                DadoImportacao.LogGrava("Erro: " + sql + Environment.NewLine + ex.Message);
            }
            return rowsAffected;
        }

        /// <summary>
        /// requires only the fields that are not null, like in Delphi
        /// vp = value, parameter
        /// </summary>
        /// <param name="SPname"></param>
        /// <param name="pv"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public Dictionary<string,string> StoredProcedureExecute(string spName, Dictionary<string, string> pv, FbTransaction trans)
        {
           
            FbCommand cmd;
            Dictionary<string, string> rpv = new Dictionary<string, string>();
            DateTime parametroData;
            
            //StringBuilder para debug
            StringBuilder sbD = new StringBuilder("Execute procedure ");
            sbD.Append(spName).Append("(");
            string sD = null;

            cmd = new FbCommand(spName, D.Bd.Con, trans);
            cmd.CommandType = CommandType.StoredProcedure;

            //Very very important!!!
            FbCommandBuilder.DeriveParameters(cmd);

            foreach (string name in pv.Keys)
            {
                if(!cmd.Parameters.Contains(name)){
                    throw new Exception("Não foi possível encontrar o parametro " + name + " na stored procedure " + spName);
                }
            }


            int count=0;
            // Populate the Input Parameters With Values Provided        
            foreach (FbParameter parameter in cmd.Parameters)
            {
                if (parameter.Direction == ParameterDirection.Input)
                {
                    //if (parameter.DbType == DbType.Date)
                    //    count = count;
                    if (pv.ContainsKey(parameter.ParameterName))
                    {
                        if (pv[parameter.ParameterName].ToUpper() == "NULL")
                        {
                            parameter.Value = DBNull.Value;
                            sbD.Append("NULL");
                        }
                        else
                        {
                            if (parameter.DbType == DbType.Int32)
                            {
                                parameter.Value = Convert.ToInt32(pv[parameter.ParameterName]);
                                sbD.Append(parameter.Value.ToString()); 
                            }
                            else
                                if (parameter.DbType == DbType.DateTime || parameter.DbType == DbType.Date)
                                {
                                    if (pv[parameter.ParameterName] == "")
                                    {
                                        parameter.Value = DBNull.Value;
                                        sbD.Append("NULL");
                                    }
                                    else
                                    {
                                        sbD.Append("'");
                                        parameter.Value = Convert.ToDateTime(pv[parameter.ParameterName], D.CultureInfoBRA);
                                        parametroData = Convert.ToDateTime(pv[parameter.ParameterName], D.CultureInfoBRA);
                                        sbD.Append(parametroData.ToString("yyyy-M-d HH:mm:ss",D.CultureInfoBRA));
                                        sbD.Append("'");
                                    }
                                }
                                else
                                    if (parameter.DbType == DbType.String)
                                    {
                                        parameter.Value = pv[parameter.ParameterName].Replace("'", "`");
                                        sbD.Append("'");
                                        sbD.Append(parameter.Value.ToString());
                                        sbD.Append("'");
                                    }
                                    else
                                        if(parameter.DbType == DbType.Decimal)
                                        {
                                            parameter.Value = Convert.ToDecimal(pv[parameter.ParameterName].Replace(",", "."), D.CultureInfoEUA);
                                            sbD.Append(parameter.Value.ToString().Replace(",", "."));
                                        }
                                        else
                                        if (parameter.DbType == DbType.Double)
                                        {
                                            parameter.Value = Convert.ToDouble(pv[parameter.ParameterName].Replace(",", "."), D.CultureInfoEUA);
                                            sbD.Append(parameter.Value.ToString().Replace(",", "."));
                                        }
                                        else
                                            if (parameter.DbType == DbType.Int16)
                                            {
                                                parameter.Value = Convert.ToInt16(pv[parameter.ParameterName]);
                                                sbD.Append(parameter.Value.ToString());
                                            }
                                        else
                                        {
                                            throw new Exception("Tipo de dado não cadastrado");
                                        }
                        }
                    }
                    else
                    {
                        parameter.Value = DBNull.Value;
                        sbD.Append("NULL");
                    }
                    sbD.Append(",");
                    sD = sbD.ToString();
                    //Remove ',' extra
                    sD = sD.Substring(0, sD.Length - 1);
                    sD += ");";
                    ++count;
                }
            }
            Debug.LogWrite(sD);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.LogWrite("Erro grave: " + ex.Message + " SQL " + sD);
                throw new Exception("Erro grave: " + ex.Message + " SQL " + sD);
            }
//            FbCommand cmd2 = new FbCommand(sD,con);
//            cmd2.ExecuteNonQuery();

            foreach (FbParameter parameter in cmd.Parameters)
            {
                if (parameter.Direction == ParameterDirection.ReturnValue || parameter.Direction == ParameterDirection.Output)
                {
                    rpv.Add(parameter.ParameterName, parameter.Value.ToString());
                }
            }
            return rpv;
        }

        public List<double> LstDCol(string sql)
        {
            FbCommand cmd = null;
            FbDataReader reader = null;
            List<double> doubleLst = new List<double>();

            cmd = new FbCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; ++i)
                doubleLst.Add(Convert.ToDouble(reader[i]));
            return doubleLst;
        }

        public List<int> LstICol(string sql)
        {
            FbCommand cmd = null;
            FbDataReader reader = null;
            List<int> Lst = new List<int>();

            cmd = new FbCommand(sql, con);
            reader = cmd.ExecuteReader();
            reader.Read();
            for (int i = 0; i < reader.FieldCount; ++i)
                Lst.Add(Convert.ToInt32(reader[i]));
            return Lst;
        }

        public List<string> LstT(string sql)
        {
            FbCommand cmd = null;
            FbDataReader reader = null;
            List<string> Lst = new List<string>();

            cmd = new FbCommand(sql, con);
            reader = cmd.ExecuteReader();
            while(reader.Read())
                Lst.Add(reader[0].ToString());
            return Lst;
        }

        public DataTable DataTablePreenche(string sql, string tabela)
        {

            FbCommand cmd = new FbCommand(sql, Con);
            FbDataAdapter da = new FbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, tabela);
            DataTable dt = ds.Tables[0];
            return dt;
        }

        public double N(string qry, FbTransaction trans)
        {  // Get Number
            FbCommand cmd = null;
            FbDataReader reader = null;
            double r = -1;

            try
            {
                cmd = new FbCommand(qry, Con, trans);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToDouble(reader[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro executando Bd.N sql: " + qry + ex.StackTrace);
            }
            finally
            {
                reader.Close();
            }
            return r;
        }

        public double N(string qry)
        {  // Get Number
            FbCommand cmd = null;
            FbDataReader reader = null;
            double r = -1;

            try
            {
                cmd = new FbCommand(qry, Con);
                reader = cmd.ExecuteReader();
                reader.Read();
                r = Convert.ToDouble(reader[0]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "Erro executando Bd.N sql: " + qry + ex.StackTrace);
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
            FbCommand cmd = null;
            FbDataReader reader = null;
            string r = "";

            cmd = new FbCommand(qry, Con);
            reader = cmd.ExecuteReader();
            reader.Read();
            r = Convert.ToString(reader[0]);
            reader.Close();
            return r;
        }


    }

}
