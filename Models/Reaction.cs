using System;
using Discord;

namespace MemeJudger.Models
{
    public class Reaction
    {
        private Emote DiscordFormat;
        private Emoji UnicodeFormat;

        private ReactionType type;

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