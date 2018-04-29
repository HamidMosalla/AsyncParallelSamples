using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part5
{
    class GetAllExceptions
    {
        private static Task ThrowInvalidOperationExceptionAsync() => throw new NotImplementedException();
        private static Task ThrowNotImplementedExceptionAsync() => throw new InvalidOperationException();

        private static async Task ObserveAllExceptionsAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            Task allTasks = Task.WhenAll(task1, task2);
            try
            {
                await allTasks;
            }
            catch
            {
                AggregateException allExceptions = allTasks.Exception;
            }
        }
    }
}
