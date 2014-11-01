using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using EventCountdownLogic;
using EventCountdownUI.Resources;
using System.Windows.Media;

namespace EventCountdownUI
{
    public partial class CountdownSummary : UserControl
    {
        const int MIN_SECONDS_TO_CHANGE = 3;
        const int MAX_SECONDS_TO_CHANGE = 8;

        DispatcherTimer Timer;

        public Countdown Countdown { get; set; }
        public TimeInterval Interval { get; set; }

        public new Brush Background
        {
            get { return LayoutRoot.Background; }
            set { LayoutRoot.Background = value; }
        }

        public new Brush Foreground
        {
            get { return SummaryText.Foreground; }
            set { SummaryText.Foreground = value; }
        }

        public new double FontSize
        {
            get { return SummaryText.FontSize; }
            set { SummaryText.FontSize = value; }
        }

        public int SecondsToChange { get; private set; }

        public CountdownSummary()
            : this(null)
        { }

        public CountdownSummary(Countdown countdown)
        {
            Countdown = countdown;
            Interval = TimeInterval.Days;
            ResetSecondsToChange();
            InitializeComponent();
            SetText();
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            SummaryText.TextWrapping = TextWrapping.Wrap;
        }

        public void ResetSecondsToChange()
        {
            var random = Utility.GetRandom;
            SecondsToChange = random.Next(MIN_SECONDS_TO_CHANGE, MAX_SECONDS_TO_CHANGE);
        }

        public void SetIntervalType()
        {
            var count = Enum.GetNames(typeof(TimeInterval)).Count();
            var random = Utility.GetRandom;

            var intervalIndex = random.Next(count);
            var interval = (TimeInterval)intervalIndex;
            Interval = interval;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            string summary;
            if (Countdown != null)
            {

                if (Countdown.IsEventOccurring(DateTime.Now))
                {
                    summary = string.Format("It's {0}!", Countdown.Title);
                }
                else
                {
                    summary = GetCountdownText();
                }
            }
            else
            {
                summary = AppResources.NoCountdown;
            }
            Dispatcher.BeginInvoke(() => SummaryText.Text = summary);

            SecondsToChange--;
            if (SecondsToChange <= 0)
            {
                SetIntervalType();
                ResetSecondsToChange();
            }
        }

        private string GetCountdownText()
        {
            double count = 0;
            string intervalSub = null;

            switch (Interval)
            {
                case TimeInterval.Days:
                    count = Countdown.NextDate.GetDays;
                    if (count == 1)
                        intervalSub = AppResources.DayName;
                    else
                        intervalSub = AppResources.DayPluralName;
                    break;
                case TimeInterval.Minutes:
                    count = Countdown.NextDate.GetMinutes;
                    if (count == 1)
                        intervalSub = AppResources.MinuteName;
                    else
                        intervalSub = AppResources.MinutePluralName;
                    break;
                case TimeInterval.Hours:
                    count = Countdown.NextDate.GetHours;
                    if (count == 1)
                        intervalSub = AppResources.HourName;
                    else
                        intervalSub = AppResources.HourPluralName;
                    break;
                case TimeInterval.Seconds:
                    count = Countdown.NextDate.GetSeconds;
                    if (count == 1)
                        intervalSub = AppResources.SecondName;
                    else
                        intervalSub = AppResources.SecondPluralName;
                    break;
                case TimeInterval.Weeks:
                    count = Countdown.NextDate.GetWeeks;
                    if (count == 1)
                        intervalSub = AppResources.WeekName;
                    else
                        intervalSub = AppResources.WeekPluralName;
                    break;
                case TimeInterval.Years:
                    count = Countdown.NextDate.GetYears;
                    if (count == 1)
                        intervalSub = AppResources.YearName;
                    else
                        intervalSub = AppResources.YearPluralName;
                    break;
            }

            string countStr;

            if (count % 1 == 0)
                countStr = count.ToString("F0");
            else
                countStr = count.ToString("F2");

            var sc = new StringCreator()
                .AddParameter(StringParam.INTERVAL_NAME, intervalSub)
                .AddParameter(StringParam.COUNT, countStr)
                .AddParameter(StringParam.EVENT_NAME, Countdown.Title);

            var summary = sc.BuildString(AppResources.GeneralCountdown);
            return summary;
        }

        public enum TimeInterval
        {
            Seconds,
            Minutes,
            Hours,
            Days,
            Weeks,
            Years
        }

    }
}
