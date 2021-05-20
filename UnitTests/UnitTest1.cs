using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRestAPI.Models;
using SimpleRestAPI.Repository;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ColorRepository cp = new ColorRepository();
            List<ColorData> list = cp.GenerateModel();
        }
    }
}
