using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookEditorDemo.Models
{
    public static class SortHelper
    {
        public static IEnumerable<TElement> OrderByDirection<TElement, TKey>(this IEnumerable<TElement> sequence, Func<TElement, TKey> selector, bool descending)
        {
            if (descending)
            {
                return sequence.OrderByDescending(selector);
            }
            else
            {
                return sequence.OrderBy(selector);
            }
        }
    }
}
