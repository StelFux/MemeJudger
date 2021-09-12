using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;

namespace MemeJudger.Services
{
    public static class Logging
    {
        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}