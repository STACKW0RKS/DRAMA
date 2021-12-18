namespace DRAMA.Extensions;

public static class IntegerExtensions
{
    /// <summary>
    ///     Returns the sum of the source value and the defined increment.
    /// </summary>
    public static int IncrementBy(this int value, int increment)
        => value + increment;

    /// <summary>
    ///     Returns the difference between the source value and the defined decrement.
    /// </summary>
    public static int DecrementBy(this int value, int decrement)
        => value - decrement;
}
