﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsyncParallelSamples.Part2;

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
