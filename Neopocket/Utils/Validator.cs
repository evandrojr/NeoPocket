﻿using System;
using System.Text.RegularExpressions;

namespace Neopocket.Utils
{
    /// <summary>
    /// Classe responsável por validações
    /// </summary>
    public class Validator
    {
        /// <summary>
        /// Válida se uma string é um CNPJ
        /// </summary>
        /// <param name="cnpj">Valor a ser comparado</param>
        /// <returns>Boolean</returns>
        public static Boolean IsCnpj(String cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Válida se uma string é um CPF
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static Boolean IsCpf(String cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Válida se uma string é um número inteiro baseado na cultura BR
        /// </summary>
        /// <param name="v">Valor a ser comparado</param>
        /// <returns>Boolean</returns>
        public static Boolean IsInteger(String v)
        {
            try
            {
                long.Parse(v, System.Globalization.NumberStyles.Integer, D.CultureInfoBRA);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Válida se uma string é um número real baseado na cultura BR
        /// </summary>
        /// <param name="v">Valor a ser comparado</param>
        /// <returns>Boolean</returns>
        public static Boolean IsReal(String v)
        {
            try
            {
                Double.Parse(v, D.CultureInfoBRA);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Válida se uma string é um cep
        /// </summary>
        /// <param name="cep">Valor a ser comparado</param>
        /// <returns>Boolean</returns>
        public static Boolean ValidaCEP(String cep)
        {
            cep = cep.Replace(".", "");
            cep = cep.Replace("-", "");
            cep = cep.Replace(" ", "");

            Regex Rgx = new Regex(@"^\d{8}$");

            if (!Rgx.IsMatch(cep))

                return false;

            else

                return true;

        }

    }
}
