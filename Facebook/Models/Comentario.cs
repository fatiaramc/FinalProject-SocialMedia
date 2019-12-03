using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Comentario
    {
        [Display(Name = "user")]
        public Persona user { get; set; }
        [Display(Name = "comentario")]
        public string comentario { get; set; }
    }
}
