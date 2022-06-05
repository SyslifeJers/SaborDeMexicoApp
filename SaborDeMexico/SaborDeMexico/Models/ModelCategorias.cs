using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelCategorias
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string ImagenModel { get; set; }
        public int Activo { get; set; }
        public int IdPorducto { get; set; }
    }
}
