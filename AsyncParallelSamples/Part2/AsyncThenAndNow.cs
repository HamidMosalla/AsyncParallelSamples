using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part2
{
    public class AsyncThenAndNow
    {
        //await is not special without async keyword
        public void PrintNumber()
        {
            var await = 2;

            Console.WriteLine(await);
        }

        private Task<decimal> CalculateAmountAsync()
        {
            return Task.FromResult(0.0m);
        }

        private void Then()
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

        private async void Now()
        {
            try
            {
                decimal result = await CalculateAmountAsync();
                Console.WriteLine(result);
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
