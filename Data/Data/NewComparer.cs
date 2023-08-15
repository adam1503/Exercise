using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// aceasta este clasa prin care nu mi-a reusit sa fac o generalizare a celorlalte doua clase,
// ci mai degraba o comasare

namespace Data
{
    public class NewComparer<TSource, TKey>
    {
        private Func<TSource, TKey> keySelector;
        private IComparer<TKey> comparer;
        internal IComparer<TSource> prev;

        public NewComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, IComparer<TSource> prev = null)
        {
            this.prev = prev;
            this.keySelector = keySelector;
            this.comparer = comparer;
        }

        public int Compare(TSource x, TSource y)
        {
            int first = 0;
            if (prev != null) { first = prev.Compare(x, y); }
            return first == 0 ? comparer.Compare(keySelector(x), keySelector(y)) : first;
        }
    }
}