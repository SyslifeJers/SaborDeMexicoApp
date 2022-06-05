using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelOrden
    {
        public int id { get; set; }
        public int? IdRuta { get; set; }
        public int? IdRepartidor { get; set; }
        public string Plato { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Total { get; set; }
    }
}
