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

        public int GetSeconds
        {
            get
            {
                return (int)Math.Ceiling(GetTimeSpan().TotalSeconds);
            }
        }

        public int GetMinutes
        {
            get
            {
                return (int)Math.Ceiling(GetTimeSpan().TotalMinutes);
            }
        }

        public int GetHours
        {
            get
            {
                return (int)Math.Ceiling(GetTimeSpan().TotalHours);
            }
        }

        public int GetDays
        {
            get
            {
                return (int)Math.Ceiling(GetTimeSpan().TotalDays);
            }
        }

        public double GetWeeks
        {
            get
            {
                return GetDays / 7.0;
            }
        }

        public double GetYears
        {
            get
            {
                return GetDays / 365.0;
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

        public static Countdown Halloween
        {
            get
            {
                var halloween = new Countdown()
                {
                    Title = "Halloween",
                    TargetDate = GetDateTime(31, 10)
                };
                return halloween;
            }
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

        public static Countdown NewYearsEve
        {
            get
            {
                var newYear = new Countdown()
                {
                    Title = "New Years Eve",
                    TargetDate = GetDateTime(31, 12)
                };
                return newYear;
            }
        }

        public static IList<Countdown> GetCountdowns()
        {
            return new[] { 
                Halloween,
                Christmas,
                NewYearsEve
            };
        }
    }
}
