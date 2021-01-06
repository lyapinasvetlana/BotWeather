using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Npgsql;

namespace Weather_bot
{
    public class SQLTable
    {
        
        public static void CreateTable(string tableName, string command)
        {
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=s8wt1ilm;Database=weather");
            var createtable = new NpgsqlCommand(
               $"CREATE TABLE IF NOT EXISTS  {tableName}({command})", connection);
            connection.Open();
            createtable.ExecuteNonQuery();
            connection.Close();

        }

    }
}
