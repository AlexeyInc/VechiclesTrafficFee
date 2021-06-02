using System;
using System.Collections.Generic;
using System.Linq;
using VechiclesTraffic.Production.Models;

namespace VechiclesTrafficFee.Helpers
{
    public static class HelperExtensions
    {
        private static IList<DayOfWeek> _weekends;
        private static IList<DateRange> _holidays;

        static HelperExtensions()
        {
            InitTollFreeDates();
        }

        public static bool IsTollFreeDate(this DateTime date)
        {
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (_weekends.Contains(date.DayOfWeek))
                return true;

            if (_holidays.Any(d => d.From <= date && date <= d.To))
                return true;

            return false;
        }

        public static double GetTimeInMinutes(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second).TotalMinutes;
        }

        private static void InitTollFreeDates()
        {
            _weekends = new List<DayOfWeek>()
            {
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

            _holidays = new List<DateRange>()
            {
                new DateRange(new DateTime(2013, 1, 1, 0, 0, 0),  new DateTime(2013, 1, 1, 23, 59, 59)),
                new DateRange(new DateTime(2013, 3, 28, 0, 0, 0), new DateTime(2013, 3, 29, 23, 59, 59)),
                new DateRange(new DateTime(2013, 4, 1, 0,0,0),    new DateTime(2013, 4, 1, 23, 59, 59)),
                new DateRange(new DateTime(2013, 4, 30, 0,0,0),   new DateTime(2013, 4, 30, 23, 59, 59)),
                new DateRange(new DateTime(2013, 5, 1, 0,0,0),    new DateTime(2013, 5, 1, 23, 59, 59)),
                new DateRange(new DateTime(2013, 5, 8, 0,0,0),    new DateTime(2013, 5, 9, 23, 59, 59)),
                new DateRange(new DateTime(2013, 6, 5, 0,0,0),    new DateTime(2013, 6, 6, 23, 59, 59)),
                new DateRange(new DateTime(2013, 6, 21, 0,0,0),   new DateTime(2013, 6, 21, 23, 59, 59)),
                new DateRange(new DateTime(2013, 7, 1, 0,0,0),    new DateTime(2013, 7, 31, 23, 59, 59)),
                new DateRange(new DateTime(2013, 11, 1, 0,0,0),   new DateTime(2013, 11, 1, 23, 59, 59)),
                new DateRange(new DateTime(2013, 12, 24, 0,0,0),  new DateTime(2013, 12, 26, 23, 59, 59)),
                new DateRange(new DateTime(2013, 12, 31, 0,0,0),  new DateTime(2013, 12, 31, 23, 59, 59))
            };
        }
    }
}
