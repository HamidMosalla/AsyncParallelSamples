using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part2
{
    public class AsyncThenAndNow
    {
        private Task<decimal> CalculateAmountAsync()
        {
            return Task.FromResult(0.0m);
        }

        private void PrintAmount()
        {
            Task<decimal> calculationTask = CalculateAmountAsync();

            calculationTask.ContinueWith(t =>
            {
                var errors = t.Exception as AggregateException;

                if (errors == null)
                {
                    Console.WriteLine(t.Result);
                }

                else
                {
                    Exception actualException = errors.InnerExceptions.First();
                    Console.WriteLine(actualException.Message);
                }

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
