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
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var createtable = new NpgsqlCommand(
               $"CREATE TABLE IF NOT EXISTS  {tableName}({command})", connection);
            connection.Open();
            createtable.ExecuteNonQuery();
            connection.Close();

            
            

        }

        public static void InsertIntoTable(string command)
        {
            var connection = new NpgsqlConnection("Server=localhost;Port=5432;User Id=postgres;Password=1234;Database=weather");
            var insert = new NpgsqlCommand(command, connection);
            connection.Open();
            insert.ExecuteNonQuery();
            connection.Close();
            
        }

        //public static string AllValues(params string[] subjects)
        //{
        //    System.Text.StringBuilder builder = new System.Text.StringBuilder();
        //    for (int i = 0; i < subjects.Length; i++)
        //    {
        //        builder.Append(subjects[i]);
        //        builder.Append(",");
        //    }
        //    return builder.ToString().Remove(builder.Length - 1); ;
        //}

    }
}
