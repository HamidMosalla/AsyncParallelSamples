using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Collections
{
    public static class ConcurrentCollections
    {

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

    }
}
