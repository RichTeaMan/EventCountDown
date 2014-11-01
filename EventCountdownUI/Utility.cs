using EventCountdownLogic;
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
            page.NavigationService.Navigate(new Uri("/CountdownPage.xaml?cd=" + countdown.Id, UriKind.Relative));
        }

        internal static void NavigateToCountdownPage(this PhoneApplicationPage page, DateTime dateTime)
        {
            page.NavigationService.Navigate(new Uri("/DatePage.xaml?ticks=" + dateTime.Ticks.ToString(), UriKind.Relative));
        }
    }
}
