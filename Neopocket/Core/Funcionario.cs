using System;
using System.Data.SqlServerCe;
using Neopocket.Utils;

namespace Neopocket.Core
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
        private bool validado=false;

        public bool Validado
        {
            get { return validado; }
        }

        public bool Valida(string nome, string senha)
        {
            if (nome.Trim() == D.APP_USER_NAME && senha.Trim() == D.APP_USER_PASS)
                validado = true;
            else
                validado = false;
            return validado;
        }

        //Chamar sempre apos carregar os parametros
        public void Carregar()
        {
            SqlCeDataReader reader = null;

            try
            {
                string sql = "SELECT * FROM funcionario WHERE id=" + Id;
                SqlCeCommand cmd = new SqlCeCommand(sql, D.Bd.Con);
                reader = cmd.ExecuteReader();
                reader.Read();

                Nome = Convert.ToString(reader["nome"]);
                DescontoMaximo = Convert.ToDouble(reader["desconto_maximo"]);
                AcrescimoMaximo = Convert.ToDouble(reader["acrescimo_maximo"]);
            }
            catch (Exception ex)
            {
                throw new Exception("Verifique se o código do funcionário '" + Id + "'está correto. ", ex);
            }
            finally
            {
                reader.Close();
            }
        }

    }
}
