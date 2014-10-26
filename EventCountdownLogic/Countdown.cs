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
        public virtual DateTime TargetDate { get; protected set; }

        protected Countdown() { }

        public Countdown(DateTime targetDate)
        {
            TargetDate = targetDate;
        }

        public virtual IEnumerable<DateTime> GetFutureDates()
        {
            yield return TargetDate;
        }

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

        public static Countdown PayDay
        {
            get
            {
                var payday = new MonthlyCountdown("Payday", 28);
                return payday;
            }
        }


        public static Countdown Halloween
        {
            get
            {
                var halloween = new AnnualCountdown("Halloween", 31, 10);
                return halloween;
            }
        }

        public static Countdown Christmas
        {
            get
            {
                var xmas = new AnnualCountdown("Christmas", 25, 12);
                return xmas;
            }
        }

        public static Countdown NewYearsEve
        {
            get
            {
                var newYear = new AnnualCountdown("New Years Eve", 31, 12);
                return newYear;
            }
        }

        public static IList<Countdown> GetCountdowns()
        {
            return new[] { 
                Halloween,
                Christmas,
                NewYearsEve,
                PayDay
            };
        }
    }
}
