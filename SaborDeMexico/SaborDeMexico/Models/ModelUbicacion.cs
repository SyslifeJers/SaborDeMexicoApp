using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelUbicacion
    {
        public int Id { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Name { get; set; }
        public string Direccion { get; set; }
        public string Token { get; set; }
    }
    public class Ubicacion
    {
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string Name { get; set; }
        public string Direccion { get; set; }
        public int IdCliente { get; set; }
        public string Nota { get; set; }
    }
}
