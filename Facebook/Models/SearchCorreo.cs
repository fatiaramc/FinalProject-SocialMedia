using Facebook.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class SearchCorreo : SearchStrategy
    {
        PersonaDataService _dataService;
        public List<Persona> BuscarAmigos(string busqueda)
        {
            _dataService = PersonaDataService.GetPersonaDataService();
            var personas = _dataService.GetPersonas();
            try
            {
                List<Persona> result = new List<Persona>(personas.FindAll(item => item.correo_electronico.Contains(busqueda)));
                return result;
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
        }
    }
}
