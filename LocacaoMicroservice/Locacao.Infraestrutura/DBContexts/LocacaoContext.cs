using Locacao.Dominio.ModeloDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Infraestrutura.DBContexts
{
    public class LocacaoContext : DbContext
    {
        public LocacaoContext(DbContextOptions<LocacaoContext> options) : base(options)
        {
        }

        public DbSet<LocacaoDB> Locacoes { get; set; }
    }
}
