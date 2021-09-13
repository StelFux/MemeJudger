using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Models;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    /// <summary>
    /// Upvote group, regroups every command related to the Upvote Emote
    /// </summary>
    [Group("Upvote")]
    public class UpvoteModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Standard action, that is called when user is not fulfilling complete command
        /// </summary>
        /// <returns></returns>
        [Command()]
        public async Task DefaultAction()
        {
            Embed embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                Title = "Upvote rules:",
                Color = Color.Orange,
                Description =
                    "set [emote]: \t set the given emote as default Upvote reaction button. Pepe the Judge will put those on every new post in the meme channel. \n\n" +
                    "get: \t get the current Upvote Reaction."

            }.Build();

            await ReplyAsync(embed: embed);
        }
        
        /// <summary>
        /// Set command for assigning a new emoji as Upvote reaction
        /// Ex: $Upvote set 👍
        /// </summary>
        /// <param name="emote">the new emote to assign as Upvote reaction</param>
        /// <returns></returns>
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Set([Remainder] string emote)
        {
            Reaction react;
            try
            {
                react = new Reaction(emote);

            }
            catch (Exception e)
            {
                await ReplyAsync("🛑 Error, reaction doesn't exist.");
                return;
            }
            
            BotProperties.SetUpvote(react);
            await ReplyAsync("✅ Successfully set " + react + " as Upvote reaction !");
        }
        
        /// <summary>
        /// Get command to see the current emoji assigned as Upvote reaction
        /// Ex: $Upvote get
        /// </summary>
        /// <returns></returns>
        [Command("get")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Get()
        {
            await ReplyAsync("The current Upvote reaction is: " + BotProperties.GetUpvote());
        }
        
        
        
    }
    /// <summary>
    ///  Downvote group, regroups every command related to the Downvote Emote
    /// </summary>
    [Group("Downvote")]
    public class DownvoteModule : ModuleBase<SocketCommandContext>
    {
        
        /// <summary>
        /// Standard action, that is called when user is not fulfilling complete command
        /// </summary>
        /// <returns></returns>
        [Command()]
        public async Task DefaultAction()
        {
            Embed embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                Title = "Downvote rules:",
                Color = Color.Orange,
                Description =
                    "set [emote]: \t set the given emote as default Downvote reaction button. Pepe the Judge will put those on every new post in the meme channel. \n\n" +
                    "get: \t get the current Downvote Reaction."

            }.Build();

            await ReplyAsync(embed: embed);
        }
        
        /// <summary>
        /// Set command for assigning a new emoji as Downvote reaction
        /// Ex: $Downvote set 👎
        /// </summary>
        /// <param name="emote">the new emote to assign as Downvote reaction</param>
        /// <returns></returns>
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Set([Remainder] string emote)
        {
            Reaction react;
            try
            {
                react = new Reaction(emote);

            }
            catch (Exception e)
            {
                await ReplyAsync("🛑 Error, reaction doesn't exist.");
                return;
            }
            BotProperties.SetDownvote(react);
            await ReplyAsync("✅ Successfully set " + react + " as Downvote Reaction !");
            
        }
        /// <summary>
        /// Get command to see the current emoji assigned as Upvote reaction
        /// Ex: $Upvote get
        /// </summary>
        /// <returns></returns>
        [Command("get")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Get()
        {
            await ReplyAsync("The current Downvote reaction is: " + BotProperties.GetDownvote());
        }
        
        
    }
}