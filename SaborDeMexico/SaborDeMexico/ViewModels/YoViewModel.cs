using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class YoViewModel: BaseViewModel
    {
        private Servicio getServicio;

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
        private string calle;

        public string Calle
        {
            get { return calle; }
            set { calle = value; OnPropertyChanged(); }
        }

        private string correo;

        public string Correo
        {
            get { return correo; }
            set { correo = value; OnPropertyChanged(); }
        }
        private string num;

        public string Num
        {
            get { return num; }
            set { num = value; OnPropertyChanged(); }
        }
        public Command SalirCommand { get; }
        public YoViewModel()
        {
            Title = "Bienvenido Usuario";
            getServicio = new Servicio();
            Load();
            SalirCommand = new Command(async () => await CerrarSession());
        }
        public async Task<bool> Load()
        {

            // This will push the ItemDetailPage onto the navigation stack
            try
            {
                string oname = await SecureStorage.GetAsync("Name");
                Name = oname.ToString();
                Title = "Hola " + Name;
                var oauthToken = await SecureStorage.GetAsync("oauth_token");
                Ubicacion ubica = await getServicio.GetDatosUbicacion(oauthToken);
                ModelYo model = await getServicio.GetInformacionUsuario(oauthToken);
                if (model!=null)
                {
                    Name = model.Nombre;
                    Correo = model.Correo;
                    Num = model.Telefono;

                }
                if (ubica != null)
                {
                    Calle = ubica.Direccion;

                }

                return true;

            }
            catch (Exception e)
            {


                return false;
            }
        }
        public async Task<bool> CerrarSession()
        {
            Services.Servicio.CleanDatosToken();
            await Shell.Current.GoToAsync("///login");
            return true;
        }
    }
}
