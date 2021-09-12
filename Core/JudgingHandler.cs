using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using MemeJudger.Models;
using MemeJudger.Services;

namespace MemeJudger.Core
{
    public class JudgingHandler
    {
        private Dictionary<SocketUser, Thread> _trials = new Dictionary<SocketUser, Thread>();
        private readonly DiscordSocketClient _client;

        public JudgingHandler(DiscordSocketClient client)
        {
            _client = client;
        }

        public async Task InstallJudging()
        {
            _client.MessageReceived += HandleTrial;
        }


        public async Task HandleTrial(SocketMessage messageParam)
        {
            SocketUserMessage message  = messageParam as SocketUserMessage;

            if (message == null)
                return;
            
            if(message.Author.IsBot || message.Channel != BotProperties.GetMemeChannel() || message.Attachments.Count == 0)
                return;

            await message.AddReactionAsync(BotProperties.GetUpvote().asIEmote());
            await message.AddReactionAsync(BotProperties.GetDownvote().asIEmote());
            Console.WriteLine("Detected new meme to be judged...");
            if (BotProperties.GetRole() != null && BotProperties.GetMemeChannel() != null)
            {
                Thread judgement = new Thread(() =>
                {
                    SocketUser user = message.Author;
                    try
                    {
                        ulong msgId = message.Id;
                        IEmote up = BotProperties.GetUpvote().asIEmote();
                        IEmote down = BotProperties.GetDownvote().asIEmote();
                        
                        Console.WriteLine("Starting Judgement for user: " + user.Username + " in " + BotProperties.GetJudgingTime());
                        Thread.Sleep(BotProperties.GetJudgingTime().InSeconds() * 1000);
                        
                        
                        Console.WriteLine("Pepe is good to go for the trial of " + user.Username);
                        while (true)
                        {
                            var msg  = BotProperties.GetMemeChannel().GetMessageAsync(msgId).Result;
                            if (msg.Reactions[up].ReactionCount < msg.Reactions[down].ReactionCount)
                            {
                                Console.WriteLine("Pepe has decided to punish " + user.Username + " by giving him role: " + BotProperties.GetRole().Name);
                                (user as IGuildUser).AddRoleAsync(BotProperties.GetRole().Id);
                                
                            }
                            else
                            {
                                Console.WriteLine("Pepe is generous to " + user.Username + " and removes role " + BotProperties.GetRole().Name);
                                IGuildUser u = user as IGuildUser;
                                if (u.RoleIds.Contains(BotProperties.GetRole().Id))
                                    u.RemoveRoleAsync(BotProperties.GetRole().Id);
                            }
                            Thread.Sleep(BotProperties.GetIntervalTime().InSeconds() * 1000);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Trial for user: " + user.Username + " is finished.");
                    }
                
                });
                if (_trials.ContainsKey(message.Author))
                {
                    _trials[message.Author].Interrupt();
                    _trials[message.Author] = judgement;
                    _trials[message.Author].Start();
                }
                else
                {
                    _trials.Add(message.Author,judgement);
                    _trials[message.Author].Start();
                }
            }
            
            
            
            

        }
        
        
    }
}