using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioMicroservice.DBContexts;
using UsuarioMicroservice.Model;

namespace UsuarioMicroservice.Repository
{
    public class OperadorRepository : IOperadorRepository
    {
        private readonly UsuarioContext _dbContext;

        public OperadorRepository(UsuarioContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AtualizarOperador(Operador operador)
        {
            _dbContext.Entry(operador).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeletarOperador(int matricula)
        {
            _dbContext.Operadores.Remove(ObterOperadorPorMatricula(matricula));
            _dbContext.SaveChanges();
        }

        public void InserirOperador(Operador operador)
        {
            _dbContext.Add(operador);
            _dbContext.SaveChanges();
        }

        public List<Operador> ListarOperadores()
        {
            return _dbContext.Operadores.ToList();
        }

        public Operador ObterOperadorPorMatricula(int matricula)
        {
            var operador = _dbContext.Operadores.Find(matricula);
            if (operador != null) return operador;
            return new Operador();
        }
    }
}
