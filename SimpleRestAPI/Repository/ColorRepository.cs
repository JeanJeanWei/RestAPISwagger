using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using SimpleRestAPI.Models;

namespace SimpleRestAPI.Repository
{
    public class ColorRepository : IRepository<Task<List<ColorData>>>
    {
        string txtPath = Path.Combine(Environment.CurrentDirectory, "Colors.txt");
        List<ColorData> Colors;

        public ColorRepository()
        {
            
        }

        public async Task<List<ColorData>> GetDataSource()
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

        public async Task<List<ColorData>> AllColorData()
        {
            if (Colors == null)
            {
                Colors = await GetDataSource();
            }
            return Colors;
        }

        

        public async Task<ColorData> ClosestColorNameByHex(string hex)
        {   
            if (Colors == null)
            {
                Colors = await GetDataSource();
            }

            int argb = int.Parse(hex.Replace("#", ""), NumberStyles.HexNumber);
            Color a = Color.FromArgb(argb);
            ColorData result = new ColorData()
            {
                Distance = int.MaxValue
            };

            for (int i = 0; i < Colors.Count; i++)
            {
                var b = Colors[i];
                var d = Distance3D(a.R, a.G, a.B, b.R, b.G, b.B);
                
                if (d < result.Distance)
                {
                    result.Distance = d;
                    result.Name = b.Name;
                    result.Hex = b.Hex;
                    result.R = b.R;
                    result.G = b.G;
                    result.B = b.G;
                }
            }
            return result;
        }

        public int Distance3D(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            //     __________________________________
            //d = √ (x2-x1)^2 + (y2-y1)^2 + (z2-z1)^2
            //

            //Our end result
            int result;
            //Take x2-x1, then square it
            double part1 = Math.Pow((x2 - x1), 2);
            //Take y2-y1, then sqaure it
            double part2 = Math.Pow((y2 - y1), 2);
            //Take z2-z1, then square it
            double part3 = Math.Pow((z2 - z1), 2);
            //Add both of the parts together
            double underRadical = part1 + part2 + part3;
            //Get the square root of the parts
            result = (int)Math.Sqrt(underRadical);
            //Return our result
            return result;
        }

        
    }
}
