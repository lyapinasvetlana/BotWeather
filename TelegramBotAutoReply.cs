using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

namespace Weather_bot
{
    internal class TelegramBotAutoReply

    {


        Telegram.Bot.TelegramBotClient ourClient;

        private readonly string token;
        public TelegramBotAutoReply(string token)
        {
            this.token = token;
        }

        //поля для города
        public double lon;
        public double lat;
        public int utc;
        public string city;

        //текст всех кнопок
        private const string button0 = "Текущая погода";
        private const string button1 = "Выбрать/Поменять город";
        private const string button2 = "Прогноз на 2 дня";
        private const string button3 = "Прогноз на 7 дней";
        private const string button4 = "Уведомлять о погоде";

        private const string button5 = "Москва";
        private const string button6 = "Санкт-Петербург";
        private const string button7 = "Екатеринбург";
        private const string button8 = "Написать другой";
        private const string button9 = "Вернуться в меню";
  
        private const string button10 = "08:00";
        private const string button11 = "12:00";
        private const string button12 = "17:00";
        private const string button13 = "Написать время";
        private const string button14 = "Вернуться в начало";

       

       //получение обновлений от Телеграма
        internal void Updates()
        {
            ourClient = new Telegram.Bot.TelegramBotClient(token);
            var informationBot = ourClient.GetMeAsync().Result;
            if(informationBot!=null && !string.IsNullOrEmpty(informationBot.Username) )
            {
                int offset = 0;
                while (true)
                {
                    try
                    {
                       
                        var updates = ourClient.GetUpdatesAsync(offset).Result;

                        if(updates!=null && updates.Count() > 0)
                        {
                            foreach (var item in updates)
                            {
                                Updating(item);
                                offset = item.Id + 1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message); ;
                    }
                    Thread.Sleep(2000);
                }
            }
        }

        private void Updating(Telegram.Bot.Types.Update update)
        {
            ClientData user = new ClientData(update.Message.Chat.Id);
            SQLTable.SQLSelectStatusClient(out user.statusChoiceCity, out user.statusChoiceTime, user.Id);

            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    
                    var text = update.Message.Text; 
                    
                    if (user.statusChoiceCity == 1 && text!= "/start" && text != "Вернуться в меню")
                    {
                        GeoData.DetectedCity(text, out lat, out lon, out utc);
                        SetCity(text, user.Id);
                        break;
                    } 


                    if (user.statusChoiceTime == true && text!= "/start" && text != "Вернуться в начало")
                    {
                        SetTimeAlert(text, user.Id);
                        break;
                    }
                    

                    switch (text)
                    {
                        case button0:
                            if (user.statusChoiceCity == 2)
                            {
                                SQLTable.SQLSelectCity($"SELECT lat, lon, city FROM public.client_data WHERE client_id = {user.Id};", out lat, out lon, out city);
                                ourClient.SendTextMessageAsync(user.Id, Weather.ForecastCurrent(lat, lon, city), replyMarkup: GetButtons());
                            }
                            else
                            {
                                NeedChooseCity(user.Id);
                            }
                            break;

                        case button1:
                            ourClient.SendTextMessageAsync(user.Id, "Выберите кнопку: написать другой, если не видите город среди представленных!", replyMarkup: GetButtons2());
                            break;

                        case button2:
                            if (user.statusChoiceCity == 2)
                            {
                                SQLTable.SQLSelectCity($"SELECT lat, lon, city FROM public.client_data WHERE client_id = {update.Message.Chat.Id};", out lat, out lon, out city);
                                ourClient.SendTextMessageAsync(user.Id, Weather.ForecastTwoDays(lat, lon, city), replyMarkup: GetButtons());
                            }
                            else
                            {
                                ourClient.SendTextMessageAsync(user.Id, "Выберите сначала город", replyMarkup: GetButtons());
                            }
                            break;
                        case button3:
                            if (user.statusChoiceCity == 2)
                            {
                                SQLTable.SQLSelectCity($"SELECT lat, lon, city FROM public.client_data WHERE client_id = {update.Message.Chat.Id};", out lat, out lon, out city);
                                ourClient.SendTextMessageAsync(user.Id, Weather.ForecastWeek(lat, lon, city, DateTime.Now.AddHours(utc - 3)), replyMarkup: GetButtons());
                            }
                            else
                            {
                                NeedChooseCity(user.Id);
                            }
                            break;
                        case button4:
                            if (user.statusChoiceCity == 2)
                            {
                                ourClient.SendTextMessageAsync(user.Id, "Можете выбирать время", replyMarkup: GetButtons4());
                            }
                            else
                            {
                                NeedChooseCity(user.Id);
                            }
                            break;

                        case button5:
                        case button6:
                        case button7:
                            GeoData.DetectedCity(text, out lat, out lon, out utc);
                            SetCity(text, user.Id);
                            break;

                        case button8:
                            SQLTable.SQLCommand($"UPDATE client_data SET status_choice_city = 1 WHERE client_id = {user.Id};");
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, "Напишите название города России", replyMarkup: GetButtons2());
                            break;

                        case button10:
                        case button11:
                        case button12:
                            if (user.statusChoiceCity == 2)
                            {
                                SetTimeAlert(text, user.Id);
                            }
                            else
                            {
                                NeedChooseCity(user.Id);
                            }
                            break;

                        case button13:
                            SQLTable.SQLCommand($"UPDATE client_data SET status_choice_time = TRUE WHERE client_id = {user.Id};");
                            ourClient.SendTextMessageAsync(user.Id, "Введите время в формате hh.mm (например, 15:42)", replyMarkup: GetButtons5());
                            break;
                        case "/stop":
                            SQLTable.SQLCommand($"DELETE FROM client_data WHERE client_id = {user.Id};");
                            ourClient.SendTextMessageAsync(user.Id, "Вы отписались от уведомлений о погоде!", replyMarkup: GetButtons());
                            break;

                        default:
                            ourClient.SendTextMessageAsync(user.Id, "Вас приветствует чат-бот погоды в России!\nВы можете подписаться на уведомления, которые будут приходить в нужное вам время, или просто узнать прогноз на ближайшие 2 дня или неделю.\n" +
                                "Для начала вам нужно выбрать город, если вы здесь впервые! Если вы не хотите больше получать уведомлений: /stop", replyMarkup: GetButtons());
                            
                            break;
                    }

                    break;
                
                default:
                    Console.WriteLine(update.Type + " необрабатываемый тип!");
                    break;
            }
        }

        

       




        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = button1 }, new KeyboardButton { Text = button2 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button3 }, new KeyboardButton { Text = button4 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button0 }

                    }
                }
            };
        }

        private IReplyMarkup GetButtons2()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = button5 }, new KeyboardButton { Text = button6 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button7 }, new KeyboardButton { Text = button8 }
                    }
                }
            };
        }

        private IReplyMarkup GetButtons3()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = button5 }, new KeyboardButton { Text = button6 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button7 }, new KeyboardButton { Text = button9 }
                    }
                }
            };
        }

        private IReplyMarkup GetButtons4()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = button10 }, new KeyboardButton { Text = button11 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button12 }, new KeyboardButton { Text = button13 }
                    }
                }
            };
        }


        private IReplyMarkup GetButtons5()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> { new KeyboardButton { Text = button10 }, new KeyboardButton { Text = button11 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button12 }, new KeyboardButton { Text = button14 }
                    }
                }
            };
        }



        private void SetTimeAlert(string text, long id)
        {
            if (DateTime.TryParseExact(text, "HH:mm", null, DateTimeStyles.None, out DateTime alertTime))
            {
                ourClient.SendTextMessageAsync(id, $"Теперь вы будете получать уведомления в {text}.", replyMarkup: GetButtons());
                SQLTable.SQLCommand($"UPDATE client_data SET time = '{alertTime.AddHours(-utc + 3).ToShortTimeString()}', status_choice_time = FALSE WHERE client_id = {id};");
            }
            else
            {
                ourClient.SendTextMessageAsync(id, "Введите время в формате hh.mm (например, 15:42) или вернитесь к главному меню: /start!", replyMarkup: GetButtons5());
            }
        }

        private void SetCity(string text, long id)
        {
            if (lat == 0)
            {
                ourClient.SendTextMessageAsync(id, "К сожалению мы не нашли такого города, попробуйте ввести снова или вернитесь к главному меню: /start!", replyMarkup: GetButtons3());
            }
            else
            {
                SQLTable.SQLCommand($"UPDATE client_data SET status_choice_city = 2 WHERE client_id = {id};");
                ourClient.SendTextMessageAsync(id, $"Вы выбрали город {text}\nТеперь вы можете подписаться на уведомления о погоде или посмотреть прогноз.", replyMarkup: GetButtons());
                SQLTable.SQLCommand($"INSERT INTO client_data (client_id, city, lat, lon) VALUES ({id}, '{text}', {lat.ToString(CultureInfo.InvariantCulture)}, {lon.ToString(CultureInfo.InvariantCulture)}) ON CONFLICT(client_id) DO UPDATE SET city =  '{text}', lat = {lat.ToString(CultureInfo.InvariantCulture)}, lon = {lon.ToString(CultureInfo.InvariantCulture)};");
            }
        }

        private void NeedChooseCity(long id)
        {
            ourClient.SendTextMessageAsync(id, "Выберите сначала город", replyMarkup: GetButtons());
        }
    }
}