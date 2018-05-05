using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Collections
{
    public class ConcurrentCollections
    {
        //A blocking queue needs to be shared by multiple threads, so it is usually defined as a private, read-only field:
        //This is currently a queue,but it also can be a stack if you want
        private readonly BlockingCollection<int> _blockingQueue = new BlockingCollection<int>();

        //Like so, stack
        BlockingCollection<int> _blockingStack = new BlockingCollection<int>(new ConcurrentStack<int>());

        //or bag (list)
        BlockingCollection<int> _blockingBag = new BlockingCollection<int>( new ConcurrentBag<int>());

        public static void UseConcurrentDictionary()
        {
            var dictionary = new ConcurrentDictionary<int, string>();

            var newValue = dictionary.AddOrUpdate(0, key => "Zero", (key, oldValue) => "Zero");

            // Using the same "dictionary" as above.
            bool keyExists = dictionary.TryGetValue(0, out var currentValue);

            // Using the same "dictionary" as above.
            bool keyExisted = dictionary.TryRemove(0, out var removedValue);

            dictionary[0] = "Zero";
        }

        public void ProducerBlockingQueue()
        {
            _blockingQueue.Add(7);
            _blockingQueue.Add(13);
            _blockingQueue.CompleteAdding();
        }

        public void ConsumeBlockingQueue()
        {
            // Displays "7" followed by "13".
            foreach (var item in _blockingQueue.GetConsumingEnumerable()) Console.WriteLine(item);
        }

        public async Task UseBlockingQueue()
        {
            var blocking = new ConcurrentCollections();

            Task.Run(() =>
            {
                Thread.Sleep(4000);
                blocking.ProducerBlockingQueue();
            });

            //This method will print after 4000 ms, right after CompleteAdding is called, before that it's blocked
            blocking.ConsumeBlockingQueue();
        }

        public void ProducerBlockingStack()
        {
            _blockingStack.Add(7);
            _blockingStack.Add(13);
            _blockingStack.CompleteAdding();
        }

        public void ConsumeBlockingStack()
        {
            // Displays "7" followed by "13".
            foreach (var item in _blockingStack.GetConsumingEnumerable()) Console.WriteLine(item);
        }

        public async Task UseBlockingCStack()
        {
            var blocking = new ConcurrentCollections();

            Task.Run(() =>
            {
                Thread.Sleep(4000);
                blocking.ProducerBlockingStack();
            });

            //This method will print after 4000 ms, right after CompleteAdding is called, before that it's blocked
            blocking.ConsumeBlockingStack();
        }
    }
}
