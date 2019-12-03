using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class SearchClass
    {
        private readonly SearchStrategy _strategy;
        private readonly string _busqueda;

        public SearchClass(SearchStrategy strategy, string busqueda)
        {
            _strategy = strategy;
            _busqueda = busqueda;
        }

        public List<Persona> Search()
        {
            return _strategy.BuscarAmigos(_busqueda);
        }
    }
}
