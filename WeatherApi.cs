using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace Weather_bot
{
    
    public static class Weather
    {
      
        public static string ForecastCurrent(double lat, double lon, string city)
        {
            var urlForecastToday = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly,daily,alerts&units=metric&lang=ru&appid=76ac94e1bd857720b1cf1f3600228d81";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastToday);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
      
            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfoDeserializer weatherInfo = JsonConvert.DeserializeObject<WeatherInfoDeserializer>(response);
            return $"Температура в городе {city}: {Math.Round(weatherInfo.Current.Temp)}°C, скорость ветра: {Math.Round(weatherInfo.Current.Wind_speed)} м/с, {weatherInfo.Current.Weather[0].Description}.";
        }
        public static string ForecastTwoDays(double lat, double lon, string city)
        {
            
            var urlForecastToday = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly,current,alerts&units=metric&lang=ru&appid=76ac94e1bd857720b1cf1f3600228d81";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastToday);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }

            WeatherInfoTwoDays weatherInfo = JsonConvert.DeserializeObject<WeatherInfoTwoDays>(response);
            return $"Сегодня в городе {city} температура днём: {Math.Round(weatherInfo.Daily[0].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[0].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[0].Wind_speed)} м/с, {weatherInfo.Daily[0].Weather[0].Description}.\n" +
                $"Завтра температура днём: {Math.Round(weatherInfo.Daily[1].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[0].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[1].Wind_speed)} м/с, {weatherInfo.Daily[1].Weather[0].Description}.";
        }

        public static string ForecastWeek(double lat, double lon, string city, DateTime localTime)
        {

            var urlForecastToday = $"https://api.openweathermap.org/data/2.5/onecall?lat={lat}&lon={lon}&exclude=minutely,hourly,current,alerts&units=metric&lang=ru&appid=76ac94e1bd857720b1cf1f3600228d81";

            
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(urlForecastToday);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = streamReader.ReadToEnd();
            }
            //string dayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.DayOfWeek);
            //dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek[1..];

            WeatherInfoTwoDays weatherInfo = JsonConvert.DeserializeObject<WeatherInfoTwoDays>(response);
            return $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.DayOfWeek)[1..]}, в городе {city} температура днём: {Math.Round(weatherInfo.Daily[0].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[0].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[0].Wind_speed)} м/с, {weatherInfo.Daily[0].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(1).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(1).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[1].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[1].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[1].Wind_speed)} м/с, {weatherInfo.Daily[1].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(2).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(2).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[2].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[2].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[2].Wind_speed)} м/с, {weatherInfo.Daily[2].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(3).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(3).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[3].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[3].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[3].Wind_speed)} м/с, {weatherInfo.Daily[3].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(4).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(4).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[4].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[4].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[4].Wind_speed)} м/с, {weatherInfo.Daily[4].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(5).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(5).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[5].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[5].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[5].Wind_speed)} м/с, {weatherInfo.Daily[5].Weather[0].Description}.\n" +
                $"{char.ToUpper(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(6).DayOfWeek)[0]) + CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(localTime.AddDays(6).DayOfWeek)[1..]}, температура днём: {Math.Round(weatherInfo.Daily[6].Temp.Day)}°C, температура ночью: {Math.Round(weatherInfo.Daily[6].Temp.Night)}°C, скорость ветра: {Math.Round(weatherInfo.Daily[6].Wind_speed)} м/с, {weatherInfo.Daily[6].Weather[0].Description}.";
            

        }
        
    }
}
