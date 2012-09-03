using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Neopocket.Core
{
    public class Registro
    {
        static bool inicializado = false;

        /// <summary>
        /// Retorna valor informando se a tabela registro já existia ou 
        /// não
        /// </summary>
        /// <returns></returns>
        private static bool inicializar(){
            if(!inicializado)
            try
            {
                D.Bd.ExecuteNonQuery("Select count(*) from registro");
            }
            catch
            {
                D.Bd.ExecuteNonQuery(@"
                Create table [registro](
                    [campo] [nvarchar](30) PRIMARY KEY,
                    [valor] [nvarchar](200)
                )");
                return false;
            }
            return true;

        }

        public static string Ler(string campo)
        {
            if(!inicializar())
                return "";
            string valor;
            try{
             valor = D.Bd.T("Select valor from registro where campo = " + Bd.SA(campo));
                return valor;
            }catch{
                return "";
            }
        }

        public static void Gravar(string campo, string valor)
        {
            inicializar();
            
            string valorArmazenado = null;

            try{
             valorArmazenado = D.Bd.T("Select valor from registro where campo = " + Bd.SA(campo));
            }catch{
            }

            if(valorArmazenado == null){
                D.Bd.ExecuteNonQuery(@"
                Insert
                        into
                        registro (campo, valor) 
                values
                        (" + Bd.SA(campo) + ", " + Bd.SA(valor) + ")");
            }else{
                D.Bd.ExecuteNonQuery(@"
                Update
                        registro set valor = " + Bd.SA(valor) + @" 
                where campo = " + Bd.SA(campo));
            }

        }


    }
}
