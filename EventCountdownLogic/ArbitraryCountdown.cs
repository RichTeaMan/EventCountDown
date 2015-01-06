using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class ArbitraryCountdown : Countdown
    {
        public List<DateTime> DateTimes { get; protected set; }

        protected ArbitraryCountdown(string title) : base(title)
        {
            DateTimes = new List<DateTime>();
        }

        public ArbitraryCountdown(string title, params DateTime[] dates) : this(title)
        {
            DateTimes.AddRange(dates);
        }

        public ArbitraryCountdown(string title, int year, int month, int day) : this(title)
        {
            AddDate(year, month, day);
        }

        public ArbitraryCountdown AddDate(DateTime dateTime)
        {
            DateTimes.Add(dateTime);
            return this;
        }

        public ArbitraryCountdown AddDate(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            return AddDate(date);
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            foreach (var dt in DateTimes)
            {
                if (dateTime < dt)
                {
                    return dt;
                }
            }
            return null;
        }

    }
}
