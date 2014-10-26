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
        public MonthlyCountdown(string title, int day)
        {
            if (day > 31 || day < 1)
                throw new ArgumentException("Invalid day.");
            Title = title;
            Day = day;
        }

        public override IEnumerable<CountdownDateTime> GetFutureDates()
        {
            var current = GetCountdownDateTime(GetMonthlyDateTime(Day));
            yield return current;
            while (true)
            {
                int nextYear = current.Year;
                int nextMonth = current.Month + 1;
                if (nextMonth > 12)
                {
                    nextMonth = 1;
                    nextYear++;
                }
                var maxDays = DateTime.DaysInMonth(nextYear, nextMonth);
                int nextDay;
                if (Day > maxDays)
                    nextDay = maxDays;
                else
                    nextDay = Day;
                current = GetCountdownDateTime(nextYear, nextMonth, nextDay);
                yield return current;
            }
        }

        public static DateTime GetMonthlyDateTime(int day)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            var date = new DateTime(year, month, day);
            var timeRemaining = date - DateTime.Now;
            if (timeRemaining.Ticks < 0)
            {
                month++;
                if (month >= 13)
                {
                    year++;
                    month = 1;
                }
                date = new DateTime(year, month, day);
            }
            return date;
        }
    }
}
