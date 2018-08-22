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
            //https://ericlippert.com/2013/01/31/the-no-lock-deadlock/
            var thread = new System.Threading.Thread(Initialize);
            thread.Start();
            thread.Join();
        }

        static void Initialize() { /* TODO: Add initialization code */ }

        //comment it out for test
        //static void Main() { }
    }
}
