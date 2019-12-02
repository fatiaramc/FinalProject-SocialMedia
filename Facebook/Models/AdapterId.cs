using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Facebook.Models
{
    public class AdapterId
    {
        [Display(Name = "id")]
        public string id { set; get; }
   
        [Display(Name = "comentario")]
        public string comentario { set; get; }
    }
}
