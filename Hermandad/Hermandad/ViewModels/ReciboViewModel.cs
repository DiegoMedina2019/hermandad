using Hermandad.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.ObjectModel;
using Plugin.DownloadManager;
using Plugin.DownloadManager.Abstractions;
using System.Threading.Tasks;
using Hermandad.Services;

namespace Hermandad.ViewModels
{
    public class ReciboViewModel:BaseViewModel
    {
        private ObservableCollection<Recibo> recibos;
        private List<Recibo> recibossDelJson;
        private bool isLoading;
        public Command<Recibo> ReciboSeleccionado { get; }

        public IDownloadFile File;
        bool isDownloading = true;

        public ReciboViewModel()
        {
            //recibos = new ObservableCollection<Recibo>()
            //{
            //    new Recibo{
            //            idrecibo ="1",
            //            nombreRecibo = "foto",
            //            //url = "https://192.168.175.97:433/recibo/recibo2_ejemplo_afi1.png",
            //            url = "https://82.159.210.91:433/recibo/recibo2_ejemplo_afi1.png",
            //// "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQEEQPXBhTNwgTecoj9Wf0nRCVqvPIxVQnP-g&usqp=CAU",
            //            Fecha = String.Format("{0:dd/MM/yyyy}", "2020-12-20") ,
            //    },
            //};
            ReciboSeleccionado = new Command<Recibo>(OnClickRecibo);

            CrossDownloadManager.Current.CollectionChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine(
                    "[DownloadManager]" + e.Action +
                    " -> New items: " + (e.NewItems?.Count ?? 0) +
                    " at " + e.NewStartingIndex +
                    " || Old items: " + (e.OldItems?.Count ?? 0) +
                    " at " + e.OldStartingIndex
                    );
            };
        }

        public ObservableCollection<Recibo> RecibosList {
            get
            {
                return recibos;
            }
            set
            {
                recibos = value; this.OnPropertyChanged();
            } 
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        internal async Task LoadRecibosAsync()
        {
            isLoading = true;

            recibossDelJson = await RecibosServices.GetRecibos();
            RecibosList = new ObservableCollection<Recibo>(recibossDelJson);

            isLoading = false;
        }
        private async void OnClickRecibo(Recibo r)
        {
            if (r == null)
                return;

            string url = "https://192.168.175.97:433/recibo/"+r.nombre; //local
            //var url = "https://82.159.210.91:433/recibo/"+r.nombre;

            //var temp_path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //var file_name = r.nombreRecibo + ".png";
            //temp_path = Path.Combine(temp_path, file_name);
            //WebClient client = new WebClient();
            //client.DownloadFileCompleted += async (s, args) =>
            //{
            //    await Application.Current.MainPage.DisplayAlert("Archivo Descargado", "El archivo ha sido descargado", "OK");
            //};
            //client.DownloadFileAsync(new Uri(r.url), temp_path);
            //await Application.Current.MainPage.DisplayAlert("Información", "Se ha comenzado la descarga del archivo", "OK");
             await Launcher.TryOpenAsync(new Uri(url)); //abre un navegador para ver el contenido

            // DownloadFile(r.url);
        }

        public async void DownloadFile(String FileName)
        {
            await Task.Yield();
            //  await Navigation.PushPopupAsync(new DownloadingPage());
            await Task.Run(() =>
            {
                var downloadManager = CrossDownloadManager.Current;
                var file = downloadManager.CreateDownloadFile(FileName);
                downloadManager.Start(file, true);
                while (isDownloading)
                {
                    isDownloading = IsDownloading(file);
                }
            });
            if (!isDownloading)
            {

                 await Application.Current.MainPage.DisplayAlert("Status", "Archivo descargado", "OK");
                // DependencyService.Get<IToast>().ShowToast("");
            }
        }
        public void StartDownloading(bool mobileNetworkAllowed)
        {
            CrossDownloadManager.Current.Start(File, mobileNetworkAllowed);
        }

        public void AbortDownloading()
        {
            CrossDownloadManager.Current.Abort(File);
        }
        public bool IsDownloading(IDownloadFile File)
        {
            if (File == null)
            {
                return false;
            }

            switch (File.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                    return true;

                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
