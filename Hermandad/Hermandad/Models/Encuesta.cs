using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hermandad.Models
{
    public class Encuesta
    {
        public int idencuestacabecera { get; set; }
        public string tipo { get; set; }
        public string nombre { get; set; }
        public DateTime DesdeFecha;
        public DateTime HastaFecha;
        public int idencuestas_afiliados { get; set; }
        public List<Pregunta_> preguntas { get; set; }
        public string FicheroPdf { get; set; }

        public string Desde
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", DesdeFecha);
            }
            set { DesdeFecha = DateTime.Parse(value); }
        }
        public string Hasta
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", HastaFecha);
            }
            set { HastaFecha = DateTime.Parse(value); }
        }


        public async Task<List<Pregunta_>> getPreguntas()
        {
            preguntas = await EncuestasServices.GetPreguntas(this.idencuestacabecera);
            return preguntas;
        }
    }
}
