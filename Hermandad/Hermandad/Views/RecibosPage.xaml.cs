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
    public partial class RecibosPage : ContentPage
    {
        private readonly ReciboViewModel vm;

        public RecibosPage()
        {
            InitializeComponent();
            BindingContext = vm = new ReciboViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadRecibosAsync();
        }

    }
}