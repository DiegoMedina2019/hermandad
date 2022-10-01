using Hermandad.Models;
using Hermandad.Services;
using Hermandad.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    internal class EncuestasViewModel: BaseViewModel
    {
        #region Atributos
        private List<Encuesta> encuestasDelJson;
        private bool isLoading;
        private ObservableCollection<Encuesta> encuestasList;
        public Command<Encuesta> EncuestaSeleccionado { get; }
        #endregion

        public EncuestasViewModel()
        {
            EncuestaSeleccionado = new Command<Encuesta>(OnClickMjs);
        }

        #region Propiedades
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public ObservableCollection<Encuesta> EncuestasList
        {
            get { return encuestasList; }
            set { encuestasList = value; this.OnPropertyChanged(); }
        }
        #endregion

        #region Metodos
        public async Task LoadEncuestas(string tipo)
        {
            isLoading = true;

            encuestasDelJson = await EncuestasServices.GetEncuestas(tipo);
            EncuestasList = new ObservableCollection<Encuesta>(encuestasDelJson);

            isLoading = false;
        }
        private async void OnClickMjs(Encuesta encu)
        {
            if (encu == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            if (encu.tipo == "E")
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new DetalleEncuestaPage(encu));
            }
            else
            {
                await Application.Current.MainPage.Navigation.PushModalAsync(new VotacionDetallePage(encu));
            }
            // await Shell.Current.GoToAsync($"{nameof(DetalleMensajePage)}?{nameof(DetalleMjsViewModel.MjsID)}={mjs.idmensajes}");
        }
        #endregion
    }
}
