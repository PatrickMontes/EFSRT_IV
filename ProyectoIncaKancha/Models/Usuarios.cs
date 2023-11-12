using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoIncaKancha.Models
{
    public class Usuarios
    {

        public int id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Dni { get; set; }

        [Required()] public Rol IdRol { get; set; }
    }
}