﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Core;

namespace NeoSync
{
    public static class Parametro
    {
        public static bool VenderSemEstoque;
        public static bool VerificarCreditoVendaAPrazo;
        public static bool UsarReferenciaProduto;
        public static int LimitePedidosEmAberto;
        public static double LimiteDeCreditoPadrao;
        public static bool ClienteEdicaoPermitida = true;
        public static bool PeriodicamenteEnviarIdClientes = false;
        public static bool EnviarTodosClientesParaTodosOsVendedores = false;

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
                return "0";
            }
            return valor;
        }

        public static bool LerBoleano(string parametro)
        {
            if (Ler(parametro) == "1")
                return  true;
            else
                return false;
        }

        public static bool Carregar(){
        
            string msg = "Não consegui carregar o(s) parametro(s) ";
            LstParametrosNaoCarregados.Clear();

            UsarReferenciaProduto = LerBoleano("POCKET_MOSTRAR_REFERENCIA_PRODUTO");
            if (Ler("POCKET_CLIENTE_EDICAO_PERMITIDA") == "1")
                ClienteEdicaoPermitida = true;
            else
                ClienteEdicaoPermitida = false;

            if (Ler("POCKET_VENDER_SEM_ESTOQUE") == "1")
                VenderSemEstoque = true;
            else
                VenderSemEstoque = false;

            if(Ler("POCKET_PERIODICAMENTE_ENVIAR_ID_CLIENTES") == "1")
                PeriodicamenteEnviarIdClientes = true;
            else
                PeriodicamenteEnviarIdClientes = false;

            if (Ler("POCKET_ENVIAR_TODOS_CLIENTES_PARA_TODOS_VENDEDORES") == "1")
                EnviarTodosClientesParaTodosOsVendedores = true;
            else
                EnviarTodosClientesParaTodosOsVendedores = false;

            LimitePedidosEmAberto = Int32.Parse(Ler("POCKET_LIMITE_PEDIDOS_EM_ABERTO"), D.CultureInfoBRA);
			if(LimitePedidosEmAberto == 0)
					LimitePedidosEmAberto = 1000;
            LimiteDeCreditoPadrao = Double.Parse(Ler("POCKET_LIMITE_CREDITO_PADRAO"), D.CultureInfoBRA);
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
