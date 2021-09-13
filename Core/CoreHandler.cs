using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Services;

namespace MemeJudger.Core
{
    /// <summary>
    /// The Handler that is the core of the bot: centralize all other handlers and set up the necessary
    /// sockets and services so the other handlers can work fine
    /// </summary>
    public class CoreHandler
    {
        //the discord client, that is attached to the bot in order to perform action with the discord api
        private DiscordSocketClient _client;
        
        //the service, that the Command Handler needs in order to aggregate each command module to it.
        private CommandService _commands;
        
        //the token of the bot
        private string token;

        public CoreHandler()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();
            token = Environment.GetEnvironmentVariable("TOKEN");

            if (token == null)
            {
               throw new ArgumentNullException("$TOKEN","Please use $TOKEN for set token.");
            }
        }
        
        public async Task Main()
        {
            //we attach to the logging event our own custom logging service
            _client.Log += Logging.Log;
            
            //we connnect to the discord api
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            //information about the status of the bot displayed on stdout
            _client.Ready += () =>
            {
                Console.WriteLine("Bot is connected !");
                return Task.CompletedTask;
            };

            //setting up every other handlers...
            CommandHandler cmdHandler = new CommandHandler(_client, _commands);
            JudgingHandler jdgHandler = new JudgingHandler(_client);
            
            await cmdHandler.InstallCommands();
            await jdgHandler.InstallJudging();
            
            //we want the core Handler to be running ad vidam eternam for the moment
            await Task.Delay(-1);
        }
    }
}
