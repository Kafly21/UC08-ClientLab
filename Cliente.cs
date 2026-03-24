using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientLab
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }

        public virtual double Pagar_Imposto(double valor)
        {
            return valor * 0.10;
        }
    }
}