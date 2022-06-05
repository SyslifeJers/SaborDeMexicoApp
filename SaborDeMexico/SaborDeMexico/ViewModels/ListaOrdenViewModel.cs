using SaborDeMexico.Models;
using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class ListaOrdenViewModel : BaseViewModel
    {
        private ObservableCollection<ModelOrden> listOrden;
        private Servicio getServicio;
        public ObservableCollection<ModelOrden> ListOrden
        {
            get { return listOrden; }
            set
            {
                listOrden = value;
                OnPropertyChanged();
            }
        }

        public Command LoadItemsCommand { get; }

        public ListaOrdenViewModel()
        {
            getServicio = new Servicio();
            ListOrden = new ObservableCollection<ModelOrden>();
            LoadItemsCommand = new Command(async async =>await RecargarITems());
            ItemTapped = new Command<ModelOrden>(OnItemSelected);
            RecargarITems();
        }

        private async Task RecargarITems()
        {
            IsBusy = true;

            try
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                var listOrdenT = await getServicio.ListaOrdenAsync(new ModelUbicacion() { Token = oauthToken });
                ListOrden.Clear();
                ListOrden = new ObservableCollection<ModelOrden>(listOrdenT);
            }
            catch (Exception)
            {



            }
            finally
            {
                IsBusy = false;
            }
        }
        public Command<ModelOrden> ItemTapped { get; }
        async void OnItemSelected(ModelOrden item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            try
            {
                await Shell.Current.GoToAsync($"{nameof(ViewOrdenPage)}?{nameof(OrdenViewModel.IdOrden)}={item.id}");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
