using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Xml;

namespace NeoCsv
{

    public class Csv
    {
        public char DelimitadorCaractere = ';';
        public string ArquivoNome;
        private TextReader tr;
        public CsvLinha Linha;


        public Csv(string arquivoNome)
        {
            ArquivoNome = arquivoNome;
        }

        public void ColumnsRead()
        {
            List<string> lstColunaNome = new List<string>();
            tr = new StreamReader(ArquivoNome, System.Text.Encoding.Default);
            lstColunaNome=LerLinhaTexto();
            Linha = new CsvLinha();

            foreach (string s in lstColunaNome)
            {
                CsvColuna col = new CsvColuna(s);
                Linha.LstCsvColuna.Add(col);
            }
        }

        public void Fechar(){
            tr.Close();
        }

        public void EscreveCsv(DataTable dt, string ArquivoNome)
        {
            StringBuilder sb = new StringBuilder();
            string valor="";
            TextWriter tw = new StreamWriter(ArquivoNome, false, System.Text.Encoding.Default);

            for (int x = 0; x < dt.Columns.Count; ++x)
            {
                sb.Append(dt.Columns[x].ColumnName);
                if(x < dt.Columns.Count -1)
                    sb.Append(DelimitadorCaractere);
            }
            tw.WriteLine(sb.ToString());

            for (int y = 0; y < dt.Rows.Count; ++y)
            {
                sb = new StringBuilder();
                for (int x = 0; x < dt.Columns.Count; ++x)
                {
                    valor = dt.Rows[y][x].ToString();
                    sb.Append("\"" + valor.Replace("\"", "\"\"") + "\"");
                    if (x < dt.Columns.Count - 1)
                        sb.Append(DelimitadorCaractere);
                }
                tw.WriteLine(sb.ToString());
            }
            tw.Close();
        }

        public bool LerLinha()
        {
            string linha, valor = "";
            linha = tr.ReadLine();
            if (linha == null)
                return false;
            int linhaLength = linha.Length;
            int delimitadorPosicao = 0, delimitadorUltimaPosicao = -1;
            bool ultimoDelimitadorEraFalso = false;
            List<int> lstAspaPosA = new List<int>();
            List<int> lstAspaPosB = new List<int>();
            int aspasPosA = 0, aspasPosB = 0, delimitadorUltimaPosicaoFalsa = 0;
            int index = 0;
            int aspasCnt = 0;
            bool encontrouAspasA, encontrouAspasB;

            //Guarda a posição das aspas duplas

            do
            {
                //if (aspasCnt == 28)
                //    aspasCnt = aspasCnt;
                encontrouAspasA = encontrouAspasB = false;
                aspasPosA = linha.IndexOf('"', aspasPosA);
                if (aspasPosA == -1)
                    break;
                //Verifica se acabou 
                if (aspasPosA + 2 > linhaLength)
                {
                    //Achou A mas não achou B, esta inconsistente!
                    break;
                }
                else
                {
                    //possivel ter outro registro
                    if(linha.Length > aspasPosA +2){

                        //Detecta aspas falsa
                        if (linha[aspasPosA + 1] == '"' && linha[aspasPosA + 2] != ';')
                        {

                            //É falsa, tente a próxima aspas
                            ++aspasPosA;
                            continue;
                        }
                        else
                        {
                            encontrouAspasA = true;
                        }
                     }
                     else
                        encontrouAspasA = true;
                }

                if (aspasPosA + 2 > linhaLength)
                {
                    throw new Exception("Faltou fecha aspas em " + linha);
                }

                aspasPosB = aspasPosA;
                do
                {
                    if (aspasPosB >= linhaLength)
                    {
                        throw new Exception("Faltou fecha aspas em " + linha);
                    }
                    else
                    {
                        aspasPosB = linha.IndexOf('"', aspasPosB + 1);
                        //Toda as aspas são verdadeiras até que se prove o contrário!
                        encontrouAspasB = true;
                        //Detecta aspas falsa
                        if (aspasPosB + 2 <= linhaLength)
                            if (linha[aspasPosB + 1] == '"')
                            {
                                //É falsa, tente a próxima aspas
                                ++aspasPosB;
                                continue;
                            }
                            else
                            {
                                encontrouAspasB = true;
                                break;
                            }
                    }
                } while (linha.IndexOf('"', aspasPosB + 1) != -1);
                if (encontrouAspasA && !encontrouAspasB)
                    throw new Exception("Faltou fecha aspas em " + linha);
                lstAspaPosA.Add(aspasPosA);
                lstAspaPosB.Add(aspasPosB);
                aspasPosA = aspasPosB + 1;
                aspasCnt++;

            } while (aspasPosA != -1);

            if (lstAspaPosA.Count != lstAspaPosB.Count)
            {
                throw new Exception("Arquivo CSV inconsistente!");
            }

            //Lê os valores
            delimitadorPosicao = -1;
            do
            {
                //Entra aqui na primeira vez pois na primeira nem existia delimitador para ser falso
                if (!ultimoDelimitadorEraFalso)
                {
                    delimitadorUltimaPosicao = delimitadorPosicao;
                }

                if(!ultimoDelimitadorEraFalso)
                    delimitadorPosicao = linha.IndexOf(DelimitadorCaractere, delimitadorUltimaPosicao + 1);
                else
                    delimitadorPosicao = linha.IndexOf(DelimitadorCaractere, delimitadorUltimaPosicaoFalsa + 1);
                //Testa para ver se achou algum delimitador
                if (delimitadorPosicao == -1 && delimitadorUltimaPosicao == -1)
                    throw new Exception("Nenhum delimitador de coluna encontrado");

                // Testa para ver se o delimitador encontra era dentro de um campo,
                // não uma delimitador separador de campos;
                //ultimoDelimitadorEraFalso = false;
                ultimoDelimitadorEraFalso = false;
                for (int i = 0; i < lstAspaPosA.Count; ++i)
                {
                    if (lstAspaPosA[i] < delimitadorPosicao && delimitadorPosicao < lstAspaPosB[i])
                    {
                        ultimoDelimitadorEraFalso = true;
                        delimitadorUltimaPosicaoFalsa = lstAspaPosB[i];
                        continue;
                    }
                }
                if(ultimoDelimitadorEraFalso)
                    continue;
                delimitadorUltimaPosicaoFalsa = 0;
                //Não chegou na última virgula ainda
                if (!(delimitadorPosicao == -1))
                    valor = linha.Substring(delimitadorUltimaPosicao + 1, delimitadorPosicao - delimitadorUltimaPosicao - 1);
                else
                    valor = linha.Substring(delimitadorUltimaPosicao + 1, linhaLength - delimitadorUltimaPosicao - 1);
                //Remove aspas duplas quando houver
                if (valor.Length > 0)
                {
                    if (valor[0] == '"' && valor[valor.Length - 1] == '"')
                        valor = valor.Substring(1, valor.Length - 2);
                    valor = valor.Replace("\"\"", "\"");
                }
                Linha.LstCsvColuna[index].Valor = valor;
                ++index;
            } while (delimitadorPosicao != -1);

            return true;
        }


        private List<string> LerLinhaTexto()
        {
            string linha, valor = "";
            linha = tr.ReadLine();
            if (linha == null){
                List<string> vazia=null;
                return vazia;
            }
            int linhaLength = linha.Length;
            int delimitadorPosicao = 0, delimitadorUltimaPosicao = -1;
            bool ultimoDelimitadorEraFalso = false;
            List<string> lstValor = new List<string>();
            List<int> lstAspaPosA = new List<int>();
            List<int> lstAspaPosB = new List<int>();
            int aspasPosA = 0, aspasPosB = 0, delimitadorUltimaPosicaoFalsa = 0;
            int index = 0;
            bool encontrouAspasA, encontrouAspasB;


            //Guarda a posição das aspas duplas

            do
            {
                encontrouAspasA = encontrouAspasB = false;
                aspasPosA = linha.IndexOf('"', aspasPosA);
                if (aspasPosA == -1)
                    break;
                //Verifica se acabou 
                if (aspasPosA + 2 > linhaLength)
                {
                    //Achou A mas não achou B, esta inconsistente!
                    break;
                }
                else
                {
                    //Detecta aspas falsa
                    if (linha[aspasPosA + 1] == '"')
                    {
                        //É falsa, tente a próxima aspas
                        ++aspasPosA;
                        continue;
                    }
                    else
                    {
                        encontrouAspasA = true;
                    }
                }

                if (aspasPosA + 2 > linhaLength)
                {
                    throw new Exception("Faltou fecha aspas em " + linha);
                }

                aspasPosB = aspasPosA;
                do
                {
                    if (aspasPosB + 1 > linhaLength)
                    {
                        throw new Exception("Faltou fecha aspas em " + linha);
                    }
                    else
                    {
                        aspasPosB = linha.IndexOf('"', aspasPosB + 1);
                        //Detecta aspas falsa
                        if (aspasPosB + 2 <= linhaLength)
                            if (linha[aspasPosB + 1] == '"')
                            {
                                //É falsa, tente a próxima aspas
                                ++aspasPosB;
                                continue;
                            }
                            else
                            {
                                encontrouAspasB = true;
                                break;
                            }
                    }
                } while (aspasPosB != -1);
                if (encontrouAspasA && !encontrouAspasB)
                    throw new Exception("Faltou fecha aspas em " + linha);
                lstAspaPosA.Add(aspasPosA);
                lstAspaPosB.Add(aspasPosB);
                aspasPosA = aspasPosB + 1;

            } while (aspasPosA != -1);

            if (lstAspaPosA.Count != lstAspaPosB.Count)
            {
                throw new Exception("Arquivo CSV inconsistente!");
            }

            //Lê os valores
            delimitadorPosicao = -1;
            do
            {
                //Entra aqui na primeira vez pois na primeira nem existia delimitador para ser falso
                if (!ultimoDelimitadorEraFalso)
                {
                    delimitadorUltimaPosicao = delimitadorPosicao;
                }

                if (!ultimoDelimitadorEraFalso)
                    delimitadorPosicao = linha.IndexOf(DelimitadorCaractere, delimitadorUltimaPosicao + 1);
                else
                    delimitadorPosicao = linha.IndexOf(DelimitadorCaractere, delimitadorUltimaPosicaoFalsa + 1);
                //Testa para ver se achou algum delimitador
                if (delimitadorPosicao == -1 && delimitadorUltimaPosicao == -1)
                    throw new Exception("Nenhum delimitador de coluna encontrado");

                // Quer dizer que a delimitador encontra era dentro de um campo,
                // não uma delimitador separadora de campos;
                //ultimoDelimitadorEraFalso = false;
                ultimoDelimitadorEraFalso = false;
                for (int i = 0; i < lstAspaPosA.Count; ++i)
                {
                    if (lstAspaPosA[i] < delimitadorPosicao && delimitadorPosicao < lstAspaPosB[i])
                    {
                        ultimoDelimitadorEraFalso = true;
                        delimitadorUltimaPosicaoFalsa = lstAspaPosB[i];
                        continue;
                    }
                }
                if (ultimoDelimitadorEraFalso)
                    continue;
                delimitadorUltimaPosicaoFalsa = 0;
                //Não chegou na última virgula ainda
                if (!(delimitadorPosicao == -1))
                    valor = linha.Substring(delimitadorUltimaPosicao + 1, delimitadorPosicao - delimitadorUltimaPosicao - 1);
                else
                    valor = linha.Substring(delimitadorUltimaPosicao + 1, linhaLength - delimitadorUltimaPosicao - 1);
                //Remove aspas duplas quando houver
                if (valor[0] == '"' && valor[valor.Length - 1] == '"')
                    valor = valor.Substring(1, valor.Length - 2);
                valor = valor.Replace("\"\"", "\"");
                lstValor.Add(valor);
                ++index;
                
            } while (delimitadorPosicao != -1);
            
             return lstValor;

        }
    
   }

    public class CsvLinha
    {
        public List<CsvColuna> LstCsvColuna;
        
        public CsvLinha(){
            LstCsvColuna = new List<CsvColuna>();
        }
    }

    public class CsvColuna
    {
        public CsvColuna(string nome)
        {
            Nome = nome;
        }
        
        public string Valor;
        public string Nome;
    }
}
