using Locacao.Dominio.Entidades;
using Locacao.Dominio.ModeloDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Dominio.Repositorios
{
    public interface ILocacaoRepositorio
    {
        void InserirLocacao(LocacaoDB locacao);
        LocacaoDB ObterLocacaoPorId(int locacaoId);
        List<LocacaoDB> ListarLocacoes();
        List<LocacaoDB> ListarLocacoesPorDataECliente(DateTime dataLocacaoInicio, DateTime dataLocacaoFim, int clienteId);
        List<LocacaoDB> ListarLocacoesPorData(DateTime dataLocacaoInicio, DateTime dataLocacaoFim);
    }
}
