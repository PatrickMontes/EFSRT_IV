using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ProyectoIncaKancha.Models
{
    public class Categoria
    {
        [Display(Name = "ID")]
        public int IdCategoria { get; set; }

        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; }
    }
}