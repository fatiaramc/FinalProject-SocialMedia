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
        private static List<Persona> Busqueda { get; set; }
        private static Persona _amigoPerfil { get; set; }
        public static List<IPost> BusquedaHashtag { get; set; }
        public static List<Persona> Personas { get; set; }


        private UsuarioActual()
        {
            _p = new Persona();
            _dataService = PersonaDataService.GetPersonaDataService();
            PostAmigos = new List<IPost>();
        }

        public static UsuarioActual GetUsuarioActual()
        {
            if (_userActual == null)
            {
                _userActual = new UsuarioActual();
            }
            return _userActual;
        }

        public Persona GetUser()
        {
            return _p;
        }
        public void SetUserActual(Persona p)
        {
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
            foreach (var amigo in Amigos)
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

        public void Buscar(List<Persona> result)
        {
            Busqueda = new List<Persona>();
            Busqueda = result;
        }

        public List<Persona> GetBusquedas()
        {
            return Busqueda;
        }

        public void ObtenerPerfilAmigo(int id)
        {
            _amigoPerfil = _dataService.GetPersonaWithId(id)[0];
        }

        public Persona GetPerfilAmigo()
        {
            return _amigoPerfil;
        }

        public List<IPost> GetBusquedaHashtags()
        {
            return BusquedaHashtag;
        }

        public void SetBusquedaHashtags(List<IPost> list)
        {
            BusquedaHashtag = list;
        }

        public void ActualizarPersonas()
        {
            Personas = _dataService.GetPersonas();
        }
        public List<Persona> GetPersonas()
        {
            return Personas;
        }

        public List<Persona> GetAmigosdeAmigo(int idPersona)
        {
            var amigos = _dataService.GetAmigos(idPersona);
            return amigos;
        }

        public List<IPost> GetPostAmigo(int idPersona)
        {
            return _dataService.GetPosts(idPersona);
        }
    }
}
