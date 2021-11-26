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
    public class ReyesViewModel:BaseViewModel
    {
        #region Atributos

        private List<Documento> reyesDelJson;
        private bool isLoading;
        private ObservableCollection<Documento> reyesList;
        public Command<Documento> ReyeSeleccionado { get; }
        #endregion

        public ReyesViewModel()
        {
            //reyesList = new ObservableCollection<Documento>
            //{
            //     new Documento
            //    {
            //        idreyes = "1",
            //        nombreFoto = "foto",
            //        url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQEEQPXBhTNwgTecoj9Wf0nRCVqvPIxVQnP-g&usqp=CAU",
            //        Fecha = "2020-12-20",

            //    }
            //};
            ReyeSeleccionado = new Command<Documento>(OnClickReye);
        }

        #region Propiedades
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public ObservableCollection<Documento> ReyesList
        {
            get { return reyesList; }
            set { reyesList = value; this.OnPropertyChanged(); }
        }
        #endregion

        #region Metodos
        public async Task LoadReyesAsync()
        {
            isLoading = true;

            reyesDelJson = await DocumentosServices.GetReyes();
            ReyesList = new ObservableCollection<Documento>(reyesDelJson);

            isLoading = false;
        }
        private async void OnClickReye(Documento d)
        {
            if (d == null)
                return;

            string url = "https://192.168.175.97:433/reyes/" + d.nombre; //local
            //var url = "https://82.159.210.91:433/reyes/" + d.nombre;
            await Launcher.TryOpenAsync(new Uri(url));
        }
        #endregion
    }
}
