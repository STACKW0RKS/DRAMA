namespace DRAMA.Extensions.Specflow;

public static class FeatureContextExtensions
{
    public static void SetProperty<T>(this FeatureContext context, string key, T value)
    {
        if (context.ContainsKey(key)) context.Set(value, key);
        else context.Add(key, value);
    }

    public static T GetProperty<T>(this FeatureContext context, string key)
        => context.TryGetValue(key, out T value) ? value : throw new NoMatchException($@"Key ""{key}"" Not Found In The Feature Context Property Bag");

    public static void SetCurrentBrowserTab(this FeatureContext context, IPage page)
        => context.SetProperty("Current Browser Tab", page);

    public static IPage GetCurrentBrowserTab(this FeatureContext context)
        => context.GetProperty<IPage>("Current Browser Tab");

    public static void SetErrorsHaveOccurred(this FeatureContext context, bool errorsHaveOccurred)
        => context.SetProperty("Errors Have Occurred", context.GetErrorsHaveOccurred() || errorsHaveOccurred);

    public static bool GetErrorsHaveOccurred(this FeatureContext context)
        => context.TryGetValue("Errors Have Occurred", out bool errorsHaveOccurred) && errorsHaveOccurred;
}
