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
    public partial class PerfilPage : ContentPage
    {
        PerfilViewModel vm;
        public PerfilPage()
        {
            InitializeComponent();
            this.BindingContext = vm = new PerfilViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.getAfiliado();
        }

    }
}