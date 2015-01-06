using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayAfterAnnualCountdown : AnnualCountdown
    {
        public DayOfWeek DayOfWeek { get; protected set; }

        public DayAfterAnnualCountdown(string title, int day, int month, DayOfWeek dayOfWeek) : base(title, day, month)
        {
            DayOfWeek = dayOfWeek;    
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var dateN = base.GetNextDate(dateTime);
            if (dateN.HasValue)
            {
                var date = dateN.Value;
                while (date.DayOfWeek != DayOfWeek)
                {
                    date = date.AddDays(1.0);
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
