using Newtonsoft.Json;
using Npgsql;
using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace Weather_bot
{
    class Program
    {
       
        static void Main(string[] args)
        {
            //var alertTime = inputAlertTime();
            //DateTime inputAlertTime()
            //{
            //     // date of birth
            //    string input;

            //    do
            //    {
            //        Console.WriteLine("Введите дату рождения в формате HH.mm дд.ММ.гггг (день.месяц.год):");
            //        input = Console.ReadLine();
            //    }
            //    while (!DateTime.TryParseExact(input, "HH.mm", null, DateTimeStyles.None, out alertTime));

            //    return alertTime; //16.01.2021 12:35:00
            //}
            //DateTime time = new DateTime();

            //Console.WriteLine("t: {0:t}", now);


            //DateTime date1 = new DateTime(0001, 01, 02, 00, 00, 00); // 20.07.2015 18:30:25

            //Console.WriteLine(date1.AddHours(-19).ToShortTimeString());


            try
            {

                Console.WriteLine(Weather.ForecastWeek(33.6, 33.7, "Кекерово", DateTime.Now) ); 

                

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                Console.WriteLine(CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek)); 
                //GeoData.DetectedCity("Каменск-Уральский", out double lan, out double lon);
                //Console.WriteLine(lan);
                //создаём таблицы в БД

                //SQLTable.CreateTable("client_data", "id SERIAL PRIMARY KEY, client_id INT UNIQUE, city VARCHAR(255), time TIME WITH TIME ZONE");
                //SQLTable.CreateTable("city_data", "id SERIAL PRIMARY KEY, city VARCHAR(255), lan REAL, lon REAL");

                //SQLTable.InsertIntoTable("clientData", "chatId, city, time", "5656, 'krksf', '04:30'");
                //SQLTable.InsertIntoTable($"INSERT INTO client_data (client_id, city, time) VALUES (1234, 'kek', '10:40') ON CONFLICT(client_id) DO UPDATE SET city =  'pek', time = '10:12';");
                Weather.ForecastTwoDays(33, 94, "Кемерово"); 
                string kek=Weather.ForecastCurrent(33,94, "Кемерово");

                //DataProcessing.ProccessingCurrent("b6ca72eadf23bc62d6855fc7d8e19d34");

                TelegramBotAutoReply newreply = new TelegramBotAutoReply(token: "1494571167:AAFF0_riKPybc-uMirRBZtJEGSR8OM-BThE");
                newreply.Updates();

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

       
    }
}
