using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Models
{
    public class InserirLocacao
    {
        public int Id { get; set; }
        public double ValorHora { get; set; }
        public DateTime DataInicioLocacao { get; set; }
        public DateTime DataFimLocacao { get; set; }
        public int ClienteId { get; set; }
        public int VeiculoId { get; set; }
    }
}
