using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayBeforeAnnualCountdown : DayAfterAnnualCountdown
    {
        public DayOfWeek DayOfWeek { get; protected set; }

        public DayBeforeAnnualCountdown(string title, int day, int month, params DayOfWeek[] daysOfWeek) : base(title, day, month, daysOfWeek)
        {
            DayAdjustment = -1.0;
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var dateN = base.GetNextDate(dateTime);
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
