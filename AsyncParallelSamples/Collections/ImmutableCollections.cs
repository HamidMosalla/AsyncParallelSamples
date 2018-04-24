using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Collections
{
    public class ImmutableCollections
    {
        public static void UseImmutableStack()
        {
            //return a new instance everytime, but the memory is shared
            var stack1 = ImmutableStack<int>.Empty;
            stack1 = stack1.Push(13);
            stack1 = stack1.Push(7);

            // Displays "7" followed by "13".
            foreach (var item in stack1)
                Console.WriteLine(item);

            int lastItem;
            stack1 = stack1.Pop(out lastItem);
            // lastItem == 7



            var stack2 = ImmutableStack<int>.Empty;

            stack2 = stack2.Push(13);

            var biggerStack = stack2.Push(7);

            // Displays "7" followed by "13".
            foreach (var item in biggerStack)
                Console.WriteLine(item);

            // Only displays "13".
            foreach (var item in stack2)
                Console.WriteLine(item);
        }

        public static void UseImmutableQueue()
        {
            var queue = ImmutableQueue<int>.Empty;
            queue = queue.Enqueue(13);
            queue = queue.Enqueue(7);
            // Displays "13" followed by "7".
            foreach (var item in queue)
                Trace.WriteLine(item);
            int nextItem;
            queue = queue.Dequeue(out nextItem);
            // Displays "13"
            Trace.WriteLine(nextItem);
        }
    }
}
