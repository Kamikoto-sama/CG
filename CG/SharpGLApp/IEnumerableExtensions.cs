using System.Collections.Generic;

namespace SharpGL
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<(T, T)> Pairs<T>(this IEnumerable<T> items)
        {
            var count = 0;
            var previous = default(T);

            foreach (var item in items)
            {
                var result = (previous, item);
                previous = item;
                if (count++ == 0) continue;
                yield return result;
            }
        }
    }
}