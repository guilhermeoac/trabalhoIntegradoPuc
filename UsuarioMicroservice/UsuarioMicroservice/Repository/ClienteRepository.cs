using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioMicroservice.DBContexts;
using UsuarioMicroservice.Model;

namespace UsuarioMicroservice.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly UsuarioContext _dbContext;

        public ClienteRepository(UsuarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AtualizarCliente(Cliente cliente)
        {
            _dbContext.Entry(cliente).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletarCliente(int clienteId)
        {
            _dbContext.Clientes.Remove(ObterClientePorId(clienteId));
            _dbContext.SaveChanges();
        }

        public void InserirCliente(Cliente cliente)
        {
            _dbContext.Add(cliente);
            _dbContext.SaveChanges();
        }

        public List<Cliente> ListarClientes()
        {
            return _dbContext.Clientes.ToList();
        }

        public Cliente ObterClientePorCpfESenha(string cpf, string senha)
        {
            var cliente = _dbContext.Clientes.Where(c => c.Cpf == cpf && c.Senha == senha).FirstOrDefault();
            if (cliente != null) return cliente;
            return new Cliente();
        }

        public Cliente ObterClientePorId(int clienteId)
        {
            var cliente = _dbContext.Clientes.Find(clienteId);
            if (cliente != null) return cliente;
            return new Cliente();
        }
    }
}
