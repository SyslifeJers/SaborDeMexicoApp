using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelGOrden
    {
        public string Token { get; set; }
        public string Nota { get; set; }
        public decimal CostoEnvio { get; set; }
        public bool Respuesta { get; set; }
        public int idOrden { get; set; }
    }
    public class ModelDetalleOrden
    {
        public string Restaurante { get; set; }
        public int Estatus { get; set; }
        public string Repartidor { get; set; }
        public string Envio { get; set; }
        public string Total { get; set; }
        public string Cant { get; set; }
        public string Fecha { get; set; }
        public string Plato { get; set; }
    }
}
