using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace MemeJudger.Core
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _client = client;
            _commands = commands;
        }

        public async Task InstallCommands()
        {
            _client.MessageReceived += HandleCommand;

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommand(SocketMessage messageParam)
        {
            SocketUserMessage message = messageParam as SocketUserMessage;
            
            if (message == null)
                return;

            int argPos = 0;

            if (!(message.HasCharPrefix('$', ref argPos) ||
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;

            SocketCommandContext context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);
        }
    }
}