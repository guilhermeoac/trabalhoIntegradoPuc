using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Locacao.TesteUnitario.Dominio
{
    public class LocacaoTest
    {
        [Fact]
        public void Deve_Instanciar_Entidade_Valida()
        {
            var locacaoDb = new Locacao.Dominio.ModeloDB.LocacaoDB()
            {
                Id = 1,
                ValorHora = 12.5,
                DataInicioLocacao = new DateTime(),
                DataFimLocacao = new DateTime(),
                ClienteId = 1,
                VeiculoId = 1,
            };
            var veiculo = new Locacao.Dominio.Entidades.Veiculo()
            {
                Id = 1,
                Placa = "HJD-2i31",
                Ano = 2015,
                ValorHora = 12.5,
                Combustivel = "Gasolina",
                LimitePortaMalas = 300,
                CategoriaString = "Basico",
                CategoriaId = 1,
                Marca = "Chevrollet",
                Modelo = "Onix 1.0",
            };
            var instance = new Locacao.Dominio.Entidades.Locacao(locacaoDb, veiculo);
            Assert.Equal("HJD-2i31", instance.Veiculo.Placa);
            Assert.Equal(1, instance.ClienteId);
            Assert.Equal("Chevrollet", instance.Veiculo.Marca);
            Assert.Equal("Onix 1.0", instance.Veiculo.Modelo);
        }

        [Fact]
        public void Deve_Instanciar_Entidade_Invalida()
        {
            var locacaoDb = new Locacao.Dominio.ModeloDB.LocacaoDB();
            var veiculo = new Locacao.Dominio.Entidades.Veiculo();
            var instance = new Locacao.Dominio.Entidades.Locacao(locacaoDb, veiculo);
            Assert.Null(instance.Veiculo.Placa);
            Assert.Equal(0, instance.ClienteId);
            Assert.Null(instance.Veiculo.Marca);
            Assert.Null(instance.Veiculo.Modelo);
        }
    }
}
