using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventCountdownLogic;
using System.Collections.Generic;

namespace EventCountdownLogicTest
{
    [TestClass]
    public class CountdownTest
    {
        [TestMethod]
        public void GetEventCountdownBeforeDate()
        {
            var startDate = new DateTime(1900, 1, 1);
            var cds = Countdown.GetCountdowns();
            foreach (var cd in cds)
            {
                var dates = cd.GetFutureCountdownDateTimes(new CountdownDateTime(cd, startDate)).Take(100);
                var firstDate = cd.GetFutureCountdownDateTimes(new CountdownDateTime(cd, startDate)).First();
                CountdownDateTime previous = cd.GetEventCountdownBeforeDate(firstDate.DateTime);
                foreach (var d in dates)
                {
                    var before = cd.GetEventCountdownBeforeDate(d.DateTime);
                    Assert.AreEqual(previous, before,
                        "{0} failed get event before {1}. Returned '{2}', expected '{3}'.",
                        cd.Title,
                        d,
                        before,
                        previous);
                    previous = d;
                }
            }
        }

        [TestMethod]
        public void IsEventOccuring()
        {
            var cds = Countdown.GetCountdowns();
            var startDate = new DateTime(1900, 1, 1);
            var endDate = new DateTime(2199, 12, 31);

            foreach (var cd in cds)
            {
                var occuringDates = new HashSet<DateTime>();
                var dates = cd.GetFutureDates(startDate).TakeWhile(d => d < endDate).ToArray();
                foreach (var d in dates)
                {
                    Assert.IsTrue(cd.IsEventOccuringOnDay(d), "{0} failed for {1}.", cd.Title, d.ToShortDateString());
                    occuringDates.Add(d);
                }

                var date = startDate;
                while ((date = date.AddDays(1)) < endDate)
                {
                    if (!occuringDates.Contains(date))
                    {
                        var occuring = cd.IsEventOccuringOnDay(date);
                        Assert.IsFalse(occuring, "{0} failed for {1}. IsEventOccuringOnDay should be false.", cd.Title, date.ToShortDateString());
                    }
                }
            }
        }
    }
}
