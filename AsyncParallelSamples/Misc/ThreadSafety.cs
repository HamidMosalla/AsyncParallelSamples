using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class ClassWithSharedState
    {
        // all threads will read and write to this same field value
        private string shareState;

        public int NonThreadSafeMethod(string parameter1)
        {
            this.shareState = parameter1;

            int number;

            // Since access to shareState is not synchronised by the class, a separate thread
            // could have changed its value between this thread setting its value at the start 
            // of the method and this line reading its value.
            number = this.shareState.Length;
            return number;
        }


        public class ClassWithoutSharedState
        {
            public int ThreadSafeMethod(string parameter1)
            {
                // each thread will have its own variable for number.
                int number;

                number = parameter1.Length;
                return number;
            }
        }

        private static void IncrementValue(ref int value, int iterations)
        {
            for (int i = 0; i < iterations; i++)
            {
                Interlocked.Increment(ref value);
            }
        }

        public class ImmutableObject
        {
            private int value = 0;

            public ImmutableObject(int value)
            {
                this.value = value;
            }

            public int GetValue()=> this.value;

            public ImmutableObject Add(int value) => new ImmutableObject(this.value + value);
        }
    }
}