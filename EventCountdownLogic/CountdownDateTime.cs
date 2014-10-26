using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class CountdownDateTime
    {
        public DateTime DateTime { get; private set; }
        public Countdown Countdown { get; private set; }

        public CountdownDateTime(Countdown countdown, DateTime dateTime)
        {
            Countdown = countdown;
            DateTime = dateTime;
        }

        public int Year
        {
            get
            {
                return DateTime.Year;
            }
        }

        public int Month
        {
            get
            {
                return DateTime.Month;
            }
        }

        public int Day
        {
            get
            {
                return DateTime.Day;
            }
        }

        public TimeSpan GetTimeSpanFromDate(DateTime date)
        {
            return DateTime - date;
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

        public override string ToString()
        {
            return DateTime.ToString();
        }

        public static explicit operator DateTime(CountdownDateTime countdownDatetime)
        {
            return countdownDatetime.DateTime;
        }

    }
}
