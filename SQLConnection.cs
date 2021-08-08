using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using Npgsql;
using Telegram.Bot.Types.ReplyMarkups;

namespace Weather_bot
{
    public class SQLTable
    {
        public static int ClientId { get; private set; }
        public static string City { get; private set; }
        public static double Lat { get; private set; }
        public static double Lon { get; private set; }

        public static DateTime time;

        public static void SQLCommand(string command)
        {
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand(command, connection);
            connection.Open();
            insert.ExecuteNonQuery();
            connection.Close();
            
        }

        public static void SQLSelectCity(string command,  out double lat, out double lon, out string city)
        {
            lat = 0;
            lon = 0;
            city = null;
           
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand(command, connection);
            connection.Open();
            var reader = insert.ExecuteReader();
           
            while (reader.Read())
            {
                lat = reader.GetDouble(0);
                lon = reader.GetDouble(1);
                city = reader.GetString(2);
            }
            connection.Close();

        }

        public static void SQLSelectStatusClient( out int statusCity, out bool statusTime, long id)
        {
            statusTime = false;
            statusCity = 0;

            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand($"SELECT status_choice_time, status_choice_city FROM public.client_data WHERE client_id = {id};", connection);
            connection.Open();
            var reader = insert.ExecuteReader();

            while (reader.Read())
            {
                statusTime = reader.GetBoolean(0);
                statusCity = reader.GetInt16(1);
                
            }
            connection.Close();

        }

        public static  async void ReplyAlertAsync()
        {
                if (DateTime.Now.Minute!=time.Minute)
                {
                    time = DateTime.Now;
                    var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
                    var getValues = new NpgsqlCommand($"SELECT client_id, city, lat, lon FROM public.client_data WHERE time = '{time.ToShortTimeString()}';", connection);
                    connection.Open();
                    var reader = getValues.ExecuteReader();
                    while (reader.Read())
                    {
                        ClientId = reader.GetInt32(0);
                        City = reader.GetString(1);
                        Lat = reader.GetDouble(2);
                        Lon = reader.GetDouble(3);
                        var bot = new Telegram.Bot.TelegramBotClient("1494571167:AAFF0_riKPybc-uMirRBZtJEGSR8OM-BThE");
                        await bot.SendTextMessageAsync(ClientId, Weather.ForecastCurrent(Lat, Lon, City));
                    }
                    connection.Close();
                    Thread.Sleep(2000);
                }
            
        }


    }
}
