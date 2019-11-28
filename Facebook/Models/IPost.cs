using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public interface IPost
    {
        string mensaje { get; set; }
        int idPersona { get; set; }
        int likes { get; set; }
        string imagen { get; set; }

        bool PublishPost();

        void Like();
    }
}
