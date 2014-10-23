using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
