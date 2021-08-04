using Locacao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Dominio.Repositorios
{
    public interface IVeiculoRepositorio
    {
        List<Veiculo> ListarVeiculos();
        Veiculo ObterVeiculoPorId(int veiculoId);
        List<Veiculo> ListarVeiculosPorCategoria(int categoria);
    }
}
