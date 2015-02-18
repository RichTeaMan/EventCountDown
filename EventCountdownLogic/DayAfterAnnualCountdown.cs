using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayAfterAnnualCountdown : DayAfterCountdown
    {

        public DayAfterAnnualCountdown(string title, int day, int month, params DayOfWeek[] daysOfWeek)
            : base(title, new AnnualCountdown(title, day, month), daysOfWeek)
        {
            
        }
    }
}
