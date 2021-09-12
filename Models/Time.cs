using System;

namespace MemeJudger.Models
{
    public class Time
    {
        private int TimeValue;
        private TimeType Type;

        public Time(string value, string type)
        {
            if (!int.TryParse(value, out TimeValue))
                throw new ArgumentException("Time constructor: argument value is not an number");

            switch (type)
            {
                case "hour":
                case "hours":
                    Type = TimeType.HOUR;
                    break;
                
                case "minute":
                case "minutes":
                    Type = TimeType.MINUTE;
                    break;
                
                case "second":
                case "seconds":
                    Type = TimeType.SECOND;
                    break;
                default:
                    throw new ArgumentException(
                        "Time constructor: argument type is not a valid time type (is not filtered as hour/minute/second");
            }
        }

        public override string ToString() 
            => TimeValue.ToString() + " " + Type.ToString().ToLower() + ( TimeValue > 1 ? "s" : "");

        public int InSeconds() => TimeValue * ((int) Math.Pow(60, (int)Type));
        
    }

    public enum TimeType
    {
        SECOND = 0,
        MINUTE,
        HOUR
    }
}