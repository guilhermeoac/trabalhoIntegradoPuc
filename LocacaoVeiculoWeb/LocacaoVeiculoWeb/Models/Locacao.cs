using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public double ValorHora { get; set; }
        public double TotalHora { get; set; }
        public DateTime DataInicioLocacao { get; set; }
        public DateTime DataFimLocacao { get; set; }
        public int ClienteId { get; set; }
        public double ValorTotal { get; set; }
        public Veiculo Veiculo { get; set; }
    }
}
