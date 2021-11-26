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
    public partial class DocCepaPage : ContentPage
    {
        private readonly DocSepaViewModel _vm;
        public DocCepaPage()
        {
            InitializeComponent();
            this.BindingContext = _vm = new DocSepaViewModel();
        }
    }
}