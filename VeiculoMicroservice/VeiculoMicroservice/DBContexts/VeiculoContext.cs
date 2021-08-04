using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.Model;
using VeiculoMicroservice.ModelDB;

namespace VeiculoMicroservice.DBContexts
{
    public class VeiculoContext : DbContext
    {
        public VeiculoContext(DbContextOptions<VeiculoContext> options) : base(options)
        {
        }

        public DbSet<Marca> Marcas { get; set; }
        public DbSet<ModeloDB> Modelos { get; set; }
        public DbSet<VeiculoDB> Veiculos { get; set; }
    }
}
