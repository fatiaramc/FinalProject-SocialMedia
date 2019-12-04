using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class NotificacionModel
    {
        [Display(Name = "id1")]
        public int id1 { set; get; }

        [Display(Name = "id2")]
        public int id2 { set; get; }

        [Display(Name = "idPost")]
        public int idPost { set; get; }

        [Display(Name = "tipo")]
        public int tipo { set; get; }
    }
}
