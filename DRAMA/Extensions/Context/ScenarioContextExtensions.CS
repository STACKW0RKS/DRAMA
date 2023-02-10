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
}
