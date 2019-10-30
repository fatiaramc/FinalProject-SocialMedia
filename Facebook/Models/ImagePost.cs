using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class ImagePost : IPost
    {
        public string mensaje { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int idPersona { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int likes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string imagen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool PublishPost()
        {
            throw new NotImplementedException();
        }

        public IPost PublishPost(string m, int id, string img)
        {
            throw new NotImplementedException();
        }
    }
}
