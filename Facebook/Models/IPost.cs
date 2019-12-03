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
        List<Comentario> comentarios { get; set; }
        List<string> hashtags { get; set; }
        List<Persona> etiquetas { get; set; }

        bool PublishPost();

        void Like();

        List<Comentario> ObtenerComentarios();
        List<string> GetHashtags();
        List<Persona> GetEtiquetas();
    }
}
