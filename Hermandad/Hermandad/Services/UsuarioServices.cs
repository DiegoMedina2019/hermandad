using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hermandad.Models;
using Xamarin.Forms;

namespace Hermandad.Services
{
    public class UsuarioServices
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
        public async static Task<bool> ValidarUsuario(string correo, string pass)
        {
            string url = "https://172.18.100.97:433/api/login";
            Usuario a = new Usuario();
            a.correo = correo;
            a.password = pass;
            string body = JsonConvert.SerializeObject(a);


            var respuesta = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<Usuario> user = JsonConvert.DeserializeObject<List<Usuario>>(json);

                Application.Current.Properties["idafiliados"] = (respuesta.IsSuccessStatusCode) ? user[0].idafiliados : 0;

            }
            return respuesta.IsSuccessStatusCode;


        }
    }
}
