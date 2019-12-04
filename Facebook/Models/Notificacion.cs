using Facebook.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public abstract class Notificacion
    {
        public int tipo = 1;
        public int _id1;
        public int _id2;
        public int _idPost;
        public PersonaDataService _dataService = PersonaDataService.GetPersonaDataService();
        public virtual void Notificar()
        {
            //agregar a base de datos
            //_dataService
        }

        public abstract void setTipo();
    }
}
