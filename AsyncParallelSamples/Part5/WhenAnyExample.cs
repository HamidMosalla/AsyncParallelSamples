using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part5
{
    public class WhenAnyExample
    {
        private HttpClient _httpClient;

        public WhenAnyExample(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> FirstRespondingUrlAsync(string urlA, string urlB)
        {
            // Start both downloads concurrently.
            Task<string> downloadTaskA = _httpClient.GetStringAsync(urlA);
            Task<string> downloadTaskB = _httpClient.GetStringAsync(urlB);

            // Wait for either of the tasks to complete.
            Task<string> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

            // Return the length of the data retrieved from that URL.
            string data = await completedTask;

            return data;
        }

        private async Task<HttpResponseMessage> FirstRespondingUrlWithCancellationAsync(string urlA, string urlB, CancellationToken ct)
        {
            // Start both downloads concurrently.
            Task<HttpResponseMessage> downloadTaskA = _httpClient.GetAsync(urlA, ct);
            Task<HttpResponseMessage> downloadTaskB = _httpClient.GetAsync(urlB, ct);

            // Wait for either of the tasks to complete.
            Task<HttpResponseMessage> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

            // Return the length of the data retrieved from that URL.
            HttpResponseMessage data = await completedTask;

            return data;
        }

        async Task UseFirstRespondingUrlWithCancellationAsync()
        {
            var cts = new CancellationTokenSource();

            var result = await FirstRespondingUrlWithCancellationAsync("url1", "url2", cts.Token);

            //Now we can cancel the rest of the tasks since we already got the result we need
            cts.Cancel();
        }
    }
}
