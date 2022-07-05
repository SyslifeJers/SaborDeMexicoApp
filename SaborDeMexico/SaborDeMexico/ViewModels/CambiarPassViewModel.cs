using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class CambiarPassViewModel : BaseViewModel
    {
        public Servicio servicio;
        private readonly ServiosMensajes _serviosMensajes;

        public Command Paso1Command { get; }
        public Command Paso2Command { get; }
        public Command Paso3Command { get; }
        public Command ReenCommand { get; }
        public Command BackCommand { get; }

        public CambiarPassViewModel()
        {
            servicio = new Servicio();
            _serviosMensajes = DependencyService.Get<ServiosMensajes>();
            Paso1Command = new Command(OnGenClicked);
            Paso2Command = new Command(OnVenClicked);
            Paso3Command = new Command(OnCambPassClicked);
            ReenCommand = new Command(ReenviarClicked);
            BackCommand = new Command(Back);
            Fase1 = true;
            Fase2 = false;
            Fase3 = false;
            Correo = "";
            Clave = "";
            Pass = "";
            ConfPass = "";
        }
        #region Propiedades
        private string correo;

        public string Correo
        {
            get { return correo; }
            set => SetProperty(ref correo, value);
        }

        private string clave;

        public string Clave
        {
            get { return clave; }
            set => SetProperty(ref clave, value);
        }
        private string pass;

        public string Pass
        {
            get { return pass; }
            set => SetProperty(ref pass, value);
        }
        private string confpass;

        public string ConfPass
        {
            get { return confpass; }
            set => SetProperty(ref confpass, value);
        }

        private bool fase1;

        public bool Fase1
        {
            get { return fase1; }
            set
            {
                fase1 = value;
                OnPropertyChanged();
            }
        }
        private bool fase2;

        public bool Fase2
        {
            get { return fase2; }
            set
            {
                fase2 = value;
                OnPropertyChanged();
            }
        }
        private bool fase3;

        public bool Fase3
        {
            get { return fase3; }
            set
            {
                fase3 = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region Funciones
        // await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        private bool Validacion()
        {
            if (Correo.Length == 10)
            {
                return true;
            }
            return false;
        }
        private bool Validacion2()
        {
            if (Clave.Length == 5)
            {
                return true;
            }
            return false;
        }
        private bool Validacion3()
        {
            if (Pass.Equals(ConfPass))
            {
                return true;
            }
            return false;
        }
        private async void OnGenClicked()
        {
            IsBusy = false;
            try
            {
                Models.ModelUsuario modele = new Models.ModelUsuario() { Correo = Correo };
                var model = await servicio.GenerarCAsync(modele);
                if (model.Id == -1)
                {
                    await _serviosMensajes.ShowAsync("Problemas de nube :( intente mas tarde");
                }
                else if (model.Id == 0)
                {
                    await _serviosMensajes.ShowAsync("ese telefono no lo tenemos resgistrado");
                }
                else
                {
                    Fase(1);
                }

            }
            finally
            {
                IsBusy = true;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }
        private async void OnVenClicked()
        {
            IsBusy = false;
            try
            {
                Models.ModelUsuario modele = new Models.ModelUsuario()
                {
                    Correo = Correo,
                    CodigoR = Clave
                };
                var model = await servicio.VerificacionCAsync(modele);
                if (model.Id == -1)
                {
                    await _serviosMensajes.ShowAsync("Problemas de nube :( intente mas tarde");
                }
                else if (model.Id == 0)
                {
                    await _serviosMensajes.ShowAsync("este codigo no es el correcto");
                }
                else
                {
                    Fase(2);
                }

            }
            finally
            {
                IsBusy = true;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }
        private async void ReenviarClicked()
        {
            IsBusy = false;
            try
            {
                Models.ModelUsuario modele = new Models.ModelUsuario()
                {
                    Correo = Correo
                };
                var model = await servicio.ReenviarCAsync(modele);
                if (model.Id == -1)
                {
                    await _serviosMensajes.ShowAsync("Problemas de nube :( intente mas tarde");
                }
                else if (model.Id == 0)
                {
                    await _serviosMensajes.ShowAsync("Espera maximo 5 mins en lo que llaga el correo");
                }
                else
                {
                    await _serviosMensajes.ShowAsync("Espera maximo 5 mins en lo que llaga el correo");
                    Fase(2);
                }

            }
            finally
            {
                IsBusy = true;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }
        private async void OnCambPassClicked()
        {
            IsBusy = false;
            try
            {
                Models.ModelUsuario modele = new Models.ModelUsuario()
                {
                    Correo = Correo,
                    CodigoR = Clave,
                    Contrasena = Pass
                };
                var model = await servicio.CambioContraseCAsync(modele);
                if (model.Id == -1)
                {
                    await _serviosMensajes.ShowAsync("Problemas de nube :( intente mas tarde");
                }
                else if (model.Id == 0)
                {
                    await _serviosMensajes.ShowAsync(model.Token);
                }
                else
                {
                    Fase(3);
                    await Shell.Current.GoToAsync($"..");
                }

            }
            finally
            {
                IsBusy = true;
            }
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

        }
        private async void Back()
        {
            await Shell.Current.GoToAsync($"..");
        }
        private void Fase(int i)
        {
            switch (i)
            {
                case 1:
                    Fase1 = false;
                    Fase2 = true;
                    Fase3 = false;
                    break;
                case 2:
                    Fase1 = false;
                    Fase2 = false;
                    Fase3 = true;
                    break;
                case 3:
                    Fase1 = false;
                    Fase2 = false;
                    Fase3 = false;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}
