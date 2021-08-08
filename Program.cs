using System;
using System.Threading;

namespace Weather_bot
{
    class Program
    {
    
        static void Main(string[] args)
        {
            try
            { 
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                //создание таблицы в базе
                SQLTable.SQLCommand($"CREATE TABLE IF NOT EXISTS  client_data (id SERIAL PRIMARY KEY, client_id INT UNIQUE, status_choice_time BOOLEAN DEFAULT FALSE, status_choice_city INT2 DEFAULT 0,  city VARCHAR(30), lat double precision, lon double precision, time TIME WITH TIME ZONE)");

                //отправка уведомлений пользователям
                var timer = new Timer(e => SQLTable.ReplyAlertAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
                //получение обновлений и автоответы
                TelegramBotAutoReply newSession = new TelegramBotAutoReply(token: "1494571167:AAFF0_riKPybc-uMirRBZtJEGSR8OM-BThE");
                newSession.Updates();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       
    }
}
