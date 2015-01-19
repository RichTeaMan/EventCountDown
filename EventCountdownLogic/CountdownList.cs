using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventCountdownLogic
{
    public class CountdownList : Countdown, IList<Countdown>, ICollection<Countdown>, IEnumerable<Countdown>
    {
        protected List<Countdown> countdowns;

        public CountdownList(string title) : base(title)
        {
            countdowns = new List<Countdown>();
        }

        public override DateTime? GetNextDate(DateTime dateTime)
        {
            var dates = countdowns
                .Select(c => c.GetNextDate(dateTime))
                .Where(dt => dt.HasValue)
                .OrderBy(dt => dt.Value)
                .ToArray();

            return dates.FirstOrDefault();
        }

        public CountdownList AddCountdown(Countdown countdown)
        {
            Add(countdown);
            return this;
        }

        #region Interface Methods

        public Countdown this[int index]
        {
            get
            {
                return countdowns[index];
            }

            set
            {
                countdowns[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return countdowns.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Countdown item)
        {
            countdowns.Add(item);
        }

        public void Clear()
        {
            countdowns.Clear();
        }

        public bool Contains(Countdown item)
        {
            return countdowns.Contains(item);
        }

        public void CopyTo(Countdown[] array, int arrayIndex)
        {
            countdowns.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Countdown> GetEnumerator()
        {
            return countdowns.GetEnumerator();
        }

        public int IndexOf(Countdown item)
        {
            return countdowns.IndexOf(item);
        }

        public void Insert(int index, Countdown item)
        {
            countdowns.Insert(index, item);
        }

        public bool Remove(Countdown item)
        {
            return countdowns.Remove(item);
        }

        public void RemoveAt(int index)
        {
            countdowns.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
