using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioMicroservice.Model
{
    public class Operador : Usuario
    {
        [Key]
        public int Matricula { get; set; }
    }
}
