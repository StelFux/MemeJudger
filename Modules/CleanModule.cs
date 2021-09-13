using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace MemeJudger.Modules
{
    [Group("clean")]
    public class CleanModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Standard action, that is called when user is not fulfilling complete command
        /// </summary>
        /// <returns></returns>
        [Command]
        public async Task DefaultAction()
        {
            Embed embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                Title = "Clean:",
                Color = Color.Green,
                Description = "message [n]: \t cleans the last n numbers of the current channel. \n\n"

            }.Build();

            await ReplyAsync(embed: embed);
        }
        
        /// <summary>
        /// Cleans x messages before the activated command 
        /// Ex: clean messages 15
        /// Command is only accessible to user with admin permission
        /// </summary>
        /// <param name="amount">the amount of messages to delete in the current channel</param>
        /// <returns></returns>
        [Command("messages")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CleanAsync(int amount)
        {
            if (amount <= 0)
            {
                await ReplyAsync("🛑 The amount of messages to remove must be positive.");
                return;
            }

            // Download X messages starting from Context.Message, which means
            // that it won't delete the message used to invoke this command.
            var messages = await Context.Channel.GetMessagesAsync(Context.Message, Direction.Before, amount)
                .FlattenAsync();

            // Ensure that the messages aren't older than 14 days,
            // because trying to bulk delete messages older than that
            // will result in a bad request.
            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);

            // Get the total amount of messages.
            var count = filteredMessages.Count();

            // Check if there are any messages to delete.
            if (count == 0)
                await ReplyAsync("🛑 Nothing to delete.");

            else
            {
                // The cast here isn't needed if you're using Discord.Net 1.x,
                // but I'd recommend leaving it as it's what's required on 2.x, so
                // if you decide to update you won't have to change this line.
                await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);
                await ReplyAsync($"♻️ Done. Removed {count} {(count > 1 ? "messages" : "message")}.");
            }
        }
    }
}