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
        public int Day { get; set; }
        public int Month { get; set; }

        public const string CountdownTypeName = "Annual";

        public override string CountdownType
        {
            get
            {
                return CountdownTypeName;
            }
        }

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

        public AnnualCountdown() : base() { }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var year = dateTime.Year;

            var date = new DateTime(year, Month, Day);
            var timeRemaining = date - dateTime;
            if (timeRemaining.Ticks <= 0)
            {
                date = new DateTime(year + 1, Month, Day);
            }
            return date;
        }

        public override bool IsEquivalent(Countdown c)
        {
            if (base.IsEquivalent(c))
            {
                var aC = c as AnnualCountdown;
                if (aC != null)
                {
                    var equal = Day == aC.Day && Month == aC.Month;
                    return equal;
                }
            }
            return false;
        }

    }
}
