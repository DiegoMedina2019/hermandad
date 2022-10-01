using Hermandad.Models;
using Hermandad.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermandad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEncuestaPage : ContentPage
    {
        private DetalleEncuestaViewModel vm;
        public DetalleEncuestaPage(Encuesta encu)
        {
            InitializeComponent();
            BindingContext = vm = new DetalleEncuestaViewModel(encu);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.getPreguntas();
        }
    }
}