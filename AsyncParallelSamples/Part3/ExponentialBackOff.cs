using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part3
{
    public class ExponentialBackOff
    {
        private void AttemptOperation() { }

        public void ExpWithThradSleep()
        {
            for (int numberOfTry = 0; numberOfTry < 3; numberOfTry++)
            {
                try
                {
                    AttemptOperation();
                    break;
                }
                catch
                {
                    // ignored
                }
                Thread.Sleep(2000);
            }
        }

        public async Task ExpWithTaskDelay()
        {
            for (int numberOfTry = 0; numberOfTry < 3; numberOfTry++)
            {
                try
                {
                    AttemptOperation();
                    break;
                }
                catch
                {
                    // ignored
                }
                await Task.Delay(2000);
            }
        }

        static async Task<string> DownloadStringWithTimeout(string uri)
        {
            using (var client = new HttpClient())
            {
                var stringTask = client.GetStringAsync(uri);
                var timeoutTask = Task.Delay(3000);

                var completedTask = await Task.WhenAny(stringTask, timeoutTask);

                if (completedTask == timeoutTask) return null;

                return await stringTask;
            }
        }
    }
}
