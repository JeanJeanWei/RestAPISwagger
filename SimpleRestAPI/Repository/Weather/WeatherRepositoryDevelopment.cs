using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleRestAPI.Models.Weather;

namespace SimpleRestAPI.Repository.Weather
{
    public class WeatherRepositoryDevelopment : IRepository<string>
    {
        static string txtPath = Path.Combine(Environment.CurrentDirectory, "citylist.json");
        static string wUrl = "http://api.openweathermap.org/data/2.5/weather";
        public WeatherRepositoryDevelopment()
        {
        }

        public WeatherForecast SearchByCityName(string cityname)
        {

            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("q", cityname);
            para.Add("APPID", "input your APPID");
            var json = GetDataSource(wUrl, para);
            var data = JsonConvert.DeserializeObject<WeatherData.Root>(json);
            WeatherForecast result = new WeatherForecast()
            {
                Date = DateTime.Now,
                Summary = data.name,
                TemperatureC = (int)data.main.temp
            };
            return result;
        }
        //public string GetDataSource1()
        //{
        //    //q = London,uk & APPID = 

            
        //}

        public string GetDataSource(string url, Dictionary<string, string> parameters = null)
        {

            Uri uri = new Uri(url);
            if (parameters != null && parameters.Count > 0)
            {
                var stringBuilder = new StringBuilder();
                string str = "?";
                foreach (string key in parameters.Keys)
                {
                    stringBuilder.Append(str +
                        WebUtility.UrlEncode(key) +
                         "=" + WebUtility.UrlEncode(parameters[key]));
                    str = "&";
                }

                uri = new Uri(url + stringBuilder.ToString());
            }

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            var result = responseStream.ReadToEnd();
            
            return result;
        }

        public void ProcessWeatherData(string jsonString)
        {
            WeatherData.Root weatherJson = JsonConvert.DeserializeObject<WeatherData.Root>(jsonString);
            List<WeatherForecast> forecasts = new List<WeatherForecast>();
            {

            }

        }

        public dynamic CityDataList()
        {
            string json = File.ReadAllText(txtPath);
            //JArray array = JArray.Parse(json);
            var cityList = JsonConvert.DeserializeObject<List<CityData.Root>>(json);
            return cityList;
        }

        public string GetDataSource()
        {
            throw new NotImplementedException();
        }
    }
}
