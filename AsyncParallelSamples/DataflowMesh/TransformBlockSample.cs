using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncParallelSamples.DataflowMesh
{
    public class TransformBlockSample
    {
        public static async Task SquareBlock()
        {
            var squareRootBlock = new TransformBlock<double, double>(x => Math.Sqrt(x));

            await squareRootBlock.SendAsync(25.0);

           double result = squareRootBlock.Receive();

            double sqrt;

            squareRootBlock.TryReceive(out sqrt);
        }
    }
}
