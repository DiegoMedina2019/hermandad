using System;
using System.Collections.Generic;
using System.Text;

namespace Hermandad.Models
{
    public class Familiar
    {
        public int idafiliadodetalle { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public string nif { get; set; }
        public string parentesco { get; set; }
        public DateTime fechanacimiento { get; set; }

        //public string FechaN
        //{
        //    get
        //    {
        //        var f = fechanacimiento.ToString();
        //        return String.Format("{0:dd/MM/yyyy}", fechanacimiento);
        //    }
        //    set { fechanacimiento = DateTime.Parse(value); }
        //}
    }

}
