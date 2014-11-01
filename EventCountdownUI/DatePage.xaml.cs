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

namespace EventCountdownUI
{
    public partial class DatePage : PhoneApplicationPage
    {
        public bool EventsBuilt { get; private set; }
        public DateTime Date { get; private set; }

        public DatePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var ticksStr = NavigationContext.QueryString["ticks"];
            long ticks;
            if (long.TryParse(ticksStr, out ticks))
            {
                var dateTime = new DateTime(ticks);
                Date = dateTime;
                Name_tb.Text = dateTime.ToShortDateString();
                BuildEvents();
            }
        }

        private void BuildEvents()
        {
            if (EventsBuilt)
                return;

            var events = Countdown.GetCountdowns().Where(cd => cd.IsEventOccuringOnDay(Date)).ToArray();
            int count = events.Length;

            var summary = new TextBlock() { Text = "Event Summary" };
            var row = new RowDefinition();// { Height = new GridLength(summary.Height + 2) };
            ContentPanel.RowDefinitions.Add(row);
            summary.SetValue(Grid.RowProperty, 0);
            ContentPanel.Children.Add(summary);

            if (count > 0)
            {
                var sc = new StringCreator().AddParameter(StringParam.COUNT, count);
                string text = sc.BuildString(AppResources.DayEventsSummary);
                summary.Text = text;

                int rowCount = 1;
                foreach (var cd in events)
                {
                    var eventBlock = new TextBlock() { Text = cd.Title };
                    var eventRow = new RowDefinition();// { Height = new GridLength(eventBlock.Height + 2) };
                    ContentPanel.RowDefinitions.Add(eventRow);
                    eventBlock.SetValue(Grid.RowProperty, rowCount);
                    ContentPanel.Children.Add(eventBlock);
                    rowCount++;
                }
            }
            else
            {
                summary.Text = AppResources.DayNoEventsSummary;
            }
            EventsBuilt = true;
        }
    }
}