using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NeoPocketUpdater
{

    //Classe para ser usada em menus drop-down 
    public class IdDescricao : Object
    {
        public int Id;
        public string Descricao;

        public IdDescricao(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public override string ToString()
        {
            return Descricao;
        }
    }

    /// <summary>
    /// Inteiro ou não inicializada (null)
    /// </summary>
    public struct IntN{
        bool iniciada;
        int valor;

        public IntN(int v)
        {
            iniciada = true;
            valor = v;
        }
        public bool Ler(out int preenchaEssaVariavel)
        {
            if (iniciada)
            {
                preenchaEssaVariavel = valor;
                return true;
            }
            else
            {
                preenchaEssaVariavel = -9999999;
                return false;
            }
        }

        public int V
        {
            set { valor = value; 
                iniciada = true; }
            get
            {
                if (!iniciada)
                {
                    throw new Exception("Este valor não foi definido");
                }
                return valor;
            }
        }

        public bool Iniciada
        {
            get { return iniciada; }
        }
    }

    /// <summary>
    /// Double ou não inicializada (null)
    /// </summary>
    public struct DoubleN
    {
        bool iniciada;
        Double valor;

        public DoubleN(Double v)
        {
            iniciada = true;
            valor = v;
        }
        public bool Ler(out double preenchaEssaVariavel)
        {
            if (iniciada)
            {
                preenchaEssaVariavel = valor;
                return true;
            }
            else
            {
                preenchaEssaVariavel = -9999999.99;
                return false;
            }
        }

        public double V
        {
            set { valor = value; 
                iniciada = true; }
            get
            {
                if (!iniciada)
                {
                    throw new Exception("Este valor não foi definido");
                }
                return valor;
            }
        }

        public bool Iniciada
        {
            get { return iniciada; }
        }
    }

    /// <summary>
    /// Boleano ou não inicializada (null)
    /// </summary>
    public struct BoolN
    {
        bool iniciada;
        bool valor;
        public BoolN(bool v)
        {
            iniciada = true;
            valor = v;
        }
        public bool Ler(out bool preenchaEssaVariavel)
        {
            if (iniciada)
            {
                preenchaEssaVariavel = valor;
                return true;
            }
            else
            {
                preenchaEssaVariavel = false;
                return false;
            }
        }

        public bool V
        {
            set { valor = value; 
                iniciada = true; }
            get
            {
                if (!iniciada)
                {
                    throw new Exception("Este valor não foi definido");
                }
                return valor;
            }
        }

        public bool Iniciada
        {
            get { return iniciada; }
        }
    }

    /// <summary>
    /// DateTime ou não inicializada (null)
    /// </summary>
    public struct DateTimeN
    {
        bool iniciada;
        DateTime valor;
        public DateTimeN(DateTime v)
        {
            iniciada = true;
            valor = v;
        }
        public bool Ler(out DateTime preenchaEssaVariavel)
        {
            if (iniciada)
            {
                preenchaEssaVariavel = valor;
                return true;
            }
            else
            {
                preenchaEssaVariavel = new DateTime();
                return false;
            }
        }
        public DateTime V
        {
            set { valor = value;
                iniciada = true; }
            get
            {
                if (!iniciada)
                {
                    throw new Exception("Este valor não foi definido");
                }
                return valor;
            }
        }

        public bool Iniciada
        {
            get { return iniciada; }
        }
    }
    
    public class NNT
    { //Number, Number. Text
        public double N0, N1;
        public string T;

        public NNT(double n0, double n1, string t)
        {
            N0 = n0; N1 = n1; T = t;
        }
    }

    public class NTT
    { //Number, Text, Text
        public double N;
        public string T0, T1;

        public NTT(double n, string t0, string t1)
        {
            N = n; T0 = t0; T1 = t1;
        }
    }

    public class N4
    { //Number, Number, Number, Number
        public double N0;
        public double N1;
        public double N2;
        public double N3;

        public N4(double n0, double n1, double n2, double n3)
        {
            N0 = n0; N1 = n1; N2 = n2; N3 = n3;
        }
    }

    public class N3T
    { //Number, Number, Number, Text
        public double N0;
        public double N1;
        public double N2;
        public string T;

        public N3T(double n0, double n1, double n2, string t)
        {
            N0 = n0; N1 = n1; N2 = n2; T = t;
        }
    }

    public class NN
    { //Number, Number
        public double N0;
        public double N1;

        public NN(double n0, double n1)
        {
            N0 = n0; N1 = n1;
        }
    }

    public class NT
    { //Number, Text
        public double N;
        public string T;

        public NT(double n, string t)
        {
            N = n; T = t;
        }
    }


    public struct IT
    { //int, text
        public int I;
        public string T;

        public IT(int i, string t)
        {
            I = i; T = t;
        }

    }


    public class DI
    { //Double, int
        public double D;
        public int I;
        
    }

}
