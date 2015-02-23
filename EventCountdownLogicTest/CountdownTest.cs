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
                CountdownDateTime previous = cd.GetBeforeDate(firstDate);
                foreach (var d in dates)
                {
                    CountdownDateTime before = cd.GetBeforeDate(d);
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

        [TestMethod]
        public void IsEquivalent()
        {
            var xmas1 = Countdown.Christmas;
            var xmas2 = Countdown.Christmas;

            Assert.IsTrue(xmas1.IsEquivalent(xmas2), "Events should be equivalent.");

            var xmas3 = Countdown.Christmas;
            xmas3.Description = xmas3.Description + xmas3.Description;
            Assert.IsFalse(xmas1.IsEquivalent(xmas3), "Events should not be equivalent.");
        }

        [TestMethod]
        public void AnnualCountdownDeserial()
        {
            var xmas = Countdown.Christmas;

            var serial = xmas.Serliaze();

            var deserial = Countdown.Deserialize(serial);

            Assert.IsTrue(xmas.IsEquivalent(deserial));
        }
    }
}
