using Locacao.Dominio.Entidades;
using Locacao.Dominio.ModeloDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Aplicacao.Interfaces
{
    public interface ILocacaoAplicacao
    {
        List<Veiculo> ListarVeiculos();
        Veiculo ObterVeiculoPorId(int veiculoId);
        void InserirLocacao(LocacaoDB locacao);
        Locacao.Dominio.Entidades.Locacao ObterLocacaoPorId(int locacaoId);
        List<Locacao.Dominio.Entidades.Locacao> ListarLocacoes();
        List<Locacao.Dominio.Entidades.Locacao> ListarLocacoesPorDataECliente(DateTime dataLocacaoInicio, DateTime dataLocacaoFim, int clienteId);
        List<Veiculo> ListarVeiculosDisponiveisParaLocacaoPorDataECategoria(int categoria, DateTime dataInicio, DateTime dataFim);
        double ObterValorTotalDiarias(int veiculoId, double totalHoras);
        Valor ObterValorTotalLocacao(int locacaoId, bool carroLimpo, bool tanqueCheio, bool amassado, bool arranhao);
        byte[] ObterModeloContrato();
    }
}
