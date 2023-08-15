using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public static class Extensions
    {
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
                   this IEnumerable<TSource> source,
                   Func<TSource, TKey> keySelector,
                   IComparer<TKey> comparer)
        {
            return new OrderedEnumerable<TSource>(source, (x, y) => comparer.Compare(keySelector(x), keySelector(y)));
        }

        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector, Comparer<TKey>.Default);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer)
        {
            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
            this IOrderedEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.ThenBy(keySelector, Comparer<TKey>.Default);
        }
    }
}