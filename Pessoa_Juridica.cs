using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientLab
{
    public class Pessoa_Juridica : Cliente
    {
        public string CPNJ {get; set;}
        public string IE {get; set;}

        public Pessoa_Juridica(int id, string nome, string endereco, double valor_compra, double valor_imposto, double total)
        {
            ID = id;
            Nome = nome;
            Endereco = endereco;
            Valor_Compra = valor_compra;
            Valor_Imposto = valor_imposto;
            Total_Pagar = total;
        }

        public override double Pagar_Imposto(double valor)
        {
            valor = Valor_Compra * 0.20;
            return valor;
        }
    }
}