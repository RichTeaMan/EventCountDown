using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCountdown
{
    public class DateUtility
    {
        public static TimeSpan GetXmasTimespan
        {
            get
            {
                var now = DateTime.Now;
                var xmas = new DateTime(now.Year, 12, 25);
                return xmas - now;
            }
        }

        public static int GetXmasDays
        {
            get
            {
                var daysF = GetXmasTimespan.TotalDays;
                var days = (int)Math.Ceiling(daysF);
                return days;
            }
        }
    }
}
