using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part5
{
    public class ProcessAsTaskComplete
    {
        static async Task<int> ExampleTaskAsync(int val)
        {
            await Task.Delay(TimeSpan.FromSeconds(val));

            return val;
        }

        static async Task AwaitAndProcessAsync(Task<int> task)
        {
            var result = await task;
            Trace.WriteLine(result);
        }

        static async Task ProcessTasksAsync()
        {
            Task<int> task1 = ExampleTaskAsync(2);
            Task<int> task2 = ExampleTaskAsync(3);
            Task<int> task3 = ExampleTaskAsync(1);

            var tasks = new[] { task1, task2, task3 };

            var processingTasks = tasks.Select(AwaitAndProcessAsync).ToList();

            await Task.WhenAll(processingTasks);
        }
    }
}
