using System;
using System.Collections.Generic;
using System.Linq;

namespace DRAMA.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///     Returns a new collection composed of the input collection's elements in a random order.
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
            => collection.OrderBy(element => new Random().Next());

        /// <summary>
        ///     Returns a random element from the collection.
        /// </summary>
        public static T RandomElement<T>(this IEnumerable<T> collection)
            => collection.Shuffle().First();

        /// <summary>
        ///     Returns TRUE if the collection is empty. Otherwise, returns FALSE.
        /// </summary>
        public static bool None<T>(this IEnumerable<T> collection)
            => !collection.Any();
    }
}