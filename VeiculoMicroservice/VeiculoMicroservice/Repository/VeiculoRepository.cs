using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.DBContexts;
using VeiculoMicroservice.ModelDB;

namespace VeiculoMicroservice.Repository
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private readonly VeiculoContext _dbContext;

        public VeiculoRepository(VeiculoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AtualizarVeiculo(VeiculoDB veiculo)
        {
            _dbContext.Entry(veiculo).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletarVeiculo(int veiculoId)
        {
            _dbContext.Veiculos.Remove(ObterVeiculoPorId(veiculoId));
            _dbContext.SaveChanges();
        }

        public void InserirVeiculo(VeiculoDB veiculo)
        {
            _dbContext.Add(veiculo);
            _dbContext.SaveChanges();
        }

        public List<VeiculoDB> ListarVeiculos()
        {
            return _dbContext.Veiculos.ToList();
        }

        public List<VeiculoDB> ListarVeiculosPorCategoria(int categoria)
        {
            return _dbContext.Veiculos.Where(v => (int)v.Categoria == categoria).ToList();
        }

        public VeiculoDB ObterVeiculoPorId(int veiculoId)
        {
            return _dbContext.Veiculos.Find(veiculoId);
        }
    }
}
