using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using EventCountdownUI.Resources;
using EventCountdownLogic;
using Microsoft.Phone.Scheduler;
using System.Reflection;

namespace EventCountdownUI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private bool builtCountdowns = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Version.Text = GetVersion();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public static string GetVersion()
        {
            var versionAttribute = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true).FirstOrDefault() as AssemblyFileVersionAttribute;

            if (versionAttribute != null)
            {
                return versionAttribute.Version;
            }
            return "";
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            BuildCountdowns();
        }

        private void BuildCountdowns()
        {
            // Only build countdowns if this page is new.
            if (builtCountdowns)
                return;

            var countdowns = Countdown.GetCountdowns()
                .Where(cd => cd.IsEventOccurring(DateTime.Now) || cd.GetNextDate(DateTime.Now) != null)
                .OrderByDescending(cd => cd.IsEventOccurring(DateTime.Now))
                .ThenBy(cd => cd.NextCountdownDateTime.GetSeconds);

            int rowCount = 0;
            foreach (var c in countdowns)
            {
                var summary = new CountdownSummary(c) {
                    Background = ContentPanel.Background
                };
                var row = new RowDefinition(){ Height = new GridLength(summary.Height + 2)};
                ContentPanel.RowDefinitions.Add(row);
                summary.SetValue(Grid.RowProperty, rowCount);
                ContentPanel.Children.Add(summary);
                rowCount++;

                summary.Tap += Summary_Tap;
            }
            builtCountdowns = true;
        }

        private void Summary_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var summary = sender as CountdownSummary;
            if (summary == null)
                return;

            e.Handled = true;
            this.NavigateToCountdownPage(summary.Countdown);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}