using System;
using System.Collections.Generic;
using System.Text;

namespace Hermandad.Models
{
    public class Recibo
    {
        public int idRecibos { get; set; }
        public string nombre { get; set; }
        public string url { get; set; }

        public int Afiliados_idAfiliados { get; set; }
        //private DateTime fecha;


        //public string Fecha { 
        //    get {
        //        return fecha.ToString(); 
        //    } 
        //    set { 
        //        fecha = DateTime.Parse(value);  
        //    } 
        //}
    }
}
