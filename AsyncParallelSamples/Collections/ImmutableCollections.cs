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

        public static void UseImmutableList()
        {
            var list = ImmutableList<int>.Empty;
            list = list.Insert(0, 13);
            list = list.Insert(0, 7);
            // Displays "7" followed by "13".
            foreach (var item in list)
                Trace.WriteLine(item);
            list = list.RemoveAt(1);
        }

        public static void UseImmutableSet()
        {
            var hashSet = ImmutableHashSet<int>.Empty;
            hashSet = hashSet.Add(13);
            hashSet = hashSet.Add(7);
            // Displays "7" and "13" in an unpredictable order.
            foreach (var item in hashSet)
                Trace.WriteLine(item);
            hashSet = hashSet.Remove(7);


            var sortedSet = ImmutableSortedSet<int>.Empty;
            sortedSet = sortedSet.Add(13);
            sortedSet = sortedSet.Add(7);
            // Displays "7" followed by "13".
            foreach (var item in hashSet)
                Trace.WriteLine(item);
            var smallestItem = sortedSet[0];
            // smallestItem == 7
            sortedSet = sortedSet.Remove(7);
        }

        public static void UseImmutableDictionary()
        {
            var dictionary = ImmutableDictionary<int, string>.Empty;

            dictionary = dictionary.Add(10, "Ten");
            dictionary = dictionary.Add(21, "Twenty-One");
            dictionary = dictionary.SetItem(10, "Diez");

            // Displays "10Diez" and "21Twenty-One" in an unpredictable order.
            foreach (var item in dictionary)
                Trace.WriteLine(item.Key + item.Value);

            var ten = dictionary[10];
            // ten == "Diez"
            dictionary = dictionary.Remove(21);


            var sortedDictionary = ImmutableSortedDictionary<int, string>.Empty;

            sortedDictionary = sortedDictionary.Add(10, "Ten");
            sortedDictionary = sortedDictionary.Add(21, "Twenty-One");
            sortedDictionary = sortedDictionary.SetItem(10, "Diez");

            // Displays "10Diez" followed by "21Twenty-One".
            foreach (var item in sortedDictionary)
                Trace.WriteLine(item.Key + item.Value);
            var ten2 = sortedDictionary[10];
            // ten == "Diez"
            sortedDictionary = sortedDictionary.Remove(21);
        }
    }
}
