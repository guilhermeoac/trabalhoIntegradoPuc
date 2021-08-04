using Locacao.Dominio.ModeloDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Dominio.Entidades
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
        
        public Locacao(LocacaoDB locacaoDB, Veiculo veiculo)
        {
            Id = locacaoDB.Id;
            ValorHora = locacaoDB.ValorHora;
            TotalHora = (locacaoDB.DataFimLocacao - locacaoDB.DataInicioLocacao).TotalHours;
            DataInicioLocacao = locacaoDB.DataInicioLocacao;
            DataFimLocacao = locacaoDB.DataFimLocacao;
            ClienteId = locacaoDB.ClienteId;
            ValorTotal = (locacaoDB.ValorHora * TotalHora);
            Veiculo = veiculo;
        }
    }
}
