using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EventCountdownLogic;
using EventCountdownUI.Resources;
using System.Windows.Threading;

namespace EventCountdownUI
{
    public partial class CountdownPage : PhoneApplicationPage
    {
        public bool DatesBuilt { get; private set; }
        public bool IntervalsBuilt { get; private set; }
        public Countdown Countdown { get; private set; }

        DispatcherTimer Timer;

        List<Tuple<TimeInterval, TextBlock>> intervalTextBlocks;

        public CountdownPage()
        {
            InitializeComponent();

            Timer = new DispatcherTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;

            intervalTextBlocks = new List<Tuple<TimeInterval, TextBlock>>();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateIntervals();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var key = NavigationContext.QueryString["cd"];
            Countdown = Countdown.GetCountdowns().FirstOrDefault(cd => cd.Title == key);

            if (Countdown == null)
                Name_tb.Text = "Error";
            else
            {
                Name_tb.Text = Countdown.Title;
                BuildIntervals();
                BuildDates();
            }

            Timer.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            Timer.Stop();
        }

        void UpdateIntervals()
        {
            foreach (var intervalBlock in intervalTextBlocks)
            {
                var text = Utility.GetCountdownText(intervalBlock.Item1, Countdown, AppResources.IntervalListCountdown);
                intervalBlock.Item2.Text = text;
            }
        }

        void BuildIntervals()
        {
            if (IntervalsBuilt)
                return;

            IntervalPanel.RowDefinitions.Clear();
            int rowCount = 0;
            foreach (var interval in Utility.GetIntervals())
            {
                var intervalBlock = new TextBlock()
                {
                    FontSize = 18
                };
                var row = new RowDefinition();// { Height = new GridLength(dateBlock.Height + 2) };
                IntervalPanel.RowDefinitions.Add(row);
                intervalBlock.SetValue(Grid.RowProperty, rowCount);
                IntervalPanel.Children.Add(intervalBlock);
                rowCount++;

                intervalTextBlocks.Add(new Tuple<TimeInterval, TextBlock>(interval, intervalBlock));
            }
            UpdateIntervals();
            IntervalsBuilt = true;
        }

        void BuildDates()
        {
            if (DatesBuilt)
                return;

            int rowCount = 0;
            var dates = Countdown.GetFutureDates().Take(10).ToArray();
            foreach (var date in dates)
            {
                var dateBlock = new TextBlock()
                {
                    Text = date.ToLongDateString(),
                    Tag = date,
                    FontSize = 18
                };
                dateBlock.Tap += dateBlock_Tap;
                var row = new RowDefinition();// { Height = new GridLength(dateBlock.Height + 2) };
                DatePanel.RowDefinitions.Add(row);
                dateBlock.SetValue(Grid.RowProperty, rowCount);
                DatePanel.Children.Add(dateBlock);
                rowCount++;
            }
            DatesBuilt = true;
        }

        private void dateBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var textblock = sender as TextBlock;
            if (textblock != null)
            {
                try
                {
                    var dateTime = (DateTime)textblock.Tag;
                    this.NavigateToCountdownPage(dateTime);
                }
                catch
                { }
            }
        }

    }
}