using Discord;
using Discord.WebSocket;
using MemeJudger.Models;

namespace MemeJudger.Services
{
    public static class BotProperties
    {
        private static SocketTextChannel MemeChannelId { get; set; } = null;
        private static IRole Role { get; set; } = null;

        private static Time _judgingTime = new Time("1", "hour"); //default value
        private static Time _intervalTime = new Time("30", "seconds"); //default value
        private static Reaction _upvoteEmote = new Reaction("👍"); //default value
        private static Reaction _downvoteEmote  = new Reaction("👎");//default value
        
        

        public static SocketTextChannel GetMemeChannel() => MemeChannelId;
        public static IRole GetRole() => Role;
        public static Time GetJudgingTime() => _judgingTime;
        public static Time GetIntervalTime() => _intervalTime;

        public static Reaction GetUpvote() => _upvoteEmote;
        public static Reaction GetDownvote() => _downvoteEmote;

        public static void SetMemeChannel(SocketTextChannel channel)
        {
            MemeChannelId = channel;
        }

        public static void SetRole(IRole irole)
        {
            Role = irole;
        }
        
        public static void SetJudgingTime(Time time)
        {
            _judgingTime = time;
        }

        public static void SetIntervalTime(Time time)
        {
            _intervalTime = time;
        }
        

        public static void SetUpvote(Reaction emote)
        {
            _upvoteEmote = emote;
        }
        
        public static void SetDownvote(Reaction emote)
        {
            _downvoteEmote = emote;
        }

    }
}