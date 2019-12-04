using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class MessagePostCreator : Post
    {
        public string _m;
        public int _id;
        public string _img;
        

        public MessagePostCreator(string m, int id)
        {
            _m = m;
            _id = id;
            //_img = img;

        }

        public override IPost CreatePost()
        {
            return new MessagePost(_m,_id,"");
        }
    }
}
