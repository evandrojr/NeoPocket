using System;
using System.Data;
using System.Windows.Forms;
using Neopocket.Utils;
using Neopocket.Core;
using System.Data.SqlServerCe;
using System.Text;

namespace Neopocket.Forms
{
    /// <summary>
    /// Formulário de listagem de pedidos
    /// </summary>
    public partial class FrmPedidoLista : FormBase
    {
        #region [ Atributos ]

        private Form formReference;

        #endregion

        #region [ Construtor ]

        public FrmPedidoLista(Form formReference)
        {
            InitializeComponent();
            this.formReference = formReference;
        }

        #endregion

        #region [ Novo pedido ]

        private void btnNovo_Click(object sender, EventArgs e)
        {
            D.Pedido = new Pedido();
            inputPanel.Enabled = false;
            D.Acao = D.AcaoEnum.PedidoCadastro;
            Util.FormExibir(new FrmPedido());
            this.Close();
        }

        #endregion

        #region [ Load ]

        private void FrmPedidoLista_Load(object sender, EventArgs e)
        {
            this.TelaAtualizar();

            if (Pedido.PedidosAEnviar())
            {
                if (Pedido.PedidosRestantes == 0)
                {
                    btnNovo.Enabled = false;
                }
            }

            if (!D.Cliente.Ativo)
            {
                btnNovo.Enabled = btnEditar.Enabled = btnExcluir.Enabled = false;
            }

            //Mostra os títulos em aberto se existirem
            if (D.Cliente.ListaNegra)
            {
                string qryTit = @"
                select 
                        count(*)
                from
                        titulo_aberto ta, especie_financeira ef
                where
                        ta.id_especie_financeira=ef.id_especie_financeira and 
                        ta.id_cliente = " + D.Cliente.IdStore;
                if (D.Bd.I(qryTit) > 0)
                {
                    FrmTitulos f = new FrmTitulos();
                    f.ShowDialog();
                }
            }
        }

        #endregion

        #region [ Atualiza a tela ]

        private void TelaAtualizar()
        {
            lblCliente.Text = "\n" + D.Cliente.NomeFantasia;
            lblLimite.Text =  "  " + Fcn.DinheiroFormata(D.Cliente.CreditoRestante);
            lblDebito.Text =  "  " + Fcn.DinheiroFormata(D.Cliente.CreditoUtilizado);
            lblCredito.Text = "  " + Fcn.DinheiroFormata(D.Cliente.LimiteCreditoBd);

            DataTable dtPedido;
            if (D.Cliente.IdTipo == Cliente.IdTipoEnum.IdPocket)
                dtPedido = D.Bd.DataTablePreenche("Select id_pedido, data, status, valor from pedido where id_cliente_pocket='" + D.Cliente.Id + "' order by status , id_pedido desc", "pedido");
            else
                dtPedido = D.Bd.DataTablePreenche("Select id_pedido, data, status, valor from pedido where id_cliente_store='" + D.Cliente.IdStore + "' order by status , id_pedido desc", "pedido");

            grdPedido.DataSource = dtPedido;

            if (grdPedido.VisibleRowCount == 0)
            {
                btnEditar.Visible = false;
                btnExcluir.Visible = false;
            }
        }

        #endregion

        #region [ Edição de um pedido ]

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int i = grdPedido.CurrentRowIndex;
            if (i == -1)
                return;

            D.Acao = D.AcaoEnum.PedidoEdicao;
            D.Pedido.Carregar((Guid)grdPedido[i, 0]);
            if (D.Pedido.Status == "N")
            {
                Util.FormExibir(new FrmPedido());
                this.Close();
            }
            else
                MessageBox.Show("Pedido sincronizado não pode ser alterado! ", "Neo");
        }

        #endregion

        #region [ Exclusão de um pedido ]

        private void btnExcluir_Click(object sender, EventArgs e) /* Excluir pedido  */
        {
            int i = grdPedido.CurrentRowIndex;
            if (i == -1)
                return;

            D.Pedido.Id = (Guid)(grdPedido[i, 0]);

            if (Convert.ToString(grdPedido[i, 2]) == "N")
            {
                if (MessageBox.Show("Deseja apagar o pedido?", "Atenção",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    D.Pedido.Carregar(D.Pedido.Id);
                    if (D.Pedido.PedidoExcluir())
                    {
                        MessageBox.Show("Pedido excluído", "Neo");
                        D.Acao = D.AcaoEnum.PedidoADefinir;
                    }
                    else
                    {
                        MessageBox.Show("Erro excluindo pedido", "Neo");
                    }
                    TelaAtualizar();
                    btnNovo.Enabled = true;
                }
            }
            else
                MessageBox.Show("Pedido sincronizado não pode ser removido! ", "Neo");
        }

        #endregion

        #region [ Voltar ]

        private void mnuVoltar_Click(object sender, EventArgs e)
        {
            if (formReference is FrmClienteLista)
            {
                Util.FormExibir(new FrmClienteLista());
                this.Close();
            }
            else if (formReference is FrmRotaProcessa)
            {
                ((FrmRotaProcessa)formReference).UpdateView();
                this.Close();
            }
        }

        #endregion

        #region [ Marcar a visita como pendente ]


        private void mnuMarcarVisitaPendente_Click(object sender, EventArgs e)
        {
            SqlCeTransaction trans = null;
            Rota r = new Rota();
            try
            {
                if (D.Cliente == null)
                    throw new Exception("Nenhum cliente carregado!");

                if (D.Funcionario == null)
                    throw new Exception("Nenhum funcionário carregado!");

                int IdCliente = D.Cliente.IdStore;
                if (IdCliente <= 0)
                    throw new Exception("Somente cliente cadastrados no NeoStore podem ter visitação agendada!");

                int IdFuncionario = D.Funcionario.Id;
                int Status = (int)Rota.StatusEnum.Pendente;
                DateTime DtVisita = DateTime.Now;

                int rowsAffected = -1;

                using (trans = D.Bd.Con.BeginTransaction())
                {
                    using (SqlCeCommand cmd = new SqlCeCommand(String.Empty, D.Bd.Con))
                    {
                        // Verificar se existe alguma visita agendada para aquele cliente no periodo de validade e que já não esteja pendente!
                        cmd.CommandText = "SELECT COUNT(*) as Total FROM visita WHERE id_cliente=@id_cliente and (data_visita >= @data_visita_ini and data_visita <= @data_visita_end)";
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = IdCliente;
                        cmd.Parameters.Add("@data_visita_ini", SqlDbType.DateTime).Value = r.ValidadeInicio;
                        cmd.Parameters.Add("@data_visita_end", SqlDbType.DateTime).Value = r.ValidadeFim;

                        Int32 total = 0;

                        try
                        {
                            total = Int32.Parse(cmd.ExecuteScalar().ToString());
                        }
                        catch { }

                        cmd.Parameters.Clear();

                        StringBuilder builder = new StringBuilder();

                        if (total == 0)
                        {
                            builder.AppendLine("INSERT INTO visita (id_cliente, id_funcionario, status, data_visita)");
                            builder.AppendLine("VALUES");
                            builder.AppendLine("(@id_cliente, @id_funcionario, @status, @data_visita)");
                            cmd.Parameters.Add("@id_funcionario", SqlDbType.Int).Value = IdFuncionario;
                            cmd.Parameters.Add("@data_visita", SqlDbType.DateTime).Value = DtVisita;
                        }
                        else
                        {
                            // Partindo do pre suposto que so deixe cadastrar uma unica visita para o cliente no determinado periodo
                            builder.AppendLine("UPDATE visita SET status=@status WHERE id_cliente=@id_cliente and (data_visita >= @data_visita_ini and data_visita <= @data_visita_end)");
                            cmd.Parameters.Add("@data_visita_ini", SqlDbType.DateTime).Value = r.ValidadeInicio;
                            cmd.Parameters.Add("@data_visita_end", SqlDbType.DateTime).Value = r.ValidadeFim;
                        }

                        cmd.CommandText = builder.ToString();
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = IdCliente;
                        cmd.Parameters.Add("@status", SqlDbType.Int).Value = Status;

                        // Executa o comando
                        rowsAffected = cmd.ExecuteNonQuery();
                        trans.Commit();
                        MessageBox.Show("Cliente marcado como pendente");
                    }
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
