using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class MessagePostCreator : Post
    {
        public override IPost CreatePost(string m, int id, string img)
        {
            return new MessagePost(m,id,img);
        }
    }
}
