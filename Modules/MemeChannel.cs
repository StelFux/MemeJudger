using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    [Group("MemeChannel")]
    public class MemeChannelModule : ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetMemeChannel([Remainder] SocketTextChannel channel)
        {
            BotProperties.SetMemeChannel(channel);
            await ReplyAsync("✅ Successfully set " + channel.Name + " as meme channel !");
            
        }

        [Command("get")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetMemeChannel()
        {
            await ReplyAsync("The current Meme channel is following: #" + BotProperties.GetMemeChannel().Name);
        }

        [Command()]
        public async Task DefaultAction()
        {
            Embed embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                Title = "Meme Channel",
                Color = Color.Orange,
                Description =
                    "set [#channelName]: \t select the channel where the bot should judge the users who post content. \n\n" +
                    "get: \t get the current Channel where Pepe judges the memes."

            }.Build();

            await ReplyAsync(embed: embed);
        }
    }
}