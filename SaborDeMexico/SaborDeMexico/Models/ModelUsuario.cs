using System;
using System.Collections.Generic;
using System.Text;

namespace SaborDeMexico.Models
{
    public class ModelUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public string Confirm { get; set; }
        public string Token { get; set; }
        public int ResgisUbi { get; set; }
        public string CodigoR { get; set; }
    }
    public class ModelCliente
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contrasena { get; set; }
    }
}
