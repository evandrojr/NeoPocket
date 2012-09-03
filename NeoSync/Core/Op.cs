using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using NeoZip;
using ShowLib;
using System.Data;
using System.Data.Common;
using FirebirdSql.Data.FirebirdClient;//provider do SGBD FireBird
using Core;
using NeoDebug;

namespace NeoSync
{
    public static class Op
    {
        public static void AtualizationInProgressCheck()
        {

            //Verificar se o Neo Update irá atualizar, se for saia!
            int tempoDesligamento =
            D.Bd.I("Select TEMPO_DESLIGAMENTO From ATUALIZACAO");
            if (tempoDesligamento >= 0)
            {
                FrmNotification.Show("A sincronização não pode ser iniciada, pois o sistema será atualizado", 4);
                Environment.Exit(1);
            }
            else
            {
                D.Bd.ExecuteNonQuery(
                   "update ATUALIZACAO set CLIENTE_CONECTADO = 1");
            }
        }
    }
}
