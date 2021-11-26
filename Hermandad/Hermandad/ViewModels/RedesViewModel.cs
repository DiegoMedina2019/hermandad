using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    class RedesViewModel
    {
        public ICommand TwitterCommand { get; }
        public ICommand FacebookCommand { get; }
        public RedesViewModel()
        {
            TwitterCommand = new Command(async () => await Browser.OpenAsync("https://mobile.twitter.com/"));
            FacebookCommand = new Command(async () => await Browser.OpenAsync("https://www.facebook.com/"));
        }
    }
}
