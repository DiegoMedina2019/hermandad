using Hermandad.Models;
using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    class FamiliaViewModel: BaseViewModel
    {
        private Usuario afiliado;
        private bool isLoading;
        private ObservableCollection<Familiar> familiaresList;
        public Command Btn_EditarFamiliares => new Command(EditarFamiliares);

        private async void EditarFamiliares()
        {
            string resp = await UsuarioServices.EditarDatosFamiliares(afiliado);
            await Application.Current.MainPage.DisplayAlert("Aviso!", resp, "OK");
        }

        public FamiliaViewModel(Usuario afiliado)
        {
            IsLoading = true;
            this.afiliado = afiliado;
            FamiliaresList = new ObservableCollection<Familiar>(this.afiliado.familiares);
            IsLoading = false;
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public ObservableCollection<Familiar> FamiliaresList
        {
            get { return familiaresList; }
            set { familiaresList = value; this.OnPropertyChanged(); }
        }
    }
}
