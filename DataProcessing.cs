using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Weather_bot
{
    
    public static class DataProcessing
    {
        
        public static void ProccessingCurrent(string api)
        {
            string cityName = Console.ReadLine();
            var urlСurrentWeather = $"http://api.openweathermap.org/data/2.5/weather?q={cityName}&&units=metric&lang=ru&appid={api}";
            var urlForecastTwoDays = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={cityName}&&units=metric&lang=ru&appid=b6ca72eadf23bc62d6855fc7d8e19d34";
            //var key = "b6ca72eadf23bc62d6855fc7d8e19d34";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlСurrentWeather);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //надо считать данные с ответа

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            Console.WriteLine($"Температура в городе {weatherInfo.Name}: {weatherInfo.Main.Temp}°, скорость ветра: {weatherInfo.Wind.Speed} м/с, {weatherInfo.Weather[0].Description}");
        }
        public static void ProccessingForecastToday(string api)
        {
            string cityName = Console.ReadLine();

            var urlForecastToday = $"http://api.openweathermap.org/data/2.5/forecast/hourly?q={cityName}&cnt=1&appid={api}";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastToday);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //надо считать данные с ответа

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            Console.WriteLine($"Температура в городе {weatherInfo.Name}: {weatherInfo.Main.Temp}°, скорость ветра: {weatherInfo.Wind.Speed} м/с, {weatherInfo.Weather[0].Description}");
        }
        public static void ProccessingForecastTwoDays(string api)
        {
            string cityName = Console.ReadLine();
            
            var urlForecastTwoDays = $"http://api.openweathermap.org/data/2.5/onecall?q={cityName}&exclude=hourly,daily&units=metric&lang=ru&&appid={api}";
            
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastTwoDays);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //надо считать данные с ответа

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            Console.WriteLine($"Температура в городе {weatherInfo.Name}: {weatherInfo.Main.Temp}°, скорость ветра: {weatherInfo.Wind.Speed} м/с, {weatherInfo.Weather[0].Description}");
        }
        public static void ProccessingForecastWeek(string api)
        {
            string cityName = Console.ReadLine();

            var urlForecastWeek = $"http://api.openweathermap.org/data/2.5/forecast/daily?q={cityName}&cnt=7&appid={api}";
            
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastWeek);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //надо считать данные с ответа

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(response);
            Console.WriteLine($"Температура в городе {weatherInfo.Name}: {weatherInfo.Main.Temp}°, скорость ветра: {weatherInfo.Wind.Speed} м/с, {weatherInfo.Weather[0].Description}");
        }
    }
}
