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
        private Mensaje_obj mjs;

        public DetalleMjsViewModel(Mensaje_obj mjs)
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

        private void LoadMjsId(Mensaje_obj m)
        {
            try
            {
                //var item = await DataStore.GetItemAsync(itemId);
                Emisor = m.nombre;
                Mensaje = m.Mensaje;
                Fecha = m.Fecha_;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public async void SetVisto()
        {
            await mjs.UpateMensaje();
        }
    }
}
