using Facebook.DataAccess;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class MessagePost : IPost
    {
        private readonly PersonaDataService _dataService;

        public int idPost { get; set; }
        public string mensaje { get; set; }
        public int idPersona { get; set; }
        public int likes { get; set; }
        public string imagen { get; set; }
        public List<Comentario> comentarios { get; set; }
        public List<string> hashtags { get; set; }
        public List<Persona> etiquetas { get; set; }
        private string _state;

        public MessagePost() { _dataService = PersonaDataService.GetPersonaDataService(); }

        public MessagePost(string m, int id, string img)
        {
            mensaje = m;
            idPersona = id;
            likes = 0;
            imagen = img;
            _dataService = PersonaDataService.GetPersonaDataService();

            _state = "Creando";
            //GetComentarios();
        }

        public bool PublishPost()
        {
            return _dataService.AgregarPost(mensaje, idPersona, imagen).Item1;
            //throw new NotImplementedException();
        }

        public void Like()
        {
            likes++;
        }

        public List<Comentario> ObtenerComentarios()
        {
            comentarios =_dataService.GetComentarios(idPost);
            return comentarios;
        }

        public List<string> GetHashtags()
        {
            BusquedaTexto busqueda = new BusquedaTexto(mensaje);
            BuscarHashtag opBuscarHashtag = new BuscarHashtag(busqueda);
            Invoker invoker = new Invoker();
            invoker.recibirOperacion(opBuscarHashtag);
            invoker.realizarOperaciones();
            var result = busqueda.resultado;

            return result;
        }

        public List<Persona> GetEtiquetas()
        {
            UsuarioActual.GetUsuarioActual().ActualizarPersonas();
            BusquedaTexto busqueda = new BusquedaTexto(mensaje);
            BuscarEtiqueta opBuscar = new BuscarEtiqueta(busqueda);
            Invoker invoker = new Invoker();
            invoker.recibirOperacion(opBuscar);
            invoker.realizarOperaciones();
            var result = busqueda.resultadoEtiquetas;
            List<Persona> etiquetas = new List<Persona>();
            foreach(var res in result)
            {
                var amigo = UsuarioActual.GetUsuarioActual().GetPersonas().Find(item => item.Nombre == res.Item1 && item.Apellido == res.Item2);
                if(amigo != null)
                {
                    etiquetas.Add(amigo);
                }
            }
            

            return etiquetas;
        }
        public void SetState(string value)
        {
            _state = value;
        }

        public string GetState()
        {
            return _state;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(this._state);
        }

        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Uknown memento class " + memento.ToString());
            }

            _state = memento.GetState();
        }
    }
}
