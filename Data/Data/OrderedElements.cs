using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class OrderedEnumerable<TElement> : IOrderedEnumerable<TElement>
    {
        private IEnumerable<TElement> source;
        private IComparer<TElement> currentComparer;
        public OrderedEnumerable(IEnumerable<TElement> source, IComparer<TElement> comparer)
        {
            this.source = source;
            this.currentComparer = comparer;
        }
        public IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var secondComparer = new Comparer<TElement, TKey>(keySelector, comparer);
            return new OrderedEnumerable<TElement>(source, new CompoundComparer<TElement>(currentComparer, secondComparer));
        }

        public IEnumerator<TElement> GetEnumerator()
        {
            TElement[] sourceArray = source.ToArray();
            InsertionSort(sourceArray);
            foreach (var item in sourceArray)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void InsertionSort(TElement[] sourceArray)
        {
            for (int i = 1; i < sourceArray.Length; i++)
            {
                for (int j = i; j > 0 && currentComparer.Compare(sourceArray[j - 1], sourceArray[j]) > 0; j--)
                {
                    Swap(sourceArray, j - 1, j);
                }
            }
        }

        private void Swap(TElement[] sourceArray, int indexLowerValue, int indexHigherValue)
        {
            var temp = sourceArray[indexLowerValue];
            sourceArray[indexLowerValue] = sourceArray[indexHigherValue];
            sourceArray[indexHigherValue] = temp;
        }
    }
}
