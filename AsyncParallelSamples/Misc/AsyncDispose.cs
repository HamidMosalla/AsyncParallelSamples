using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    class AsyncDispose : IDisposable
    {
        private readonly CancellationTokenSource _disposeCts = new CancellationTokenSource();

        public async Task<int> CalculateValueAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(2), _disposeCts.Token);
            return 13;
        }

        public async Task<int> CalculateValueAsync(CancellationToken cancellationToken)
        {
            using (var combinedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _disposeCts.Token))
            {
                await Task.Delay(TimeSpan.FromSeconds(2), combinedCts.Token);
                return 13;
            }
        }

        async Task Test()
        {
            Task<int> task;
            using (var resource = new AsyncDispose())
            {
                task = CalculateValueAsync();
            }
            // Throws OperationCanceledException.
            var result = await task;
        }

        public void Dispose()
        {
            _disposeCts.Cancel();
        }
    }
}
