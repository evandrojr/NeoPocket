﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms; // Only because of the progress bar
using System.Data.SqlServerCe;
using Neopocket.Forms;
using Neopocket.Utils;
using System.Collections;

namespace Neopocket
{
    /* Evandro!! Encapsula os atributos!!!!!!!!!! */
    public class Rota
    {
        public int Ordem { get; set;}
        public int Percurso { get; set; }
        public int IdCidade { get; set; }
        public string Status { get; set; }
        public string Cidade { get; set; }
        public string IdUf { get; set; }
        public DateTime ValidadeInicio { get; set; }
        public DateTime ValidadeFim { get; set; }

        public enum StatusEnum : byte{
            NaoFoiVisitado=0,
            Visitado=1,
            Pendente=2,
            Bloqueado=3,
        }

        int periodoValidade = 7;
        DateTime dataUltimoDomigo;
       
        public Rota()
        {
            dataUltimoDomigo = Fcn.DomingoPassadoData();
            ValidadeInicio = dataUltimoDomigo;
            ValidadeFim = ValidadeInicio.AddDays(periodoValidade);
        }

        public Rota(int ordem, int rota, int idCidade, string cidade, string idUf)
        {
            Ordem = ordem;
            IdCidade = idCidade;
            Cidade = cidade;
            Percurso = rota;
            IdUf = idUf;
            Status = "a visitar";
            
        }

        public static IList RotaPreenche()
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            IList l = new List<Rota>();
            string sql = @"
            Select 
                    visitacao_ordem, id_rota, id_cidade, cidade, id_uf
            from
                    rota_cidade
            where 
                    id_vendedor=" + D.Funcionario.Id; 
            cmd = new SqlCeCommand(sql,D.Bd.Con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                l.Add(new Rota((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4]));
            }
            return l;
        }
    }

    public class RotaCliente
    {

        public int Ordem { get; set;}
        public int Percurso { get; set; }
        public int IdCidade { get; set; }
        public string Status { get; set; }
        public string Cidade { get; set; }
        public string IdUf { get; set; }

        int periodoValidade = 7;
        DateTime dataUltimoDomigo;
       
        public RotaCliente()
        {
            
        }

        public RotaCliente(int ordem, int rota, int idCidade, string cidade, string idUf)
        {
            Ordem = ordem;
            IdCidade = idCidade;
            Cidade = cidade;
            Percurso = rota;
            IdUf = idUf;
            Status = "a visitar";
            dataUltimoDomigo = Fcn.DomingoPassadoData();
        }

        public static IList RotaPreenche()
        {
            SqlCeCommand cmd = null;
            SqlCeDataReader reader = null;
            IList l = new List<Rota>();
            string sql = @"
            Select 
                    visitacao_ordem, id_rota, id_cidade, cidade, id_uf
            from
                    rota_cidade
            where 
                    id_vendedor=" + D.Funcionario.Id; 
            cmd = new SqlCeCommand(sql,D.Bd.Con);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                l.Add(new Rota((int)reader[0], (int)reader[1], (int)reader[2], (string)reader[3], (string)reader[4]));
            }
            return l;
        }


    }


}
