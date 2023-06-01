using MyTestBot.Configuration;
using MyTestBot.Services;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace MyTestBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly AppSettings _appSettings;
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly TextMessageController _textMessageController;
        private readonly IFunctionalHandler _functionalHandler;


        public InlineKeyboardController(IFunctionalHandler functionalHandler, TextMessageController textMessageController, AppSettings appSettings, ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _appSettings = appSettings;
            _textMessageController = textMessageController;
            _functionalHandler = functionalHandler;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} обнаружил нажатие на кнопку {callbackQuery.Data}");

            if (callbackQuery?.Data == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).Functional = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                "length" => "Кол-во символов",
                "sum" => " Вычисление суммы чисел",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
               $"<b>Режим: - \n{languageText}.{Environment.NewLine}</b>\nВведите текст для подсчета символов", cancellationToken: ct, parseMode: ParseMode.Html);

        }





    }




}


