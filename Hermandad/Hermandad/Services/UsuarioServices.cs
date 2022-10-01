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
        //private static Usuario defalut;

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

        internal async static Task<bool> SetPassword(string idafiliado, string pass)
        {
            string url = App.api + "set_pass_afiliado_hermandad/" + idafiliado;
            object a = new
            {
                Pass = pass
            };
            string body = JsonConvert.SerializeObject(a);
            var respuesta = await client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                if (json.Contains("\"affectedRows\":1"))
                {
                    return respuesta.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return respuesta.IsSuccessStatusCode;
            }
        }

        //Sobre escribo el metodo para setPass para inicios de usuarios por primera ves, que el server se encargara 
        //de cambiar el estado de inicio, es decir de 0 (no inicio sesion nunca) a 1 (inicio sesion por primera vez)
        internal async static Task<bool> SetPassword(string idafiliado, string pass, bool isInit)
        {
            string url = App.api + "set_pass_afiliado_hermandad/" + idafiliado;
            object a = new
            {
                password = pass,
                isInit
            };
            string body = JsonConvert.SerializeObject(a);
            var respuesta = await client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                if (json.Contains("\"affectedRows\":1"))
                {
                    return respuesta.IsSuccessStatusCode;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return respuesta.IsSuccessStatusCode;
            }
        }


        internal async static Task<bool> ValidarAfiliado(string emailTxt, string nombreTxt, string v)//sobrecarga para el registro
        {
            string url = App.api + "login";
            object a = new
            {
                email = emailTxt,
                nombre = nombreTxt,
                registro = v,
                db = "hermandad"
            };

            string body = JsonConvert.SerializeObject(a);


            var respuesta = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<Usuario> afi = JsonConvert.DeserializeObject<List<Usuario>>(json);

                Application.Current.Properties["RegistroIdAfiliado"] = (respuesta.IsSuccessStatusCode) ? afi[0].idafiliado : 0;

            }
            return respuesta.IsSuccessStatusCode;
        }

        internal async static Task<string> AddNacimiento(object a)
        {
            string url = App.api + "nacimiento";
            string body = JsonConvert.SerializeObject(a);

            var respuesta = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            string resp;
            if (respuesta.IsSuccessStatusCode)
            {
                resp = "Su solicitud fue enviada.";
            }
            else
            {
                resp = "Hubo un inconveniente al enviar su solicitud, por favor revise su internet";
            }
            return resp;
        }

        public async static Task<bool> ValidarUsuario(string correo, string pass)
        {
            string url = App.api + "login";
            object a = new
            {
                email = correo,
                Pass = pass,
                db = "hermandad"
            };

            string body = JsonConvert.SerializeObject(a);


            var respuesta = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<Usuario> user = JsonConvert.DeserializeObject<List<Usuario>>(json);

                Application.Current.Properties["idafiliados"] = (respuesta.IsSuccessStatusCode) ? user[0].idafiliado : 0;
                App.Current.Properties["login_init"] = user[0].login_init;

            }
            return respuesta.IsSuccessStatusCode;
        }


        public async static Task<Usuario> GetUsuario()
        {
            var id = Application.Current.Properties["idafiliados"].ToString();
            string url = App.api + "afiliado_Hermandad/" + id;

            var respuesta = await client.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<Usuario> user = JsonConvert.DeserializeObject<List<Usuario>>(json);
                return user[0];
            }
            return default;
        }

        public async static Task<List<Familiar>> GetFamilia()
        {
            var id = Application.Current.Properties["idafiliados"].ToString();
            string url = App.api + "familiares/" + id;

            var respuesta = await client.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<Familiar> _familiares = JsonConvert.DeserializeObject<List<Familiar>>(json);
                return _familiares;
            }
            return default;
        }

        public static async Task<string> PuedeEditar(object a)
        {
            Usuario u = (Usuario)a;
            string url = App.api + "afiliadoAux/" + u.idafiliado;
            //string body = JsonConvert.SerializeObject(datos);

            var respuesta = await client.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                List<AfiliadoAux> user = JsonConvert.DeserializeObject<List<AfiliadoAux>>(json);
                string resp;
                if (user.Count > 0)
                {
                    resp = await EditarDatos(u);
                }
                else
                {
                    resp = await CrearAfiliadoAux(a);
                }
                return resp;
            }
            else
            {
                return "Ya hay una solicitud en proceso.";
            }
        }
        public static async Task<string> CrearAfiliadoAux(object a)
        {
            Usuario u = (Usuario)a;
            object o = new
            {
                domicilio = u.domicilio,
                u.email,
                u.telefono1,
                editado = 1,
                Afiliados_idAfiliados = u.idafiliado
            };
            string url = App.api + "afiliadoAux";

            string body = JsonConvert.SerializeObject(o);

            var respuesta = await client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            string resp;
            if (respuesta.IsSuccessStatusCode)
            {
                resp = "Su solicitud fue enviada, los datos se visualizaran en los proximos dias.";
            }
            else
            {
                resp = "Hubo un inconveniente al enviar su solicitud, por favor revise su internet";
            }
            return resp;
        }
        public static async Task<string> EditarDatos(Usuario a)
        {            
            object o = new
            {
                afiliado = a,
                db = "hermandad"
            };
            string url = App.api + "afiliado/" + a.idafiliado;

            string body = JsonConvert.SerializeObject(o);

            var respuesta = await client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            string resp;
            if (respuesta.IsSuccessStatusCode)
            {
                string json = await respuesta.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<rsp>(json).callMjs;
            }
            else
            {
                resp = "Hubo un inconveniente al enviar su solicitud, por favor revise su internet";
            }
            return resp;
        }
        public static async Task<string> EditarDatosFamiliares(Usuario a)
        {
            object o = new
            {
                a.familiares
            };
            string url = App.api + "afiliado_familia_Update";

            string body = JsonConvert.SerializeObject(o);

            var respuesta = await client.PutAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));
            string resp;
            if (respuesta.IsSuccessStatusCode)
            {

                string json = await respuesta.Content.ReadAsStringAsync();
                resp = JsonConvert.DeserializeObject<rsp>(json).callMjs;
            }
            else
            {
                resp = "Hubo un inconveniente al enviar su solicitud, por favor revise su internet";
            }
            return resp;
        }

    }

}
