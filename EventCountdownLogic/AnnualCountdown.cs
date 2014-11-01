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

        public AnnualCountdown(string title, int day, int month)
        {
            if (day < 1 || day > 31)
                throw new ArgumentException("Invalid day.");
            if (month < 1 || month > 12)
                throw new ArgumentException("Invalid month.");

            Title = title;
            Day = day;
            Month = month;
            Duration = TimeSpan.FromDays(1);
        }

        public override CountdownDateTime GetNextDate(DateTime dateTime)
        {
            var year = dateTime.Year;

            var date = new DateTime(year, Month, Day);
            var timeRemaining = date - dateTime;
            if (timeRemaining.Ticks <= 0)
            {
                date = new DateTime(year + 1, Month, Day);
            }
            var countdownDate = new CountdownDateTime(this, date);
            return countdownDate;
        }

    }
}
