using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Telegram.Bot.Types.ReplyMarkups;

namespace Weather_bot
{
    internal class TelegramBotAutoReply
    {
        public double lon;
        public double lan;
        public bool status=false;
        
        //текст кнопок начальных
        private const string button1 = "Выбрать/Поменять город";
        private const string button2 = "Прогноз на 2 дня";
        private const string button3 = "Прогноз на 7 дней";
        private const string button4 = "Уведомлять о погоде";


        //города-топ4
        private const string button5 = "Москва";
        private const string button6 = "Санкт-Петербург";
        private const string button7 = "Екатеринбург";
        private const string button8 = "Написать другой";
        private const string button9 = "Вернуться в меню";
        //текст кнопок

        private string token { get; set; }

        Telegram.Bot.TelegramBotClient ourClient;


        public TelegramBotAutoReply(string token)
        {
            this.token = token;
        }

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
            switch (update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    
                    var text = update.Message.Text; //получаем текст от клиента
                    
                    if (status==true && text!= "/start" && text != "Вернуться в меню")
                    {
                        GeoData.DetectedCity(text, out lan, out lon);
                        if (lan == 0)
                        {
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, "К сожалению мы не нашли такого города, попробуйте ввести снова или вернитесь к главному меню: /start!", replyMarkup: GetButtons3());
                        }
                        else
                        {
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы выбрали город {text}, теперь выберите время", replyMarkup: GetButtons());
                            status = false;
                        }
                        break;
                    } 

                    //обрабатываем текст
                    switch (text)
                    {
                       
                        case button1:
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, "Выберите кнопку: написать другой, если не видите город среди представленных!", replyMarkup: GetButtons2());
                            
                            //switch (status)
                            //{
                            //    case true:
                            //        ourClient.SendTextMessageAsync(update.Message.Chat.Id, "Теперь выберите время, когда вам будут приходить уведомления", replyMarkup: GetButtons());
                            //        break;
                            //    case false:
                            //        break;

                            //}
                           
                            break;
                        case button2:
                            break;
                        case button3:
                            break;
                        case button4:
                            break;
                        case button5:
                        case button6:
                        case button7:
                            GeoData.DetectedCity(text, out lan, out lon);
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы выбрали город {text}, теперь выберите время", replyMarkup: GetButtons());
                            break;
                        case button8:
                            status = true;
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, "Напишите название города России", replyMarkup: GetButtons2());
                            break;
                        
                        default:
                            status = false;
                            ourClient.SendTextMessageAsync(update.Message.Chat.Id, "Вас приветствует чат-бот погоды в России!\nВы можете подписаться на уведомления, которые будут приходить в нужное вам время, или просто узнать прогноз на ближайшие 2 дня или неделю.\n" +
                                "Для начала вам нужно выбрать город, если вы здесь впервые!", replyMarkup: GetButtons());
                            
                            break;
                    }

                    //if (update.Message.Voice != null)
                    //{
                    //    ourClient.SendTextMessageAsync(update.Message.Chat.Id, "К сожалению бот приболел и плохо слышит, напишите ему в чат!");
                        
                    //}
                    //if(text!= null)
                    //{
                    //    //ourClient.SendTextMessageAsync(update.Message.Chat.Id, $"Вы выбрали город - {text} для уведомлений о погоде!", replyMarkup:GetButtons());
                    //}

                    break;
                
                default:
                    Console.WriteLine(update.Type + " необрабатываемый тип!");
                    break;
            }
        }

        

        private IReplyMarkup GetButtons2()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> {new KeyboardButton { Text = button5 }, new KeyboardButton { Text = button6 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button7 }, new KeyboardButton { Text = button8 }
                    }
                }
            };
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton> {new KeyboardButton { Text = button1 }, new KeyboardButton { Text = button2 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button3 }, new KeyboardButton { Text = button4 }
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
                    new List<KeyboardButton> {new KeyboardButton { Text = button5 }, new KeyboardButton { Text = button6 } },
                    new List<KeyboardButton> { new KeyboardButton { Text = button7 }, new KeyboardButton { Text = button9 }
                    }
                }
            };
        }
    }
}