using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Models
{
    public class BuscarVeiculo
    {
        public DateTime DataReservaInicio { get; set; }
        public DateTime DataReservaFim { get; set; }
        public int Categoria { get; set; }
    }
}
