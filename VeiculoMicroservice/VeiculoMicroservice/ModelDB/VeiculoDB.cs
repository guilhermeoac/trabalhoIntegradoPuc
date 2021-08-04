using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.Model;

namespace VeiculoMicroservice.ModelDB
{
    public class VeiculoDB
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public double ValorHora { get; set; }
        public TipoCombustivel Combustivel { get; set; }
        public int LimitePortaMalas { get; set; }
        public TipoCategoria Categoria { get; set; }
        public int MarcaId { get; set; }
        public int ModeloId { get; set; }
    }
}
