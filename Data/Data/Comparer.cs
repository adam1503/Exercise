using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class Comparer<TElement, TKey> : IComparer<TElement>
    {
        private Func<TElement, TKey> keySelector;
        private IComparer<TKey> comparer;

        public Comparer(Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
        {
            this.keySelector = keySelector;
            this.comparer = comparer ?? Comparer<TKey>.Default;
        }

        public int Compare(TElement x, TElement y)
        {
            return comparer.Compare(keySelector(x), keySelector(y));
        }
    }
}