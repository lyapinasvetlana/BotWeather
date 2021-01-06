using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Weather_bot
{
    public class GeoData
    {
        public string CityName { get; set; }
        public int TimeZone { get; set; }
        public double Lat { get; set; } //широта
        public double Lon { get; set; } //долгота


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
        public static void DetectedCity(string cityName, out double outputlat, out double outputlon)
        {
            outputlon = 0;
            outputlat = 0;

             var list6 = File.ReadAllLines("cities.csv", Encoding.GetEncoding(1251));
            IEnumerable<GeoData> list = File.ReadAllLines("cities.csv", Encoding.GetEncoding(1251))
                                         .Skip(1)
                                         .Select(GeoData.ParseFideCsv);
            try
            {
                outputlat = list.FirstOrDefault(city => city.CityName == cityName).Lat;
                outputlon = list.FirstOrDefault(city => city.CityName == cityName).Lon;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            
             
            

        }
        
        
    }
    
}
