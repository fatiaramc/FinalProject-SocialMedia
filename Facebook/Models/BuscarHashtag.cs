using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class BuscarHashtag : ICommand
    {
        private BusquedaTexto busqueda;

        public BuscarHashtag(BusquedaTexto busqueda)
        {
            this.busqueda = busqueda;
        }

        public void execute()
        {
            this.busqueda.buscarHashtag();
        }
    }
}
