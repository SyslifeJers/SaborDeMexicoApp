using SaborDeMexico.Models;
using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class CarritoViewModel : BaseViewModel
    {
        private ObservableCollection<ModelCarrito> listCarritos;
        public ObservableCollection<ModelCarrito> ListCarritos
        {
            get
            {
                return listCarritos;
            }
            set { listCarritos = value; OnPropertyChanged(); }
        }
        Servicio getServicio;
        public string Id { get; set; }
        private ModelCarrito _selectedItem;
        private string itemId;
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                OnPropertyChanged();
            }
        }
        public Command<ModelCarrito> ItemTapped { get; }
        public Command GoCarrito { get; }
        public Command BackCarrito { get; }
        public Command ComandRefreshItems { get; }
        public Command ComandGoViewOrden { get; }
        public Command ComandGoClean { get; }

        private string fecha;

        public string Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private string distancia;

        public string Distancia
        {
            get { return distancia; }
            set
            {
                distancia = value;
                OnPropertyChanged();
            }

        }

        private decimal costoEnvio;

        public decimal CostoEnvio
        {
            get { return costoEnvio; }
            set
            {
                costoEnvio = value;
                OnPropertyChanged();
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
        private string nota;

        public string Nota
        {
            get { return nota; }
            set
            {
                nota = value;
                OnPropertyChanged();
            }
        }

        private string error;

        public string Error
        {
            get { return error; }
            set { error = value;
                OnPropertyChanged();
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Ubicacion ubicacion { get; set; }
        private async Task Datos()
        {

            string oname = await SecureStorage.GetAsync("Name");
            Name = oname.ToString();
            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            Ubicacion ubica = await getServicio.GetDatosUbicacion(oauthToken);

            if (ubica != null)
            {
                Calle = ubica.Direccion;
                ubicacion = ubica;
            }
            else
            {
                Calle = "Por favor regrese al inicio para agregar una dirreción";
            }
            await LoadItemId();
        }


        public CarritoViewModel()
        {

            getServicio = new Servicio();
            ListCarritos = new ObservableCollection<ModelCarrito>();
            ComandRefreshItems = new Command(async () => await LoadItemId());
            ItemTapped = new Command<ModelCarrito>(OnItemSelected);
            GoCarrito = new Command(GoListCarrito);
            BackCarrito = new Command(Back);
            ComandGoViewOrden = new Command(GoViewOrden);
            ComandGoClean = new Command(Clean);
            Total = 0;
            Fecha = DateTime.Now.ToLongDateString();
            Datos();
        }
        async void OnItemSelected(ModelCarrito item)
        {
            if (item == null)
                return;

            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            if (await getServicio.DeleteProducCarrito(new ModelCarrito() { Id = item.Id, Token = oauthToken }))
            {
                await LoadItemId();
            }

        }
        async void Clean()
        {

            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            //if (await Servicio.CleanCarrito(new ModelCarrito() { Token = oauthToken }))
            //{
            //    await LoadItemId();
            //}

        }
        async void GoViewOrden()
        {

            if (!string.IsNullOrEmpty(Calle))
            {
                if (ListCarritos.Count==0)
                {
                    Error = "No hay productos en el carrito";
                    return;
                }
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                //mandar a llamar el servico de generar orden
                ModelGOrden model = await getServicio.GenOrden(new ModelGOrden() { CostoEnvio = CostoEnvio, Nota = Nota, Token = oauthToken });

                if (model != null)
                {
                    if (model.idOrden != 0)
                    {
                        await Shell.Current.GoToAsync($"{nameof(ViewOrdenPage)}?{nameof(OrdenViewModel.IdOrden)}={model.idOrden}");
                    }
                    Error = model.Nota;
                }
                else
                {
                    Error = "Problemas en la nube";
                }
            }
            else
            {
                Error = "Por favor regrese al inicio para agregar una dirreción";
            }

        }
        async void Back()
        {
            //await Shell.Current.GoToAsync("..?ItemId=" + itemId);
        }
        async void GoListCarrito()
        {
         //   await Shell.Current.GoToAsync($"{nameof(OrdenPage)}");
        }
        public ModelCarrito SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        private decimal total;

        public decimal Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }
        private decimal subtotal;

        public decimal SubTotal
        {
            get { return subtotal; }
            set
            {
                subtotal = value;
                OnPropertyChanged();
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Command ClickCommand => new Command<string>((url) =>
        {
             Device.OpenUri(new System.Uri(url));
        });
        public async Task LoadItemId()
        {
            IsBusy = true;

            try
            {
                ListCarritos.Clear();

                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                var items = await getServicio.ListaCarritoAsync(new ModelCarrito() { Token = oauthToken });
                if (items.Count == 0)
                {
                   // await Shell.Current.GoToAsync("..?IdRest=" + itemId);
                }
                Total = 0;
                decimal produ = 0;
                SubTotal = 0;
                CostoEnvio = 0;
                ListCarritos = new ObservableCollection<ModelCarrito>(items);
                foreach (var item in items)
                {
                    //    //ListCarritos.Add(item);
                    //    ListCarritos.Insert(idl, item);
                    //    idl++;
                    produ += (decimal)item.Costo * Convert.ToDecimal(item.Cantidad);
                }
                if (items.Count > 0)
                {
                    //var id = items[0].Restaurant;

                    //_modelRestau = await Servicio.DevolDistacia(_modelRestau, (double)ubicacion.Lat, (double)ubicacion.Lon);
                    //Restau = _modelRestau.Nombre;
                    //Distancia = _modelRestau.DistanciaDefin;

                    //CostoEnvio = decimal.Round((decimal)(_modelRestau.Distancia * 4 + 7), 2);

                    Total = CostoEnvio + produ;

                }
                CostoEnvio = 30;
                if (produ >= 200)
                {
                    CostoEnvio = 0;
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
