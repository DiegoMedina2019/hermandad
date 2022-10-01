using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Hermandad.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xamarin.Forms;

namespace Hermandad.Services
{
    public static class MensajesServices
    {
        public static readonly HttpClient client = CrearHttpClient();

        private static HttpClient CrearHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            // client = new HttpClient(httpClientHandler);
            /*
             este parte de codigo de arriba la agregue para descartar la exepciones SSL q me daba al traer el listado
            al parecer al ser certificados ssl autofirmados los detecta como invalidos desde la maquina virtual del cel
            al cer algo "externo a mi PC la cual los detecta como legitimos, 
            debo realizar algunas configuraciones para solicionar esto, ya que esto que hice es una mala practica
            ignorar la seguridad ssl"
            */
            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
            return httpClient;

        }
        public async static Task<List<Mensaje_obj>> GetMensajes()
        {
            var id = Application.Current.Properties["idafiliados"].ToString();

            string url = App.api + "mensajes_hermndad/" + id;

            var response = await client.GetAsync(url);// apesar q ssl es con localhost desde la virtual vede ser una ip
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var format = "dd-MM-yyyy hh:mm:ss"; // your datetime format
                //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                return JsonConvert.DeserializeObject<List<Mensaje_obj>>(json);
            }
            return default;
        }
        public async static Task UpdateMensajes(int id, Mensaje_obj mjs)
        {
            string url = App.api + "mensajes/" + id.ToString();

            object o = new
            {
                mjs.FechaVisto,
                mjs.idModificacion,
                db = "hermandad"
            };
            string body = JsonConvert.SerializeObject(o);

            var response = await client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));// apesar q ssl es con localhost desde la virtual vede ser una ip
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                //var format = "dd-MM-yyyy hh:mm:ss"; // your datetime format
                //var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };
                //  JsonConvert.DeserializeObject<Mensaje>(json);
            }

        }
    }
}
