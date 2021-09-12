using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Services;

namespace MemeJudger.Core
{
    public class CoreHandler
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private string token = "";

        public CoreHandler()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
        }
        
        public async Task Main()
        {
            _client.Log += Logging.Log;
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected !");
                return Task.CompletedTask;
            };


            CommandHandler cmdHandler = new CommandHandler(_client, _commands);
            JudgingHandler jdgHandler = new JudgingHandler(_client);
            
            await cmdHandler.InstallCommands();
            await jdgHandler.InstallJudging();
            
            
            await Task.Delay(-1);
        }
    }
}