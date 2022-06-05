using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public partial class ModelProducto
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string DescriCorta { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modificado { get; set; }
        public int? Categoria { get; set; }
    }
    
}
