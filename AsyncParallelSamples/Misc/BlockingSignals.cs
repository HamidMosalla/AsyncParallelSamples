using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class BlockingSignals
    {
        private readonly ManualResetEventSlim _initialized =  new ManualResetEventSlim();
        private int _value;

        public int WaitForInitialization()
        {
            _initialized.Wait();
            return _value;
        }

        public void InitializeFromAnotherThread()
        {
            _value = 13;
            _initialized.Set();
        }

        public static void Block()
        {
            var blockThingy = new BlockingSignals();

            int value = 0;

            Task.Run(() =>
            {
                Thread.Sleep(5000);
                blockThingy.InitializeFromAnotherThread();
            });

            value = blockThingy.WaitForInitialization();
        }
    }
}
