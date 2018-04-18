using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncParallelSamples.Misc;
using AsyncParallelSamples.Part2;

namespace AsyncParallelSamples
{
    class Program
    {
        static int value;
        static BlockingSignals blockThingy = new BlockingSignals();

        static void Main(string[] args)
        {
            AwaitReleaseThreadToCaller.Print();

            Task.Run(() => MainAsync());

            var theValue = value;

            blockThingy.InitializeFromAnotherThread();

            theValue = value;
        }

        private static async void MainAsync()
        {
            await Task.Run(() =>
            {
                value = blockThingy.WaitForInitialization();
            });
        }
    }
}
