using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        private IEnumerable<TSource> Source;
        private Comparator<TSource> Comparer;

        public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TSource, int> comparer)
        {
            this.Source = source;
            this.Comparer = new Comparator<TSource>(comparer);
        }

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
        {
            Comparer.AddComparer((x, y) => comparer.Compare(keySelector(x), keySelector(y)));
            return this;
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            TSource[] sourceArray = Source.ToArray();
            QuickSort(sourceArray, 0, sourceArray.Length - 1);
            foreach (var item in sourceArray)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void QuickSort(TSource[] elements, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(elements, left, right);
                QuickSort(elements, left, pivot - 1);
                QuickSort(elements, pivot + 1, right);
            }
        }

        private int Partition(TSource[] elements, int left, int right)
        {
            var pivot = elements[left];
            while (true)
            {
                while (Comparer.Compare(elements[left], pivot) < 0) { left++; }
                while (Comparer.Compare(elements[right], pivot) > 0) { right--; }
                if (left < right)
                {
                    if (Comparer.Compare(elements[left], elements[right]) == 0) { return right; }
                    Swap(elements, left, right);
                }

                else { return right; }
            }
        }

        private void Swap(TSource[] sourceArray, int indexLowerValue, int indexHigherValue)
        {
            var temp = sourceArray[indexLowerValue];
            sourceArray[indexLowerValue] = sourceArray[indexHigherValue];
            sourceArray[indexHigherValue] = temp;
        }
    }
}

