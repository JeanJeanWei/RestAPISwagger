using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRestAPI.Repository.Weather;

namespace UnitTests
{
    [TestClass]
    public class WeatherTest
    {
        [TestMethod]
        public  void TestMethod1()
        {
            WeatherRepositoryDevelopment cp = new WeatherRepositoryDevelopment();
            var list =  cp.CityDataList();
            Assert.IsNotNull(list);
        }
    }
}
