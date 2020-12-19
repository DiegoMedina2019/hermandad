using Hermandad.Models;
using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Hermandad.Views;

namespace Hermandad.ViewModels
{
    public class MensajesViewModel: BaseViewModel
    {
        #region Atributos
        private List<Mensaje> mensajesDelJson;
        private bool isLoading;
        private ObservableCollection<Mensaje> mensajesList;
        public Command<Mensaje> MensajeSeleccionado { get; }
        #endregion

        public MensajesViewModel()
        {
            MensajeSeleccionado = new Command<Mensaje>(OnClickMjs);
        }

        #region Propiedades
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public ObservableCollection<Mensaje> MensajesList
        {
            get { return mensajesList; }
            set { mensajesList = value; this.OnPropertyChanged(); }
        }
        #endregion

        #region Metodos
        public async Task LoadMensajes()
        {
            isLoading = true;

            mensajesDelJson = await MensajesServices.GetMensajes();
            MensajesList = new ObservableCollection<Mensaje>(mensajesDelJson);

            isLoading = false;
        }
        private async void OnClickMjs(Mensaje mjs)
        {
            if (mjs == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
           await Application.Current.MainPage.Navigation.PushModalAsync(new DetalleMensajePage(mjs));
           // await Shell.Current.GoToAsync($"{nameof(DetalleMensajePage)}?{nameof(DetalleMjsViewModel.MjsID)}={mjs.idmensajes}");
        }
        #endregion
    }
}
