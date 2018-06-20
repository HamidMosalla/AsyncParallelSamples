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

        public static void AggregateException()
        {
            var task1 = Task.Run(() => throw new ArgumentException("This exception is expected!"));

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    // Handle the custom exception.
                    if (e is ArgumentException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    // Rethrow any other exception.
                    else
                    {
                        throw;
                    }
                }
            }
        }

        public static void NestedAggregateException()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                var child1 = Task.Factory.StartNew(() =>
                {
                    var child2 = Task.Factory.StartNew(() => throw new InvalidOperationException("Attached child2 faulted."), TaskCreationOptions.AttachedToParent);

                    throw new InvalidOperationException("Attached child1 faulted.");

                }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                var nestedExceptions = task1.Exception;
                var flatNestedExceptions = task1.Exception.Flatten();
            }
        }

        public static void HanleException()
        {
            var task1 = Task.Run(() => throw new InvalidOperationException("InvalidOperationException exception is expected!"));
            var task2 = Task.Run(() => throw new ArgumentNullException("ArgumentNullException exception is expected!"));

            try
            {
                task2.Wait();
            }
            catch (AggregateException ae)
            {
                // Call the Handle method to handle the custom exception, otherwise rethrow the exception.
                ae.Handle(ex =>
                {
                    if (ex is InvalidOperationException) Console.WriteLine(ex.Message);

                    return ex is InvalidOperationException;
                });
            }
        }
    }
}
