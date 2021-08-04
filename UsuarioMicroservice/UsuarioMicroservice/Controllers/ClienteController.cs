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
    /// Serviço de Cliente
    /// </summary>
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        /// <summary>
        /// Buscar todos os clientes.
        /// </summary>
        /// <returns>Lista de objeto cliente</returns>
        // GET: Buscar todos os clientes
        [HttpGet]
        public IActionResult Get()
        {
            var clientes = _clienteRepository.ListarClientes();
            return new OkObjectResult(clientes);
        }

        /// <summary>
        /// Buscar um cliente pelo id.
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Objeto cliente</returns>
        // GET Buscar o cliente por id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var cliente = _clienteRepository.ObterClientePorId(id);
            return new OkObjectResult(cliente);
        }

        /// <summary>
        /// Buscar um cliente pelo CPF e senha de login.
        /// </summary>
        /// <param name="cpf">CPF do cliente</param>
        /// <param name="senha">Senha do cliente</param>
        /// <returns>Objeto cliente</returns>
        // GET Buscar o cliente por id
        [HttpGet("{cpf}/{senha}")]
        public IActionResult Get(string cpf, string senha)
        {
            var cliente = _clienteRepository.ObterClientePorCpfESenha(cpf, senha);
            return new OkObjectResult(cliente);
        }

        /// <summary>
        /// Inserir um cliente.
        /// </summary>
        /// <returns>Objeto cliente</returns>
        // POST Inserir um cliente
        [HttpPost]
        public IActionResult Post([FromBody] Cliente cliente)
        {
            using (var scope = new TransactionScope())
            {
                _clienteRepository.InserirCliente(cliente);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = cliente.Id }, cliente);
            }
        }

        /// <summary>
        /// Atualizar os dados de um cliente.
        /// </summary>
        /// <param name="cliente">Objeto cliente para atualizar</param>
        /// <returns>Objeto cliente</returns>
        // PUT Atualizar um cliente
        [HttpPut("{cliente}")]
        public IActionResult Put([FromBody] Cliente cliente)
        {
            if (cliente != null)
            {
                using (var scope = new TransactionScope())
                {
                    _clienteRepository.AtualizarCliente(cliente);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        /// <summary>
        /// Excluir um cliente pelo id.
        /// </summary>
        /// <param name="id">Id do cliente</param>
        /// <returns>Mensagem de sucesso</returns>
        // DELETE Deletar um cliente
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _clienteRepository.DeletarCliente(id);
            return new OkResult();
        }
    }
}
