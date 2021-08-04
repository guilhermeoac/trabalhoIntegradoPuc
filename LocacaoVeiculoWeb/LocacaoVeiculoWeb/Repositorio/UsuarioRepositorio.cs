using LocacaoVeiculoWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        static HttpClient _httpClient;

        public UsuarioRepositorio()
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("http://localhost:57291/api/");
            }
        }

        public Cliente BuscarUsuario(BuscarUsuario buscarUsuario)
        {
            var retorno = _httpClient.GetAsync($"cliente/{buscarUsuario.Cpf}/{buscarUsuario.Senha}").Result;
            var resultado = retorno.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Cliente>(resultado);
        }

        public Cliente InserirUsuario(Cliente cliente)
        {
            var body = SerializeToString(cliente);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var retorno = _httpClient.PostAsync($"cliente", content).Result;
            var resultado = retorno.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Cliente>(resultado);
        }

        protected string SerializeToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
