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
    public class Roteiro
    {
        string usuario, senha, servidor, diretorio;
        List<string> lstArquivosCarregar = new List<string>();
        List<string> lstDiretoriosCarregar = new List<string>();
        List<string> lstClienteEnviar = new List<string>();
        List<string> lstTituloAbertoEnviar = new List<string>();
        List<string> lstRotaCidadeEnviar = new List<string>();
        List<string> lstArquivoVelhoCliente = new List<string>();
        public static List<string> lstPedidoFormaPagamento;
        public static List<string> lstPedidoCodigo;
        // Guarda código dos pedidos que acidentalmente estão sendo reinseridos
        public static List<Guid> lstPedidoJaCadastradoAnteriomente; 
        public static bool Sincronizando = false;
        ShowLib.FTP ftpCon = new ShowLib.FTP();
        FrmPrincipal janela;
        List<string> lstArquivosCompactar = new List<string>();
        List<string> lstString = new List<string>();
        string atributoArquivoEnviarNome = "ATRIBUTO.csv";
        string cidadeArquivoEnviarNome = "CIDADE.csv";
        string especieFinanceiraArquivoEnviarNome = "ESPECIEFINANCEIRA.csv";
        string formaPagamentoArquivoEnviarNome = "FORMADEPAGAMENTO.csv";
        string gradeArquivoEnviarNome = "GRADE.csv";
        string itemAtributoArquivoEnviarNome = "ITEMATRIBUTO.csv";
        string itemFormaPagamentoArquivoEnviarNome = "ITEMFORMAPAGAMENTO.csv";
        string tabelaPrecoFormaPagamentoArquivoEnviarNome = "FORMAPAGAMENTOTABELAPRECO.csv";
        string itemGradeArquivoEnviarNome = "ITEMGRADE.csv";
        string itemTabelaPrecoArquivoEnviarNome = "ITEMTABELAPRECO.csv";
        string motivoArquivoEnviarNome = "MOTIVO.csv";
        string parametroArquivoEnviarNome = "PARAMETRO.csv";
        string produtoArquivoEnviarNome = "PRODUTO.csv";
        string saldoGradeArquivoEnviarNome = "SALDOGRADE.csv";
        string tabelaPrecoArquivoEnviarNome = "TABELAPRECO.csv";
        string clientesCadastradosArquivoEnviarNome = "CLIENTESCADASTRADOS.csv";
        string rotaCidadeArquivoEnviarNome = "ROTACIDADE.csv";
        string funcionarioArquivoEnviarNome = "FUNCIONARIO.csv";
        
        public FbTransaction Transacao;

        public Roteiro()
        {
            lstPedidoFormaPagamento = new List<string>();
            lstPedidoCodigo = new List<string>();
            lstPedidoJaCadastradoAnteriomente  = new List<Guid>();
            D.Roteiro = this;
            // create reader & open file
            TextReader tr = null;
            try
            {
                tr = new StreamReader(D.ApplicationDirectory + "neosync.ini");
                D.BancoAlvo = tr.ReadLine().Trim();
                // close the stream
            }
            catch (Exception ex)
            {
                janela.MsgAppend("Não consegui localizar o arquivo neosync.ini " + ex.Message);
            }
            finally
            {
                tr.Close();
            }

            Bd bd = new Bd();
            D.Bd = bd;

            bd.ConStr = D.ConexaoParamentros();

            try
            {
                bd.Connect();
            }
            catch(Exception ex) {
                NeoDebug.Debug.ErrorRecord("Não foi possível conectar ao banco de dados " + ex.Message + " " + ex.StackTrace);
                FE.Show("Não foi possível conectar ao banco de dados ", "Erro", ex.Message + " " + ex.StackTrace);
                System.Environment.Exit(1);
            }
            ParametroCriar();
            Parametro.Carregar();
        }

        public void ParametroCriar(){
            //Cria esses parametros automaticamente caso eles não existam na base
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_FTP_SERVIDOR', 'C', '', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_FTP_USUARIO', 'C', '', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_FTP_SENHA', 'C', '', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_FTP_DIRETORIO', 'C', '', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_VENDER_SEM_ESTOQUE', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_CLIENTE_SEM_REFERENCIA', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_DIAS_PERMANENCIA', 'I', '15', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_ESCOLHER_TABELA', 'B', '0', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_LIMITE_CREDITO_PADRAO', 'F', '500', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_LIMITE_PEDIDOS_EM_ABERTO', 'I', '1000', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_MOSTRAR_REFERENCIA_PRODUTO', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_VERIFICAR_CREDITO', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_MINUTOS_ENTRE_SINCRONIZACOES', 'I', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_CLIENTE_EDICAO_PERMITIDA', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_CLIENTE_EDICAO_PERMITIDA', 'B', '1', 1)");
            }
            catch { }
            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_PERIODICAMENTE_ENVIAR_ID_CLIENTES', 'B', '1', 1)");
            }
            catch { }

            try
            {
                D.Bd.ExecuteNonQuery("Insert into parametro (NOME, TIPO, VALOR, IMPORTA_TRANSFERENCIA) VALUES ('POCKET_ENVIAR_TODOS_CLIENTES_PARA_TODOS_VENDEDORES', 'B', '1', 1)");
            }
            catch { }
        }

        public Roteiro(FrmPrincipal j):this(){
            janela = j;
        }

        public string Sincronizar()
        {
            string msg = "";
            /*
             * Listar arquivos do servidor
             * Para cada arquivo na lista:
             *      Baixe
             *      Descompacte
             *      Apague
             * Lista pastas locais
             * Para cada pasta na lista:
             *      Importar Recusa
             *      Deletar recusa
             * Para cada pasta na lista:
             *      Para cada arquivo na pasta
             *          Tente:         
             *              Iniciar transacao
             *                  Importar clientes
             *                  Importar pedido
             *                  Importar item pedido
             *                  Importar item pedido grade
             *                  Gerar formar de pagamento
             *              Finalizar transacao
             *          Capture erros:    
             *              Desfaz transacão em caso de erro
             *              Gera log de erro
             *      Fim <Para cada arquivo na pasta>
             * FIM <cada pasta na lista:>     
             * //Exporta tabelas
             * Criar diretorio no ftp com nome BASE_ENVIANDO_ARQUIVOS
             * Copiar arquivos
             * Renomear diretório para BASE_2009-09-12-h09m07s45
             * Apagar o 5 último diretório no FTP
             * 
             * 
             * */

            Op.AtualizationInProgressCheck();
            
            configurar();

            if ((msg = listar()) != "")
                return msg;
            else
                janela.MsgAppend("Encontrados " + lstArquivosCarregar.Count + " arquivos para serem coletados");
            
            foreach (string ftpArquivo in lstArquivosCarregar)
            {
                if ((msg = Receber(ftpArquivo)) != "")
                    return msg;
                if ((msg = descompacta(ftpArquivo)) != "")
                    return msg;
                //Apaga arquivos enviado pelo pocket no FTP
                if ((msg = ftpApagarArquivo(ftpArquivo)) != "")
                    return msg; 
            }

            if ((msg = listarDiretorios()) != "")
                return msg;

            //importa a recusa, não tem transação por que a recusa não tem dependências
            foreach (string diretorioImportacao in lstDiretoriosCarregar)
            {
                try
                {
                    recusaImportar(diretorioImportacao);
                }
                catch (Exception ex)
                {
                    NeoDebug.Debug.ErrorRecord("Não foi possível sincronizar a pasta " + diretorioImportacao + " avise o suporte da Neo, vou continuar processando os outros pedidos " + ex.Message + " " + ex.StackTrace);
                    FE.Show("Não foi possível sincronizar a pasta " + diretorioImportacao + " avise o suporte da Neo, vou continuar processando os outros pedidos ", "Erro", ex.Message + " " + ex.StackTrace);
                    continue;
                }
            }

            //Importar pedidos, clientes e deletar arquivos
            foreach (string diretorioImportacao in lstDiretoriosCarregar)
            {
                lstPedidoFormaPagamento.Clear();
                lstPedidoCodigo.Clear();
                try
                {
                    transacaoImportacaoIniciar();
                    clienteImportarEApagar(diretorioImportacao);
                    pedidoImportar(diretorioImportacao);
                    itemPedidoImportar(diretorioImportacao);
                    itemPedidoGradeImportar(diretorioImportacao);
                    formaPagamentoGerar();
                    transacaoImportacaoFinalizar();
                }
                catch (Exception ex)
                {
                    transacaoImportacaoDesfazer();
                    NeoDebug.Debug.ErrorRecord("Não foi possível sincronizar a pasta " + diretorioImportacao + " avise o suporte da Neo, vou continuar processando os outros pedidos " + ex.Message + " " + ex.StackTrace);
                    FE.Show("Não foi possível sincronizar a pasta " + diretorioImportacao + " avise o suporte da Neo, vou continuar processando os outros pedidos ", "Erro", ex.Message + " " + ex.StackTrace);
                    continue;
                }
                foreach(string arquivoImportado in Directory.GetFiles(diretorioImportacao))
                {
                    File.Delete(arquivoImportado);
                }
            }

            //Aviso sonoro e piscante para dizer que um arquivo chegou, caso tenha chegado
            arquivoChegouAvisar();
            if ((msg = clientesExportar()) != "")
                return msg;

            if (Parametro.PeriodicamenteEnviarIdClientes)
            {
                if (DateTime.Now.Day == 1 || DateTime.Now.Day == 7 || DateTime.Now.Day == 14 || DateTime.Now.Day == 21)
                {
                    if ((msg = clientesCadastradosExportar()) != "")
                        return msg;
                }
                else
                {
                    janela.MsgAppend("Hoje não é dia para mandar pacote com todos os clientes para checagem de cadastro duplicado. Só nos dias 1, 7, 14 e 21.");
                }
            }
            if ((msg = rotaCidadeExportar()) != "")
                return msg;
            if ((msg = produtoExportar()) != "")
                return msg;
            if ((msg = funcionarioTabelaExportar()) != "")
                return msg;
            if ((msg = tabelaAtributoExportar()) != "")
                return msg;
            if ((msg = tabelaCidadeExportar()) != "")
                return msg;            
            if ((msg = tabelaEspecieFinaceiraExportar()) != "")
                return msg;            
            if ((msg = tabelaFormaPagamentoExportar()) != "")
                return msg;            
            if ((msg = tabelaGradeExportar()) != "")
                return msg;            
            if ((msg = tabelaItemAtributoExportar()) != "")
                return msg;            
            if ((msg = tabelaItemFormaPagamentoExportar()) != "")
                return msg;
            if ((msg = tabelaTabelaPrecoFormaPagamentoExportar()) != "")
                return msg;            
            if ((msg = tabelaItemGradeExportar()) != "")
                return msg;            
            if ((msg = tabelaItemTabelaPrecoExportar()) != "")
                return msg;            
            if ((msg = tabelaMotivoExportar()) != "")
                return msg;            
            if ((msg = tabelaParametroExportar()) != "")
                return msg;            
            if ((msg = tabelaPrecoExportar()) != "")
                return msg;            
            if ((msg = tabelaSaldoGradeExportar()) != "")
                return msg;
            if ((msg = Compactar()) != "")
                return msg; 
            if((msg = ArquivoAntigoClienteListar()) !="")
                return msg;
            if ((msg = ArquivoClienteAntigoLimpar()) != "")
                return msg; 
            if ((msg = Enviar()) != "")
                return msg;

            //Apagar arquivos locais
            arquivosLocaisApagar();
            janela.MsgAppend(">>>Sincronização executada com sucesso<<<");
            return "";
        }

        private string arquivosLocaisApagar()
        {
            string[] aDirApagar = Directory.GetDirectories(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio);
            
            try {
                foreach (string s in aDirApagar)
                {
                    if(Directory.GetFiles(s).Length == 0)
                         Directory.Delete(s); 
                }
            }
            catch (Exception ex)
            {
                janela.MsgAppend(ex.Message);
                Debug.ErrorRecord(ex.Message);
                return ex.Message;
            }
            return "";
        }

        private string ftpApagarArquivo(string f)
        {
            string msg = "";
            janela.MsgAppend( "Removendo arquivo " + f + " enviados pelo pocket " );
            try
            {
                ftpCon.RemoveFile(f);
            }
            catch (Exception ex)
            {
                janela.MsgAppend(ex.Message);
                Debug.ErrorRecord(ex.Message);
            }
            return msg;
        }

        private string arquivoChegouAvisar()
        {
            try
            {
                if (lstArquivosCarregar.Count >= 1)
                {
                    janela.AlertaAtivo(true);
                    System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
                    myPlayer.SoundLocation = D.ApplicationDirectory + @"media\tada.wav";
                    myPlayer.Play();
                }
            }
            catch { }
            return "";
        }

        private string ArquivoClienteAntigoLimpar()
        {
            string msg = "";
            janela.MsgAppend("Removendo arquivos antigos de clientes " );
            foreach (string f in lstArquivoVelhoCliente)
            {
                try
                {
                    ftpCon.RemoveFile(f);
                }
                catch (Exception ex)
                {
                    janela.MsgAppend("Erro ao apagar o arquivo: " + f + " " + ex.Message);
                    Debug.ErrorRecord(ex.Message);
                }
            }
            return msg;
        }            
            
        private string exportar()
        {
            string msg = "";
            clientesExportar();
            return msg;
        }

        private string transacaoImportacaoIniciar()
        {
            string msg = "";
            Transacao = D.Bd.Con.BeginTransaction(); 
            return msg;
        }

        private string transacaoImportacaoFinalizar()
        {
            string msg = "";
            Transacao.Commit();
            return msg;
        }

        private string transacaoImportacaoDesfazer()
        {
            string msg = "";
            Transacao.Rollback();
            return msg;
        }


        private string formaPagamentoGerar()
        {
            string msg = "";
            double total=0;
            Dictionary<string, string> p; //par [Campo, valor] do banco de dados
            for(int i=0; i < lstPedidoCodigo.Count; ++i){
                total = D.Bd.N("Select TOTAL from Pedido where Codigo=" + lstPedidoCodigo[i] + " and TipoRegistro ='S'", D.Roteiro.Transacao);
                p = new Dictionary<string, string>();

//                p.Add("@ITIPOTABELA", "P");
                p.Add("@ITIPOMOVIMENTACAO", "S");
                p.Add("@IACAO", "I");
                p.Add("@ICOD_FORMAPAGAMENTO", lstPedidoFormaPagamento[i]);
                p.Add("@IGERARITENS", "1");
                p.Add("@IVALOR", total.ToString());
                p.Add("@ICODIGO", lstPedidoCodigo[i]);

                D.Bd.StoredProcedureExecute("SP_PKT_IMPORTA_FORMAPAGTO", p, D.Roteiro.Transacao);
            }
            return msg;
        }
        
        

        private string clientesExportar()
        {
            DataTable dtClientes;
            string msg = "";
            StringBuilder clienteArquivoEnviarNome;
            List<string> lstClienteCompactar; // Só possui um cliente de cada vez mas é melhor do que fazer outra função que compacte apenas um arquivo
            StringBuilder qryCliente;
            List<string> lstVendedor = new List<string>();

            lstVendedor = D.Bd.LstT("select CODIGO from funcionario where PARTICIPA_FORCA_VENDA='1'");
            if(Parametro.EnviarTodosClientesParaTodosOsVendedores==false){
                janela.MsgAppend("Exportando " + lstVendedor.Count + " arquivos de clientes");
                qryCliente = new StringBuilder(@"
                            select 
                                  *
                            from 
                                    VW_CLIENTE_POCKET
                            where
                                    COD_FUNCIONARIO=");
                StringBuilder qryCodFuncionario;
                for (int i = 0; i < lstVendedor.Count; ++i)
                {
                    //Exportando clientes
                    clienteArquivoEnviarNome = new StringBuilder("CLR");
                    qryCodFuncionario = new StringBuilder("'");
                    qryCodFuncionario.Append(lstVendedor[i]).Append("'");
                    dtClientes = D.Bd.DataTablePreenche(qryCliente.ToString() + qryCodFuncionario.ToString());

                    clienteArquivoEnviarNome.Append(lstVendedor[i]);
                    lstClienteCompactar = new List<string>();
                    lstClienteCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".csv");
                    Csv csvCliente = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome);

                    csvCliente.EscreveCsv(dtClientes, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".csv");
                    NeoZip.Zip.ZipFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".zip", lstClienteCompactar);
                    lstClienteEnviar.Add(clienteArquivoEnviarNome + ".zip");

                    titulosEmAbertoExportar(lstVendedor[i]);
               }
            }
            else{ //
                janela.MsgAppend("Exportando todos os clientes para todos os " + lstVendedor.Count + " vendedores");
                qryCliente = new StringBuilder(@"
                            select 
                                  *
                            from 
                                    VW_CLIENTE_POCKET");
                for (int i = 0; i < lstVendedor.Count; ++i)
                {
                    //Exportando clientes
                    clienteArquivoEnviarNome = new StringBuilder("CLR");
                    dtClientes = D.Bd.DataTablePreenche(qryCliente.ToString());

                    clienteArquivoEnviarNome.Append(lstVendedor[i]);
                    lstClienteCompactar = new List<string>();
                    lstClienteCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".csv");
                    Csv csvCliente = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome);

                    csvCliente.EscreveCsv(dtClientes, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".csv");
                    NeoZip.Zip.ZipFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clienteArquivoEnviarNome + ".zip", lstClienteCompactar);
                    lstClienteEnviar.Add(clienteArquivoEnviarNome + ".zip");

                    titulosEmAbertoExportar(lstVendedor[i]);
                }
            }
            return msg;
        }

        /// <summary>
        /// Exporta os títulos em aberto de um cliente
        /// Se o codFuncionario for nulo exporta o de todos os clientes
        /// </summary>
        /// <param name="codFuncionario"></param>
        /// <returns></returns>
        private string titulosEmAbertoExportar(string codFuncionario)
        {
            List<string> titulosEmAbertoLst;
            string msg = "";

            string qryTitulos = @"
            select
                    c.codigo as id_cliente,
                    tr.COD_ESPECIEFINANCEIRA as id_especie_financeira,
                    tr.VALOR as valor,
                    tr.DATAVENCIMENTO as vencimento_data,
                    tr.VALOR_PAGO as pago,
                    tr.VALORJUROS_DEVIDO as juros_dinheiro,
                    tr.SALDO_RECEBER_JUROS as a_receber
            from
                     VW_TITULOS_ABERTOS_RECEBER tr, CLIENTE c
            where   
                    tr.VERIFICAR_CREDITO = 1 and
                    tr.cod_sacado = 'C' || c.codigo and
                    c.listanegra=1 and
                    current_date > tr.datavencimento ";

            if(Parametro.EnviarTodosClientesParaTodosOsVendedores==false)
            {
                qryTitulos += " and c.COD_FUNCIONARIO= '" + codFuncionario + "'";
            }

            StringBuilder TitulosAbertosEnviarNome = new StringBuilder("TITULOS_ABERTOS");
            DataTable dtTitulosAbertos = D.Bd.DataTablePreenche(qryTitulos);

            TitulosAbertosEnviarNome.Append(codFuncionario);
            titulosEmAbertoLst = new List<string>();
            titulosEmAbertoLst.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + TitulosAbertosEnviarNome + ".csv");
            Csv csvCliente = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + TitulosAbertosEnviarNome + ".csv");

            csvCliente.EscreveCsv(dtTitulosAbertos, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + TitulosAbertosEnviarNome + ".csv");
            NeoZip.Zip.ZipFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + TitulosAbertosEnviarNome + ".zip", titulosEmAbertoLst);
            lstTituloAbertoEnviar.Add(TitulosAbertosEnviarNome + ".zip");

            return msg;
        }

        private string rotaCidadeExportar()
        {

            DataTable dtRotaCidade;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela de rotas para as cidades");

            qry = @"select 
                        *
                from 
                        VW_CIDADES_POCKET";
            dtRotaCidade = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + rotaCidadeArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + rotaCidadeArquivoEnviarNome);
            csv.EscreveCsv(dtRotaCidade, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + rotaCidadeArquivoEnviarNome);
            return msg;
        }

        private string clientesCadastradosExportar()
        {
            DataTable dtClientesCadastrados;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela de CNPJs e CPFs");

            qry = @"select distinct CGC_CPF as CPNJ_CPF from cliente";
            dtClientesCadastrados = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clientesCadastradosArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clientesCadastradosArquivoEnviarNome);
            csv.EscreveCsv(dtClientesCadastrados, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + clientesCadastradosArquivoEnviarNome);
            return msg;
        }


        private string produtoExportar()
        {
            DataTable dtProdutos;
            string msg = "";
            string qry="";

            string ApenasProdutoComEstoquePositivo =" and (S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA) > 0 ";

            if (Parametro.VenderSemEstoque)
                ApenasProdutoComEstoquePositivo = "";

            janela.MsgAppend( "Exportando produtos " );
            if(D.Loja!="000000"){
            qry = @"
            SELECT
                      P.CODIGO,
                      P.DESCRICAO,
                      P.COD_UNIDADE_VENDA,
                      P.COD_GRADE,
                      P.REFERENCIA,
                      P.PERMITIR_VENDER_PESSOA_FISICA as PERMITIR_VENDA_NAO_CONTRIBUINTE,
                      COALESCE(S.PRECOVENDA, 0.0) as PRECOVENDA,
                      COALESCE(S.PRECOPROMOCAO, 0.0) as PRECOPROMOCAO,
                      S.DATAINICIOPROMOCAO,
                      S.DATAFIMPROMOCAO,
                      S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA AS QUANTIDADEESTOQUE, 
                      U.FRACIONADA,
                      (case
                          when (U.fator is null) then '1'
                       else
                         case
                           when (U.fator in ('0')) then '1'
                         else
                             U.fator
                         end
                      end) as UNIDADE_FATOR
                      FROM
                              PRODUTO P INNER JOIN
                              SALDO S ON S.COD_PRODUTO = P.CODIGO INNER JOIN
                              UNIDADE U ON P.COD_UNIDADE_VENDA = U.CODIGO
                      WHERE 
                              P.APLICACAO IN ('A','V','B')and P.ATIVO ='1' 
                              " + ApenasProdutoComEstoquePositivo + @" and 
                              S.COD_LOJA='" + D.Loja + "'";
            }
            dtProdutos = D.Bd.DataTablePreenche(qry);
            // Criar pasta
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + produtoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + produtoArquivoEnviarNome);
            csv.EscreveCsv(dtProdutos, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + produtoArquivoEnviarNome);
            return msg;
        }


        private string tabelaPrecoExportar()
        {
            DataTable dtTabelaPreco;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela de preço " );

            qry = @"select CODIGO,DESCRICAO,PERCENTUALAJUSTE,TIPOAJUSTE from TABELAPRECO";
            dtTabelaPreco = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoArquivoEnviarNome);
            csv.EscreveCsv(dtTabelaPreco, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoArquivoEnviarNome);
            return msg;
        }

        private string tabelaItemTabelaPrecoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela de preço específica " );

            qry = @"select
                        COD_TABELAPRECO, COD_PRODUTO, VALOR, QTD_MINIMA,
                        VALOR1, QTD_MINIMA1, VALOR2, QTD_MINIMA2, VALOR3, QTD_MINIMA3,
                        DESCONTO_MAXIMO, ACRESCIMO_MAXIMO, TIPOVALOR
                    from
                         ITEMTABELAPRECOPRODUTO";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemTabelaPrecoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemTabelaPrecoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemTabelaPrecoArquivoEnviarNome);
            return msg;
        }

        private string tabelaFormaPagamentoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela forma de pagamento ");

            qry = @"select 
                        f.codigo,
                        f.prazo_medio,
                        f.descricao,
                        (select count(i.cod_formapagamento)from itemformapagamento i
                            where 
                                i.cod_formapagamento = f.codigo)  as parcelas,
                        parcela_minima 
                   from 
                        formapagamento f
                   WHERE 
                        PERMITIRRECEBIMENTO='1'";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + formaPagamentoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + formaPagamentoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + formaPagamentoArquivoEnviarNome);
            return msg;
        }

        private string tabelaItemFormaPagamentoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando tabela item forma pagamento" );

            qry = @"
            SELECT
                   i.COD_FORMAPAGAMENTO, i.CODIGO, i.COD_ESPECIEFINANCEIRA, i.PRAZOVENCIMENTO, i.PERCENTUALPAGAMENTO
            FROM
                   ITEMFORMAPAGAMENTO i, FORMAPAGAMENTO F
            where
                   i.COD_FORMAPAGAMENTO = f.CODIGO
                   AND f.PERMITIRRECEBIMENTO = 1";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemFormaPagamentoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemFormaPagamentoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemFormaPagamentoArquivoEnviarNome);
            return msg;
        }

        private string tabelaTabelaPrecoFormaPagamentoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando as formas de pagamento das tabelas de preço");

            qry = @"
            SELECT
                    COD_TABELAPRECO, COD_FORMAPAGAMENTO
            FROM
                    FORMAPAGAMENTOTABELAPRECO";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoFormaPagamentoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoFormaPagamentoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + tabelaPrecoFormaPagamentoArquivoEnviarNome);
            return msg;
        }



        private string tabelaEspecieFinaceiraExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela espécie financeira" );

            qry = @"SELECT CODIGO, DESCRICAO, VERIFICA_CREDITO FROM ESPECIEFINANCEIRA";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + especieFinanceiraArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + especieFinanceiraArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + especieFinanceiraArquivoEnviarNome);
            return msg;
        }

        private string tabelaParametroExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando tabela de parametros" );

            qry = @"select NOME, TIPO, VALOR from PARAMETRO where substring(NOME from 1 for 6) = 'POCKET'";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + parametroArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + parametroArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + parametroArquivoEnviarNome);
            return msg;
        }

        private string tabelaCidadeExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando tabela de cidades " );

            qry = @"
                SELECT CODIGO,
                       DESCRICAO,
                       COD_UF
                FROM
                CIDADE
                       WHERE
                COD_UF='BA'
            UNION
                SELECT
                        CIDADE.CODIGO,
                        CIDADE.descricao,
                        CIDADE.COD_UF
                from
                        CIDADE, cliente
                WHERE cliente.cod_cidade = cidade.codigo
                                                            ";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + cidadeArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + cidadeArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + cidadeArquivoEnviarNome);
            return msg;
        }

        private string tabelaMotivoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando tabela de motivos de recusa de pedidos " );

            qry = @"SELECT CODIGO, DESCRICAO FROM MOTIVONAOCOMPRA";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + motivoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + motivoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + motivoArquivoEnviarNome);
            return msg;
        }


        private string funcionarioTabelaExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend("Exportando tabela de vendedores ");

            qry = @"SELECT CODIGO, NOME, DESCONTO_MAXIMO, ACRESCIMO_MAXIMO FROM FUNCIONARIO WHERE PARTICIPA_FORCA_VENDA ='1'";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + funcionarioArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + funcionarioArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + funcionarioArquivoEnviarNome);
            return msg;
        }

        private string tabelaGradeExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando informações das grades " );

            qry = @"SELECT CODIGO, DESCRICAO FROM GRADE";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + gradeArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + gradeArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + gradeArquivoEnviarNome);
            return msg;
        }

        private string tabelaItemGradeExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando informações dos itens das grades " );

            qry = @"SELECT COD_GRADE, CODIGO, DESCRICAO FROM ITEMGRADE";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemGradeArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemGradeArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemGradeArquivoEnviarNome);
            return msg;
        }

        private string tabelaAtributoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando informações dos atributos " );

            qry = @"SELECT CODIGO, DESCRICAO FROM ATRIBUTO";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + atributoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + atributoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + atributoArquivoEnviarNome);
            return msg;
        }

        private string tabelaItemAtributoExportar()
        {
            DataTable dt;
            string msg = "";
            string qry;

            janela.MsgAppend( "Exportando informações dos item atributo " );

            qry = @"SELECT COD_ATRIBUTO, CODIGO, DESCRICAO FROM ITEMATRIBUTO";
            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemAtributoArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemAtributoArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + itemAtributoArquivoEnviarNome);
            return msg;
        }

        private string tabelaSaldoGradeExportar()
        {
            DataTable dt;
            string msg = "";
            string qry="";

            janela.MsgAppend( "Exportando informações do saldo da grade " );


            if (D.Loja != "000000")
            {
                qry = @"
                SELECT
                        G.COD_PRODUTO,
                        G.COD_GRADE,
                        G.COD_ITEMGRADE,
                        G.COD_ATRIBUTO,
                        G.COD_ITEMATRIBUTO,
                            (G.QUANTIDADEESTOQUE - G.QUANTIDADEEMPENHADA) AS
                        QUANTIDADEESTOQUE,
                        P.CODIGO
                FROM
                        PRODUTO P INNER JOIN
                            SALDO S ON S.COD_PRODUTO = P.CODIGO INNER JOIN
                            SALDOGRADE G ON P.CODIGO = G.COD_PRODUTO
                WHERE
                        (G.QUANTIDADEESTOQUE - G.QUANTIDADEEMPENHADA > 0) AND
                        (S.PRECOVENDA IS NOT NULL) AND
                        (P.APLICACAO IN ('A', 'V', 'B')) AND
                        (P.ATIVO = '1') AND 
                        (S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA > 0) and 
                        S.COD_LOJA='" + D.Loja + "'";
            }
//            else
//            {
//                qry = @"
//                SELECT P.CODIGO, P.DESCRICAO, P.COD_UNIDADE_VENDA, P.COD_GRADE, u.fracionada,
//                P.REFERENCIA, S.PRECOVENDA, S.PRECOPROMOCAO, S.DATAINICIOPROMOCAO, S.DATAFIMPROMOCAO,
//                (S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA) as QUANTIDADEESTOQUE,
//                       sum(s.quantidadeestoque - s.quantidadeempenhada) as estoque
//                  FROM saldo s
//                  join produto p on (p.codigo = s.cod_produto)
//                  join unidade u on (u.codigo = p.cod_unidade_venda)
//                 WHERE PRECOVENDA is NOT NULL and P.APLICACAO IN ('A','V','B')and 
//                        P.ATIVO ='1' and 
//                        (S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA) > 0
//                 group by P.CODIGO, P.DESCRICAO, P.COD_UNIDADE_VENDA, P.COD_GRADE, u.fracionada, P.REFERENCIA, S.PRECOVENDA, S.PRECOPROMOCAO, S.DATAINICIOPROMOCAO, S.DATAFIMPROMOCAO,
//                (S.QUANTIDADEESTOQUE - S.QUANTIDADEEMPENHADA)";
//            }

            dt = D.Bd.DataTablePreenche(qry);
            lstArquivosCompactar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + saldoGradeArquivoEnviarNome);
            Csv csv = new Csv(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + saldoGradeArquivoEnviarNome);
            csv.EscreveCsv(dt, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + saldoGradeArquivoEnviarNome);
            return msg;
        }


        private string descompacta(string f)
        {
            string msg = "";

            try
            {
                Directory.CreateDirectory(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + f.Substring(0, f.Length - 4));
            }
            catch(Exception ex)
            {
                janela.MsgAppend( ex.Message);
            }

            try
            {
                janela.MsgAppend("Descompactando " + f);
                Zip.UnzipFiles(D.ApplicationDirectory + D.BackupDiretorio + f, D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + f.Substring(0, f.Length - 4) + @"\");
            }
            catch(Exception ex)
            {
                return ex.Message;
            }

            return msg;
        }

        private string clienteImportarEApagar(string diretorio)
        {
            string msg = "";
            string[] arCliente = Directory.GetFiles(diretorio + @"\", "CLT*");

            MapeamentoBdCsv map;

            foreach (string arquivo in arCliente)
            {
                map = new MapeamentoBdCsv("cliente", arquivo, D.Bd);
                map.ClienteCarregar();
            }
            return msg;
        }

        private string pedidoImportar(string diretorio)
        {

            string msg = "";
            string[] arPedido = Directory.GetFiles(diretorio + @"\", "PEDIDOS*");
            MapeamentoBdCsv map;

            foreach (string arquivo in arPedido)
            {   
                map = new MapeamentoBdCsv("pedido", arquivo, D.Bd);
                map.PedidoCarregar();
            }
            return msg;
        }

        private string itemPedidoImportar(string diretorio)
        {
            string msg = "";
            string[] arPedido = Directory.GetFiles(diretorio + @"\", "ITENSDOPEDIDO*");
            MapeamentoBdCsv map;

            foreach (string arquivo in arPedido)
            {
                map = new MapeamentoBdCsv("item_pedido", arquivo, D.Bd);
                map.ItemPedidoCarregar();
            }
            return msg;
        }


        private string itemPedidoGradeImportar(string diretorio)
        {
            string msg = "";
            string[] arPedido = Directory.GetFiles(diretorio + @"\", "ITEMDOPEDIDOGRADE*");
            MapeamentoBdCsv map;

            foreach (string arquivo in arPedido)
            {
                map = new MapeamentoBdCsv("item_pedido_grade", arquivo, D.Bd);
                map.ItemPedidoGradeCarregar();
            }
            return msg;
        }

        private string recusaImportar(string diretorio)
        {
            string msg = "";
            string[] arPedido = Directory.GetFiles(diretorio + @"\", "REC*");
            MapeamentoBdCsv map;

            foreach (string arquivo in arPedido)
            {
                map = new MapeamentoBdCsv("recusa", arquivo, D.Bd);
                map.RecusaCarregar();
                try
                {
                    File.Delete(arquivo);
                }catch{}
            }
            return msg;
        }

        public string Iniciar()
        {

            if (!Directory.Exists(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio))
                Directory.CreateDirectory(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio);
            string[] aArqApagar = Directory.GetFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio);
            foreach (string s in aArqApagar)
            {
                try { File.Delete(s); }
                catch (Exception ex)
                {
                    janela.MsgAppend(ex.Message);
                    Debug.ErrorRecord(ex.Message);
                }
            }
            
            return "";
        }

        private void configurar()
        {
            servidor = D.Bd.T("Select VALOR from PARAMETRO where NOME='POCKET_FTP_SERVIDOR'");
            usuario = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_USUARIO'");
            senha = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_SENHA'");
            //porta = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_PORTA'");
            diretorio = D.Bd.T("Select valor from parametro where nome='POCKET_FTP_DIRETORIO'");
            if(diretorio != "")
                if (diretorio[(diretorio.Length - 1)] != '/')
                    diretorio += "/";
        }

        private string listar()
        {
            
            int inicioPos; 
            try
            {
                ftpCon = new ShowLib.FTP();
                ftpCon.Connect(servidor, usuario, senha);
                if(diretorio != "")
                    ftpCon.ChangeDir(diretorio);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            try
            {
                foreach (string f in ftpCon.List())
                {
                    if(f.Contains("POCKETRT")){
                        inicioPos = f.IndexOf("POCKETRT");
                        lstArquivosCarregar.Add(f.Substring(inicioPos, f.Length - inicioPos));
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        private string listarDiretorios()
        {
            int inicioPos;
            try
            {
                foreach (string f in Directory.GetDirectories(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio))
                {
                    if (f.Contains("POCKETRT"))
                    {
                        inicioPos = f.IndexOf("POCKETRT");
                        lstDiretoriosCarregar.Add(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + f.Substring(inicioPos, f.Length - inicioPos));
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        private string ArquivoAntigoClienteListar()
        {

            int inicioPos;
            try
            {
                ftpCon.Connect(servidor, usuario, senha);
                if(diretorio!="")
                    ftpCon.ChangeDir(diretorio);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                foreach (string f in ftpCon.List())
                {
                    if (f.Contains("CLR"))
                    {
                        inicioPos = f.IndexOf("CLR");
                        lstArquivoVelhoCliente.Add(f.Substring(inicioPos, f.Length - inicioPos));
                    }
                }
            }
            catch (Exception ex)
            {
                janela.MsgAppend("Observacão: não consegui encontrar o arquivo de clientes da última sincronização " + ex.Message);
            }
            return "";
        }
        /// <summary>
        /// Baixa todos os aquivos que correspondem ao padrão
        /// </summary>
        /// <returns></returns>
        public string Receber(string s)
        {
            try
            {
                int perc = 0;

                // open the file with resume support if it already exists, the last 
                // peram should be false for no resume


                try
                {
                    Directory.CreateDirectory(D.ApplicationDirectory + D.BackupDiretorio);
                }
                catch (Exception ex)
                {
               //     janela.MsgAppend(ex.Message);
                }

                ftpCon.OpenDownload(s, D.ApplicationDirectory + D.BackupDiretorio + s);
                
                while (ftpCon.DoDownload() > 0)
                {
                    perc = (int)((ftpCon.BytesTotal * 100) / ftpCon.FileSize);
                    janela.MsgAppend("Downloading: " + ftpCon.BytesTotal + "/" + ftpCon.FileSize + " " + perc + "%");
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            janela.MsgAppend( "Arquivos recebidos " );
            return "";
        }


        public string Compactar()
        {
            Zip.ZipFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + D.arquivoEnvioNome, lstArquivosCompactar);
            janela.MsgAppend( "Arquivos compactados " );
            return "";
        }

        //private static StringBuilder montaCodigoVendedor(string codigoVendedor)
        //{
        //    StringBuilder nomeDoArquivo = new StringBuilder();
        //    nomeDoArquivo.Append("Vend").Append(codigoVendedor);
        //    return nomeDoArquivo;
        //}

        private static StringBuilder montarNomeArquivo()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Ano" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("yyyy"));
            s.Append("Mes" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("MM"));
            s.Append("Dia" + System.DateTime.Parse(DateTime.Now.ToString()).ToString("dd") + "_h");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("HH") + "m");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("mm") + "s");
            s.Append(System.DateTime.Parse(DateTime.Now.ToString()).ToString("ss"));
            //s.Append(montaCodigoVendedor(D.Funcionario.Id.ToString()));
            return s;
        }

        public void ErroVisualizar(Exception ex)
        {
            janela.MsgAppend("Erro" + ex.Message);
            Debug.ErrorRecord(ex);
        }

        private List<string> pastasFtp(FTP ftplib)
        {
            List<string> lstPastas = new List<string>();
            foreach (string d in ftplib.ListDirectories())
            {
                if (d.ToUpper().Substring(d.LastIndexOf(" ") + 1, Math.Min(3, d.Length - 1 - d.LastIndexOf(" "))) == "ANO")
                {
                    lstPastas.Add(d.ToUpper().Substring(d.LastIndexOf(
                    " ") + 1, d.Length - 1 - d.LastIndexOf(" ")));
                }
            }
            lstPastas.Sort();
            return lstPastas;
        }

        public string Enviar()
        {
            try
            {
                //Pacotão geral
                janela.MsgAppend("Enviando arquivos para servidor");
                ftpCon.OpenUpload(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + D.arquivoEnvioNome, "BASE_ENVIANDO_ARQUIVOS\\" + D.arquivoEnvioNome);
                while (ftpCon.DoUpload() > 0) { }
                //Especifico para cada funcionário
                foreach (string s in lstClienteEnviar)
                {
                    ftpCon.OpenUpload(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + s, "BASE_ENVIANDO_ARQUIVOS\\" +s);
                    while (ftpCon.DoUpload() > 0) { }
                }
                //Especifico para cada funcionário
                foreach (string s in lstTituloAbertoEnviar)
                {
                    ftpCon.OpenUpload(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio + s, "BASE_ENVIANDO_ARQUIVOS\\" + s);
                    while (ftpCon.DoUpload() > 0) { }
                }
                ftpCon.RenameFile(diretorio + @"\BASE_ENVIANDO_ARQUIVOS", diretorio + montarNomeArquivo());
                foreach (string f in Directory.GetFiles(D.ApplicationDirectory + D.TabelasSincronizacaoDiretorio))
                {
                    File.Delete(f);
                }

                List<string> lstPastas = pastasFtp(ftpCon);

                janela.MsgAppend("Excluindo pastas antigas do servidor FTP");
                int intPastasExcluidas = 0;
                for (int intIndex = 0; intIndex < lstPastas.Count - 5; intIndex++) 
                {
                    ftpCon.ChangeDir("/" + lstPastas[intIndex].ToString());
                    foreach (string f in ftpCon.ListFiles())
                    {
                        ftpCon.RemoveFile("/" + lstPastas[intIndex].ToString() + "/" + f.ToUpper().Substring(f.LastIndexOf(" ")+1, f.Length - 1 - f.LastIndexOf(" ")));
                        intPastasExcluidas++;
                    }
                    ftpCon.ChangeDir("/");
                    ftpCon.RemoveDir("/" + lstPastas[intIndex].ToString());
                }
                if (intPastasExcluidas>0)
                janela.MsgAppend(intPastasExcluidas + " pastas excluídas");
            }
            catch (Exception ex)
            {
                ErroVisualizar(ex);
                return ex.Message;
            }
            janela.MsgAppend( "Arquivos enviados " );
            return "";
        }


         


    }
}
