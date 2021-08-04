using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.Model;
using VeiculoMicroservice.ModelDB;

namespace VeiculoMicroservice.Repository
{
    public interface IModeloRepository
    {
        void InserirModelo(ModeloDB modelo);
        void DeletarModelo(int modeloId);
        void AtualizarModelo(ModeloDB modelo);
        ModeloDB ObterModeloPorId(int modeloId);
        List<ModeloDB> ListarModelos();
    }
}
