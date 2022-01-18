namespace DRAMA.Extensions.TargetFramework;

public static class TaskExtensions
{
    /// <summary>
    ///     Forces the Task to time out after the specified amount of time.
    /// </summary>
    public static async Task TimeoutAfter(this Task task, TimeSpan timeout, string? customExceptionMessage = null)
    {
        using CancellationTokenSource timeoutCancellationTokenSource = new();
        Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

        if (completedTask.Equals(task)) { timeoutCancellationTokenSource.Cancel(); }

        else
        {
            if (customExceptionMessage is null) throw new TimeoutException();
            if (customExceptionMessage is not null) throw new TimeoutException(customExceptionMessage);
        }
    }

    /// <summary>
    ///     Forces the Task<TResult> to time out after the specified amount of time.
    /// </summary>
    public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout, string? customExceptionMessage = null)
    {
        using CancellationTokenSource timeoutCancellationTokenSource = new();
        Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

        if (completedTask.Equals(task).Equals(false))
        {
            if (customExceptionMessage is null) throw new TimeoutException();
            if (customExceptionMessage is not null) throw new TimeoutException(customExceptionMessage);
        }

        timeoutCancellationTokenSource.Cancel();

        return await task;
    }

    // TODO: Add Unit Tests For The Methods Below

    /// <summary>
    ///     Shorthand for GetAwaiter().GetResult() for tasks with no return type.
    ///     This method should only be used in scenarios where the async/await pattern is unavailable, e.g. constructors/finalisers or auto-implemented properties.
    /// </summary>
    public static void Run(this Task task)
        => task.GetAwaiter().GetResult();

    /// <summary>
    ///     Shorthand for GetAwaiter().GetResult() for tasks with a return type.
    ///     This method should only be used in scenarios where the async/await pattern is unavailable, e.g. constructors/finalisers or auto-implemented properties.
    /// </summary>
    public static TResult Get<TResult>(this Task<TResult> task)
        => task.GetAwaiter().GetResult();
}
