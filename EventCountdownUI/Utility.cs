using EventCountdownLogic;
using EventCountdownUI.Resources;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace EventCountdownUI
{
    internal static class Utility
    {
        static Random _random;
        internal static Random GetRandom
        {
            get
            {
                if (_random == null)
                    _random = new Random();
                return _random;
            }
        }

        internal static void NavigateToCountdownPage(this PhoneApplicationPage page, Countdown countdown)
        {
            page.NavigationService.Navigate(new Uri("/CountdownPage.xaml?cd=" + countdown.Title, UriKind.Relative));
        }

        internal static void NavigateToCountdownPage(this PhoneApplicationPage page, DateTime dateTime)
        {
            page.NavigationService.Navigate(new Uri("/DatePage.xaml?ticks=" + dateTime.Ticks.ToString(), UriKind.Relative));
        }

        public static TimeInterval[] GetIntervals()
        {
            var intervals = Enum.GetValues(typeof(TimeInterval)).Cast<TimeInterval>().ToArray();
            return intervals;
        }

        public static IntervalNameCount GetIntervalString(TimeInterval timeInterval, Countdown countdownDateTime)
        {
            string intervalSub = null;
            double count = 0;

            switch (timeInterval)
            {
                case TimeInterval.Days:
                    count = countdownDateTime.NextCountdownDateTime.GetDays;
                    if (count == 1)
                        intervalSub = AppResources.DayName;
                    else
                        intervalSub = AppResources.DayPluralName;
                    break;
                case TimeInterval.Minutes:
                    count = countdownDateTime.NextCountdownDateTime.GetMinutes;
                    if (count == 1)
                        intervalSub = AppResources.MinuteName;
                    else
                        intervalSub = AppResources.MinutePluralName;
                    break;
                case TimeInterval.Hours:
                    count = countdownDateTime.NextCountdownDateTime.GetHours;
                    if (count == 1)
                        intervalSub = AppResources.HourName;
                    else
                        intervalSub = AppResources.HourPluralName;
                    break;
                case TimeInterval.Seconds:
                    count = countdownDateTime.NextCountdownDateTime.GetSeconds;
                    if (count == 1)
                        intervalSub = AppResources.SecondName;
                    else
                        intervalSub = AppResources.SecondPluralName;
                    break;
                case TimeInterval.Weeks:
                    count = countdownDateTime.NextCountdownDateTime.GetWeeks;
                    if (count == 1)
                        intervalSub = AppResources.WeekName;
                    else
                        intervalSub = AppResources.WeekPluralName;
                    break;
                case TimeInterval.Years:
                    count = countdownDateTime.NextCountdownDateTime.GetYears;
                    if (count == 1)
                        intervalSub = AppResources.YearName;
                    else
                        intervalSub = AppResources.YearPluralName;
                    break;
            }

            var result = new IntervalNameCount(timeInterval, intervalSub, count);
            return result;
        }

        public static string GetCountdownText(TimeInterval timeInterval, Countdown countdown, string countdownTemplate)
        {
            var intervalNameCount = Utility.GetIntervalString(timeInterval, countdown);

            string countStr;
            var count = intervalNameCount.Count;
            if (intervalNameCount.Count % 1 == 0)
                countStr = count.ToString("F0");
            else
                countStr = count.ToString("F2");

            var sc = new StringCreator()
                .AddParameter(StringParam.INTERVAL_NAME, intervalNameCount.TimeIntervalSub)
                .AddParameter(StringParam.COUNT, countStr)
                .AddParameter(StringParam.EVENT_NAME, countdown.Title);

            var summary = sc.BuildString(countdownTemplate);
            return summary;
        }
    }
}
