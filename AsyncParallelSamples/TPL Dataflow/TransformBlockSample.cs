using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncParallelSamples.TPL_Dataflow
{
    public class TransformBlockSample
    {
        public static async Task SquareBlock()
        {
            var squareRootBlock = new TransformBlock<double, double>(x => Math.Sqrt(x));
            var subtractBlock = new TransformBlock<double, double>(item => item - 2.0);

            //var options = new DataflowLinkOptions { PropagateCompletion = true };
            //squareRootBlock.LinkTo(subtractBlock, options);
            //// The first block's completion is automatically propagated to the second block.
            //squareRootBlock.Complete();
            //await subtractBlock.Completion;

            await squareRootBlock.SendAsync(25.0);

           double result = squareRootBlock.Receive();

            double sqrt;

            squareRootBlock.TryReceive(out sqrt);
        }
    }
}
