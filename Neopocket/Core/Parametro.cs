﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Neopocket.Utils;

namespace Neopocket.Core
{
    public static class Parametro
    {
        public static bool VenderSemEstoque;
        public static bool VerificarCreditoVendaAPrazo;
        public static bool UsarReferenciaProduto;
        public static int LimitePedidosEmAberto;
        public static int ProdutoPrecoCasasDecimais;
        public static double LimiteDeCreditoPadrao;
        public static bool ClienteEdicaoPermitida = true; 


        public static List<string> LstParametrosNaoCarregados = new List<string>();

        public static string Ler(string parametro)
        {
            string valor;
            parametro=parametro.ToUpper();
            try
            {
                valor = D.Bd.T(@"
                SELECT
                        valor
                FROM
                        parametro
                WHERE
                        (UPPER(nome) = '" + parametro + "')", true);
            }
            catch
            {
                LstParametrosNaoCarregados.Add(parametro);
                return null;
            }
            return valor;
        }

        public static void Gravar(string parametro, string valor)
        {
            if (Ler(parametro) != null)
            {
                D.Bd.ExecuteNonQuery("update parametro set valor='" + valor + "' where nome='" + parametro + "'");
            }
            else
            {
                D.Bd.ExecuteNonQuery("insert into parametro (nome, valor) values ('" + parametro + "','" + valor + "')");
            }
        }

        public static bool LerBoleano(string parametro)
        {
            if (Ler(parametro) == "1")
                return  true;
            else
                return false;
        }

        public static bool Carregar(){
        
            string msg = "Não consegui carregar o(s) parâmetro(s) ";
            string checker = ""; // string para verificar ser o valor é null ou não
            LstParametrosNaoCarregados.Clear();

            checker = Ler("POCKET_PRODUTO_PRECO_CASAS_DECIMAIS");
            if (checker == null)
                ProdutoPrecoCasasDecimais = 4;
            else
                ProdutoPrecoCasasDecimais = Int32.Parse(checker, D.CultureInfoBRA);

            if (Ler("POCKET_CLIENTE_EDICAO_PERMITIDA") == "1")
                ClienteEdicaoPermitida = true;
            else
                ClienteEdicaoPermitida = false;

            if (Ler("POCKET_VENDER_SEM_ESTOQUE") == "1")
                VenderSemEstoque = true;
            else
                VenderSemEstoque = false;

            if (Ler("POCKET_MOSTRAR_REFERENCIA_PRODUTO") == "1")
                UsarReferenciaProduto = true;
            else
                UsarReferenciaProduto = false;

            checker = Ler("POCKET_LIMITE_PEDIDOS_EM_ABERTO");
            if (checker == null)
                LimitePedidosEmAberto = 1000;
            else
                LimitePedidosEmAberto = Int32.Parse(checker, D.CultureInfoBRA);
            
            checker = Ler("POCKET_LIMITE_CREDITO_PADRAO");
            if(checker==null)         
                LimiteDeCreditoPadrao = 1000;
            else
                LimiteDeCreditoPadrao = Double.Parse(checker, D.CultureInfoBRA);

            if (Ler("POCKET_VERIFICAR_CREDITO") == "1")
                VerificarCreditoVendaAPrazo = true;
            else
                VerificarCreditoVendaAPrazo = false;

            foreach (string s in LstParametrosNaoCarregados)
            {
                msg+= s+ Environment.NewLine;
            }
            if (LstParametrosNaoCarregados.Count > 0)
            {
                msg += "' vou tentar continuar assim mesmo";
                MessageBox.Show(msg);
            }
            if (LstParametrosNaoCarregados.Count > 0)
                return false;
            else
                return true;
        }


        static Parametro(){

        }
    }
}
