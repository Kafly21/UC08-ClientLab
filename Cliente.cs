using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientLab
{
    public class Cliente
    {
        public int ID {get; set;}

        public string Nome {get; set;}

        public string Endereco {get; set;}

        public double Valor_Compra {get; set;}

        public double Valor_Imposto {get; set;}

        public double Total_Pagar {get; set;}

        public virtual double Pagar_Imposto(double valor)
        {
            valor = Valor_Compra * 0.10;
            return valor;
        }
    }
}