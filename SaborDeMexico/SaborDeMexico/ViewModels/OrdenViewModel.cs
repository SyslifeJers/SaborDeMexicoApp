using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    [QueryProperty(nameof(IdOrden), nameof(IdOrden))]
    public class OrdenViewModel : BaseViewModel
    {
        private bool botonC;

        public bool BotonC
        {
            get { return botonC; }
            set { botonC = value;
                OnPropertyChanged();

            }
        }

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
        private string error;

        public string Error
        {
            get { return error; }
            set
            {
                error = value;
                OnPropertyChanged();
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
            CancelarPedidoCommand = new Command(CancelarAsync);
        }

        private bool Validar()
        {
            if (EstatusId == 0)
                return true;
            else
                return false;
        }

        async void CancelarAsync(object obj)
        {
            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            //mandar a llamar el servico de generar orden
            ModelGOrden model = await getServicio.CanOrden(new ModelGOrden() { Token = oauthToken, idOrden = Convert.ToInt32(IdOrden) });

            if (model != null)
            {
                if (model.Respuesta)
                {
                    Estatus = "Pedido Cancelado";
                    EstatusId = -1;
                }
                else
                {
                    Error = model.Nota;
                }

            }
            else
            {
                Error = "Problemas en la nube";
            }
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
        private int estatusId;

        public int EstatusId
        {
            get { return estatusId; }
            set { estatusId = value;
                OnPropertyChanged();
                BotonC = Validar();
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
        public Command CancelarPedidoCommand { get; }

        private async Task LoadItemId()
        {
            List<ModelDetalleOrden> list = await getServicio.DevGenOrden(Convert.ToInt32(IdOrden));

            ListCarritos.Clear();
            foreach (var item in list)
            {
                ListCarritos.Add(item);
            }
            Estatus = EstatusG(list[0].Estatus);
            EstatusId = list[0].Estatus;
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
                    return "Pedido Cancelado";
                case 0:
                    return "Su pedido fue registrado exitosamente y sera enviado en nuestra proxima ruta";
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
