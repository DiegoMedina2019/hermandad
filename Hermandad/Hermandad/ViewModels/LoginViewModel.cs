using Hermandad.Services;
using Hermandad.ViewModels;
using Hermandad.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegistrarseLabel { get; }
        private string email;
        private string pass;

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            RegistrarseLabel = new Command(LinkRegister);
    }

        public string EmailTxt
        {
            get { return this.email; }
            set
            {
                SetProperty(ref this.email, value);
            }
        }
        public string PassTxt
        {
            get { return this.pass; }
            set { SetProperty(ref this.pass, value); }
        }
        private async void OnLoginClicked(object obj)
        {
            bool respuesta = false;
            if (!String.IsNullOrWhiteSpace(EmailTxt) && !String.IsNullOrWhiteSpace(PassTxt))
            {
                respuesta = await UsuarioServices.ValidarUsuario(EmailTxt.ToString(), PassTxt.ToString());
            }
            //bool respuesta = true;
            //Application.Current.Properties["idafiliados"] = 91356;
            if (respuesta)
            {
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Disculpe!", "Las credenciales no son correctas", "ok");
            }
        }
        private async void LinkRegister()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
        }
    }
}
