using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using MemeJudger.Models;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    [Group("Time")]
    public class TimeModule : ModuleBase<SocketCommandContext>
    {
        [Command()]
        public async Task DefaultAction()
        {
            Embed embed = new EmbedBuilder()
            {
                Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                Title = "Time",
                Color = Color.LightOrange,
                Description =
                    "JudgingTime: \t Module to define the time to wait before Pepe judges the memes. \n\n" +
                    "IntervalTime: \t Module to define the time Pepe has to wait each time to change the role of the memes poster according to their likes."

            }.Build();

            await ReplyAsync(embed: embed);
        }
        
        [Group("JudgingTime")]
        public class JudgingTime : ModuleBase<SocketCommandContext> 
        {
            [Command()]
            public async Task DefaultAction()
            {
                Embed embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                    Title = "Judging Time",
                    Color = Color.DarkBlue,
                    Description =
                        "set whole_number [hour(s)/minute(s)/second(s)] : \t set the time to wait before Pepe the Judge starts its trial. \n\n" +
                        "get: \t get the current waiting trial time before Pepe judges the memes."

                }.Build();

                await ReplyAsync(embed: embed);
            }
            
            
            [Command("set")]
            [RequireUserPermission(GuildPermission.Administrator)]
            public async Task Set(string value, string type)
            {
                Time time;
                try
                {
                    time = new Time(value, type);
                }
                catch (Exception e)
                {
                    await ReplyAsync("🛑 Error: Wrong format, value must be: whole_number [hour(s)/minute(s)/second(s)].");
                    return;
                }
                BotProperties.SetJudgingTime(time);
            
                await ReplyAsync("✅ Successfully set Judging time as: " + time + ".");
            }
        
            [Command("get")]
            [RequireUserPermission(GuildPermission.Administrator)]
            public async Task Get()
            {
                await ReplyAsync("Currently, Judging time is set as: " + BotProperties.GetJudgingTime());
            }
        }
        
        
        [Group("IntervalTime")]
        public class IntervalTime : ModuleBase<SocketCommandContext> 
        {
            [Command()]
            public async Task DefaultAction()
            {
                Embed embed = new EmbedBuilder()
                {
                    Author = new EmbedAuthorBuilder() {Name = Context.User.Username},
                    Title = "Interval Time",
                    Color = Color.Blue,
                    Description =
                        "set whole_number [hour(s)/minute(s)/second(s)]: \t Set the time to wait before Pepe the judges changes his trial decision (or not). \n\n" +
                        "get: \t get the current waiting time before Pepe The Judge redefines his judgement."

                }.Build();

                await ReplyAsync(embed: embed);
            }
            
            [Command("set")]
            [RequireUserPermission(GuildPermission.Administrator)]
            public async Task Set(string value, string type)
            {
                Time time;
                try
                {
                    time = new Time(value, type);
                }
                catch (Exception e)
                {
                    await ReplyAsync("🛑 Error: Wrong format, value must be: whole_number [hour(s)/minute(s)/second(s)].");
                    return;
                }
                BotProperties.SetIntervalTime(time);
            
                await ReplyAsync("✅ Successfully set Interval time as: " + time + ".");
            }
        
            [Command("get")]
            [RequireUserPermission(GuildPermission.Administrator)]
            public async Task Get()
            {
                await ReplyAsync("Currently, Interval time is set as: " + BotProperties.GetJudgingTime());
            }
        }
    }
    
    
}