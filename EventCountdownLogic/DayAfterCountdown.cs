using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayAfterCountdown : Countdown
    {
        public DayOfWeek[] DaysOfWeek { get; protected set; }

        public double DayAdjustment { get; protected set; }

        protected Countdown FixedCountdown { get; set; }

        public DayAfterCountdown(string title, Countdown countdown, params DayOfWeek[] daysOfWeek) : base(title)
        {
            DaysOfWeek = daysOfWeek;
            DayAdjustment = 1.0;
            FixedCountdown = countdown;
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var startDate = dateTime;
            while (!DaysOfWeek.Contains(startDate.DayOfWeek))
            {
                startDate = startDate.AddDays(-1);
            }
            var dateN = FixedCountdown.GetNextDate(startDate);
            if (dateN.HasValue)
            {
                var date = dateN.Value;
                while (!DaysOfWeek.Contains(date.DayOfWeek))
                {
                    date = date.AddDays(DayAdjustment);
                }
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
