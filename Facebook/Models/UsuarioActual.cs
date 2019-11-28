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

        private UsuarioActual()
        {
            _p = new Persona();
            _dataService = PersonaDataService.GetPersonaDataService();
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
    }
}
