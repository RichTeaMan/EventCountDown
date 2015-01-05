using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCountdownUI
{
    public class IntervalNameCount
    {
        public TimeInterval TimeInterval { get; private set; }
        public string TimeIntervalSub { get; private set; }
        public double Count { get; private set; }

        public IntervalNameCount(TimeInterval timeInterval, string timeIntervalSub, double count)
        {
            TimeInterval = timeInterval;
            TimeIntervalSub = timeIntervalSub;
            Count = count;
        }
    }
}
