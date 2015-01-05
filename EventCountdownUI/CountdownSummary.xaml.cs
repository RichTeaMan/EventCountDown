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

        public Brush SummaryForeground
        {
            get { return SummaryText.Foreground; }
            set { SummaryText.Foreground = value; }
        }

        public double SummaryFontSize
        {
            get { return SummaryText.FontSize; }
            set { SummaryText.FontSize = value; }
        }

        public Brush DetailForeground
        {
            get { return DetailText.Foreground; }
            set { DetailText.Foreground = value; }
        }

        public double DetailFontSize
        {
            get { return DetailText.FontSize; }
            set { DetailText.FontSize = value; }
        }

        public int SecondsToChange { get; private set; }

        private IList<TimeInterval> _detailTimeIntervals;
        public virtual IList<TimeInterval> DetailTimeIntervals
        {
            get
            {
                if (_detailTimeIntervals == null)
                {
                    var intervals = Utility.GetIntervals().Where(ti => ti != TimeInterval.Days).ToArray();
                    _detailTimeIntervals = intervals;
                }
                return _detailTimeIntervals;
            }
            set
            {
                _detailTimeIntervals = value;
            }
        }

        public TimeInterval SummaryTimeInterval { get; set; }

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
            SummaryTimeInterval = TimeInterval.Days;
        }

        public void ResetSecondsToChange()
        {
            var random = Utility.GetRandom;
            SecondsToChange = random.Next(MIN_SECONDS_TO_CHANGE, MAX_SECONDS_TO_CHANGE);
        }

        public void SetIntervalType()
        {
            var count = DetailTimeIntervals.Count();
            var random = Utility.GetRandom;

            var intervalIndex = random.Next(count);
            var interval = DetailTimeIntervals[intervalIndex];
            Interval = interval;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            string summary;
            string detailText = string.Empty;
            if (Countdown != null)
            {
                if (Countdown.IsEventOccurring(DateTime.Now))
                {
                    var sc = new StringCreator()
                        .AddParameter(StringParam.EVENT_NAME, Countdown.Title);
                    summary = sc.BuildString(AppResources.EventOccuringNow);
                }
                else
                {
                    summary = GetSummaryCountdownText();
                    detailText = GetDetailCountdownText();
                }
            }
            else
            {
                summary = AppResources.NoCountdown;
            }
            Dispatcher.BeginInvoke(() => SummaryText.Text = summary);
            Dispatcher.BeginInvoke(() => DetailText.Text = detailText);

            SecondsToChange--;
            if (SecondsToChange <= 0)
            {
                SetIntervalType();
                ResetSecondsToChange();
            }
        }

        private string GetSummaryCountdownText()
        {
            var summary = Utility.GetCountdownText(SummaryTimeInterval, Countdown, AppResources.GeneralCountdown);
            return summary;
        }

        private string GetDetailCountdownText()
        {
            var summary = Utility.GetCountdownText(Interval, Countdown, AppResources.DetailCountdown);
            return summary;
        }
        
    }
}
