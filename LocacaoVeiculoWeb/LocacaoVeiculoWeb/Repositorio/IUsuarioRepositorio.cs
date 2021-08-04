using LocacaoVeiculoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocacaoVeiculoWeb.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Cliente BuscarUsuario(BuscarUsuario buscarUsuario);
        Cliente InserirUsuario(Cliente cliente);
    }
}
