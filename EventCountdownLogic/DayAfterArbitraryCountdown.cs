using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayAfterArbitraryCountdown : ArbitraryCountdown
    {
        public DayOfWeek[] DaysOfWeek { get; protected set; }

        public double DayAdjustment { get; protected set; }

        public DayAfterArbitraryCountdown(string title, IEnumerable<DayOfWeek> daysOfWeek, params DateTime[] dates) : base(title, dates)
        {
            DaysOfWeek = daysOfWeek.ToArray();
        }

        public DayAfterArbitraryCountdown(string title, DayOfWeek dayOfWeek, params DateTime[] dates) : this(title, new[] { dayOfWeek }, dates)
        {
            DayAdjustment = 1.0;
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var startDate = dateTime;
            while (!DaysOfWeek.Contains(startDate.DayOfWeek))
            {
                startDate = startDate.AddDays(-1);
            }
            var dateN = base.GetNextDate(startDate);
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
