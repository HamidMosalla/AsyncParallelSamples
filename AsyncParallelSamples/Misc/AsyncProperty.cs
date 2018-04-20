using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class AsyncProperty
    {
        // As a cached value.
        public AsyncLazy<int> Data
        {
            get { return _data; }
        }

        private readonly AsyncLazy<int> _data = new AsyncLazy<int>(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return 13;
        });
    }
}
