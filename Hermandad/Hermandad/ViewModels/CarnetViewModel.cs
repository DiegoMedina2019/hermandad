using Hermandad.Models;
using Hermandad.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Hermandad.ViewModels
{
    internal class CarnetViewModel : BaseViewModel
    {
        private Usuario _afiliado;
        private bool isLoading;

        private string afiliado;
        private string emaildelegado;
        private string nif;
        private string domicilio;
        private string codigopostal;
        private string poblacion;
        private string provincia;
        private string telefono1;
        private string telefono2;

        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; this.OnPropertyChanged(); }
        }
        public string Afiliado
        {
            get { return afiliado; }
            set { afiliado = value; this.OnPropertyChanged(); }
        }
        public string Email
        {
            get { return emaildelegado; }
            set { emaildelegado = value; this.OnPropertyChanged(); }
        }
        public string Domicilio
        {
            get { return domicilio; }
            set { domicilio = value; this.OnPropertyChanged(); }
        }
        public string CodigoPostal
        {
            get { return codigopostal; }
            set { codigopostal = value; this.OnPropertyChanged(); }
        }
        public string NIF
        {
            get { return nif; }
            set { nif = value; this.OnPropertyChanged(); }
        }
        public string Poblacion
        {
            get { return poblacion; }
            set { poblacion = value; this.OnPropertyChanged(); }
        }
        public string Provincia
        {
            get { return provincia; }
            set { provincia = value; this.OnPropertyChanged(); }
        }
        public string Telefono1
        {
            get { return telefono1; }
            set { telefono1 = value; this.OnPropertyChanged(); }
        }
        public string Telefono2
        {
            get { return telefono2; }
            set { telefono2 = value; this.OnPropertyChanged(); }
        }
        public async Task LoadDelegado()
        {
            isLoading = true;

            _afiliado = await UsuarioServices.GetUsuario();
            try
            {
                Afiliado = _afiliado.razonsocial +  ", " + _afiliado.nombre;
                NIF = _afiliado.nif;
                Domicilio = _afiliado.domicilio;
                CodigoPostal = _afiliado.codigopostal;
                Poblacion = _afiliado.poblacion;
                Provincia = _afiliado.provincia;
                Telefono1 = _afiliado.telefono1;
                Telefono2 = _afiliado.telefono2;
                Email = _afiliado.email;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }

            isLoading = false;
        }
    }
}
