using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public interface IProxy
    {
        WeatherObject weather(string ciudad);
        ForecastObject forecast(string ciudad);
    }
}
