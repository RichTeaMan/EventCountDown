using EventCountdownLogic;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace EventCountdownScheduledTaskAgent
{
    public class TileFactory
    {
        public static void SetTile()
        {
            var random = new Random();

            var events = EventCountdownLogic.Countdown.GetCountdowns();
            var eventI = random.Next(events.Count);
            var ev = events[eventI];

            var intervals = GetIntervals();
            int count = 0;
            string title = string.Empty;
            while (count > 99 || count < 1)
            {
                var intI = random.Next(intervals.Count());
                var interval = intervals[intI];
                var intName = Enum.GetName(typeof(TimeInterval), interval);
                count = (int)Math.Floor(GetIntervalCount(interval, ev));
                title = string.Format("{0} to {1}", intName, ev.Title);
            }

            var xmasCountdown = EventCountdownLogic.Countdown.Christmas;
            var tileData = new IconicTileData()
            {
                Title = title,
                WideContent1 = "Event Countdown",
                WideContent2 = string.Empty,
                WideContent3 = string.Empty,
                Count = count,
                IconImage = null,
                SmallIconImage = null

            };

            var appTile = ShellTile.ActiveTiles.FirstOrDefault();
            if (appTile != null)
            {
                appTile.Update(tileData);
            }

        }

        public static TimeInterval[] GetIntervals()
        {
            var intervals = Enum.GetValues(typeof(TimeInterval))
                .Cast<TimeInterval>()
                .Where(ti => ti != TimeInterval.Seconds && ti != TimeInterval.Minutes && ti != TimeInterval.Years)
                .ToArray();

            return intervals;
        }

        public static double GetIntervalCount(TimeInterval timeInterval, Countdown countdownDateTime)
        {
            double count = 0;

            switch (timeInterval)
            {
                case TimeInterval.Days:
                    count = countdownDateTime.NextCountdownDateTime.GetDays;
                    break;
                case TimeInterval.Minutes:
                    count = countdownDateTime.NextCountdownDateTime.GetMinutes;
                    break;
                case TimeInterval.Hours:
                    count = countdownDateTime.NextCountdownDateTime.GetHours;
                    break;
                case TimeInterval.Seconds:
                    count = countdownDateTime.NextCountdownDateTime.GetSeconds;
                    break;
                case TimeInterval.Weeks:
                    count = countdownDateTime.NextCountdownDateTime.GetWeeks;
                    break;
                case TimeInterval.Years:
                    count = countdownDateTime.NextCountdownDateTime.GetYears;
                    break;
            }

            return count;
        }

    }
}
