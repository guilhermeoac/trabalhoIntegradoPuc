using Locacao.Aplicacao.Interfaces;
using Locacao.Dominio.Entidades;
using Locacao.Dominio.ModeloDB;
using Locacao.Dominio.Repositorios;
using System;
using System.Collections.Generic;
using System.IO;

namespace Locacao.Aplicacao
{
    public class LocacaoAplicacao : ILocacaoAplicacao
    {
        private readonly ILocacaoRepositorio _locacaoRepositorio;
        private readonly IVeiculoRepositorio _veiculoRepositorio;

        public LocacaoAplicacao(ILocacaoRepositorio locacaoRepositorio, IVeiculoRepositorio veiculoRepositorio)
        {
            _locacaoRepositorio = locacaoRepositorio;
            _veiculoRepositorio = veiculoRepositorio;
        }

        public void InserirLocacao(LocacaoDB locacao)
        {
            if(locacao!=null) _locacaoRepositorio.InserirLocacao(locacao);
        }

        public List<Dominio.Entidades.Locacao> ListarLocacoes()
        {
            var locacoes = _locacaoRepositorio.ListarLocacoes();
            return construirLocacoesParaRetorno(locacoes);
        }

        public List<Dominio.Entidades.Locacao> ListarLocacoesPorDataECliente(DateTime dataLocacaoInicio, DateTime dataLocacaoFim, int clienteId)
        {
            if (clienteId > 0)
            {
                var locacoes = _locacaoRepositorio.ListarLocacoesPorDataECliente(dataLocacaoInicio, dataLocacaoFim, clienteId);
                return construirLocacoesParaRetorno(locacoes);
            }
            return new List<Dominio.Entidades.Locacao>();
        }

        public List<Veiculo> ListarVeiculos()
        {
            return _veiculoRepositorio.ListarVeiculos();
        }

        public List<Veiculo> ListarVeiculosDisponiveisParaLocacaoPorDataECategoria(int categoria, DateTime dataInicio, DateTime dataFim)
        {
            if (categoria > 0)
            {
                var locacoes = _locacaoRepositorio.ListarLocacoesPorData(dataInicio, dataFim);
                var veiculosRetorno = _veiculoRepositorio.ListarVeiculosPorCategoria(categoria);
                if (locacoes.Count > 0)
                {
                    var veiculosId = new List<int>();
                    foreach (LocacaoDB locacaoDB in locacoes)
                    {
                        var veiculo = _veiculoRepositorio.ObterVeiculoPorId(locacaoDB.VeiculoId);
                        if (veiculo.CategoriaId == categoria) veiculosId.Add(veiculo.Id);
                    }
                    veiculosRetorno = construirVeiculosParaRetorno(veiculosRetorno, veiculosId);
                }
                return veiculosRetorno;
            }
            return new List<Veiculo>();
        }

        public Dominio.Entidades.Locacao ObterLocacaoPorId(int locacaoId)
        {
            var locacao = _locacaoRepositorio.ObterLocacaoPorId(locacaoId);
            if (locacao != null)
            {
                var veiculo = _veiculoRepositorio.ObterVeiculoPorId(locacao.VeiculoId);
                return new Dominio.Entidades.Locacao(locacao, veiculo);
            }
            return null;
        }

        public double ObterValorTotalDiarias(int veiculoId, double totalHoras)
        {
            if(totalHoras > 0)
            {
                var veiculo = _veiculoRepositorio.ObterVeiculoPorId(veiculoId);
                if (veiculo != null)
                {
                    return (veiculo.ValorHora * totalHoras);
                }
            }
            return 0;
        }

        public Veiculo ObterVeiculoPorId(int veiculoId)
        {
            if (veiculoId > 0) return _veiculoRepositorio.ObterVeiculoPorId(veiculoId);
            else return new Veiculo();
        }

        public Valor ObterValorTotalLocacao(int locacaoId, bool carroLimpo, bool tanqueCheio, bool amassado, bool arranhao)
        {
            var locacao = ObterLocacaoPorId(locacaoId);
            if (locacao != null)
            {
                return new Valor(locacao.ValorTotal, CalcularValorTaxa(locacao.ValorTotal, carroLimpo, tanqueCheio, amassado, arranhao));
            }
            return null;
        }

        public byte[] ObterModeloContrato()
        {
            return File.ReadAllBytes("Templates/contratoLocacao.pdf");
        }

        private double CalcularValorTaxa(double valorDiarias, bool carroLimpo, bool tanqueCheio, bool amassado, bool arranhao)
        {
            var cobrar = 0;
            if (!carroLimpo) cobrar++;
            if (!tanqueCheio) cobrar++;
            if (amassado) cobrar++;
            if (arranhao) cobrar++;
            return (cobrar * 0.3) * valorDiarias;
        }

        private List<Dominio.Entidades.Locacao> construirLocacoesParaRetorno(List<LocacaoDB> locacoes)
        {
            var locacoesRetorno = new List<Dominio.Entidades.Locacao>();
            if (locacoes.Count > 0)
            {
                foreach (LocacaoDB locacaoDB in locacoes)
                {
                    var veiculo = _veiculoRepositorio.ObterVeiculoPorId(locacaoDB.VeiculoId);
                    locacoesRetorno.Add(new Dominio.Entidades.Locacao(locacaoDB, veiculo));
                }
            }
            return locacoesRetorno;
        }

        private List<Veiculo> construirVeiculosParaRetorno(List<Veiculo> veiculos, List<int> veiculosId)
        {
            var veiculosRetorno = new List<Veiculo>();
            foreach (Veiculo vc in veiculos)
            {
                bool add = true;
                foreach (int v in veiculosId)
                {
                    if (vc.Id == v)
                    {
                        add = false;
                        break;
                    }
                }
                if (add) veiculosRetorno.Add(vc);
            }
            return veiculosRetorno;
        }
    }
}
