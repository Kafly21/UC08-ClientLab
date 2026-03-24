using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

namespace ClientLab
{
    public class Pessoa_Juridica : Cliente
    {
        public string CNPJ { get; set; }
        public string IE { get; set; }

        public Pessoa_Juridica(string nome, string endereco, string cnpj, string ie)
        {
            Nome = nome;
            Endereco = endereco;
            CNPJ = cnpj;
            IE = ie;
        }

        public Pessoa_Juridica() { }

        public override double Pagar_Imposto(double valor)
        {
            return valor * 0.20;
        }
    }
}