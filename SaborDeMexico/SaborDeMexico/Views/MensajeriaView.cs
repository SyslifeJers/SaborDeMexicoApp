using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SaborDeMexico.Views
{
    public class MensajeriaView : Services.ServiosMensajes
    {
        public async Task ShowAsync(string menssage)
        {
            await App.Current.MainPage.DisplayAlert("Ups", menssage, "Ok");
        }
    }
}
