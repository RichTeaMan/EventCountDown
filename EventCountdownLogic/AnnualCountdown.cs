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
            TargetDate = GetAnnualDateTime(day, month);
        }

        public override IEnumerable<DateTime> GetFutureDates()
        {
            var current = TargetDate;
            yield return current;
            while (true)
            {
                int nextYear = current.Year + 1;
                var maxDays = DateTime.DaysInMonth(nextYear, Month);
                int nextDay;
                if (Day > maxDays)
                    nextDay = maxDays;
                else
                    nextDay = Day;
                current = new DateTime(nextYear, Month, nextDay);
                yield return current;
            }
        }

        public static DateTime GetAnnualDateTime(int day, int month)
        {
            var year = DateTime.Now.Year;

            var date = new DateTime(year, month, day);
            var timeRemaining = date - DateTime.Now;
            if (timeRemaining.Ticks < 0)
            {
                date = new DateTime(year + 1, month, day);
            }
            return date;
        }

    }
}
