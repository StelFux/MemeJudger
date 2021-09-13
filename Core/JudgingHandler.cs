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
    /// <summary>
    /// The Handler for the Judgement functionnality: operate on the selected meme channel with the selected "not funny" role
    /// This Handler has to activate a trial on each user that post memes in the selected channel
    /// It stops a current trial if the user repost another meme
    /// When the bot has to give his judgement, it collects the number of upvotes and downvotes and gives the "not funny" role
    /// when the user has more downvotes than upvotes, and delete the role (if possible) from the user on the contrary
    /// </summary>
    public class JudgingHandler
    {
        //a data structure that organize and attach each trial to the user tha tis judged
        private Dictionary<SocketUser, Thread> _trials = new Dictionary<SocketUser, Thread>();
        
        //the discord client, that is attached to the bot in order to perform action with the discord api
        private readonly DiscordSocketClient _client;

        public JudgingHandler(DiscordSocketClient client)
        {
            _client = client;
        }
    
        /// <summary>
        /// attach the Handle Trial that will be triggered each time the bot receives a message in a guild he is connected to
        /// </summary>
        /// <returns></returns>
        public async Task InstallJudging()
        {
            _client.MessageReceived += HandleTrial;
        }

        /// <summary>
        /// HandleTrial is triggered every time the bot receives a message.
        /// It detects new memes posted in the selected channel and begin a new judgement for the meme poster
        /// </summary>
        /// <param name="messageParam">the message received</param>
        /// <returns></returns>
        public async Task HandleTrial(SocketMessage messageParam)
        {
            
            SocketUserMessage message  = messageParam as SocketUserMessage;
            
            //we abort the trial procedure if we wrongly parsed the message received
            if (message == null)
                return;
            
            //condition to prevent any trigger by a bot message, or a message that isn't a meme post, or a message in the wrong channel
            if(message.Author.IsBot || message.Channel != BotProperties.GetMemeChannel() || message.Attachments.Count == 0)
                return;
            
            
            //we add the reaction emojis to the meme that is posted
            await message.AddReactionAsync(BotProperties.GetUpvote().asIEmote());
            await message.AddReactionAsync(BotProperties.GetDownvote().asIEmote());
            Console.WriteLine("Detected new meme to be judged...");
            
            //if the admin of the guild hasn't setup the bot, we abort the handle trial
            if (BotProperties.GetRole() != null && BotProperties.GetMemeChannel() != null)
            {
                //we begin a new judgement by setting up a thread for it
                Thread judgement = new Thread(() =>
                {
                    SocketUser user = message.Author;
                    
                    //try catch structure, that catches the interrupt signal and proceed to stop the thread
                    try
                    {
                        ulong msgId = message.Id;
                        //making the upvotes and downvote button constant so that when an admin changes 
                        //those buttons during trial, it won't affect those trial but the next one
                        IEmote up = BotProperties.GetUpvote().asIEmote();
                        IEmote down = BotProperties.GetDownvote().asIEmote();
                        
                        //waiting JudgingTime time 
                        Console.WriteLine("Starting Judgement for user: " + user.Username + " in " + BotProperties.GetJudgingTime());
                        Thread.Sleep(BotProperties.GetJudgingTime().InSeconds() * 1000);
                        
                        
                        Console.WriteLine("Pepe is good to go for the trial of " + user.Username);
                        
                        //constant loop of revision of judgement, so we can update the role of the sentenced based on the numbers of upvotes and downvotes
                        while (true)
                        {
                            var msg  = BotProperties.GetMemeChannel().GetMessageAsync(msgId).Result; //we search the message in the channel based on id
                            //we base the bot decision of giving or deleting role on the number of upvote and downvote
                            //the post has
                            if (msg.Reactions[up].ReactionCount < msg.Reactions[down].ReactionCount)
                            {
                                Console.WriteLine("Pepe has decided to punish " + user.Username + " by giving him role: " + BotProperties.GetRole().Name);
                                (user as IGuildUser).AddRoleAsync(BotProperties.GetRole().Id);
                                
                            }
                            else
                            {
                                Console.WriteLine("Pepe is generous to " + user.Username + " and removes role " + BotProperties.GetRole().Name);
                                IGuildUser u = user as IGuildUser;
                                //we assure ourselves that we delete a role that the user has
                                if (u.RoleIds.Contains(BotProperties.GetRole().Id))
                                    u.RemoveRoleAsync(BotProperties.GetRole().Id);
                            }
                            Thread.Sleep(BotProperties.GetIntervalTime().InSeconds() * 1000);
                        }
                    }
                    catch (Exception e)
                    {
                        //we catched interruptexception: we stop the thread
                        Console.WriteLine("Trial for user: " + user.Username + " is finished.");
                    }
                
                });
                //if we start a new judgement for a user that has already been judged one time, we stop its judgement and start another
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