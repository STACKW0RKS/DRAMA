namespace DRAMA.Extensions.TargetFramework;

public static class EnumerableExtensions
{
    /// <summary>
    ///     Returns a new collection composed of the input collection's elements in a random order.
    /// </summary>
    public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> collection)
        => collection.OrderBy(element => Random.Shared.Next());

    /// <summary>
    ///     Returns a random element from the collection.
    /// </summary>
    public static TSource RandomElement<TSource>(this IEnumerable<TSource> collection)
        => collection.Shuffle().First();

    /// <summary>
    ///     Returns TRUE if the collection is empty. Otherwise, returns FALSE.
    /// </summary>
    public static bool None<TSource>(this IEnumerable<TSource> collection)
        => !collection.Any();

    /// <summary>
    ///     Returns TRUE if no element in the collection satisfies the condition specified by a predicate. Otherwise, returns FALSE.
    /// </summary>
    public static bool None<TSource>(this IEnumerable<TSource> collection, Func<TSource, bool> predicate)
        => !collection.Any(predicate);

    /// <summary>
    ///     Returns TRUE if the source collection does not contain the specified element. Otherwise, returns FALSE.
    /// </summary>
    public static bool ContainsNot<TSource>(this IEnumerable<TSource> collection, TSource element)
        => !collection.Contains(element);

    /// <summary>
    ///     Returns TRUE if the source collection does not contain the specified element. Otherwise, returns FALSE.
    ///     <br />
    ///     The method's signature also includes a parameter for specifying the equality comparer.
    /// </summary>
    public static bool ContainsNot<TSource>(this IEnumerable<TSource> collection, TSource element, IEqualityComparer<TSource>? comparer)
        => !collection.Contains(element, comparer);
}
