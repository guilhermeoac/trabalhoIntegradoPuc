using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using UsuarioMicroservice.Model;
using UsuarioMicroservice.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsuarioMicroservice.Controllers
{
    /// <summary>
    /// Serviço de Operador
    /// </summary>
    [Route("api/operador")]
    [ApiController]
    public class OperadorController : ControllerBase
    {
        private readonly IOperadorRepository _operadorRepository;

        public OperadorController(IOperadorRepository operadorRepository)
        {
            _operadorRepository = operadorRepository;
        }

        /// <summary>
        /// Buscar todos os operadores.
        /// </summary>
        /// <returns>Lista de objeto operador</returns>
        // GET: Buscar todos os operadores
        [HttpGet]
        public IActionResult Get()
        {
            var operadores = _operadorRepository.ListarOperadores();
            return new OkObjectResult(operadores);
        }

        /// <summary>
        /// Buscar um operador pela matricula.
        /// </summary>
        /// <param name="matricula">Matricula do operador</param>
        /// <returns>Objeto operador</returns>
        // GET Buscar operador pela matricula
        [HttpGet("{matricula}")]
        public IActionResult Get(int matricula)
        {
            var operadore = _operadorRepository.ObterOperadorPorMatricula(matricula);
            return new OkObjectResult(operadore);
        }

        /// <summary>
        /// Inserir um operador.
        /// </summary>
        /// <returns>Objeto operador</returns>
        // POST Inserir um operador
        [HttpPost]
        public IActionResult Post([FromBody] Operador operador)
        {
            using (var scope = new TransactionScope())
            {
                _operadorRepository.InserirOperador(operador);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { matricula = operador.Matricula }, operador);
            }
        }

        /// <summary>
        /// Atualizar os dados de um operador.
        /// </summary>
        /// <param name="operador">Objeto operador para atualizar</param>
        /// <returns>Objeto operador</returns>
        // PUT Atualizar um operador
        [HttpPut("{operador}")]
        public IActionResult Put([FromBody] Operador operador)
        {
            if (operador != null)
            {
                using (var scope = new TransactionScope())
                {
                    _operadorRepository.AtualizarOperador(operador);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Excluir um operador pela matricula.
        /// </summary>
        /// <param name="matricula">Matricula do operador</param>
        /// <returns>Mensagem de sucesso</returns>
        // DELETE Deletar um operador
        [HttpDelete("{matricula}")]
        public IActionResult Delete(int matricula)
        {
            _operadorRepository.DeletarOperador(matricula);
            return new OkResult();
        }
    }
}
