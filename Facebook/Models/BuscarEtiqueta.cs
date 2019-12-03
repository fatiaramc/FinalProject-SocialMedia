using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class BuscarEtiqueta :ICommand
    {
        private BusquedaTexto busqueda;
        public BuscarEtiqueta(BusquedaTexto busqueda)
        {
            this.busqueda = busqueda;
        }

        public void execute()
        {
            this.busqueda.buscarEtiqueta();
        }
    }
}
