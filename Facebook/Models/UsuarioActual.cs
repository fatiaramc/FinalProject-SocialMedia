using Facebook.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class UsuarioActual
    {
        private static UsuarioActual _userActual { get; set; }
        private readonly PersonaDataService _dataService;
        private static Persona _p { get; set; }
        private static List<Persona> Amigos { get; set; }
        private static List<IPost> PostPropios { get; set; }
        private static List<IPost> PostAmigos { get; set; }

        private UsuarioActual()
        {
            _p = new Persona();
            _dataService = PersonaDataService.GetPersonaDataService();
            PostAmigos = new List<IPost>();
        }

        public static UsuarioActual GetUsuarioActual() {
            if(_userActual == null)
            {
                _userActual = new UsuarioActual();
            }
            return _userActual;
        }

        public Persona GetUser()
        {
            return _p;
        }
        public void SetUserActual(Persona p) {
            _p = p;
            ActualizarAmigos();
            ActualizarMisPost();
            ActualizarPostAmigos();
        }
        public void ClearUser()
        {
            _userActual = null;
        }

        public List<Persona> GetAmigos()
        {
            return Amigos;
        }

        public void ActualizarAmigos()
        {
            Amigos = _dataService.GetAmigos(_p.idPersona);
        }

        public void EditarDescripcion(string desc)
        {
            _p.descripcion = desc;
        }

        public void ActualizarUsuario()
        {
            _p = _dataService.GetPersonaWithId(_p.idPersona)[0];
        }

        public void ActualizarMisPost()
        {
            PostPropios = _dataService.GetPosts(_p.idPersona);
        }

        public void ActualizarPostAmigos()
        {
            PostAmigos = new List<IPost>();
            foreach(var amigo in Amigos)
            {
                PostAmigos.AddRange(_dataService.GetPosts(amigo.idPersona));
            }
        }

        public List<IPost> GetMisPosts()
        {
            return PostPropios;
        }

        public List<IPost> GetPostsAmigos()
        {
            return PostAmigos;
        }

        public Persona GetAmigo(int id)
        {
            return _dataService.GetPersonaWithId(id)[0];
        }
    }
}
