using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part2
{
    public class AsyncThenAndNow
    {
        private Task<decimal> CalculateMeaningOfLifeAsync()
        {
            return Task.FromResult(0.0m);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Task<decimal> calculation = CalculateMeaningOfLifeAsync();

            calculation.ContinueWith(calculationTask =>
            {
                if (!(calculationTask.Exception is AggregateException errors))
                {
                    Console.WriteLine(calculation.Result);
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
