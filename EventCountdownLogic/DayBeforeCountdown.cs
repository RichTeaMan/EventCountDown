using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayBeforeCountdown : Countdown
    {
        public DayOfWeek[] DaysOfWeek { get; protected set; }
        public double DayAdjustment { get; set; }

        protected Countdown FixedCountdown { get; set; }

        public DayBeforeCountdown(string title, Countdown countdown, params DayOfWeek[] daysOfWeek) : base(title)
        {
            DayAdjustment = -1.0;
            DaysOfWeek = daysOfWeek;
            FixedCountdown = countdown;
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var dateN = FixedCountdown.GetNextDate(dateTime);
            if (dateN.HasValue)
            {
                var date = dateN;
                var totalAdjustment = 0.0;
                while (!DaysOfWeek.Contains(date.Value.DayOfWeek) || date <= dateTime)
                {
                    date = date.Value.AddDays(DayAdjustment);
                    totalAdjustment += DayAdjustment;
                    if (date <= dateTime)
                    {
                        date = GetNextDate(dateTime.AddDays(7.0 + totalAdjustment));
                        break;
                    }
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
