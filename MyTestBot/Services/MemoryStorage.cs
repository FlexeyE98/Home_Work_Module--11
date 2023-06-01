using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using MyTestBot.Models;

namespace MyTestBot.Services
{
    public class MemoryStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Sessions> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Sessions>();
        }

        public Sessions GetSession(long chatId)
        {
            
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            
            var newSession = new Sessions() { Functional = "length" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }

    }
}
