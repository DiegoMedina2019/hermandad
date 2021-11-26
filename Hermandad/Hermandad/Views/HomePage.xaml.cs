using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace Hermandad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        ObservableCollection<FileImageSource> imageSources = new ObservableCollection<FileImageSource>();
        public HomePage()
        {
            InitializeComponent();
            imageSources.Add("icono_2.jpeg");
            imageSources.Add("icono_1.jpeg");

            imgSlider.Images = imageSources;
        }
    }
}