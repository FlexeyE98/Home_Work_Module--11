using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace MyTestBot.Services
{
    public interface IFunctionalHandler
    {
        Task StringCounterr(Message mess, string message, CancellationToken ct);
        Task Counter(Message mess, string message, CancellationToken ct);

    }
}
