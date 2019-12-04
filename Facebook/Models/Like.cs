using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Like : Decorator
    {
        private readonly Notificacion notificacion;

        public Like(Notificacion notiLike)
        {
            notificacion = notiLike;
        }

        public override void Notificar()
        {
            //hablar a base de datos
            _dataService.AgregarNotificacion(notificacion._id1, notificacion._id2, notificacion._idPost,tipo);
        }

        public override void setTipo()
        {
            tipo = 1;
        }
    }
}
