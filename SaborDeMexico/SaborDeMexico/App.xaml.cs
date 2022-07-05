using SaborDeMexico.Services;
using SaborDeMexico.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaborDeMexico
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyService.Register<ServiosMensajes, MensajeriaView>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
