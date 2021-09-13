using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    /// <summary>
    /// MemeChannel group, regroups every command that are related to the assignement or the getter of the meme channel 
    /// </summary>
    [Group("MemeChannel")]
    public class MemeChannelModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Set command for the meme channel
        /// Command is only accessible to user with admin permission
        /// </summary>
        /// <param name="channel">the new channel to assign as Meme Channel</param>
        /// <returns></returns>
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetMemeChannel([Remainder] SocketTextChannel channel)
        {
            //Stores the new channel as meme channel
            BotProperties.SetMemeChannel(channel);
            await ReplyAsync("✅ Successfully set " + channel.Name + " as meme channel !");
            
        }
        
        /// <summary>
        /// Get command for the meme channel
        /// Command is only accessible to user with admin permission
        /// </summary>
        /// <returns></returns>
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