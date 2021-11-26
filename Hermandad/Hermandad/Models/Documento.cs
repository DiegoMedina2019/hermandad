using System;
using System.Collections.Generic;
using System.Text;

namespace Hermandad.Models
{
    public class Documento //podra ser foto reyes magos o doc Sepa
    {
        public string idDocumentos { get; set; }
        public string nombre { get; set; }
        public string url { get; set; }

        public int Afiliados_idAfiliados { get; set; }
        public int TipoDocumento_idTipoDocumento { get; set; }

        //private DateTime fecha;


        //public string Fecha
        //{
        //    get
        //    {
        //        return fecha.ToString();
        //    }
        //    set
        //    {
        //        fecha = DateTime.Parse(value);
        //    }
        //}
    }
}
