using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Mencion : Decorator
    {
        private readonly Notificacion notificacion;

        public Mencion(Notificacion notiLike)
        {
            notificacion = notiLike;
        }

        public override void Notificar()
        {
            //hablas base de datos
            _dataService.AgregarNotificacion(notificacion._id1, notificacion._id2, notificacion._idPost,tipo);
        }

        public override void setTipo()
        {
            tipo = 2;
        }
    }
}
