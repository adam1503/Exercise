using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    public class CompoundComparer<T> : IComparer<T>
    {
        private IComparer<T> first;
        private IComparer<T> second;

        public CompoundComparer(IComparer<T> first, IComparer<T> second)
        {
            this.first = first;
            this.second = second;
        }
        public int Compare(T x, T y)
        {
            int firstResult = first.Compare(x, y);
            if (firstResult != 0)
            {
                return firstResult;
            }

            return second.Compare(x, y);
        }
    }
}