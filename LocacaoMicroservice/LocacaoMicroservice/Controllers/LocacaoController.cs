using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Locacao.Aplicacao.Interfaces;
using Locacao.Dominio.Entidades;
using Locacao.Dominio.ModeloDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Locacao.Api.Controllers
{
    /// <summary>
    /// Serviço de Locação
    /// </summary>
    [Route("api/locacao")]
    [ApiController]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoAplicacao _locacaoAplicacao;

        public LocacaoController(ILocacaoAplicacao locacaoAplicacao)
        {
            _locacaoAplicacao = locacaoAplicacao;
        }

        /// <summary>
        /// Buscar todas as locações de veículos.
        /// </summary>
        /// <returns>Lista de objeto locacao</returns>
        // GET: Buscar todas as locacoes
        [HttpGet]
        public IActionResult Get()
        {
            var locacoes = _locacaoAplicacao.ListarLocacoes();
            return new OkObjectResult(locacoes);
        }

        /// <summary>
        /// Buscar uma locação de veículo pelo id.
        /// </summary>
        /// <param name="id">Id da locacao</param>
        /// <returns>Objeto locacao</returns>
        // GET Buscar a locacao pelo id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var locacao = _locacaoAplicacao.ObterLocacaoPorId(id);
            if(locacao != null) return new OkObjectResult(locacao);
            return new NoContentResult();
        }

        /// <summary>
        /// Listar veículos disponíveis para locacação em um período e por categoria.
        /// </summary>
        /// <param name="dataInicio">Data inicio da locacao</param>
        /// <param name="dataFim">Data fim da locacao</param>
        /// <param name="categoria">Categoria dos veiculos</param>
        /// <returns>Lista de objeto veiculo</returns>
        // GET Buscar veículos disponíveis para locacação em um período e por categoria
        [HttpGet("{dataInicio}/{dataFim}/{categoria}/categoria")]
        public IActionResult Get(DateTime dataInicio, DateTime dataFim, int categoria)
        {
            var veiculos = _locacaoAplicacao.ListarVeiculosDisponiveisParaLocacaoPorDataECategoria(categoria, dataInicio, dataFim);
            return new OkObjectResult(veiculos);
        }

        /// <summary>
        /// Listar locações pelo cliente e data de referencia.
        /// </summary>
        /// <param name="clienteId">Id do cliente</param>
        /// <param name="dataLocacaoInicio">Data inicio da locacao</param>
        /// <param name="dataLocacaoFim">Data fim da locacao</param>
        /// <returns>Lista de objeto locacao</returns>
        // GET Buscar locações pelo cliente e data de referencia
        [HttpGet("{dataLocacaoInicio}/{dataLocacaoFim}/{clienteId}/cliente")]
        public IActionResult Get(int clienteId, DateTime dataLocacaoInicio, DateTime dataLocacaoFim)
        {
            var locacoes = _locacaoAplicacao.ListarLocacoesPorDataECliente(dataLocacaoInicio, dataLocacaoFim, clienteId);
            return new OkObjectResult(locacoes);
        }

        /// <summary>
        /// Buscar valor total de diarias por hora e veículo.
        /// </summary>
        /// <param name="totalHoras">Total de horas</param>
        /// <param name="veiculoId">Id do veiculo</param>
        /// <returns>double valor da diaria</returns>
        // GET Buscar valor total de diarias pelo veiculo, valor hora e total de horas
        [HttpGet("{totalHoras}/{veiculoId}/veiculo")]
        public IActionResult Get(int veiculoId, double totalHoras)
        {
            var valor = _locacaoAplicacao.ObterValorTotalDiarias(veiculoId, totalHoras);
            return new OkObjectResult(valor);
        }

        /// <summary>
        /// Buscar valor total da locacao calculando valores do checklist de devolução.
        /// </summary>
        /// <param name="locacaoId">Id da locacao</param>
        /// <param name="carroLimpo">Veiculo limpo</param>
        /// <param name="tanqueCheio">Veiculo com tanque cheio?</param>
        /// <param name="amassado">Veiculo amassado?</param>
        /// <param name="arranhao">Veiculo arranhado?</param>
        /// <returns>Objeto valor</returns>
        // GET Buscar valor total da locacao calculando valores do checklist de devolução
        [HttpGet("{locacaoId}/{carroLimpo}/{tanqueCheio}/{amassado}/{arranhao}")]
        public IActionResult Get(int locacaoId, bool carroLimpo, bool tanqueCheio, bool amassado, bool arranhao)
        {
            var valor = _locacaoAplicacao.ObterValorTotalLocacao(locacaoId, carroLimpo, tanqueCheio, amassado, arranhao);
            if(valor != null) return new OkObjectResult(valor);
            return new NoContentResult();
        }

        /// <summary>
        /// Buscar o pdf do modelo de contrato de locação.
        /// </summary>
        /// <returns>Array de byte</returns>
        // GET Buscar o pdf do modelo de contrato de locação
        [HttpGet("pdf")]
        public IActionResult GetPdf()
        {
            var pdf = _locacaoAplicacao.ObterModeloContrato();
            return new OkObjectResult(pdf);
        }

        /// <summary>
        /// Inserir uma locação.
        /// </summary>
        /// <returns>Objeto locacao</returns>
        // POST Inserir uma locação
        [HttpPost]
        public IActionResult Post([FromBody] LocacaoDB locacao)
        {
            if (locacao.DataInicioLocacao > locacao.DataFimLocacao)
            {
                return new BadRequestObjectResult("Data inicial não pode ser superior à data final");
            }

            using (var scope = new TransactionScope())
            {
                _locacaoAplicacao.InserirLocacao(locacao);
                scope.Complete();
            }
            if (locacao.Id > 0)
            {
                var veiculo = _locacaoAplicacao.ObterVeiculoPorId(locacao.VeiculoId);
                if (veiculo != null)
                {
                    var locacaoRetorno = new Locacao.Dominio.Entidades.Locacao(locacao, veiculo);
                    return CreatedAtAction(nameof(Get), new { id = locacao.Id }, locacaoRetorno);
                }
            }
            return new NoContentResult();
        }
    }
}
