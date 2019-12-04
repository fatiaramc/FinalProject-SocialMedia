using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public abstract class Decorator : Notificacion
    {
        public abstract override void Notificar();
    }
}
