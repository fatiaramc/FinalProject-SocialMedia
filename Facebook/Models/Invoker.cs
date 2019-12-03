using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Invoker
    {
        private List<ICommand> operaciones = new List<ICommand>();

        public void recibirOperacion(ICommand operacion)
        {
            this.operaciones.Add(operacion);
        }

        public void realizarOperaciones()
        {
            foreach(var o in operaciones)
            {
                o.execute();
            }
            operaciones.Clear();
        }
    }
}
