using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    /// <summary>
    /// Counts to a fixed day every month. If the month does not have enough
    /// days the last day in that month is returned.
    /// </summary>
    public class MonthlyCountdown : Countdown
    {
        public int Day { get; protected set; }

        public MonthlyCountdown(string title, int day) : base(title)
        {
            if (day > 31 || day < 1)
                throw new ArgumentException("Invalid day.");
            Day = day;

            Duration = TimeSpan.FromDays(1);
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var year = dateTime.Year;
            var month = dateTime.Month;

            var date = new DateTime(year, month, Day);
            var timeRemaining = date - dateTime;
            if (timeRemaining.Ticks <= 0)
            {
                month++;
                if (month >= 13)
                {
                    year++;
                    month = 1;
                }
                date = new DateTime(year, month, Day);
            }
            return date;
        }

    }
}
