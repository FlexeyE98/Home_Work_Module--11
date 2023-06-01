using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTestBot.Models;

namespace MyTestBot.Services
{
    public interface IStorage
    {
        Sessions GetSession(long chatId);
    }
}
