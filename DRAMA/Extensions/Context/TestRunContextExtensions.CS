namespace DRAMA.Extensions.Context;

public static class TestRunContextExtensions
{
    public static void Add<T>(this Dictionary<string, object> dictionary, string key, T value) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            throw new DuplicateException($@"Property With Key ""{key}"" Already Exists In The Test Run Context Property Bag");

        dictionary.Add(key, value);
    }

    public static void Set<T>(this Dictionary<string, object> dictionary, string key, T value) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;

        else dictionary.Add<T>(key, value);
    }

    public static T Get<T>(this Dictionary<string, object> dictionary, string key) where T : notnull
        => dictionary.TryGetValue(key, out object? value) ? (T)value : throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Test Run Context Property Bag");

    public static void Remove<T>(this Dictionary<string, object> dictionary, string key) where T : notnull
    {
        if (dictionary.Remove(key).Equals(false))
            throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Test Run Context Property Bag");
    }

    public static void CreateOrAddToList<T>(this Dictionary<string, object> dictionary, string key, T value) where T : notnull
    {
        if (dictionary.ContainsKey(key).Equals(false))
            dictionary[key] = new List<T> { value };

        else if (dictionary[key] is not List<T>)
            throw new NoMatchException($@"Test Run Context Property With Key ""{key}"" Is Not A ""List<{typeof(T)}>"" Type");

        else (dictionary[key] as List<T> ?? new List<T>()).Add(value);
    }
}
