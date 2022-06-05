using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    [QueryProperty(nameof(IdOrden), nameof(IdOrden))]
    public class OrdenViewModel : BaseViewModel
    {
        public string IdOrden
        {
            get
            {
                return idOrden;
            }
            set
            {
                idOrden = value;
                OnPropertyChanged();
                LoadItemId();
            }
        }
        private Servicio getServicio;

        public ObservableCollection<ModelDetalleOrden> ListCarritos { get; }

        public OrdenViewModel()
        {
            getServicio = new Servicio();
            ListCarritos = new ObservableCollection<ModelDetalleOrden>();
            Estatus = "";
            BackCommand = new Command(Back);
        }

        private async void Back(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        private string estatus;
        private string idOrden;

        public string Estatus
        {
            get { return estatus; }
            set
            {

                estatus = value;
                OnPropertyChanged();
            }

        }
        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value;
                OnPropertyChanged();
            }
        }
        
        private decimal envio;

        public decimal Envio
        {
            get { return envio; }
            set {
                envio = value;
                OnPropertyChanged();
            }
        }


        public Command BackCommand { get; }

        private async Task LoadItemId()
        {
            List<ModelDetalleOrden> list = await getServicio.DevGenOrden(Convert.ToInt32(IdOrden));

            ListCarritos.Clear();
            foreach (var item in list)
            {
                ListCarritos.Add(item);
            }
            Estatus = EstatusG(list[0].Estatus);
            try
            {
                Total = Convert.ToDecimal(list[0].Total) + Convert.ToDecimal(list[0].Envio);
                Envio = Convert.ToDecimal(list[0].Envio);

            }
            catch (Exception)
            {

            }

        }
        private string EstatusG(int id)
        {
            switch (id)
            {
                case -1:
                    return "Cancelado por Adminitración";
                case 0:
                    return "Pedido en espera de pago";
                case 1:
                    return "Ya estamos preparando tu orden";
                case 2:
                    return "Orden enviada";
                case 10:
                    return "Completada";
                default:
                    return "Error";

            }
        }
    }
}
