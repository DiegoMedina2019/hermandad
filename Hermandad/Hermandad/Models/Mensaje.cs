using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace Hermandad.Models
{
    public class Mensaje_obj
    {
        //public int idMensaje { get; set; }
        //public string emisor { get; set; }
        //public string mensaje { get; set; }
        //public DateTime fecha { get; set; }
        //public DateTime? visto { get; set; } 
        //public int Afiliados_idAfiliados { get; set; }

        public string nombre { get; set; }//es el nombre de la empresas
        public int idmensaje { get; set; }
        public int idafiliado { get; set; }
        //public int idoperador { get; set; }
        public string Mensaje { get; set; }
        //public string Fichero { get; set; }
        public DateTime Fecha;
        public DateTime? FechaVisto { get; set; }
        public string idEmpresa { get; set; }
        //public int borrado { get; set; }
        //public DateTime idCreacion;
        public DateTime? idModificacion;
        //public DateTime idBorrado;
        //public int insertado { get; set; }

        public string Fecha_
        { //pude formatear la fecha rara q viene desde node.Js ejemplo: 2020-12-05T21:24:14.000Z 
          //pero no estoy seguro si es la hora correcta
            get {
                var f = Fecha.ToString();
                return String.Format("{0:dd/MM/yyyy}", Fecha);
            }
            set { Fecha = DateTime.Parse(value); }
        }
        public string Visto
        { //pude formatear la fecha rara q viene desde node.Js ejemplo: 2020-12-05T21:24:14.000Z 
          //pero no estoy seguro si es la hora correcta
            get
            {
                var f = FechaVisto.ToString();
                return String.Format("{0:dd/MM/yyyy}", FechaVisto);
            }
            set { FechaVisto = DateTime.Parse(value); }
        }
        
        public string GetFechaMysql(DateTime f)
        {
            IFormatProvider culture =
                    new CultureInfo("es-ES", true);
            return f.GetDateTimeFormats('u', culture)[0];
        }
        public async Task UpateMensaje()
        {
            //this.Visto = DateTime.Now.ToString();
            FechaVisto = DateTime.Now;
            idModificacion = DateTime.Now;
            await MensajesServices.UpdateMensajes(this.idmensaje, this);
        }
    }

}
