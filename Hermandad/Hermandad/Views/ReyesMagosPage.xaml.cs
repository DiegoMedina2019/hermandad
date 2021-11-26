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
    public partial class ReyesMagosPage : ContentPage
    {
        private ReyesViewModel vm;

        public ReyesMagosPage()
        {
            InitializeComponent();
            this.BindingContext = vm = new ReyesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.LoadReyesAsync();
        }
    }
}