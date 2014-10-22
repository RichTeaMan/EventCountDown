using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCountdownLogic
{
    public class Countdown
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime TargetDate { get; set; }

        public TimeSpan GetTimeSpanFromDate(DateTime date)
        {
            return TargetDate - date;
        }

        public TimeSpan GetTimeSpan()
        {
            return GetTimeSpanFromDate(DateTime.Now);
        }

        public int GetDays
        {
            get
            {
                return (int)Math.Ceiling(GetTimeSpan().TotalDays);
            }
        }

        public static DateTime GetDateTime(int day, int month)
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

        public static Countdown Christmas
        {
            get
            {
                var xmas = new Countdown()
                {
                    Title = "Christmas",
                    TargetDate = GetDateTime(25, 12)
                };
                return xmas;
            }

        }

        public static IList<Countdown> GetCountdowns()
        {
            return new[] { Christmas };
        }
    }
}
