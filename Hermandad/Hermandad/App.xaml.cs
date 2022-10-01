using Hermandad.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hermandad
{
    public partial class App : Application
    {
        public static string api { get; set; }
        public App()
        {
            InitializeComponent();
            //if (false)
            //{
            //    MainPage = new AppShell();
            //}
            //else
            //{
                MainPage = new LoginPage();
            //}
        }

        protected override void OnStart()
        {
            //variable de entorno propia para definir cuado estamos en Produccion o DEV
            bool prod = true;
            api = prod ? "https://82.159.210.91:433/api/" : "https://192.168.1.42:433/api/";
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
