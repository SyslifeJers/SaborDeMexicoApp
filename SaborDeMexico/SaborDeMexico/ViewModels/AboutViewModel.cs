using SaborDeMexico.Models;
using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private ModelCategorias _selectedItem;
        private Servicio getServicio;
        private ObservableCollection<ModelCategorias> listCategorias;
        public ObservableCollection<ModelUbicacion> ListUbicacion { get; }
        public ObservableCollection<ModelCategorias> ListCategorias
        {
            get { return listCategorias; }
            set
            {
                listCategorias = value;
            }
        }

        private string calle;

        public string Calle
        {
            get { return calle; }
            set
            {
                calle = value;
                OnPropertyChanged();
            }
        }

        private string notacalle;

        public string NotaCalle
        {
            get { return notacalle; }
            set
            {
                notacalle = value;
                OnPropertyChanged();
            }
        }
        private string error;

        public string Error
        {
            get { return error; }
            set { error = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public Command ReLoadCommand { get; }
        public Command<string> LoadItemsCommand { get; }
        public Command UbicacionCommand { get; }
        public Command MapsCommand { get; }
        public Command<ModelCategorias> ItemTapped { get; }
        public Command<string> CommandLink { get; }
        public ModelCategorias SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);

            }
        }

        public async void OnItemSelected(string item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            try
            {

                    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item}");

            }
            catch (Exception)
            {

                throw;
            }
        }     
        public async void CambiarUbi()
        {
            // This will push the ItemDetailPage onto the navigation stack
            try
            {

                await Shell.Current.GoToAsync($"/{nameof(UbicacionPage)}");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
              //  await Load();
            }
        }       
        public async void Maps()
        {
            // This will push the ItemDetailPage onto the navigation stack
            try
            {

                await Shell.Current.GoToAsync($"/{nameof(MapsPage)}");
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
              //  await Load();
            }
        }       
        public async Task<bool> Load()
        {

            // This will push the ItemDetailPage onto the navigation stack
            try
            {
                string oname = await SecureStorage.GetAsync("Name");
                Name = oname.ToString();
                Title = "Hola "+ Name;
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                Ubicacion ubica = await getServicio.GetDatosUbicacion(oauthToken);

                    if (ubica != null)
                    {
                        Calle = ubica.Direccion;

                }
                //var toke = Preferences.Get("TokenFirebase","");
               // getServicio.SetDatosTokenNotifi(new ModelUsuario() { CodigoN = toke, Token = oauthToken });
                return true;

            }
            catch (Exception e)
            {


                return false;
            }
        }

        public AboutViewModel()
        {
            Title = "Bienvenido Usuario";
            getServicio = new Servicio();
           // ItemTapped = new Command<ModelCategorias>(OnItemSelected);
            ListCategorias = new ObservableCollection<ModelCategorias>();
            LoadItemsCommand = new Command<string>(OnItemSelected);
            UbicacionCommand = new Command(CambiarUbi);
            MapsCommand = new Command(Maps);
            CommandLink = new Command<string>(RunLink);
            ReLoadCommand = new Command(async () => await Load());
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            Load();
        }

        private async void RunLink(string obj)
        {
            switch (obj)
            {
                case "1":

                    break;
                case "2":

                    break;
                case "3":
                    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={3}");
                    break;
                case "4":
                    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={1}");
                    break;
                case "5":
                    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={1}");
                    break;
                    
                case "p1":
                    await Browser.OpenAsync("https://www.facebook.com/SabordeMx");
                    break;                    
                case "p2":
                  
                    break;                    

                default:
                    break;
            }
        }

        public void ExecuteLoadItemsCommand(string da)
        {
            IsBusy = true;

            //try
            //{
            //    ListCategorias.Clear();
            //  switch(da)
            //    {
            //        case "1":
            //        item.ImagenModel = "https://dayandnightautospa.com/img/c" + item.Id + ".jpg";
                    
            //    }
            //    ListCategorias = new ObservableCollection<ModelCategorias>(items);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
            //finally
            //{
            //    IsBusy = false;
            //}
        }
        public ICommand OpenWebCommand { get; }
    }
}