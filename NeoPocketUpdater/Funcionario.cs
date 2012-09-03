using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Data.Common;
using Config;

namespace NeoPocketUpdater
{
    /// <summary>
    /// Classe pode ser melhorada
    /// </summary>
    public class Funcionario
    {
        public int Id; //Carregado pelo aquivo criptografado
        public double DescontoMaximo;
        public double AcrescimoMaximo;
        public string Nome;

        public bool Valida(string nome, string senha){
            return true;
        }

        //Chamar sempre apos carregar os parametros
        public void Carregar()
        {
            SqlCeDataReader reader=null;
            try
            {
                string sql = "select * from funcionario where id=" + Id;
                SqlCeCommand cmd = new SqlCeCommand(sql, D.Bd.Con);
                reader = cmd.ExecuteReader();
                reader.Read();

                Nome = Convert.ToString(reader["nome"]);
                DescontoMaximo = Convert.ToDouble(reader["desconto_maximo"]);
                AcrescimoMaximo = Convert.ToDouble(reader["acrescimo_maximo"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Não consegui obter os dados do funcionário, configure e sincronize antes de utilizar o dispositivo " + ex.Message);
            }
            finally
            {
                try
                {
                    reader.Close();
                }
                catch { }
            }
        }

    }
}
