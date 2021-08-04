using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.ModelDB;

namespace VeiculoMicroservice.Repository
{
    public interface IVeiculoRepository
    {
        void InserirVeiculo(VeiculoDB veiculo);
        void DeletarVeiculo(int veiculoId);
        void AtualizarVeiculo(VeiculoDB veiculo);
        VeiculoDB ObterVeiculoPorId(int veiculoId);
        List<VeiculoDB> ListarVeiculos();
        List<VeiculoDB> ListarVeiculosPorCategoria(int categoria);
    }
}
