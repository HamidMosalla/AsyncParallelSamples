using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AsyncParallelSamples.Misc;
using AsyncParallelSamples.Part2;

namespace AsyncParallelSamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AwaitReleaseThreadToCaller.Print();
            BlockingSignals.Block();


        }
    }
}
