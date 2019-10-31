using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class ImagePostCreator : Post
    {
        public string _m;
        public int _id;
        public string _img;

        public ImagePostCreator(string m, int id, string img)
        {
            _m = m;
            _id = id;
            _img = img;

        }

        public override IPost CreatePost()
        {
            return new ImagePost(_m, _id, _img);
        }
    }
}
