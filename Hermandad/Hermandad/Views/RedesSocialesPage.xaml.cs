using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermandad.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermandad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RedesSocialesPage : ContentPage
    {
        private RedesViewModel vm;
        public RedesSocialesPage()
        {
            InitializeComponent();
            BindingContext = vm = new RedesViewModel();
        }
    }
}