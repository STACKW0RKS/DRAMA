using System;
using System.Threading;
using System.Threading.Tasks;
using DRAMA.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TRIAL
{
    [TestClass]
    public class TaskExtensions
    {
        [TestClass]
        public class TimeoutAfter
        {
            [TestMethod]
            public async Task Task_With_No_Return_Type_Completes_Before_It_Times_Out()
            {
                var task = Task.Run(async () => await Task.Delay(250));

                await task.TimeoutAfter(new TimeSpan(00, 00, 05));

                Assert.IsTrue(task.IsCompletedSuccessfully, "Task With No Return Type Has Completed Successfully");
            }

            [TestMethod]
            public async Task Task_With_No_Return_Type_Throws_Exception_After_It_Times_Out()
            {
                var timeoutCancellationTokenSource = new CancellationTokenSource();
                var task = Task.Run(async () => await Task.Delay(1250, timeoutCancellationTokenSource.Token));

                try { await task.TimeoutAfter(new TimeSpan(00, 00, 01)); }
                catch (TimeoutException) { timeoutCancellationTokenSource.Cancel(); }

                Assert.IsTrue(task.IsCanceled, "Task With No Return Type Has Successfully Timed Out");
            }

            [TestMethod]
            public async Task Task_With_Return_Type_Completes_Before_It_Times_Out()
            {
                var task = Task.Run(async () => { await Task.Delay(250); return true; });
                var result = await task.TimeoutAfter(new TimeSpan(00, 00, 05));

                Assert.IsTrue(result, "Task With Return Type Has Completed Successfully");
            }

            [TestMethod]
            public async Task Task_With_Return_Type_Throws_Exception_After_It_Times_Out()
            {
                var timeoutCancellationTokenSource = new CancellationTokenSource();
                var task = Task.Run(async () => { await Task.Delay(1250, timeoutCancellationTokenSource.Token); return true; });
                bool result;

                try { result = await task.TimeoutAfter(new TimeSpan(00, 00, 01)); }
                catch (TimeoutException) { timeoutCancellationTokenSource.Cancel(); result = false; }

                Assert.IsFalse(result, "Task With Return Type Has Successfully Timed Out");
            }
        }
    }
}