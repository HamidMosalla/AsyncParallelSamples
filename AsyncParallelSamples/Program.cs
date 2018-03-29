using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            AwaitReleaseThreadToCaller.Print();
        }
    }
}
