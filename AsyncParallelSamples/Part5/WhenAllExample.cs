using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part5
{
    public class WhenAllExample
    {
        public static async Task DownLoadAsync(params string[] downloads)
        {
            var client = new HttpClient();
            foreach (var uri in downloads)
            {
                string content = await client.GetStringAsync(uri);
                UpdateUI(content);
            }
        }

        public static async Task DownLoadAsync2(params string[] downloads)
        {
            var tasks = new List<Task<string>>();

            foreach (var uri in downloads)
            {
                var client = new HttpClient();
                tasks.Add(client.GetStringAsync(uri));
            }
            await Task.WhenAll(tasks);
            tasks.ForEach(t => UpdateUI(t.Result));
        }

        private static void UpdateUI(string content)
        {
            throw new NotImplementedException();
        }
    }
}
