using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeiculoMicroservice.ModelDB;

namespace VeiculoMicroservice.Model
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public int Ano { get; set; }
        public double ValorHora { get; set; }
        public string Combustivel { get; set; }
        public int LimitePortaMalas { get; set; }
        public string CategoriaString { get; set; }
        public int CategoriaId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }

        public Veiculo(VeiculoDB veiculoDB, string marca, string modelo)
        {
            Id = veiculoDB.Id;
            Placa = veiculoDB.Placa;
            Ano = veiculoDB.Ano;
            ValorHora = veiculoDB.ValorHora;
            Combustivel = veiculoDB.Combustivel.ToString();
            LimitePortaMalas = veiculoDB.LimitePortaMalas;
            CategoriaString = veiculoDB.Categoria.ToString();
            CategoriaId = (int)veiculoDB.Categoria;
            Marca = marca;
            Modelo = modelo;
        }
    }
}
