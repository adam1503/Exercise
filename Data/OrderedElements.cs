using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class OrderedEnumerable<TSource> : IOrderedEnumerable<TSource>
    {
        private IEnumerable<TSource> source;
        private Func<TSource, TSource, int> compare;
        public OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TSource, int> compare)
        {
            this.source = source;
            this.compare = compare;
        }
        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<TSource> GetEnumerator()
        {
            TSource[] sourceArray = source.ToArray();
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

        private void InsertionSort(TSource[] sourceArray)
        {
            for (int i = 1; i < sourceArray.Length; i++)
            {
                for (int j = i; j > 0 && compare(sourceArray[j - 1], sourceArray[j]) > 0; j--)
                {
                    Swap(sourceArray, j - 1, j);
                }
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
