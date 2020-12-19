using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hermandad.Models
{
    public class Mensaje
    {
        public int idmensajes { get; set; }
        public string emisor { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha;
        public int visto { get; set; }
        public int afiliados_idafiliados { get; set; }

        public string Fecha
        { //pude formatear la fecha rara q viene desde node.Js ejemplo: 2020-12-05T21:24:14.000Z 
          //pero no estoy seguro si es la hora correcta
            get { return fecha.ToString(); }
            set { fecha = DateTime.Parse(value); }
        }

        public async Task UpateMensaje()
        {
            this.visto = 1;
            await MensajesServices.UpdateMensajes(this.idmensajes, this);
        }
    }
}
