using System;
using System.Collections.Generic;
using System.Text;

namespace Hermandad.Models
{
    public class Usuario
    {
        public int idafiliado { get; set; }
        public string razonsocial { get; set; }
        public string nombre { get; set; }
        public string nif { get; set; }
        public string domicilio { get; set; }
        public string codigopostal { get; set; }
        public string poblacion { get; set; }
        public string provincia { get; set; }
        public string telefono1 { get; set; }
        public string telefono2 { get; set; }
        public string email { get; set; }
        public string Pass { get; set; }
        public int login_init { get; set; }
        public string foto { get; set; }
        public List<Familiar> familiares { get; set; }
        //public DateTime FechaNacimiento { get; set; }
        //public int Sexo { get; set; }
        //public string User { get; set; }
    }
}


