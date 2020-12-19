using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    public class PerfilViewModel: BaseViewModel
    {
        private string domicilio;
        private string correo;
        private string telefono;
        private readonly Command btnEditar;

        public PerfilViewModel()
        {
            btnEditar = new Command(EditarPerfil);
        }

        public string Domicilio {
            get => domicilio;
            set => SetProperty(ref domicilio, value);
        }
        public string Telefono
        {
            get => telefono;
            set => SetProperty(ref telefono, value);
        }
        public string Correo
        {
            get => correo;
            set => SetProperty(ref correo, value);
        }
        private void EditarPerfil()
        {
            throw new NotImplementedException();
        }
    }
}
