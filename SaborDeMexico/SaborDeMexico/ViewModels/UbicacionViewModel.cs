using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class UbicacionViewModel : BaseViewModel
     {
        ServiceMaps services;
        Servicio getServicio;

        public Command<Predicion> ItemTapped { get; }
        public Command LoadItemsCommand { get; }

        private string calle;
        public ObservableCollection<Predicion> CompleteResult { get; }

        public string MiCalle
        {
            get { return calle; }
            set
            {
                calle = value;
                SetProperty(ref calle, value);
                LoadItemId(value);

            }
        }
        private string nota;
        private Predicion _selectedItem;

        public string Nota
        {
            get { return nota; }
            set { nota = value; }
        }
        private string cp;

        public string Cp
        {
            get { return cp; }
            set { cp = value; }
        }

        private string mensaje;
        public string Mensaje
        {
            get { return mensaje; }
            set { mensaje = value;
                OnPropertyChanged();
            }
        }
        public Predicion SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnItemSelected(Predicion value)
        {
            if (value != null)
            {
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                GooglePlace googlePlace = await services.DatosLocationId(value.IdRuta);
                var dat = await getServicio.AltaUbicaAsync(new ModelUbicacion() { Lat = (decimal)googlePlace.Latitude, Lon = (decimal)googlePlace.Longitude, Direccion = value.Description, Name = value.IdRuta, Token = oauthToken, Cp = Cp });
                if (dat.Token.Equals("Ok"))
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    Mensaje = dat.Token;
                }
            }
        }

        public UbicacionViewModel()
        {
            services = new ServiceMaps();
            CompleteResult = new ObservableCollection<Predicion>();
            getServicio = new Servicio();
            ItemTapped = new Command<Predicion>(OnItemSelected);
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

        }

        private async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (calle.Length > 4)
                {
                    CompleteResult.Clear();
                    var kir = await services.GetListaDeDireccionesPosibles(calle);
                    foreach (var item in kir)
                    {
                        CompleteResult.Add(new Predicion()
                        {
                            Description = item.Description,
                            IdRuta = item.PlaceId
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void LoadItemId(string calles)
        {
            IsBusy = true;

            try
            {
                if (calles.Length > 4)
                {
                    CompleteResult.Clear();
                    var kir = await services.GetListaDeDireccionesPosibles(calle);
                    foreach (var item in kir)
                    {
                        CompleteResult.Add(new Predicion()
                        {
                            Description = item.Description,
                            IdRuta = item.PlaceId
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
