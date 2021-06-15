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
        static string yourAppId = "input your AppId";
        public WeatherRepositoryDevelopment()
        {
        }

        public WeatherForecast SearchByCityName(string cityname)
        {

            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("q", cityname);
            para.Add("units", "metric");
            para.Add("APPID", yourAppId);
            var json = GetDataSource(wUrl, para);
            var data = JsonConvert.DeserializeObject<WeatherData.Root>(json);
            WeatherForecast result = ProcessResult(data);
            return result;
        }

        public async Task<WeatherForecast> SearchByCityNameAsync(string cityname)
        {

            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("q", cityname);
            para.Add("units", "metric");
            para.Add("APPID", yourAppId);
            var json = await GetDataSourceAsync(wUrl, para);
            var data = JsonConvert.DeserializeObject<WeatherData.Root>(json);
            WeatherForecast result = ProcessResult(data);
            return result;
        }

        private WeatherForecast ProcessResult(WeatherData.Root data)
        {
            WeatherForecast result = new WeatherForecast()
            {
                Date = UnixTimestampToDateTime(data.dt),
                CityName = data.name,
                TemperatureC = data.main.temp,
                TemperatureMinC = data.main.temp_min,
                TemperatureMaxC = data.main.temp_max,
                TemperatureFeelsLikeC = data.main.feels_like,
                CountryCode = data.sys.country,
                Weather = data.weather[0]?.main,
                WeatherDescription = data.weather[0]?.description
            };
            return result;
        }

        private Uri GenerateURI(string url, Dictionary<string, string> parameters = null)
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
            return uri;
        }

        public string GetDataSource(string url, Dictionary<string, string> parameters = null)
        {
            Uri uri = GenerateURI(url, parameters);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            var result = responseStream.ReadToEnd();
            webresponse.Close();

            return result;
        }

        public async Task<string> GetDataSourceAsync(string url, Dictionary<string, string> parameters = null)
        {
            Uri uri = GenerateURI(url, parameters);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse webresponse = (HttpWebResponse) await webrequest.GetResponseAsync();
            Encoding enc = Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            var result = responseStream.ReadToEnd();
            webresponse.Close();

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

        public string GetSource()
        {
            throw new NotImplementedException();
        }

        public DateTime UnixTimestampToDateTime(double unixTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInTicks = (long)(unixTime * TimeSpan.TicksPerSecond);
            return new DateTime(unixStart.Ticks + unixTimeStampInTicks, DateTimeKind.Utc);
        }
    }
}
