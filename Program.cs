using Newtonsoft.Json;
using Npgsql;
using System;
using System.IO;
using System.Net;

namespace Weather_bot
{
    class Program
    {
       
        static void Main(string[] args)
        {
            
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                //GeoData.DetectedCity("Каменск-Уральский", out double lan, out double lon);
                //Console.WriteLine(lan);
                //создаём таблицы в БД
                //SQLTable.CreateTable("clientData", "id SERIAL PRIMARY KEY, chatId INT, city VARCHAR(255), time1 time with time zone, time2 time with time zone, time3 time with time zone");
                //SQLTable.CreateTable("cityData", "id SERIAL PRIMARY KEY, cityName VARCHAR(255), lan REAL, lon REAL");


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
