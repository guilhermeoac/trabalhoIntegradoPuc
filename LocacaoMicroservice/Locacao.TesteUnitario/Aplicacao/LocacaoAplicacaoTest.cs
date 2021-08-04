using Locacao.Aplicacao;
using Locacao.Dominio.Entidades;
using Locacao.Dominio.Repositorios;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Locacao.TesteUnitario.Aplicacao
{
    public class LocacaoAplicacaoTest
    {
        protected Mock<ILocacaoRepositorio> mockLocacao;
        protected Mock<IVeiculoRepositorio> mockVeiculo;

        public LocacaoAplicacaoTest()
        {
            mockLocacao = new Mock<ILocacaoRepositorio>();
            mockVeiculo = new Mock<IVeiculoRepositorio>();
        }
        [Fact]
        public void Deve_Inserir_Locacao_Valida()
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
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.InserirLocacao(locacaoDb);
            mockLocacao.Verify(m => m.InserirLocacao(locacaoDb), Times.Once);
        }

        [Fact]
        public void Deve_Inserir_Locacao_Invalida()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.InserirLocacao(null);
            mockLocacao.Verify(m => m.InserirLocacao(null), Times.Never);
        }

        [Fact]
        public void Deve_Listar_Locacoes_Validas()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>()
                        {
                            new Locacao.Dominio.ModeloDB.LocacaoDB()
                            {
                                Id = 1,
                                ValorHora = 12.5,
                                DataInicioLocacao = new DateTime(),
                                DataFimLocacao = new DateTime(),
                                ClienteId = 1,
                                VeiculoId = 1,
                            }
                        };
            mockLocacao.Setup(a => a.ListarLocacoes()).Returns(locacoes);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarLocacoes();
            Assert.Single(locacoes);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Listar_Locacoes_Vazia()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>();
            mockLocacao.Setup(a => a.ListarLocacoes()).Returns(locacoes);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarLocacoes();
            Assert.Empty(locacoes);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Listar_Locacoes_Por_Data_E_Cliente_Valida()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>()
                        {
                            new Locacao.Dominio.ModeloDB.LocacaoDB()
                            {
                                Id = 1,
                                ValorHora = 12.5,
                                DataInicioLocacao = new DateTime(),
                                DataFimLocacao = new DateTime(),
                                ClienteId = 1,
                                VeiculoId = 1,
                            }
                        };
            mockLocacao.Setup(a => a.ListarLocacoesPorDataECliente(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(locacoes);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarLocacoesPorDataECliente(new DateTime(), new DateTime(), 1);
            Assert.Single(locacoes);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Listar_Locacoes_Por_Data_E_Cliente_Vazia()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>();
            mockLocacao.Setup(a => a.ListarLocacoesPorDataECliente(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>())).Returns(locacoes);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarLocacoesPorDataECliente(new DateTime(), new DateTime(), 1);
            Assert.Empty(locacoes);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Listar_Locacoes_Por_Data_E_Cliente_Invalida()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>();
            mockLocacao.Setup(a => a.ListarLocacoesPorDataECliente(It.IsAny<DateTime>(), It.IsAny<DateTime>(), 0)).Returns(locacoes);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarLocacoesPorDataECliente(new DateTime(), new DateTime(), 0);
            Assert.Empty(locacoes);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
            mockLocacao.Verify(m => m.ListarLocacoesPorDataECliente(new DateTime(), new DateTime(), 0), Times.Never);
        }

        [Fact]
        public void Deve_Listar_Veiculos_Validos()
        {
            var veiculos = new List<Veiculo>()
                        {
                            new Veiculo()
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
                            }
                        };
            mockVeiculo.Setup(a => a.ListarVeiculos()).Returns(veiculos);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarVeiculos();
            Assert.Single(veiculos);
            mockVeiculo.Verify(m => m.ListarVeiculos(), Times.Once);
        }

        [Fact]
        public void Deve_Listar_Veiculos_Vazia()
        {
            var veiculos = new List<Veiculo>();
            mockVeiculo.Setup(a => a.ListarVeiculos()).Returns(veiculos);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarVeiculos();
            Assert.Empty(veiculos);
            mockVeiculo.Verify(m => m.ListarVeiculos(), Times.Once);
        }

        [Fact]
        public void Deve_Listar_Veiculos_Disponiveis_Para_Locacao_Por_Data_E_Categoria_Valida()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>()
                        {
                            new Locacao.Dominio.ModeloDB.LocacaoDB()
                            {
                                Id = 1,
                                ValorHora = 12.5,
                                DataInicioLocacao = new DateTime(),
                                DataFimLocacao = new DateTime(),
                                ClienteId = 1,
                                VeiculoId = 1,
                            }
                        };
            var veiculos = new List<Veiculo>()
                        {
                            new Veiculo()
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
                            }
                        };
            var veiculo = new Veiculo()
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
            mockLocacao.Setup(a => a.ListarLocacoesPorData(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(locacoes);
            mockVeiculo.Setup(a => a.ListarVeiculosPorCategoria(It.IsAny<int>())).Returns(veiculos);
            mockVeiculo.Setup(a => a.ObterVeiculoPorId(It.IsAny<int>())).Returns(veiculo);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarVeiculosDisponiveisParaLocacaoPorDataECategoria(1, new DateTime(), new DateTime());
            Assert.Single(locacoes);
            Assert.Single(veiculos);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Listar_Veiculos_Disponiveis_Para_Locacao_Por_Data_E_Categoria_Vazia()
        {
            var locacoes = new List<Locacao.Dominio.ModeloDB.LocacaoDB>();
            var veiculos = new List<Veiculo>();
            mockLocacao.Setup(a => a.ListarLocacoesPorData(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(locacoes);
            mockVeiculo.Setup(a => a.ListarVeiculosPorCategoria(It.IsAny<int>())).Returns(veiculos);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ListarVeiculosDisponiveisParaLocacaoPorDataECategoria(1, new DateTime(), new DateTime());
            Assert.Empty(locacoes);
            Assert.Empty(veiculos);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Obter_Locacao_Por_Id_Valido()
        {
            var locacao = new Locacao.Dominio.ModeloDB.LocacaoDB()
            {
                Id = 1,
                ValorHora = 12.5,
                DataInicioLocacao = new DateTime(),
                DataFimLocacao = new DateTime(),
                ClienteId = 1,
                VeiculoId = 1,
            };
            mockLocacao.Setup(a => a.ObterLocacaoPorId(It.IsAny<int>())).Returns(locacao);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterLocacaoPorId(1);
            Assert.NotNull(locacao);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Obter_Locacao_Por_Id_Invalido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterLocacaoPorId(0);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Obter_Valor_Total_Diarias_Valido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterValorTotalDiarias(1, 100);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Obter_Valor_Total_Diarias_Invalido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterValorTotalDiarias(1, 0);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Obter_Veiculo_Por_Id_Valido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterVeiculoPorId(1);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Once);
        }

        [Fact]
        public void Deve_Obter_Veiculo_Por_Id_Invalido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterVeiculoPorId(0);
            mockVeiculo.Verify(m => m.ObterVeiculoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Obter_Valor_Total_Locacao_Valido()
        {
            var valor = new Valor(2800, 530);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterValorTotalLocacao(1, true, true, false, false);
            mockLocacao.Verify(m => m.ObterLocacaoPorId(1), Times.Once);
            Assert.Equal(2800, valor.ValorTotalDiaria);
            Assert.Equal(530, valor.ValorTotalVistoria);
            Assert.Equal(3330, valor.ValorTotalLocacao);
        }

        [Fact]
        public void Deve_Obter_Valor_Total_Locacao_Invalido()
        {
            var valor = new Valor(2800, 530);
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterValorTotalLocacao(0, true, true, false, false);
            mockLocacao.Verify(m => m.ObterLocacaoPorId(1), Times.Never);
        }

        [Fact]
        public void Deve_Obter_Modelo_Contrato_Valido()
        {
            var aplicacao = new LocacaoAplicacao(mockLocacao.Object, mockVeiculo.Object);
            aplicacao.ObterModeloContrato();
            Assert.NotNull(File.ReadAllBytes("Templates/contratoLocacao.pdf"));
        }

    }
}
