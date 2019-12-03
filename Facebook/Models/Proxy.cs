using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class Proxy : IProxy
    {
        private RestClient _client;
        private string appid = "b1e34d4d55487b41db609a28e5854900";
        private string metric = "metric";

        public Proxy()
        {
            _client = new RestClient("http://api.openweathermap.org/data/2.5/");
        }

        public ForecastObject forecast(string ciudad)
        {
            var request = new RestRequest($"forecast?q={ciudad}&APPID={appid}&units={metric}");
            var response = _client.Get<ForecastObject>(request);
            return response.Data;
        }

        public WeatherObject weather(string ciudad)
        {
            var request = new RestRequest($"weather?q={ciudad}&APPID={appid}&units={metric}");
            var response = _client.Get<WeatherObject>(request);
            return response.Data;
        }
    }
}
