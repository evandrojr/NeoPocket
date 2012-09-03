using System;
using System.Data.SqlServerCe;
using Neopocket.Forms;
using Neopocket.Utils;

namespace Neopocket.Core
{
    public class Recusa
    {
        public int Id;// id_recusa
        public static int IdCliente;
        public int IdFuncionario;
        public DateTime Data;//data_visita
        public int IdMotivo;
        public string Observacao = "";
        public string Status;

        public Recusa()
        {
            Data = new DateTime();
            Data = DateTime.Now;
            if (D.Acao == D.AcaoEnum.RecusaCadastro)
                Id = IdRecusaProximo();
        }
       
        
        public static int IdRecusaProximo()
        {
            try
            {
                return D.Bd.I("Select max(id_recusa) + 1 from recusa", true);
            }
            catch
            {
                return 1;
            }
        }

        //................................................................................
        public bool Carregar(int IdRecusa)
        {
            Id = IdRecusa;
            string sqlRecusa = @"select * from recusa where id_recusa=" + IdRecusa;
            SqlCeDataReader rdCarga = D.Bd.Linha(sqlRecusa);

            IdCliente = Convert.ToInt32(rdCarga["id_cliente"]);
            Data = Convert.ToDateTime(rdCarga["data_visita"]);
            IdFuncionario = Convert.ToInt32(rdCarga["id_funcionario"]);
            IdMotivo = Convert.ToInt32(rdCarga["id_motivo"]);
            Observacao = Convert.ToString(rdCarga["observacao"]);
            Status = Convert.ToString(rdCarga["status"]);
            return true;

        }
        //................................................................................
        public bool inserirTabelaRecusa()
        {          

            string sql = @"
            insert into
                        recusa
            (id_cliente,             
             id_funcionario,
             data_visita,
             id_motivo,             
             observacao,
             status)
                    values (" +
            Bd.SN(IdCliente) + "," +
            Bd.S(D.Funcionario.Id) + "," +
            Bd.TimeStampParaBd(Data) + "," +
            Bd.S(IdMotivo) + "," +
            Bd.SN(Observacao) + ","+
            "'N'" + ")";
             


            try
            {
                D.Bd.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao inserir recusa !", "Neo", "Sql=" + sql + ex.Message);
                return false;
            }
        }
        //...............................................................................
        public bool atualizaTabelaRecusa(int IdRecusa)
        {
             Id = IdRecusa;

            string sqlInsertRecusa = @"
            update recusa
            set
            id_motivo=" + Bd.S(IdMotivo) + "," +
            "observacao=" + Bd.SN(Observacao) + "," +
            "status='N'" +
            " where id_recusa=" + IdRecusa;

           D.Bd.ExecuteNonQuery(sqlInsertRecusa);            
           return true;

        }
        public bool ExcluirRecusa(int IdRecusa)
        {

            Id = IdRecusa;
            String sql = @"Delete from recusa where id_recusa=" + IdRecusa;
            try
            {
                D.Bd.ExecuteNonQuery(sql);
                return true;
            }
            catch (Exception ex)
            {
                FE.Show("Erro ao excluir recusa", "Neo", "Sql=" + sql + ex.Message);
                return false;
            }
        }




    }
}
