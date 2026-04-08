using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientLab;
using Xunit;

namespace Teste
{
    public class PessoaFisicaTests
    {
        [Fact]
        public void PessoaFisica_CalculoImposto_Funcionando()
        {
            double valor = 2500;

            // Arrange
            Pessoa_Fisica pf = new Pessoa_Fisica("Kauan", "SaoVicente", "123", "456");
            double resultado = pf.CalcularTotal(valor);

            // Assert
            Assert.Equal(2750, resultado);
        }

        [Fact]
        public void Pessoa_Juridica_CalcularImposto_Funcionando()
        {


            // Arrange
            double valor = 2500;
            Pessoa_Juridica pj = new Pessoa_Juridica("Kauan", "SaoVicente", "123", "456");
            double resultado = pj.CalcularTotal(valor);

            // Assert
            Assert.Equal(3000, resultado);
        }

        [Fact]
        public void Calculo_Com_Valor0_pf_CalculoImposto_Funcionando()
        {

            // Arrange
            double valor = 0;
            Pessoa_Fisica pf = new Pessoa_Fisica("Kauan", "SaoVicente", "123", "456");
            double resultado = pf.CalcularTotal(valor);

            // Assert
            Assert.Equal(0, resultado);
        }

        [Fact]
        public void Calculo_Com_Valor0_pj_CalculoImposto_Funcionando()
        {

            // Arrange
            double valor = 2500;
            Pessoa_Juridica pj = new Pessoa_Juridica("Kauan", "SaoVicente", "123", "456");
            double resultado = pj.CalcularTotal(valor);

            // Assert
            Assert.Equal(3000, resultado);
        }

        [Fact]
        public void Numero_Virgula_pf_CalculoImposto_Funcionando()
        {
            double valor = 2549;

            // Arrange
            Pessoa_Fisica pf = new Pessoa_Fisica("Kauan", "SaoVicente", "123", "456");
            double resultado = pf.CalcularTotal(valor);

            // Assert
            Assert.Equal(2803.9, resultado);
        }

        [Fact]
        public void Numero_Virgula_pj_CalculoImposto_Funcionando()
        {
            double valor = 2549;

            // Arrange
            Pessoa_Juridica pj = new Pessoa_Juridica("Kauan", "SaoVicente", "123", "456");
            double resultado = pj.CalcularTotal(valor);

            // Assert
            Assert.Equal(3058.8, resultado);
        }
    }
}