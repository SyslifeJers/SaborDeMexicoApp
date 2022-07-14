using SaborDeMexico.ViewModels;
using SaborDeMexico.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SaborDeMexico
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MapsPage), typeof(MapsPage));
            Routing.RegisterRoute(nameof(ListaOrdenPage), typeof(ListaOrdenPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(UbicacionPage), typeof(UbicacionPage));
            Routing.RegisterRoute(nameof(ViewOrdenPage), typeof(ViewOrdenPage));
            Routing.RegisterRoute(nameof(CarritoPage), typeof(CarritoPage));
            Routing.RegisterRoute(nameof(DetalleProductoPage), typeof(DetalleProductoPage));
            Routing.RegisterRoute(nameof(YoPage), typeof(YoPage));
            Routing.RegisterRoute("registration", typeof(RegistroPage));
            Application.Current.MainPage = new AboutPage();
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Services.Servicio.CleanDatosToken();
            NavigationPage.SetHasNavigationBar(this, false);
            await Shell.Current.GoToAsync("//LoginPage");
        }

    }
}
