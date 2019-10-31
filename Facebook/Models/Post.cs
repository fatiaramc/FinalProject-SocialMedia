using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    //creator
    public abstract class Post
    {
        public abstract IPost CreatePost();
    }
}
