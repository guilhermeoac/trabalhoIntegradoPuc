using Locacao.Dominio.Repositorios;
using Locacao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Locacao.Infraestrutura.DBContexts;
using System.Linq;
using Locacao.Dominio.ModeloDB;

namespace Locacao.Infraestrutura.Repositorios
{
    public class LocacaoRepositorio : ILocacaoRepositorio
    {
        private readonly LocacaoContext _dbContext;

        public LocacaoRepositorio(LocacaoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InserirLocacao(LocacaoDB locacao)
        {
            _dbContext.Add(locacao);
            _dbContext.SaveChanges();
        }

        public List<LocacaoDB> ListarLocacoes()
        {
            return _dbContext.Locacoes.ToList();
        }

        public List<LocacaoDB> ListarLocacoesPorData(DateTime dataLocacaoInicio, DateTime dataLocacaoFim)
        {
            return _dbContext.Locacoes.Where(l =>
                    (l.DataInicioLocacao < dataLocacaoInicio &&
                     dataLocacaoInicio < l.DataFimLocacao)
                    ||
                    (l.DataInicioLocacao < dataLocacaoFim &&
                     dataLocacaoFim <= l.DataFimLocacao)
                    ||
                    (dataLocacaoInicio < l.DataInicioLocacao &&
                     l.DataInicioLocacao < dataLocacaoFim)
                    ||
                    (dataLocacaoInicio < l.DataFimLocacao &&
                     l.DataFimLocacao <= dataLocacaoFim)
                    ).ToList();
        }

        public List<LocacaoDB> ListarLocacoesPorDataECliente(DateTime dataLocacaoInicio, DateTime dataLocacaoFim, int clienteId)
        {
            return _dbContext.Locacoes.Where(l => l.ClienteId == clienteId &&
                (l.DataInicioLocacao < dataLocacaoInicio &&
                     dataLocacaoInicio < l.DataFimLocacao)
                    ||
                    (l.DataInicioLocacao < dataLocacaoFim &&
                     dataLocacaoFim <= l.DataFimLocacao)
                    ||
                    (dataLocacaoInicio < l.DataInicioLocacao &&
                     l.DataInicioLocacao < dataLocacaoFim)
                    ||
                    (dataLocacaoInicio < l.DataFimLocacao &&
                     l.DataFimLocacao <= dataLocacaoFim)
                                                ).ToList();
        }

        public LocacaoDB ObterLocacaoPorId(int locacaoId)
        {
            return _dbContext.Locacoes.Find(locacaoId);
        }
    }
}
