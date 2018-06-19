using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part5
{
    public class TaskAndExceptions
    {
        public static async Task ObserveAllExceptionsAsync()
        {
            var task1 = Task.Run(() => throw new NotImplementedException("NotImplementedException is expected!"));
            var task2 = Task.Run(() => throw new InvalidOperationException("InvalidOperationException is expected!"));

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

        public static async Task OnlyTheFirstOne()
        {
            var task1 = Task.Run(() => throw new NotImplementedException("NotImplementedException is expected!"));
            var task2 = Task.Run(() => throw new InvalidOperationException("InvalidOperationException is expected!"));

            Task allTasks = Task.WhenAll(task1, task2);

            try
            {
                await allTasks;
            }
            catch (AggregateException e)
            {

            }
            catch (Exception e)
            {

            }
        }
    }
}
