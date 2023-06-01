using MyTestBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyTestBot.Services
{
    public class TextCounterHamdler : IFunctionalHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;

        public TextCounterHamdler(ITelegramBotClient telegramBotClient, AppSettings appSettings)
        {
            _appSettings = appSettings;
            _telegramBotClient = telegramBotClient;


        }

        public async Task StringCounterr(Message mess, string message, CancellationToken ct)
        {

            {
                Console.WriteLine($"Длина сообщения: {message.Length}");
                await _telegramBotClient.SendTextMessageAsync(mess.Chat.Id, $"Длина сообщения: {message.Length}");

            }

        }

        public async Task Counter(Message mess, string message, CancellationToken ct)
        {
            List<int> intList = new List<int>();
            string temp = "";


            for (int i = 0; i < message.Length; i++)
            {

                temp += message[i];
                if (message[i].ToString().Contains(" "))
                {
                    bool result = int.TryParse(temp, out int intresult);
                    if (!result)
                    {
                        Console.WriteLine("Произошла ошибка конвертации");
                        await _telegramBotClient.SendTextMessageAsync(mess.Chat.Id, "Произошла ошибка, введено не число", cancellationToken: ct);
                        temp = "";
                    }
                    else
                    {
                        intList.Add(intresult);
                        temp = "";
                    }
                }
            }

            if (!message.EndsWith(" "))
            {
                bool result = int.TryParse(temp, out int intresult2);
                if (result == true)
                    intList.Add(intresult2);
                else
                {
                    Console.WriteLine("Произошла ошибка конвертации");
                    await _telegramBotClient.SendTextMessageAsync(mess.Chat.Id, "Произошла ошибка, введено не число", cancellationToken: ct);
                }
            }

            int counter = 0;
            foreach (var x in intList)
            {
                counter += x;

            }
            Console.WriteLine($"Сумма элементов равна: {counter}");

            if(counter != 0)
            await _telegramBotClient.SendTextMessageAsync(mess.Chat.Id, $"Сумма цифр равна: {counter}");

        }
    }
}
