using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using SimpleRestAPI.Models;

namespace SimpleRestAPI.Repository
{
    public class ColorRepository
    {
        static string txtPath = Path.Combine(Environment.CurrentDirectory, "Colors.txt");
        public ColorRepository()
        {
            
        }

        public async Task<List<ColorData>> GenerateModel()
        {
            string[] colorCode = await File.ReadAllLinesAsync(txtPath);
            List<ColorData> cdList = new List<ColorData>();
            for (int i = 0; i < colorCode.Length; i++)
            {

                string[] arr = colorCode[i].Split(':');
                ColorData cd = new ColorData();
                cd.Hex = arr[0];
                cd.Name = arr[1];
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
