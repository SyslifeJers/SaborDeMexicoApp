using SaborDeMexico.Models;
using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private ModelProducto _selectedItem;
        private Servicio getServicio;
        private ObservableCollection<ModelProducto> listCategorias;

        public ObservableCollection<ModelProducto> ListCategorias
        {
            get { return listCategorias; }
            set
            {
                listCategorias = value;
                OnPropertyChanged();
            }
        }

        public Command LoadItemsCommand { get; }
        public Command<ModelProducto> ItemTapped { get; }
        public Command goCarrito { get; }

        public ModelProducto SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
               // OnItemSelected(value);
            }
        }

        private int itemId;



        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId();
            }
        }
        public ItemDetailViewModel()
        {
            getServicio = new Servicio();
            ListCategorias = new ObservableCollection<ModelProducto>();
            LoadItemsCommand = new Command(LoadItemId);
            ItemTapped = new Command<ModelProducto>(OnItemSelected);
            goCarrito = new Command(OnCarrito);
        }

        private async void OnItemSelected(ModelProducto obj)
        {
            if (obj == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetalleProductoPage)}?id={obj.Id}&Cate={obj.Categoria}");
        }       
        private async void OnCarrito()
        {

            // This will push the ItemDetailPage onto the navigation stack
         //   await Shell.Current.GoToAsync($"{nameof(CarritoPage)}?ItemId={ItemId}");
        }

        public async void LoadItemId()
        {

            IsBusy = true;

            try
            {
                switch(ItemId)
                {
                    case 1:
                        Title = "Quesos";
                        break;
                    case 2:
                        Title = "Vinos";
                        break;
                    case 3:
                        Title = "Yogurth";
                        break;
                    case 4:
                        Title = "Dulces";
                        break;
                    
                }
                if (ItemId == 0)
                {
                    return;
                }
                ListCategorias.Clear();
                var items = await getServicio.ListaProductosAsync(itemId);

                ListCategorias = new ObservableCollection<ModelProducto>(items);
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
