using System;
namespace SimpleRestAPI.Models.Weather
{
    public class CityData
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }

        public class Root
        {
            public int id { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public Coord coord { get; set; }
        }
    }
}
