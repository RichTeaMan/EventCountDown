using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    /// <summary>
    /// Counts down to fixed yearly date.
    /// </summary>
    public class AnnualCountdown : Countdown
    {
        public int Day { get; protected set; }
        public int Month { get; protected set; }

        public AnnualCountdown(string title, int day, int month) : base(title)
        {
            if (day < 1 || day > 31)
                throw new ArgumentException("Invalid day.");
            if (month < 1 || month > 12)
                throw new ArgumentException("Invalid month.");

            Day = day;
            Month = month;
            Duration = TimeSpan.FromDays(1);
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var year = dateTime.Year;

            DateTime? date = new DateTime(year, Month, Day);
            var timeRemaining = date - dateTime;
            if (timeRemaining.Value.Ticks <= 0)
            {
                if (year < 9999)
                {
                    date = new DateTime(year + 1, Month, Day);
                }
                else
                {
                    date = null;
                }
            }
            return date;
        }

        public override DateTime? GetBeforeDate(DateTime dateTime)
        {
            var year = dateTime.Year;

            DateTime? date = new DateTime(year, Month, Day);
            var timeRemaining = date - dateTime;
            if (timeRemaining.Value.Ticks >= 0)
            {
                if (year > 1)
                {
                    date = new DateTime(year - 1, Month, Day);
                }
                else
                {
                    date = null;
                }
            }
            return date;
        }

    }
}
