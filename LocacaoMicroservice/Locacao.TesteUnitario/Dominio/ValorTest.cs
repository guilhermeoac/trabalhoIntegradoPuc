using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Locacao.TesteUnitario.Dominio
{
    public class ValorTest
    {
        [Fact]
        public void Deve_Instanciar_Entidade_Valida()
        {
            var instance = new Locacao.Dominio.Entidades.Valor(1800, 450);
            Assert.Equal(2250, instance.ValorTotalLocacao);
            Assert.Equal(1800, instance.ValorTotalDiaria);
            Assert.Equal(450, instance.ValorTotalVistoria);
            Assert.True(instance.ValorTotalLocacao > 0);
            Assert.True(instance.ValorTotalDiaria > 0);
            Assert.True(instance.ValorTotalVistoria > 0);
        }

        [Fact]
        public void Deve_Instanciar_Entidade_Invalida()
        {
            var instance = new Locacao.Dominio.Entidades.Valor(-200, -300);
            Assert.False(instance.ValorTotalLocacao > 0);
            Assert.False(instance.ValorTotalDiaria > 0);
            Assert.False(instance.ValorTotalVistoria > 0);
        }
    }
}
