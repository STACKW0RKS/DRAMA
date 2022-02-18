namespace DRAMA.Extensions.Specflow;

public static class TestThreadContextExtensions
{
    public static void SetProperty<T>(this TestThreadContext context, string key, T value)
    {
        if (context.ContainsKey(key)) context.Set(value, key);
        else context.Add(key, value);
    }

    public static T GetProperty<T>(this TestThreadContext context, string key)
        => context.Get<T>(key) ?? throw new NoMatchException($@"Key ""{key}"" Not Found In The Test Thread Context Property Bag");

    public static void SetTestRunProfile(this TestThreadContext context, Profile profile)
        => context.SetProperty("Test Run Profile", profile);

    public static Profile GetTestRunProfile(this TestThreadContext context)
        => context.GetProperty<Profile>("Test Run Profile");
}
