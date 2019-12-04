using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class NotificacionLike : Notificacion
    {
        public NotificacionLike(int id1, int id2, int idPost)
        {
            _id1 = id1;
            _id2 = id2;
            _idPost = idPost;
        }

        public override void setTipo()
        {
            tipo = 1;
        }
    }
}
