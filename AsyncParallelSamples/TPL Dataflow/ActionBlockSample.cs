using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncParallelSamples.TPL_Dataflow
{
    class ActionBlockExample1
    {
        static public async Task ActionBlockExample1RunAsync()
        {
            var actionBlock = new ActionBlock<int>(n => Console.WriteLine(n));

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }

            actionBlock.Complete();

            await actionBlock.Completion;

            Console.WriteLine("Done");
        }
    }

    class ActionBlockExample2
    {
        static public async Task ActionBlockExample2RunAsync()
        {
            var actionBlock = new ActionBlock<int>(n =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(n);
            });

            for (int i = 0; i < 10; i++)
            {
                actionBlock.Post(i);
            }

            actionBlock.Complete();

            await actionBlock.Completion;

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
