namespace DRAMA.Extensions;

public static class JsonElementExtensions
{
    /// <summary>
    ///     Traverses a JsonElement and returns the sub-element with the specified key name.
    ///     <br/>
    ///     EXAMPLE USAGE: response?.Get("one")?.Get("two")?.Get("three")?.GetString();
    /// </summary>
    public static JsonElement? Get(this JsonElement element, string name)
        => element.ValueKind != JsonValueKind.Null && element.ValueKind != JsonValueKind.Undefined && element.TryGetProperty(name, out JsonElement value) ? value : null;

    /// <summary>
    ///     Traverses a JsonElement and returns the sub-element with the specified array index.
    ///     <br/>
    ///     EXAMPLE USAGE: response?.Get(1)?.Get(2)?.Get(3)?.GetString();
    /// </summary>
    public static JsonElement? Get(this JsonElement element, int index)
    {
        if (element.ValueKind is JsonValueKind.Null or JsonValueKind.Undefined) return null;

        JsonElement value = element.EnumerateArray().ElementAtOrDefault(index);

        return value.ValueKind != JsonValueKind.Undefined ? value : null;
    }
}