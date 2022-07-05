using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace SaborDeMexico.Models
{
    public class ModelUbicacion
    {
        public int Id { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Name { get; set; }
        public string Cp { get; set; }
        public string Direccion { get; set; }
        public string Token { get; set; }
    }
    public class Ubicacion
    {
        public string Cp { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal Rango { get; set; }
        public string Name { get; set; }
        public string Direccion { get; set; }
        public int IdCliente { get; set; }
        public string Nota { get; set; }
    }
    public class UbicacionGet
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public decimal Rango { get; set; }
        public string Nombre { get; set; }
        public string Id { get; set; }

    }
    public class ListCircles
    {
        public Circle Circles { get; set; }
    }
}
