using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioMicroservice.Model;

namespace UsuarioMicroservice.Repository
{
    public interface IOperadorRepository
    {
        void InserirOperador(Operador operador);
        void DeletarOperador(int matricula);
        void AtualizarOperador(Operador operador);
        Operador ObterOperadorPorMatricula(int matricula);
        List<Operador> ListarOperadores();
    }
}
