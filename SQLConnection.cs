using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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

        public static void SQLCommand(string command)
        {
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand(command, connection);
            connection.Open();
            insert.ExecuteNonQuery();
            connection.Close();
            
        }

        public static void SQLSelect(string command,  out double lat, out double lon, out string city)
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

        public static async void ReplyAlertAsync()
        {
            List<DateTime> timeAlerts = new List<DateTime>();

            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand("SELECT time FROM public.client_data WHERE time IS NOT NULL;", connection);
            connection.Open();
            var reader = insert.ExecuteReader();
            while (reader.Read())
            {
                if(reader.GetDateTime(0).ToString("HH\\:mm") == DateTime.Now.ToString("HH\\:mm")) timeAlerts.Add(reader.GetDateTime(0));

            }
            connection.Close();


            foreach (DateTime item in timeAlerts)
            {
                var getValues = new NpgsqlCommand($"SELECT client_id, city, lat, lon FROM public.client_data WHERE time = '{item.ToShortTimeString()}';", connection);
                connection.Open();
                reader = getValues.ExecuteReader();
                //var secondReader = getValues.ExecuteReader();
                while (reader.Read())
                {
                    ClientId = reader.GetInt32(0);
                    City = reader.GetString(1);
                    Lat = reader.GetDouble(2);
                    Lon = reader.GetDouble(3);
                    var bot = new Telegram.Bot.TelegramBotClient("1494571167:AAFF0_riKPybc-uMirRBZtJEGSR8OM-BThE");
                    var reply = await bot.SendTextMessageAsync(ClientId, Weather.ForecastCurrent(Lat, Lon, City));
                }
                connection.Close();

            }


        }


    }
}
