using Hermandad.Models;
using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    internal class DetalleEncuestaViewModel : BaseViewModel
    {
        private List<Pregunta_> preguntasDelJson;
        private bool isLoading;
        private bool isVotacion;
        private bool isEncuesta;
        private ObservableCollection<Pregunta_> preguntasList;
        private Encuesta encu;
        private bool noPregutas;
        private bool quitarBTN;

        public Command Btn_EnviarEncuesta { get; }
        public Command Btn_descargarDoc { get; }

        public DetalleEncuestaViewModel(Encuesta encu)
        {
            this.encu = encu;
            IsVotacion = encu.tipo == "V";
            IsEncuesta = encu.tipo == "E";
            QuitarBTN = encu.FicheroPdf != "";
            Btn_EnviarEncuesta = new Command(OnEnviarEncuesta);
            Btn_descargarDoc = new Command(OnDescargarDoc);
        }

        private async void OnDescargarDoc()
        {
            string url = App.api + "doc_encuestas_h/" + encu.idencuestacabecera;
            //await Launcher.TryOpenAsync(new Uri(url));

            try
            {
                await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
                await Application.Current.MainPage.DisplayAlert("Aviso!", "Hubo un inconveniente al intentar abrir su navegador", "ok");
            }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public bool IsVotacion
        {
            get { return isVotacion; }
            set { isVotacion = value; this.OnPropertyChanged(); }
        }
        public bool IsEncuesta
        {
            get { return isEncuesta; }
            set { isEncuesta = value; this.OnPropertyChanged(); }
        }
        public bool NoPregutas
        {
            get { return noPregutas; }
            set { noPregutas = value; this.OnPropertyChanged(); }
        }
        public bool QuitarBTN
        {
            get { return quitarBTN; }
            set { quitarBTN = value; this.OnPropertyChanged(); }
        }
        private async void OnEnviarEncuesta()
        {
            var respuestas = new List<Pregunta_>(PreguntasList);
            string resp = await EncuestasServices.ResponderEncuesta(respuestas, encu.idencuestacabecera);
            await Application.Current.MainPage.DisplayAlert("Aviso!", resp, "OK");
        }

        public ObservableCollection<Pregunta_> PreguntasList
        {
            get { return preguntasList; }
            set { preguntasList = value; this.OnPropertyChanged(); }
        }
        public async Task getPreguntas()
        {
            IsLoading = true;
            preguntasDelJson = await encu.getPreguntas();
            PreguntasList = new ObservableCollection<Pregunta_>(preguntasDelJson);

            foreach (Pregunta_ pregunta in preguntasList)
            {
                pregunta.setTipo();//para poder evaluar un condicional de visibilidad en la vista xaml 
            }
            NoPregutas = preguntasDelJson.Count == 0;
            IsLoading = false;
        }
    }
}
