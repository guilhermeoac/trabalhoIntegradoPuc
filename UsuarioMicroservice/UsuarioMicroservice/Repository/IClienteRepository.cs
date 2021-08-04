using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioMicroservice.Model;

namespace UsuarioMicroservice.Repository
{
    public interface IClienteRepository
    {
        void InserirCliente(Cliente cliente);
        void DeletarCliente(int clienteId);
        void AtualizarCliente(Cliente cliente);
        Cliente ObterClientePorId(int clienteId);
        Cliente ObterClientePorCpfESenha(string cpf, string senha);
        List<Cliente> ListarClientes();
    }
}
