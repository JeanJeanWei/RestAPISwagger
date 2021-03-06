using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleRestAPI.Models;
using SimpleRestAPI.Repository;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        static string txtPath = Path.Combine(Environment.CurrentDirectory, "Colors.txt");

        [TestMethod]
        public async void TestMethod1()
        {
            ColorRepository cp = new ColorRepository();
            List<ColorData> list = await cp.GetSource();
        }

        [TestMethod]
        public void TestWriteToFile()
        {

            string[] colorCode = File.ReadAllLines(txtPath);
            for (int i = 0; i < colorCode.Length; i++)
            {

                string[] arr = colorCode[i].Split(':');
                string[] line = { arr[0].Trim()+ ":"+ arr[1].Trim() };

                 File.AppendAllLines("WriteLines.txt", line);

            }
            
        }
    }
}
