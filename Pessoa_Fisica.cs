using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientLab;

namespace ClientLab
{
    public class Pessoa_Fisica : Cliente
    {
        public string CPF {get; set;}

        public string RG {get; set;}
        
        public Pessoa_Fisica(string nome, string endereco, string cpf, string rg)
        {
            Nome = nome;
            Endereco = endereco;
            CPF = cpf;
            RG = rg;
        }

        public Pessoa_Fisica()
        {
            ID = 0;
            Nome = "";
            Endereco = "";
            Valor_Compra = 0; 
            Valor_Imposto = 0;
            Total_Pagar = 0;
            CPF = "";
            RG = "";    
        }
    }
}