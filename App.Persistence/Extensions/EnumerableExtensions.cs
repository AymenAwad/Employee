using System;
using System.Collections.Generic;

namespace Persistence.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in children.SelectRecursive(selector))
                    yield return child;
            }
        }
    }
}
