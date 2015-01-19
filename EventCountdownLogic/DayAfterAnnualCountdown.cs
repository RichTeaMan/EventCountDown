﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class DayAfterAnnualCountdown : AnnualCountdown
    {
        public DayOfWeek[] DaysOfWeek { get; protected set; }

        public double DayAdjustment { get; protected set; }

        public DayAfterAnnualCountdown(string title, int day, int month, params DayOfWeek[] daysOfWeek) : base(title, day, month)
        {
            DaysOfWeek = daysOfWeek;
            DayAdjustment = 1.0;
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var dateN = base.GetNextDate(dateTime);
            if (dateN.HasValue)
            {
                var date = dateN.Value;
                while (!DaysOfWeek.Contains(date.DayOfWeek))
                {
                    date = date.AddDays(DayAdjustment);
                }
                return date;
            }
            else
            {
                return null;
            }
        }
    }
}
