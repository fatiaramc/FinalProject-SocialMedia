using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public interface IPost
    {
        int idPost { get; set; }
        string mensaje { get; set; }
        int idPersona { get; set; }
        int likes { get; set; }
        string imagen { get; set; }
        List<AdapterId> comentarios { get; set; }

        bool PublishPost();

        void Like();

        List<AdapterId> ObtenerComentarios();
    }
}
