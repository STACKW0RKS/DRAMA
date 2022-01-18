namespace SPOTLIGHT.Tests;

[TestFixture]
public class TaskExtensionsTests
{
    [TestFixture]
    public class TimeoutAfter
    {
        [Test]
        public async Task Task_With_No_Return_Type_Completes_Before_It_Times_Out()
        {
            Task task = Task.Delay(250);
            await task.TimeoutAfter(new TimeSpan(00, 00, 05));
            Assert.IsTrue(task.IsCompletedSuccessfully, "Task With No Return Type Has Completed Successfully");
        }

        [Test]
        public async Task Task_With_No_Return_Type_Throws_Exception_After_It_Times_Out()
        {
            CancellationTokenSource timeoutCancellationTokenSource = new();
            Task task = Task.Delay(1250, timeoutCancellationTokenSource.Token);

            try { await task.TimeoutAfter(new TimeSpan(00, 00, 01)); }
            catch (TimeoutException) { timeoutCancellationTokenSource.Cancel(); }

            Assert.IsTrue(task.IsCanceled, "Task With No Return Type Has Successfully Timed Out");
        }

        [Test]
        public async Task Task_With_Return_Type_Completes_Before_It_Times_Out()
        {
            Task<bool> task = Task.Run(async () => { await Task.Delay(250); return true; });
            bool result = await task.TimeoutAfter(new TimeSpan(00, 00, 05));
            Assert.IsTrue(result, "Task With Return Type Has Completed Successfully");
        }

        [Test]
        public async Task Task_With_Return_Type_Throws_Exception_After_It_Times_Out()
        {
            bool result;
            CancellationTokenSource timeoutCancellationTokenSource = new();

            Task<bool> task = Task.Run(async () => { await Task.Delay(1250, timeoutCancellationTokenSource.Token); return true; }, timeoutCancellationTokenSource.Token);

            try { result = await task.TimeoutAfter(new TimeSpan(00, 00, 01)); }

            catch (TimeoutException)
            {
                timeoutCancellationTokenSource.Cancel();
                result = false;
            }

            Assert.IsFalse(result, "Task With Return Type Has Successfully Timed Out");
        }
    }
}
