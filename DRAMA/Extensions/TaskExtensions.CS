using System;
using System.Threading;
using System.Threading.Tasks;

namespace DRAMA.Extensions
{
    public static class TaskExtensions
    {
        /// <summary>
        ///     Forces the Task<T> to time out after the specified amount of time.
        /// </summary>
        public static async Task<TResult> TimeoutAfter<TResult>(this Task<TResult> task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

            if (completedTask.Equals(task)) { timeoutCancellationTokenSource.Cancel(); return await task; }
            else { throw new TimeoutException(); }
        }

        /// <summary>
        ///     Forces the Task to time out after the specified amount of time.
        /// </summary>
        public static async Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            using var timeoutCancellationTokenSource = new CancellationTokenSource();
            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

            if (completedTask.Equals(task)) { timeoutCancellationTokenSource.Cancel(); }
            else { throw new TimeoutException(); }
        }
    }
}