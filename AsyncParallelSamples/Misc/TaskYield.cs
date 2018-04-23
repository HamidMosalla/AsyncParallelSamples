using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class TaskYield
    {
        public static async Task WorkThenWait()
        {
            //await Task.Yield();
            //long running synchronous operation
            Thread.Sleep(1000);
            Console.WriteLine("work");
            await Task.Delay(1000);
        }

        public static async Task Demo()
        {
            var child = WorkThenWait();
            Console.WriteLine("started");
            await child;
            Console.WriteLine("completed");
        }
    }
}
