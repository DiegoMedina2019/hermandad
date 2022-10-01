using Hermandad.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hermandad.Services
{
    public static class DocumentosServices
    {
        public static readonly HttpClient client = CrearHttpClient();

        private static HttpClient CrearHttpClient()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );
            return httpClient;

        }
        public async static Task<List<Documento>> GetReyes()
        {
            
            var id = Application.Current.Properties["idafiliados"].ToString();

            string url = App.api + "reyes_magos/" + id;

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Documento>>(json);
            }
            return default;
        }
        public static async Task<bool> SubirSepa(MediaFile file)
        {
            try
            {
                string url = App.api + "sepa";

                StreamContent scontent = new StreamContent(file.GetStream());
                scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    FileName = "DocSepa_afiliado_" + Application.Current.Properties["idafiliados"],
                    Name = "image"

                };
                scontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                //var client = new HttpClient();
                var multi = new MultipartFormDataContent();
                object a = new
                {
                    ruta = "/public/documentos/sepa"
                };

                string body = JsonConvert.SerializeObject(a);
                
                multi.Add(scontent);
                multi.Add(new StringContent(body, Encoding.UTF8, "application/json"));
               // client.BaseAddress = new Uri(Constants.API_ROOT_URL);
                var result =  client.PostAsync(url, multi).Result;
                Debug.WriteLine(result.ReasonPhrase);
                return true;
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }
    }
}
