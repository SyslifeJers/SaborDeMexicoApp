using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    [QueryProperty(nameof(ItemId), "id")]
    [QueryProperty(nameof(Cate), "Cate")]
    public class DetalleDeProductoViewModel : BaseViewModel
    {
        private int cate;
        public int Cate
        {
            get
            {
                return cate;
            }
            set
            {
                cate = value;
            }
        }
        private String nombre;
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
        private String imagen;
        public string Imagen
        {
            get => imagen;
            set => SetProperty(ref imagen, value);
        }
        private String decrip;
        public string Descripci
        {
            get => decrip;
            set => SetProperty(ref decrip, value);
        }
        private decimal costo;
        public decimal Costo
        {
            get => costo;
            set => SetProperty(ref costo, value);
        }
        private decimal total;
        public decimal Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }
        private int cant;
        public int Cant
        {
            get => cant;
            set => SetProperty(ref cant, value);
        }   
        private int idPresentacion;
        public int IdPresentacion
        {
            get => idPresentacion;
            set => SetProperty(ref idPresentacion, value);
        }
        public Command ComSubir { get; }
        public Command ComSave { get; }
        public Command ComBack { get; }
        public Command<ModelDetalleProductos> SelectCom { get; }
        public Command ComBajar { get; }
        private ObservableCollection<ModelDetalleProductos> getProductos;
        public ObservableCollection<ModelDetalleProductos> GetProductos
        {
            get => getProductos;
            set
            {
                SetProperty(ref getProductos, value);
               OnPropertyChanged();
            }
        }
        private Servicio getServicio { get; set; }
        private string itemId;

        public DetalleDeProductoViewModel()
        {
            getServicio = new Servicio();
            Cant = 1;
            Costo = 0;
            ComSubir = new Command(SubirCant);
            ComBajar = new Command(BajarCant);
            ComSave = new Command(OnSave, validacion);
            this.PropertyChanged +=
(_, __) => ComSave .ChangeCanExecute();
            ComBack = new Command(OnBack);
            SelectCom = new Command<ModelDetalleProductos>(SelectItem);


        }
        public bool validacion()
        {
            if (IdPresentacion == 0) { return false; }
            bool vista = false;
            foreach (ModelDetalleProductos item in getProductos) {
                if (item.Id_Presentacion==IdPresentacion)
                {
                    return true;
                }
            }
            return vista;
        }
        private ModelDetalleProductos _selectedCity;
        public ModelDetalleProductos SelectedCity
        {
            get
            {
                return _selectedCity;
            }
            set
            {
                SetProperty(ref _selectedCity, value);
                //put here your code  
                SelectItem(value);
            }
        }
        private void SelectItem(ModelDetalleProductos obj)
        {
            Costo = obj.Precio;
            Total = Costo * Cant;
            IdPresentacion = obj.Id_Presentacion;
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(string itemId)
        {
            IsBusy = true;

            try
            {

                var ListadeProductos = await getServicio.ProductoReturn(itemId);
                GetProductos = new ObservableCollection<ModelDetalleProductos>(ListadeProductos);
                if (ListadeProductos.Count != 0)
                {
                    Nombre = ListadeProductos[0].Nombre;
                    Descripci = ListadeProductos[0].Descripcion;
                    Imagen = ListadeProductos[0].Imagen;
                }
               
                Total = 0;
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
        public void SubirCant()
        {

            try
            {
                if (Cant < 20)
                {
                    Cant++;
                    Total = Cant * Costo;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }
        private async void OnBack()
        {
           await Shell.Current.GoToAsync("..?ItemId=" + Cate);
        }
        private async void OnSave()
        {
            var oauthToken = await SecureStorage.GetAsync("oauth_token");

            ModelCarrito model = new ModelCarrito()
            {

                Cantidad = Cant,
                IdProducto = Convert.ToInt32(ItemId),
                IdPresentacion = IdPresentacion,
                Token = oauthToken
            };
            if (await getServicio.AddCarrito(model))
            {

            }
            await Shell.Current.GoToAsync("..?ItemId=" + Cate);
        }
        public void BajarCant()
        {

            try
            {
                if (Cant > 0)
                {
                    Cant--;
                    Total = Cant * Costo;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {

            }
        }
    }
}
