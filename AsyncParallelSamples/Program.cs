using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncParallelSamples.Collections;
using AsyncParallelSamples.Misc;
using AsyncParallelSamples.Part2;
using AsyncParallelSamples.Part5;
using AsyncParallelSamples.TPL_Dataflow;

namespace AsyncParallelSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //new Deadlock();

            //AwaitReleaseThreadToCaller.Print();
            //BlockingSignals.Block();
            //await TaskYield.Demo();
            //ImmutableCollections.UseImmutableStack();
            //await TransformBlockSample.SquareBlock();

            //Task task = null;

            //TaskAwaiter awaiter = task.GetAwaiter();


            //var blog = new ConcurrentCollections();
            //await blog.UseBlockingQueue();
            await ActionBlockExample1.ActionBlockExample1RunAsync();

        }

        public static async Task<int> Task1()
        {
            await Task.Delay(2000);
            return 2;
        }

        public static async Task<int> Task2()
        {
            await Task.Delay(2000);
            return 2;
        }
    }
}
