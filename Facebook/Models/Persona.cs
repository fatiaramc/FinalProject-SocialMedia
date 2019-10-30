using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Facebook.Models
{
    public class Persona
    {
        [Key]
        [Display(Name = "idPersona")]
        public int idPersona { set; get; }

        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "El Nombre es Requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El Apellido es Requerido")]
        public string Apellido { set; get; }

        [Display(Name = "Correo Electronico")]
        [Required(ErrorMessage = "El correo electronico es Requerido")]
        public string correo_electronico { set; get; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La contraseña es Requerida")]
        public string contraseña { set; get; }

        [Display(Name = "Cumpleaños")]
        [Required(ErrorMessage = "El cumpleaños es Requerido")]
        public string fecha_nac { set; get; }

        [Display(Name = "Género")]
        public string genero { set; get; }

        [Display(Name = "Descripción")]
        public string descripcion
        { set; get; }

    }
}
