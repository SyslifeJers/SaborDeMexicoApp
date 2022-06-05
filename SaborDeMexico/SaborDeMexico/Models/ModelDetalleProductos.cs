using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelDetalleProductos
    {

            public int Id_Presentacion { get; set; }
            public string Imagen { get; set; }
            public string Precentacion { get; set; }
            public string Medida { get; set; }
            public decimal Precio { get; set; }
            public int IdProducto { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
        
    }
}
