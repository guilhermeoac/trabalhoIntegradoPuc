using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.DBContexts;
using VeiculoMicroservice.Model;

namespace VeiculoMicroservice.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly VeiculoContext _dbContext;

        public MarcaRepository(VeiculoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AtualizarMarca(Marca marca)
        {
            _dbContext.Entry(marca).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletarMarca(int marcaId)
        {
            _dbContext.Marcas.Remove(ObterMarcaPorId(marcaId));
            _dbContext.SaveChanges();
        }

        public void InserirMarca(Marca marca)
        {
            _dbContext.Add(marca);
            _dbContext.SaveChanges();
        }

        public List<Marca> ListarMarcas()
        {
            return _dbContext.Marcas.ToList();
        }

        public Marca ObterMarcaPorId(int marcaId)
        {
            return _dbContext.Marcas.Find(marcaId);
        }
    }
}
