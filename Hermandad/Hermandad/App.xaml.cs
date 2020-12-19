using Hermandad.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermandad
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (false)
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
