using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncParallelSamples.Collections;
using AsyncParallelSamples.Misc;
using AsyncParallelSamples.Part2;

namespace AsyncParallelSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //AwaitReleaseThreadToCaller.Print();
            //BlockingSignals.Block();
            //await TaskYield.Demo();
            //ImmutableCollections.UseImmutableStack();

            var result2 = await Task.WhenAny(Task1(), Task2());

            var result1 = await Task.WhenAll(Task1(), Task2());

            Task.WaitAny(Task1(), Task2());
            Task.WaitAll(Task1(), Task2());
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
