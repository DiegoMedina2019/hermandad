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
    public partial class DetalleMensajePage : ContentPage
    {
        private DetalleMjsViewModel vm;
        public DetalleMensajePage(Mensaje_obj mjs)
        {
            InitializeComponent();
            BindingContext = vm = new DetalleMjsViewModel(mjs);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm.SetVisto();
        }
    }
}