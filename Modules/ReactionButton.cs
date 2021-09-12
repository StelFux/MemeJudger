using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Models;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    [Group("Upvote")]
    public class UpvoteModule : ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetMemeChannel([Remainder] string emote)
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

        [Command("get")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetUpvote()
        {
            await ReplyAsync("The current Upvote reaction is: " + BotProperties.GetUpvote());
        }
        
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
    }
    
    [Group("Downvote")]
    public class DownvoteModule : ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetDownvote([Remainder] string emote)
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

        [Command("get")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task GetDownvote()
        {
            await ReplyAsync("The current Downvote reaction is: " + BotProperties.GetDownvote());
        }
        
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
    }
}