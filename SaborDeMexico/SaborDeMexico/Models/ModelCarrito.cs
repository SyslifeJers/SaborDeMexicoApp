using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelCarrito
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int? IdProducto { get; set; }
        public int? IdPresentacion { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Costo { get; set; }
        public string Nota { get; set; }
        public string NameProduct { get; set; }
        public string NamePresentacion { get; set; }
    }
}
