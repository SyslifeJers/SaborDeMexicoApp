using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class RegistroViewModel:BaseViewModel
    {
        public ModelUsuario Model;

        private string cel;
        private string email;
        private string name;
        private string contrasena;
        private string confirPass;
        private string Fake { get; set; }
        private Servicio servicio;
        public Command RegresarCommand { get; }
        public Command GuardarCommand { get; }
        public string Cel
        {
            get => cel;
            set => SetProperty(ref cel, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Contrasena
        {
            get => contrasena;
            set => SetProperty(ref contrasena, value);
        }
        public string ConfirPass
        {
            get => confirPass;
            set => SetProperty(ref confirPass, value);
        }
        private bool ver;
        public bool Ver
        {
            get { return ver; }
            set { SetProperty(ref ver, value); }
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
        private bool counterVer;

        public bool CounterVer
        {
            get { return counterVer; }
            set { SetProperty(ref counterVer, value); }
        }
        public RegistroViewModel()
        {
            RegresarCommand = new Command(OnBack);
            GuardarCommand = new Command(OnSave, ValidationF);
            servicio = new Servicio();
            this.PropertyChanged +=
    (_, __) => GuardarCommand.ChangeCanExecute();
        }

        private bool ValidationF()
        {
            if (string.IsNullOrWhiteSpace(Cel)) return false;
            if (Cel.Length != 10) return false;
            if (string.IsNullOrWhiteSpace(Email)) return false;
            if (string.IsNullOrWhiteSpace(Name)) return false;
            if (string.IsNullOrWhiteSpace(Contrasena)) return false;
            if (string.IsNullOrWhiteSpace(ConfirPass)) return false;
            if (!Contrasena.Equals(ConfirPass)) { Fake = "Error en password"; return false; }

            return true;
        }
        private async void OnBack()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private string fake2;
        public string Fake2
        {
            get => fake2;
            set => SetProperty(ref fake2, value);
        }
        private async void OnSave()
        {

            IsBusy = false;
            CounterIsBusy = true;

            try
            {
                bool pasa = true;
                if (Name.Length < 2) { Fake2 = "Nombre muy corto"; pasa = false; }
                if (Contrasena.Length < 5) { Fake2 = "Contraseña mayor a 6 caracteres"; pasa = false; }
                // if (IsValidEmail(Email)) { Fake2 = "Correo no valido"; pasa = false; }
                if (!Contrasena.Equals(ConfirPass)) { Fake2 = "Las contraseñas no son iguales"; pasa = false; }


                if (pasa)
                {
                    Model = new ModelUsuario()
                    {
                        Nombre = Name,
                        Contrasena = contrasena,
                        Correo = Email,
                        Telefono = Cel
                    };
                    ModelUsuario model = await servicio.Registrarme(Model);
                    if (model != null)
                    {
                        if (model.Id != 0)
                        {
                            await Shell.Current.GoToAsync("..");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Ups", model.Token, "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Ups", "Problemas en la nube intente mas tarde", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Ups", Fake2, "Ok");
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = true;
                CounterIsBusy = false;
            }
            //   await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack

        }
    }
}
