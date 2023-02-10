namespace DRAMA.Extensions.Context;

public static class FeatureContextExtensions
{
    public static void Add<T>(this FeatureContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key))
            throw new DuplicateException($@"Property With Key ""{key}"" Already Exists In The Feature Context Property Bag");

        context.Add(key, value);
    }

    public static void Set<T>(this FeatureContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key))
            context[key] = value;

        else context.Add<T>(key, value);
    }

    public static T Get<T>(this FeatureContext context, string key) where T : notnull
        => context.TryGetValue(key, out T value) ? value : throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Feature Context Property Bag");

    public static void SetCurrentBrowserTab(this FeatureContext context, IPage page)
        => context.Set<IPage>("Current Browser Tab", page);

    public static IPage GetCurrentBrowserTab(this FeatureContext context)
        => context.Get<IPage>("Current Browser Tab");

    public static void SetErrorsHaveOccurred(this FeatureContext context, bool errorsHaveOccurred)
        => context.Set<bool>("Errors Have Occurred", context.GetErrorsHaveOccurred() || errorsHaveOccurred);

    public static bool GetErrorsHaveOccurred(this FeatureContext context)
        => context.TryGetValue("Errors Have Occurred", out bool errorsHaveOccurred) && errorsHaveOccurred;

    public static void SetSkipFeatureSteps(this FeatureContext context, bool skipFeatureSteps)
        => context.Set<bool>("Skip Feature Steps", skipFeatureSteps);

    public static bool GetSkipFeatureSteps(this FeatureContext context)
        => context.TryGetValue("Skip Feature Steps", out bool skipFeatureSteps) && skipFeatureSteps;

    public static void SetDatabaseConnection(this FeatureContext context, IDbConnection databaseConnection)
        => context.Set<IDbConnection>("Database Connection", databaseConnection);

    public static IDbConnection GetDatabaseConnection(this FeatureContext context)
        => context.Get<IDbConnection>("Database Connection");
}
