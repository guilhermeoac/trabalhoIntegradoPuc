using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LocacaoVeiculoWeb.Models;
using LocacaoVeiculoWeb.Repositorio;

namespace LocacaoVeiculoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILocacaoRepositorio _locacaoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public HomeController(ILogger<HomeController> logger, ILocacaoRepositorio locacaoRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _logger = logger;
            _locacaoRepositorio = locacaoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BuscarCarrosDisponíveisPorData(BuscarVeiculo buscarVeiculo)
        {
            var veiculos = _locacaoRepositorio.BuscarVeiculosDisponiveis(buscarVeiculo);
            return Json(veiculos);
        }

        [HttpPost]
        public ActionResult BuscarLocacoesDoClientePorData(BuscarLocacao buscarLocacao)
        {
            var locacoes = _locacaoRepositorio.BuscarLocacoes(buscarLocacao);
            return Json(locacoes);
        }

        [HttpPost]
        public ActionResult BuscarUsuarioPorCpfESenha(BuscarUsuario buscarUsuario)
        {
            var usuario = _usuarioRepositorio.BuscarUsuario(buscarUsuario);
            return Json(usuario);
        }

        [HttpPost]
        public ActionResult InserirUsuario(Cliente cliente)
        {
            var usuario = _usuarioRepositorio.InserirUsuario(cliente);
            return Json(usuario);
        }

        [HttpPost]
        public ActionResult InserirLocacao(InserirLocacao inserirLocacao)
        {
            var locacao = _locacaoRepositorio.InserirLocacao(inserirLocacao);
            return Json(locacao);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
