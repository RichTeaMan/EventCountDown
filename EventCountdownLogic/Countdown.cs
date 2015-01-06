using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCountdownLogic
{
    public abstract class Countdown
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        protected Countdown(string title)
        {
            Title = title;
            Duration = TimeSpan.FromDays(1);
        }

        /// <summary>
        /// Gets the next date this event occurs on after the one given, or null
        /// if there is no date.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public abstract DateTime? GetNextDate(DateTime dateTime);

        /// <summary>
        /// Gets the next date this event occurs on after the one given, or null
        /// if there is no date.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public CountdownDateTime GetNextDate(CountdownDateTime countdownDateTime)
        {
            var dateTime = GetNextDate(countdownDateTime.DateTime);
            if (dateTime == null)
                return null;
            else
                return GetCountdownDateTime(dateTime.Value);
        }

        /// <summary>
        /// Gets if the given DateTime will occur while this event is in progress.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public bool IsEventOccurring(DateTime dateTime)
        {
            var startCountdownDate = GetEventCountdownBeforeDate(dateTime);
            if (startCountdownDate == null)
                return false;
            var startDate = startCountdownDate.DateTime;
            var endDate = startDate + Duration;

            var result = dateTime >= startDate && dateTime < endDate;
            return result;
        }

        /// <summary>
        /// Gets the date this event occurs on before the one given, or null
        /// if there is no date.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private CountdownDateTime GetEventCountdownBeforeDate(DateTime dateTime)
        {
            var before = dateTime - Duration;
            var beforeDateTime = GetNextDate(before);
            if (beforeDateTime != null)
                return GetCountdownDateTime(beforeDateTime.Value);
            else
                return null;
        }

        public bool IsEventOccuringOnDay(DateTime dateTime)
        {
            var startCountdownDate = GetEventCountdownBeforeDate(dateTime);
            if (startCountdownDate == null)
                return false;
            var startDate = startCountdownDate.DateTime.Date;
            var endDate = (startCountdownDate.DateTime + Duration).Date.AddDays(1);

            var result = dateTime >= startDate && dateTime < endDate;
            return result;
        }

        protected CountdownDateTime GetCountdownDateTime(DateTime datetime)
        {
            var cdt = new CountdownDateTime(this, datetime);
            return cdt;
        }

        protected CountdownDateTime GetCountdownDateTime(int year, int month, int day)
        {
            var datetime = new DateTime(year, month, day);
            var cdt = GetCountdownDateTime(datetime);
            return cdt;
        }

        public IEnumerable<DateTime> GetFutureDates()
        {
            var dates = GetFutureDates(DateTime.Now);
            foreach (var date in dates)
            {
                yield return date;
            }
        }

        public IEnumerable<CountdownDateTime> GetFutureCountdownDateTimes()
        {
            var dates = GetFutureCountdownDateTimes(GetCountdownDateTime(DateTime.Now));
            foreach (var date in dates)
            {
                yield return date;
            }
        }

        public IEnumerable<DateTime> GetFutureDates(DateTime dateTime)
        {
            var countdownDateTime = GetNextDate(dateTime);
            while (countdownDateTime != null)
            {
                yield return countdownDateTime.Value;
                countdownDateTime = GetNextDate(countdownDateTime.Value);
            }
        }

        public IEnumerable<CountdownDateTime> GetFutureCountdownDateTimes(CountdownDateTime countdownDateTime)
        {
            foreach (var dt in GetFutureDates(countdownDateTime.DateTime))
            {
                yield return GetCountdownDateTime(dt);
            }
        }

        public CountdownDateTime NextCountdownDateTime
        {
            get
            {
                return GetFutureCountdownDateTimes().FirstOrDefault();
            }
        }

        public DateTime? NextDateTime
        {
            get
            {
                return GetFutureDates().FirstOrDefault();
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

        public static Countdown Valentines
        {
            get
            {
                var ev = new AnnualCountdown("Valentines Day", 14, 2);
                return ev;
            }
        }

        public static Countdown ReadingMarathon
        {
            get
            {
                var ev = new ArbitraryCountdown("Reading Half Marathon", 2015, 3, 22);
                return ev;
            }
        }

        public static Countdown Easter
        {
            get
            {
                var ev = new ArbitraryCountdown("Easter", 2015, 4, 5)
                    .AddDate(2016, 3, 27)
                    .AddDate(2017, 4, 16)
                    .AddDate(2018, 4, 1)
                    .AddDate(2019, 4, 21)
                    .AddDate(2020, 4, 12)
                    .AddDate(2021, 4, 4)
                    .AddDate(2022, 4, 17)
                    .AddDate(2023, 4, 9)
                    .AddDate(2024, 3, 31)
                    .AddDate(2025, 4, 20);
                return ev;
            }
        }

        public static Countdown GeneralElection
        {
            get
            {
                var ev = new ArbitraryCountdown("UK General Election", 2015, 5, 5);
                return ev;
            }
        }

        public static Countdown DayLightSavingsStart
        {
            get
            {
                var ev = new DayAfterAnnualCountdown("Daylight Savings", 25, 3, DayOfWeek.Sunday);
                return ev;
            }
        }

        public static Countdown DayLightSavingsEnd
        {
            get
            {
                var ev = new DayAfterAnnualCountdown("Daylight Savings End", 25, 10, DayOfWeek.Sunday);
                return ev;
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

        private static IList<Countdown> _countdowns = null;
        public static IList<Countdown> GetCountdowns()
        {
            if (_countdowns == null)
            {
                _countdowns = new[] {
                    Halloween,
                    Christmas,
                    NewYearsEve,
                    PayDay,
                    Valentines,
                    ReadingMarathon,
                    Easter,
                    DayLightSavingsStart,
                    DayLightSavingsEnd,
                    GeneralElection
                };
            }
            return _countdowns;
        }
    }
}
