using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace AsyncParallelSamples.TPL_Dataflow
{
    public class YieldReturnWithTask
    {
        public async Task ReadAsyncDataflowSolution(StreamReader streamReader)
        {
            //public Task<IEnumerable<Data>>  Read(StreamReader streamReader)
            //{
            //    using (var connection = new SqlConnection())
            //    {
            //        while (streamReader.Read() > 0)
            //        {
            //            Task.Delay(1000);
            //            yield return Task.FromResult(1);
            //        }
            //    }
            //}

            var block = new ActionBlock<Data>(
                data => ProcessDataAsync(data),
                new ExecutionDataflowBlockOptions
                {
                    BoundedCapacity = 1000,
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                });

            using (var connection = new SqlConnection())
            {
                // ...
                while (await streamReader.ReadAsync(null, 0, 100).ConfigureAwait(false) > 0)
                {
                    // ...
                    var item = new Data();
                    await block.SendAsync(item);
                }
            }
        }

        public async Task BatchProcessingWithTaskRun()
        {
            var queue = new Queue<int>();

            //while (queue.Any())
            //{
            //    var item = queue.Dequeue();
            //    var task = Task.Run(() =>
            //   {
            //       using (var context = new Resource())
            //       {
            //           context.MyItem.Add(item);

            //           //Suppose we don't have an async version
            //           context.SaveToResource();
            //       }
            //   });
            //}

            var block = new ActionBlock<int>(item =>
            {
                using (var context = new Resource())
                {
                    context.MyItem.Add(item);

                    // Run Calculations

                    context.SaveToResource();
                }
            }, new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = 2 });

            while (queue.Any())
            {
                var item = queue.Dequeue();
                await block.SendAsync(item);
            }

            block.Complete();
            await block.Completion;
        }

        private void ProcessDataAsync(Data data)
        {
            throw new NotImplementedException();
        }
    }

    public class Resource : IDisposable
    {
        public void Dispose()
        {
        }

        public List<int> MyItem { get; set; }

        public Task SaveToResource()
        {
            throw new NotImplementedException();
        }
    }

    public class Data { }
}
