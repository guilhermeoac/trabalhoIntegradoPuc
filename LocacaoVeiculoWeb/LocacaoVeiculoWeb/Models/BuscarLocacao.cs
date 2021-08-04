using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Models
{
    public class BuscarLocacao
    {
        public DateTime DataLocacaoInicio { get; set; }
        public DateTime DataLocacaoFim { get; set; }
        public int ClienteId { get; set; }
    }
}
