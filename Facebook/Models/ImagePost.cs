﻿using Facebook.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class ImagePost : IPost
    {
        private readonly PersonaDataService _dataService;

        public int idPost { get; set; }
        public string mensaje { get; set; }
        public int idPersona { get; set; }
        public int likes { get; set; }
        public string imagen { get; set; }
        public List<AdapterId> comentarios { get; set; }

        public ImagePost() { _dataService = PersonaDataService.GetPersonaDataService(); }

        public ImagePost(string m, int id, string img)
        {
            mensaje = m;
            idPersona = id;
            likes = 0;
            imagen = img;
            _dataService = PersonaDataService.GetPersonaDataService();
            //GetComentarios();
        }
        public bool PublishPost()
        {
            return _dataService.AgregarPost(mensaje, idPersona, imagen);
            //throw new NotImplementedException();
        }

        public void Like()
        {
            likes++;
        }

        public List<AdapterId> ObtenerComentarios()
        {
            comentarios = _dataService.GetComentarios(idPost);
            return comentarios;
        }
    }
}
