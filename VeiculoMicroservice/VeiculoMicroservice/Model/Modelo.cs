using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculoMicroservice.Model
{
    public class Modelo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Marca Marca { get; set; }

        public Modelo(int id, string nome, Marca marca)
        {
            Id = id;
            Nome = nome;
            Marca = marca;
        }
    }
}
