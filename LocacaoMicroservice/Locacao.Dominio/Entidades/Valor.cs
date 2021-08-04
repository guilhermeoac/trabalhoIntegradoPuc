using System;
using System.Collections.Generic;
using System.Text;

namespace Locacao.Dominio.Entidades
{
    public class Valor
    {
        public double ValorTotalDiaria { get; set; }
        public double ValorTotalVistoria { get; set; }
        public double ValorTotalLocacao { get => (ValorTotalDiaria+ ValorTotalVistoria); }

        public Valor(double valorTotalDiaria, double valorTotalVistoria)
        {
            ValorTotalDiaria = valorTotalDiaria;
            ValorTotalVistoria = valorTotalVistoria;
        }
    }
}
