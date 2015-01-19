using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayBeforeArbitraryCountdown : DayAfterArbitraryCountdown
    {
        public DayBeforeArbitraryCountdown(string title, IEnumerable<DayOfWeek> daysOfWeek, params DateTime[] dates) : base(title, daysOfWeek, dates)
        {
            DayAdjustment = -1.0;
        }

        public DayBeforeArbitraryCountdown(string title, DayOfWeek dayOfWeek, params DateTime[] dates) : this(title, new[] { dayOfWeek }, dates)
        {
            
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
                        date = GetNextDate(dateTime.AddDays(7.0 - totalAdjustment));
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
