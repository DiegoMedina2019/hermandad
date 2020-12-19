using Hermandad.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Hermandad.ViewModels
{
    [QueryProperty(nameof(MjsID), nameof(MjsID))]
    public class DetalleMjsViewModel:BaseViewModel
    {
        private string mjsId;
        private string emisor;
        private string mensaje;
        private string fecha;
        private Mensaje mjs;

        public DetalleMjsViewModel(Mensaje mjs)
        {
            this.mjs = mjs;
            LoadMjsId(mjs);
        }

        public string Emisor
        {
            get => emisor;
            set => SetProperty(ref emisor, value);
        }

        public string Mensaje
        {
            get => mensaje;
            set => SetProperty(ref mensaje, value);
        }
        public string Fecha
        {
            get => fecha;
            set => SetProperty(ref fecha, value);
        }
        public string MjsID
        {
            get
            {
                return mjsId;
            }
            set
            {
                mjsId = value;
               // LoadMjsId(value);
            }
        }

        private void LoadMjsId(Mensaje m)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                Emisor = m.emisor;
                Mensaje = m.mensaje;
                Fecha = m.Fecha;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
