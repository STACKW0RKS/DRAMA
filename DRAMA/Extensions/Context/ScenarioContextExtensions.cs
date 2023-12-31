namespace DRAMA.Extensions.Context;

public static class ScenarioContextExtensions
{
    public static void Add<T>(this ScenarioContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key))
            throw new DuplicateException($@"Property With Key ""{key}"" Already Exists In The Scenario Context Property Bag");

        context.Add(key, value);
    }

    public static void Set<T>(this ScenarioContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key))
            context[key] = value;

        else context.Add<T>(key, value);
    }

    public static T Get<T>(this ScenarioContext context, string key) where T : notnull
        => context.TryGetValue(key, out T value) ? value : throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Scenario Context Property Bag");

    public static void Remove<T>(this ScenarioContext context, string key) where T : notnull
    {
        if (context.Remove(key).Equals(false))
            throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Scenario Context Property Bag");
    }

    public static void CreateOrAddToList<T>(this ScenarioContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key).Equals(false))
            context[key] = new List<T> { value };

        else if (context[key] is not List<T>)
            throw new NoMatchException($@"Scenario Context Property With Key ""{key}"" Is Not A ""List<{typeof(T)}>"" Type");

        else (context[key] as List<T> ?? new List<T>()).Add(value);
    }

    public static void RemoveFromList<T>(this ScenarioContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key).Equals(false))
            throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Scenario Context Property Bag");

        else if (context[key] is not List<T>)
            throw new NoMatchException($@"Scenario Context Property With Key ""{key}"" Is Not A ""List<{typeof(T)}>"" Type");

        else (context[key] as List<T> ?? throw new NullReferenceException($@"Scenario Context Property With Key ""{key}"" Is NULL")).Remove(value);
    }

    public static void SetStartDateTime(this ScenarioContext context, DateTime datetime)
        => context.Set<DateTime>("Scenario Start DateTime", datetime);

    public static DateTime GetStartDateTime(this ScenarioContext context)
        => context.Get<DateTime>("Scenario Start DateTime");

    public static void SetFeatureIdentifier(this ScenarioContext context, Guid identifier)
        => context.Set<Guid>("Unique Feature Identifier", identifier);

    public static Guid GetFeatureIdentifier(this ScenarioContext context)
        => context.Get<Guid>("Unique Feature Identifier");
}
