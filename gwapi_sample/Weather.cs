using System.Collections.Generic;
using System.Net;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace gwapi_sample
{
    internal class Weather
    {
        /// <summary>
        ///     The function that returns the current conditions for the specified location.
        /// </summary>
        /// <param name="location">City or ZIP code</param>
        /// <returns></returns>
        public static Conditions GetCurrentConditions(string location)
        {
            var conditions = new Conditions();
            var results = "";
            using (var wc = new WebClient())
            {
                results = wc.DownloadString(string.Format(
                    "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22saopaulo%2C%20sp%22)%20and%20u%3D%22c%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys",
                    location));
            }

            dynamic jo = JObject.Parse(results);
            var items = jo.query.results.channel.item.condition;
            conditions.Code = items.code;
            conditions.City = items.city;
            conditions.TempC = items.temp;
            conditions.Condition = items.text;
            conditions.Humidity = items.humidity;
            conditions.Title = items.title;
            var wind = jo.query.results.channel.wind;
            conditions.Wind = wind.speed;
            var atmosphere = jo.query.results.channel.atmosphere;
            conditions.Humidity = atmosphere.humidity;

            return conditions;
        }

        /// <summary>
        ///     The function that gets the forecast for the next four days.
        /// </summary>
        /// <param name="location">City or ZIP code</param>
        /// <returns></returns>
        public static List<Conditions> GetForecast(string location)
        {
            var conditions = new List<Conditions>();
            var results = "";
            using (var wc = new WebClient())
            {
                results = wc.DownloadString(string.Format(
                    "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22saopaulo%2C%20sp%22)%20and%20u%3D%22c%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys",
                    location));
            }
            dynamic jo = JObject.Parse(results);
            var forecast = jo.query.results.channel.item.forecast;

            foreach (var items in forecast)
                {
                    var condition = new Conditions
                    {
                       
                       DayOfWeek = items.date+" "+ items.day,
                       High = items.high,
                       Low = items.low,
                       Condition = items.text
                    };
                    conditions.Add(condition);
                }

            return conditions;
        }
    }
}
