using Facebook.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class SearchLastname : SearchStrategy
    {
        PersonaDataService _dataService;
        public List<Persona> BuscarAmigos(string busqueda)
        {
            _dataService = PersonaDataService.GetPersonaDataService();
            var personas = _dataService.GetPersonas();
            try
            {
                List<Persona> result = new List<Persona>(personas.FindAll(item => item.Apellido.Contains(busqueda)));
                return result;
            }
            catch (Exception)
            {
                return new List<Persona>();
            }
            
            
        }
    }
}
