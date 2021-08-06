using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Npgsql;

namespace Weather_bot
{
    public class SQLTable
    {
       
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

        public static void SQLReplyToAlert(out double lat, out double lon, out string city, out int clientId)
        {
            clientId=0;
            city = null;
            lat = 0;
            lon = 0;
            List<DateTime> numbers = new List<DateTime>();

            DateTime time = new DateTime();
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand("SELECT time FROM public.client_data WHERE time IS NOT NULL;", connection);
            connection.Open();
            var reader = insert.ExecuteReader();
            
            while (reader.Read())
            {
                time = reader.GetDateTime(0);

                if (time.ToString("HH\\:mm") == DateTime.Now.ToString("HH\\:mm"))
                {
                    connection.Close();
                    var getValues = new NpgsqlCommand($"SELECT client_id, city, lat, lon FROM public.client_data WHERE time = '11:47';", connection);
                    //var getValues = new NpgsqlCommand($"SELECT client_id, city, lat, lon FROM public.client_data WHERE time = '{time.ToShortTimeString()}';", connection);
                    connection.Open();
                    var secondReader= getValues.ExecuteReader();
                    while (secondReader.Read())
                    {
                        clientId= secondReader.GetInt32(0);
                        city = secondReader.GetString(1);
                        lat = secondReader.GetDouble(2);
                        lon = secondReader.GetDouble(3);
                        

                    }
                    
                }
                

            }
            connection.Close();

        }


    }
}
