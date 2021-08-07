using Newtonsoft.Json;
using Npgsql;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

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
                
                while (true)
                {
                    SQLTable.ReplyAlertAsync();
                    Thread.Sleep(10000);
                }

                //double lat;
                //double lon;
                //string city;
                //SQLTable.SQLSelect("SELECT lat, lon, city FROM public.client_data WHERE client_id = 730132317;", out lat, out lon, out city);
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


                //SQLTable.SQLCommand( $"CREATE TABLE IF NOT EXISTS  client_data (id SERIAL PRIMARY KEY, client_id INT UNIQUE, city VARCHAR(255), lat double precision, lon double precision, time TIME WITH TIME ZONE)");
                

                //SQLTable.CreateTable("city_data", "id SERIAL PRIMARY KEY, city VARCHAR(255), lan REAL, lon REAL");

                //SQLTable.InsertIntoTable("clientData", "chatId, city, time", "5656, 'krksf', '04:30'");
                //SQLTable.InsertIntoTable($"INSERT INTO client_data (client_id, city, time) VALUES (1234, 'kek', '10:40') ON CONFLICT(client_id) DO UPDATE SET city =  'pek', time = '10:12';");
                

                

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
