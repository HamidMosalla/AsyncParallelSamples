using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncParallelSamples.TPL_Dataflow
{
    static class ActionBlockExample1
    {
        static public void ActionBlockExample1Run()
        {
            var actionBlock = new ActionBlock<int>(n => Console.WriteLine(n));

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }

            actionBlock.Complete();

            actionBlock.Completion.Wait();

            Console.WriteLine("Done");
        }
    }

    public class ActionBlockSample
    {
        void ActionBlockWithConfiguration()
        {
            Action<int> slowConsumer = x => { };

            var blockConfiguration = new ExecutionDataflowBlockOptions()
            {
                NameFormat = "Type:{0},Id:{1}", // Effects ToString() on block (useful for debugging/logging)
                MaxDegreeOfParallelism = 2, // Up to two tasks will be used to process items
            };

            var consumerBlock = new ActionBlock<int>(slowConsumer, blockConfiguration);
        }

    }
}
