using Facebook.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class MessagePost : IPost
    {
        private readonly PersonaDataService _dataService;

        public string mensaje { get; set; }
        public int idPersona { get; set; }
        public int likes { get; set; }
        public string imagen { get; set; }

        public MessagePost(string m, int id, string img)
        {
            mensaje = m;
            idPersona = id;
            likes = 0;
            imagen = img;
            //_dataService = new PersonaDataService(connectionString);
        }

        public IPost PublishPost(string m, int id, string img)
        {
            throw new NotImplementedException();
        }
        /*public IPost PublishPost(string m, int id, string img)
{
   return throw NotImplementedException;
   //return new MessagePost(m, id, img);
   //_dataService.
}*/
    }
}
