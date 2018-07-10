using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    class Deadlock
    {
        static Deadlock()
        {
            // Let's run the initialization on another thread!
            var thread = new System.Threading.Thread(Initialize);
            thread.Start();
            thread.Join();
        }

        static void Initialize() { /* TODO: Add initialization code */ }

        static void Main() { }
    }
}
