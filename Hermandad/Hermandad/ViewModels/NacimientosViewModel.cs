using Hermandad.Models;
using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    class NacimientosViewModel :BaseViewModel
    {
        private DateTime fecha_nacimiento;
        private string nombre;
        private string apellido;

        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
        public string Apellido
        {
            get => apellido;
            set => SetProperty(ref apellido, value);
        }
        public DateTime Fecha_Nacimiento
        {
            get => fecha_nacimiento;
            set => SetProperty(ref fecha_nacimiento, value);
        }
        public Command BtnAltaNacimiento => new Command(Alta_Nacimiento);
        private async void Alta_Nacimiento()
        {
            int id = int.Parse(Application.Current.Properties["idafiliados"].ToString());
            object a = new 
            {
                fecha_nacimiento = Fecha_Nacimiento,
                nombre = Nombre,
                apellido = Apellido,
                Afiliados_idAfiliados = id
            };
            string resp = await UsuarioServices.AddNacimiento(a);
            await Application.Current.MainPage.DisplayAlert("Aviso!", resp, "OK");
            Nombre = "";
            Apellido = "";
            Fecha_Nacimiento = DateTime.Now;
        }
    }
}
