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
        public DetalleMensajePage(Mensaje mjs)
        {
            InitializeComponent();
            BindingContext =  new DetalleMjsViewModel(mjs);
        }
    }
}