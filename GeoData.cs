using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Weather_bot
{
    public class GeoData
    {
        //параметры выбранного города
        public string CityName { get; set; }
        public int TimeZone { get; set; }
        public double Lat { get; set; } 
        public double Lon { get; set; } 

        public static GeoData ParseFideCsv(string line)
        {
            string[] parts = line.Split(',');
            var parsed = new GeoData
            {
                CityName = parts[6],
                TimeZone = int.Parse(parts[16].Split('C')[1]),
                Lat = double.Parse(parts[17], System.Globalization.CultureInfo.InvariantCulture),
                Lon = double.Parse(parts[18], System.Globalization.CultureInfo.InvariantCulture)
            };

            return parsed;
        }
        public static void DetectedCity(string cityName, out double outputlat, out double outputlon, out int outputTimeZone)
        {
            outputTimeZone = 0;
            outputlon = 0;
            outputlat = 0;

            IEnumerable<GeoData> list = File.ReadAllLines("cities.csv", Encoding.GetEncoding(1251))
                                         .Skip(1)
                                         .Select(GeoData.ParseFideCsv);
            try
            {
                var geo = list.FirstOrDefault(city => city.CityName == cityName);
                outputlat = geo.Lat;
                outputlon = geo.Lon;
                outputTimeZone = geo.TimeZone;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
    
}
