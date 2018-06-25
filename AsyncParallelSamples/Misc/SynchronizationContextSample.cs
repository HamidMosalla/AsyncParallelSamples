using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    class SynchronizationContextSample
    {
        public static void DoWorkSynchronizationContext()
        {
            //On UI thread
            var sc = SynchronizationContext.Current;

            ThreadPool.QueueUserWorkItem(delegate
            {
                // do work on ThreadPool
                sc.Post(delegate
                {
                    // do work on the original context (UI)
                }, null);
            });
        }

        public static void DoWorkExecutionContext()
        {
            // ambient state captured into ec
            ExecutionContext ec = ExecutionContext.Capture();

            ExecutionContext.Run(ec, delegate
            {
                // code here will see ec’s state as ambient
            }, null);
        }

        public static async Task UnderTheHood()
        {
            await SomethingAsync();

            RestOfMethod();

            //roughly turns into this
            var task = SomethingAsync();
            var currentSyncContext = SynchronizationContext.Current;

            await task.ContinueWith(delegate
            {
                if (currentSyncContext == null) RestOfMethod();
                else currentSyncContext.Post(delegate { RestOfMethod(); }, null);

            }, TaskScheduler.Current);
        }

        private static void RestOfMethod()
        {
            throw new NotImplementedException();
        }

        private static Task SomethingAsync()
        {
            throw new NotImplementedException();
        }
    }
}
