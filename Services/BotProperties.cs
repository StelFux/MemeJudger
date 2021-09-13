using Discord;
using Discord.WebSocket;
using MemeJudger.Models;

namespace MemeJudger.Services
{
    public static class BotProperties
    {
        private static SocketTextChannel MemeChannelId { get; set; } = null; //channel where The bot will detect if any meme is posted
        private static IRole Role { get; set; } = null; //the role to assign when nbupvote < nbdownvote

        private static Time _judgingTime = new Time("1", "hour"); //default value
        private static Time _intervalTime = new Time("30", "seconds"); //default value
        private static Reaction _upvoteEmote = new Reaction("👍"); //default value
        private static Reaction _downvoteEmote  = new Reaction("👎");//default value
        
        
        /// <summary>
        /// Getter for the Meme Channel
        /// </summary>
        /// <returns></returns>
        public static SocketTextChannel GetMemeChannel() => MemeChannelId;
        
        /// <summary>
        /// Getter for the Role to assign after judgement
        /// </summary>
        /// <returns></returns>
        public static IRole GetRole() => Role;
        
        /// <summary>
        /// Getter for the waiting time before The bot will judge the meme
        /// </summary>
        /// <returns></returns>
        public static Time GetJudgingTime() => _judgingTime;
        
        /// <summary>
        /// Getter for the waiting time between each revision of judgement
        /// </summary>
        /// <returns></returns>
        public static Time GetIntervalTime() => _intervalTime;
        
        /// <summary>
        /// Getter for the Upvote Emoji
        /// </summary>
        /// <returns></returns>
        public static Reaction GetUpvote() => _upvoteEmote;
        
        /// <summary>
        /// Getter for the Downvote Emoji
        /// </summary>
        /// <returns></returns>
        public static Reaction GetDownvote() => _downvoteEmote;
        
        /// <summary>
        /// Setter for the Meme Channel
        /// </summary>
        /// <param name="channel">the new Channel to assign as Meme Channel</param>
        public static void SetMemeChannel(SocketTextChannel channel)
        {
            MemeChannelId = channel;
        }
        
        /// <summary>
        /// Setter for the Role to assign after judgement
        /// </summary>
        /// <param name="irole">The new role to assign after judgement</param>
        public static void SetRole(IRole irole)
        {
            Role = irole;
        }
        /// <summary>
        /// Setter for the waiting time before The bot will judge the meme
        /// </summary>
        /// <param name="time">the new waiting time before The bot will judge the meme</param>
        public static void SetJudgingTime(Time time)
        {
            _judgingTime = time;
        }
        
        /// <summary>
        /// Setter for the waiting time between each revision of judgement
        /// </summary>
        /// <param name="time">the new waiting time between each revision of judgement</param>
        public static void SetIntervalTime(Time time)
        {
            _intervalTime = time;
        }
        
        /// <summary>
        /// Setter for the Upvote Emoji 
        /// </summary>
        /// <param name="emote">the new Emote to assign as Upvote Emoji</param>
        public static void SetUpvote(Reaction emote)
        {
            _upvoteEmote = emote;
        }
        
        /// <summary>
        /// Setter for the Downvote Emoji 
        /// </summary>
        /// <param name="emote">the new Emote to assign as Downvote Emoji</param>
        public static void SetDownvote(Reaction emote)
        {
            _downvoteEmote = emote;
        }

    }
}