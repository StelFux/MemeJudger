using System;

namespace MemeJudger.Models
{
    /// <summary>
    /// Time class: Data structure that defines a duration based on a unit and a positive integer value
    /// </summary>
    public class Time
    {
        private int TimeValue;
        private TimeType Type; 

        public Time(string value, string type)
        {
            //if the constructor receives value wrongly formatted, it throw exceptions
            if (!int.TryParse(value, out TimeValue))
                throw new ArgumentException("Time constructor: argument value is not an number");

            if (TimeValue < 0)
                throw new ArgumentException("Time constructor: cannot have negative timevalue");
            
            //we define the unit based on the "type" argument 
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
                    throw new ArgumentException( //argument is not a valid format 
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