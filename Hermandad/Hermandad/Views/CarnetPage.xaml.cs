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
    public partial class CarnetPage : ContentPage
    {
        private CarnetViewModel vm;
        public CarnetPage()
        {
            InitializeComponent();
            BindingContext = vm = new CarnetViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadDelegado();
        }
    }
}