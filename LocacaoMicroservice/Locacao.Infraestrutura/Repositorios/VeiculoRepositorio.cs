using Locacao.Dominio.Repositorios;
using Locacao.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;

namespace Locacao.Infraestrutura.Repositorios
{
    public class VeiculoRepositorio : IVeiculoRepositorio
    {
        static HttpClient _httpClient;

        public VeiculoRepositorio()
        {
            if(_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("http://localhost:55523/api/");
            }
        }

        public List<Veiculo> ListarVeiculos()
        {
            var retorno = _httpClient.GetAsync($"veiculo").Result;
            var resultado = retorno.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<Veiculo>>(resultado);
        }

        public List<Veiculo> ListarVeiculosPorCategoria(int categoria)
        {
            var retorno = _httpClient.GetAsync($"veiculo/{categoria}/categoria").Result;
            var resultado = retorno.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<List<Veiculo>>(resultado);
        }

        public Veiculo ObterVeiculoPorId(int veiculoId)
        {
            var retorno = _httpClient.GetAsync($"veiculo/{veiculoId}").Result;
            var resultado = retorno.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Veiculo>(resultado);
        }
    }
}
