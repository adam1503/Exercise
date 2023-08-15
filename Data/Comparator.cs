using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Comparator<TSource> : IComparer<TSource>
    {
        private List<Func<TSource, TSource, int>> Comparers;

        public Comparator(Func<TSource, TSource, int> comparer)
        {
            this.Comparers = new List<Func<TSource, TSource, int>>() { comparer };
        }

        public int Compare(TSource? x, TSource? y)
        {
            foreach (var comparer in Comparers)
            {
                int result = comparer(x, y);
                if (result != 0) { return result; }
            }

            return 0;
        }

        public void AddComparer(Func<TSource, TSource, int> comparer)
        {
            Comparers.Add(comparer);
        }
    }
}