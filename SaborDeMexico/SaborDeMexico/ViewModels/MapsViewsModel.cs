using SaborDeMexico.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Distance = Xamarin.Forms.Maps.Distance;

namespace SaborDeMexico.ViewModels
{
    public class MapsViewsModel : BaseViewModel
    {
        Services.Servicio Servicio;

        private Map mapa;
        public Map Mapa
        {
            get { return mapa; }
            set { mapa = value; 
                OnPropertyChanged(); }
        }
        public MapsViewsModel()
        {
            Servicio = new Services.Servicio();
            Mapa = new Map(new MapSpan(new Position(19.5251784, -99.2794155), 0.01, 0.01));
            Load();
        }
        public async void Load ()
        {
            try
            {
                var datos = await Servicio.GetDatosReparto();
                foreach (var item in datos)
                {
                  
                    Circle Circles = new Circle()
                    {
                        Center = new Position((double)item.Lat, (double)item.Lon),
                        Radius = new Distance((double)item.Rango*1000),
                        StrokeColor = Color.FromHex("#88FF0000"),
                        StrokeWidth = 4,
                        FillColor = Color.FromHex("#88FFC0CB")
                    };
                    Pin pin = new Pin()
                    {
                        Position = new Position((double)item.Lat, (double)item.Lon),
                        Label = item.Nombre + " " + item.Id
                    };

                    Mapa.MapElements.Add(Circles);
                    Mapa.Pins.Add(pin);
                }
            }
            catch (Exception w)
            {

                var s = w.Message;
            }

        }
    }
}
