using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Rain
    {
        public double __invalid_name__3h { get; set; }
    }

    public class List
    {
        public int dt { get; set; }
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Clouds clouds { get; set; }
        public Wind wind { get; set; }
        public Sys sys { get; set; }
        public string dt_txt { get; set; }
        public Rain rain { get; set; }
    }

    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public Coord coord { get; set; }
        public string country { get; set; }
        public int population { get; set; }
        public int timezone { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class ForecastObject
    {
        public string cod { get; set; }
        public int message { get; set; }
        public int cnt { get; set; }
        public List<WeatherObject> list { get; set; }
        public City city { get; set; }
    }
}
