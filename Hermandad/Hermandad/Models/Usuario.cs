using System;
using System.Collections.Generic;
using System.Text;

namespace Hermandad.Models
{
    class Usuario
    {
        public int idafiliados { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public object token { get; set; }
    }
}
