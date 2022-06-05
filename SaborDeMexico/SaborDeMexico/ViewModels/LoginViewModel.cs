using SaborDeMexico.Models;
using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }
        public Command RegistroCommand { get; }
        public Command OlvidaPassCommand { get; }

        private Servicio servicio;
        private string cel;
        private string pass;
        private string fake;
        private bool error;

        public string Cel
        {
            get => cel;
            set => SetProperty(ref cel, value);
        }
        public string Pass
        {
            get => pass;
            set => SetProperty(ref pass, value);
        }
        public string Fake
        {
            get => fake;
            set => SetProperty(ref fake, value);
        }
        public bool Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
        private bool counterIsBusy;
        public bool CounterIsBusy
        {
            get { return counterIsBusy; }
            set
            {
                SetProperty(ref counterIsBusy, value);
                OnPropertyChanged();
            }
        }
        public LoginViewModel()
        {
           
            IsBusy = true;
            RegistroCommand = new Command(OnRegistro);
            OlvidaPassCommand = new Command(OnOlvPass);
            LoginCommand = new Command(OnLoginClicked, ValidationError);
            this.PropertyChanged +=
(_, __) => LoginCommand.ChangeCanExecute();
            servicio = new Servicio();
            Verificar();
        }

        private async void OnLoginClicked()
        {
                IsBusy = false;
            CounterIsBusy = true;
            try
            {
                var model = await servicio.LoginCAsync(new ModelUsuario() { Contrasena = pass, Correo = cel });
                if (model.Id > 0)
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    Error = false;
                }
                else
                {
                    Fake = model.Nombre;
                    await Application.Current.MainPage.DisplayAlert("Ups", Fake, "Ok");
                }
            }
            finally
            {
                IsBusy = true;
                CounterIsBusy = false;
            }

        }
        private async void Verificar()
        {
            var oauthToken = await SecureStorage.GetAsync("oauth_token");
            if (String.IsNullOrEmpty(oauthToken))
            {
                return;
            }
            else
            {
                if (await servicio.GetDatosUsuario(oauthToken))
                {
                    await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    Error = false;
                }
            }
        }

        private bool ValidationError()
        {
            if (string.IsNullOrWhiteSpace(Cel)) { Fake = "usuario no registrado"; return false; }

            if (string.IsNullOrWhiteSpace(Pass)) { Fake = "Error en contrase/a"; return false; }

            return true;
        }
        private async void OnRegistro(object obj)
        {
            await Shell.Current.GoToAsync("//login/registration");
        }
        private async void OnOlvPass(object obj)
        {
           // await Shell.Current.GoToAsync($"{nameof(CambiarPassPage)}");
        }
    }
}
