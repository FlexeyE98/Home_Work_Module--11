using MyTestBot.Models;
using MyTestBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyTestBot.Controllers
{
    public class TextMessageController
    {
        private readonly IFunctionalHandler _functionalHandler;
        private readonly ITelegramBotClient _telegramClient;
        private readonly InlineKeyboardController _keyboardController;
        private readonly Sessions _sessions;
        private readonly IStorage _storage;



        public TextMessageController(ITelegramBotClient telegramBotClient, IFunctionalHandler functionalHandler, IStorage storage)
        {
            _telegramClient = telegramBotClient;
            _functionalHandler = functionalHandler;
            _storage = storage;

        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение {message.Text}");
            if (message.Text == "/start")
            {
                var buttons = new List<InlineKeyboardButton[]>();
                buttons.Add(new[]
              {
                        InlineKeyboardButton.WithCallbackData($" Кол-во символов", $"length"),
                        InlineKeyboardButton.WithCallbackData($" Вычисление суммы чисел", $"sum")
                    });
                await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>Этот бот умеет считать за вас количество символов в предложении и суммировать введенные числа.</b> {Environment.NewLine}" +
                 $"{Environment.NewLine}Можете обращаться к нему, если нужно подсчитать быстренько сумму чисел либо сумму символов.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));
            }

            if (message.Text != "/start")
            {
                var test = _storage.GetSession(message.Chat.Id).Functional;
                if (test == null)
                {
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду (меню - /start)");
                    return;
                }
                if (test == "length")
                {
                    await _functionalHandler.StringCounterr(message, message.Text, ct);
                }

                if (test == "sum")
                {
                    await _functionalHandler.Counter(message, message.Text, ct);

                }
            }
        }
    }
}











