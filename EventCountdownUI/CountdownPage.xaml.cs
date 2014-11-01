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

namespace EventCountdownUI
{
    public partial class CountdownPage : PhoneApplicationPage
    {
        public bool DatesBuilt { get; private set; }
        public Countdown Countdown { get; private set; }

        public CountdownPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var key = NavigationContext.QueryString["cd"];
            Countdown = Countdown.GetCountdowns().FirstOrDefault(cd => cd.Id == key);

            if (Countdown == null)
                Name_tb.Text = "Error";
            else
            {
                Name_tb.Text = Countdown.Title;
                BuildDates();
            }
        }

        void BuildDates()
        {
            if (DatesBuilt)
                return;

            int rowCount = 0;
            foreach (var date in Countdown.GetFutureDates().Take(100))
            {
                var dateBlock = new TextBlock()
                {
                    Text = date.DateTime.ToShortDateString(),
                    //Height = 22,
                    Tag = date.DateTime,
                    FontSize = 22
                };
                dateBlock.Tap += dateBlock_Tap;
                var row = new RowDefinition();// { Height = new GridLength(dateBlock.Height + 2) };
                ContentPanel.RowDefinitions.Add(row);
                dateBlock.SetValue(Grid.RowProperty, rowCount);
                ContentPanel.Children.Add(dateBlock);
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