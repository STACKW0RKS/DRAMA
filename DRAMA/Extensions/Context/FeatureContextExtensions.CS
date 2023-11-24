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

    public static void Remove<T>(this FeatureContext context, string key) where T : notnull
    {
        if (context.Remove(key).Equals(false))
            throw new NoMatchException($@"Property With Key ""{key}"" Not Found In The Feature Context Property Bag");
    }

    public static void CreateOrAddToList<T>(this FeatureContext context, string key, T value) where T : notnull
    {
        if (context.ContainsKey(key).Equals(false))
            context[key] = new List<T> { value };

        else if (context[key] is not List<T>)
            throw new NoMatchException($@"Feature Context Property With Key ""{key}"" Is Not A ""List<{typeof(T)}>"" Type");

        else (context[key] as List<T> ?? new List<T>()).Add(value);
    }

    public static void SetStartDateTime(this FeatureContext context, DateTime datetime)
        => context.Set<DateTime>("Feature Start DateTime", datetime);

    public static DateTime GetStartDateTime(this FeatureContext context)
        => context.Get<DateTime>("Feature Start DateTime");

    public static void SetIdentifier(this FeatureContext context, Guid identifier)
        => context.Set<Guid>("Unique Identifier", identifier);

    public static Guid GetIdentifier(this FeatureContext context)
        => context.Get<Guid>("Unique Identifier");

    public static void SetCurrentBrowserTab(this FeatureContext context, IPage page)
        => context.Set<IPage>("Current Browser Tab", page);

    public static IPage GetCurrentBrowserTab(this FeatureContext context)
        => context.Get<IPage>("Current Browser Tab");

    public static void SetCurrentHTTPCallContext(this FeatureContext context, IAPIRequestContext api)
        => context.Set<IAPIRequestContext>("Current API Call Context", api);

    public static IAPIRequestContext GetCurrentHTTPCallContext(this FeatureContext context)
        => context.Get<IAPIRequestContext>("Current API Call Context");

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

    public static void SetAPIAuthenticationToken(this FeatureContext context, string token)
        => context.Set<string>(key: "API Authentication Token", value: token);

    public static string GetAPIAuthenticationToken(this FeatureContext context)
        => context.TryGetValue<string>(key: "API Authentication Token", value: out string token) ? token : string.Empty;
}
