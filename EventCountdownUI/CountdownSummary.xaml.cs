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
        DispatcherTimer Timer;

        public Countdown Countdown { get; set; }

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

        public CountdownSummary()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            SummaryText.TextWrapping = TextWrapping.Wrap;
            SetText();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            if (Countdown != null)
            {
                var sc = new StringCreator()
                    .AddParameter(StringParam.DAY_COUNT, Countdown.GetDays)
                    .AddParameter(StringParam.EVENT_NAME, Countdown.Title);
                SummaryText.Text = sc.BuildString(AppResources.DayCountdown);
            }
            else
            {
                SummaryText.Text = AppResources.NoCountdown;
            }
        }

    }
}
