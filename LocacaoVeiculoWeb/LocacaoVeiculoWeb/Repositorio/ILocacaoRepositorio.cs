using LocacaoVeiculoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Repositorio
{
    public interface ILocacaoRepositorio
    {
        List<Veiculo> BuscarVeiculosDisponiveis(BuscarVeiculo buscarVeiculo);
        List<Locacao> BuscarLocacoes(BuscarLocacao buscarLocacao);
        Locacao InserirLocacao(InserirLocacao inserirLocacao);
    }
}
