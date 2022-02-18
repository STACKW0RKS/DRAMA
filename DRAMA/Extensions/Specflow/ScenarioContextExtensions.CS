namespace DRAMA.Extensions.Specflow;

public static class ScenarioContextExtensions
{
    public static void SetProperty<T>(this ScenarioContext context, string key, T value)
    {
        if (context.ContainsKey(key)) context.Set(value, key);
        else context.Add(key, value);
    }

    public static T GetProperty<T>(this ScenarioContext context, string key)
        => context.Get<T>(key) ?? throw new NoMatchException($@"Key ""{key}"" Not Found In The Scenario Context Property Bag");
}
