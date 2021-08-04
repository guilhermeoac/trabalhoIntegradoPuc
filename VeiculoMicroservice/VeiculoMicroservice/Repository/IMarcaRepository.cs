using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.Model;

namespace VeiculoMicroservice.Repository
{
    public interface IMarcaRepository
    {
        void InserirMarca(Marca marca);
        void DeletarMarca(int marcaId);
        void AtualizarMarca(Marca marca);
        Marca ObterMarcaPorId(int marcaId);
        List<Marca> ListarMarcas();
    }
}
