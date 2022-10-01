﻿using Hermandad.ViewModels;
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
    public partial class EncuestasPage : ContentPage
    {
        EncuestasViewModel vm;
        public EncuestasPage()
        {
            InitializeComponent();
            BindingContext = vm = new EncuestasViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await vm.LoadEncuestas("E");
        }
    }
}