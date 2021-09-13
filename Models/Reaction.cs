using System;
using Discord;

namespace MemeJudger.Models
{
    /// <summary>
    /// gives the right format of reaction between unicode and discord format
    /// </summary>
    public class Reaction
    {
        private Emote DiscordFormat; // ex: <upvote:983420423874>
        private Emoji UnicodeFormat; // ex: 😊

        private ReactionType type; //the type to reference the current format used 

        public Reaction(string emote)
        {
            if (emote.Contains("<"))
            {
                type = ReactionType.DISCORDFORMAT;
                DiscordFormat = Emote.Parse(emote);
            }
            else
            {
                type = ReactionType.UNICODEFORMAT;
                UnicodeFormat = new Emoji(emote);
            }
        }
        
        public override string ToString()
        {
            switch (type)
            {
                case ReactionType.DISCORDFORMAT:
                    return DiscordFormat.ToString();
                case ReactionType.UNICODEFORMAT:
                    return UnicodeFormat.ToString();
                default:
                    throw new InvalidCastException("Reaction.ToString: Enum has not been well assigned.");
            }
        }
        /// <summary>
        /// returns the format as its interface that is common with the other format
        /// </summary>
        /// <returns>the common interface of the two formats</returns>
        /// <exception cref="InvalidCastException">For some reason, the reaction type is of type null</exception>
        public IEmote asIEmote()
        {
            switch (type)
            {
                case ReactionType.DISCORDFORMAT:
                    return DiscordFormat;
                case ReactionType.UNICODEFORMAT:
                    return UnicodeFormat;
                default:
                    throw new InvalidCastException("Reaction.ToString: Enum has not been well assigned.");
            }
        }
    }

    public enum ReactionType
    {
        DISCORDFORMAT = 0,
        UNICODEFORMAT
    }
}