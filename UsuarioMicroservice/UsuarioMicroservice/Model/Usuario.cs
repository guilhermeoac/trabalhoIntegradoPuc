using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioMicroservice.Model
{
    public abstract class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
