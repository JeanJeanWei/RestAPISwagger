using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using SimpleRestAPI.Models;

namespace SimpleRestAPI.Repository
{
    public class ColorRepository
    {
        static string txtPath = Path.Combine(Environment.CurrentDirectory, "Colors.txt");
        public ColorRepository()
        {
            
        }

        public List<ColorData> GenerateModel()
        {
            string[] colorCode = File.ReadAllLines(txtPath);
            List<ColorData> cdList = new List<ColorData>();
            for (int i = 0; i < colorCode.Length; i++)
            {

                string[] arr = colorCode[i].Split(':');
                ColorData cd = new ColorData();
                cd.Hex = arr[0].Trim();
                cd.Name = arr[1].Trim();
                int argb = int.Parse(cd.Hex, NumberStyles.HexNumber);
                Color clr = Color.FromArgb(argb);
                cd.R = clr.R;
                cd.G = clr.G;
                cd.B = clr.B;
                cdList.Add(cd);
            }
            return cdList;
        }
    }
}
