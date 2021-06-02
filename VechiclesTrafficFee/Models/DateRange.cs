using System;

namespace VechiclesTraffic.Production.Models
{
    public struct DateRange
    {
        public DateTime From { get; }

        public DateTime To { get; }

        public DateRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }
    }
}
