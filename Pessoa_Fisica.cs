using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientLab
{
    public class Pessoa_Fisica : Cliente
    {
        public string CPF {get; set;}

        public string RG {get; set;}
        
        public Pessoa_Fisica(int id, string nome, string endereco, double valor_compra, double valor_imposto, double total)
        {
            ID = id;
            Nome = nome;
            Endereco = endereco;
            Valor_Compra = valor_compra;
            Valor_Imposto = valor_imposto;
            Total_Pagar = total;
        }
    }
}