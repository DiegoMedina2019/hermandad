using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Hermandad.Services;
using Hermandad.Models;
using System.Threading.Tasks;
using Hermandad.Views;

namespace Hermandad.ViewModels
{
    public class PerfilViewModel: BaseViewModel
    {
        private bool isLoading;
        private string razonsocial;
        private string nombre;
        private string nif;
        private string domicilio;
        private string codigopostal;
        private string poblacion;
        private string provincia;
        private string telefono1;
        private string telefono2;
        private string email;
        private Usuario afiliado;

        //private readonly Command btnEditar;
        public Command BtnEditarPerfil => new Command(EditarPerfil);
        public Command BtnEditarFamiliar => new Command(EditarFamiliar);

        public PerfilViewModel()
        {
            //btnEditar = new Command(EditarPerfil);
        }
        internal async Task getAfiliado()
        {
            IsLoading = true;

            afiliado = await UsuarioServices.GetUsuario();
            Apellido = afiliado.razonsocial;
            Nombre = afiliado.nombre;
            NIF = afiliado.nif;
            Domicilio = afiliado.domicilio;
            Codigopostal = afiliado.codigopostal;
            Poblacion = afiliado.poblacion;
            Provincia = afiliado.provincia;
            Telefono1 = afiliado.telefono1;
            Telefono2 = afiliado.telefono2;
            Correo = afiliado.email;

            IsLoading = false;
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public string Apellido
        {
            get => razonsocial;
            set => SetProperty(ref razonsocial, value);
        }
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
        public string NIF
        {
            get => nif;
            set => SetProperty(ref nif, value);
        }
        public string Codigopostal
        {
            get => codigopostal;
            set => SetProperty(ref codigopostal, value);
        }
        public string Poblacion
        {
            get => poblacion;
            set => SetProperty(ref poblacion, value);
        }
        public string Provincia
        {
            get => provincia;
            set => SetProperty(ref provincia, value);
        }
        public string Domicilio {
            get => domicilio;
            set => SetProperty(ref domicilio, value);
        }

        public string Telefono1
        {
            get => telefono1;
            set => SetProperty(ref telefono1, value);
        }
        public string Telefono2
        {
            get => telefono2;
            set => SetProperty(ref telefono2, value);
        }
        public string Correo
        {
            get => email;
            set => SetProperty(ref email, value);
        }



        private async void  EditarPerfil()
        {
           // int id = int.Parse( Application.Current.Properties["idafiliados"].ToString() );
            afiliado.razonsocial = Apellido;
            afiliado.nombre = Nombre;
            afiliado.nif = NIF;
            afiliado.domicilio = Domicilio;
            afiliado.codigopostal = Codigopostal;
            afiliado.poblacion = Poblacion;
            afiliado.provincia = Provincia;
            afiliado.telefono1 = Telefono1;
            afiliado.telefono2 = Telefono2;
            afiliado.email = Correo;

            string resp = await UsuarioServices.EditarDatos(afiliado);
            await Application.Current.MainPage.DisplayAlert("Aviso!", resp, "OK");
        }
        private async void EditarFamiliar()
        {
            afiliado.familiares = await UsuarioServices.GetFamilia();
            await Application.Current.MainPage.Navigation.PushModalAsync(new FamiliaresPage(afiliado));
        }
    }
}
