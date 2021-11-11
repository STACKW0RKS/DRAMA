namespace DRAMA.Extensions;

public static class TaskExtensions
{
    /// <summary>
    ///     Forces the Task<T> to time out after the specified amount of time.
    /// </summary>
    public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
    {
        using CancellationTokenSource timeoutCancellationTokenSource = new();
        Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

        if (completedTask.Equals(task).Equals(false)) throw new TimeoutException();

        timeoutCancellationTokenSource.Cancel();

        return await task;
    }

    /// <summary>
    ///     Forces the Task to time out after the specified amount of time.
    /// </summary>
    public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
    {
        using CancellationTokenSource timeoutCancellationTokenSource = new();
        Task completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

        if (completedTask.Equals(task)) timeoutCancellationTokenSource.Cancel();
        else throw new TimeoutException();
    }
}