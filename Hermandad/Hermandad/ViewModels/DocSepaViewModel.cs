using System;
using System.Collections.Generic;
using System.Text;
using Hermandad.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    class DocSepaViewModel:BaseViewModel
    {
        public MediaFile file { get; set; }
        private ImageSource foto;
        public Command BtnTomarFoto { get; }
        public Command BtnSeleccionarFoto { get; }
        public Command BtnSubir { get; }
        public DocSepaViewModel()
        {
            BtnTomarFoto = new Command(onTomarFoto);
            BtnSeleccionarFoto = new Command(onSeleccionarFoto);
            BtnSubir = new Command(onSubirFoto);
        }

        private async void onSubirFoto()
        {
            bool resp =  await DocumentosServices.SubirSepa(file);
            if (resp)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "El documento fue subido con exito!", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Compruebe su conexion a Internet", "OK");
            }
        }

        public ImageSource FOTO
        {
            get
            {
                return this.foto;
            }
            set
            {
                SetProperty(ref this.foto, value);
            }
        }
        private async void onSeleccionarFoto()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("Foto no Soportda", "Permisos no concedidos para la foto.", "OK");
                }
                this.file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight
                });
                if (file == null)
                    return;

                this.FOTO = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    //file.Dispose();
                    return stream;
                });
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Disculpe", "Hubo un error.\nError: " + ex.Message, "Ok");
            }

        }

        private async void onTomarFoto()
        {
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert("No hay camara", "No se deteca la camara.", "Ok");
                    return;
                }

                this.file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    // Variable para guardar la foto en el album público
                    SaveToAlbum = true
                });

                if (file == null)
                    return;

                this.FOTO = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                await Application.Current.MainPage.DisplayAlert("Foto realizada", "Localización: " + file.AlbumPath, "Ok");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Permiso denegado", "Da permisos de cámara al dispositivo.\nError: " + ex.Message, "Ok");
            }
        }
    }
}
