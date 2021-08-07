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

            try
            {
                
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                var timer = new Timer(e => SQLTable.ReplyAlertAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
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
