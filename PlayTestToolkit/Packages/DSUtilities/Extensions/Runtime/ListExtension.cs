using System.Collections.Generic;
using System.Linq;

namespace Dutchskull.Utilities.Extensions
{
    public static class ListExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list) =>
            list == null || list.Count() <= 0;

        public static bool IsNullOrEmpty<T>(this IList<T> list) =>
            list == null || list.Count <= 0;

        public static bool IsNullOrEmpty<T, Y>(this IDictionary<T, Y> dictionary) =>
            dictionary == null || dictionary.Count <= 0;

        public static bool IsNullOrEmpty<T>(this T[] array) =>
            array == null || array.Length <= 0;
    }
}
