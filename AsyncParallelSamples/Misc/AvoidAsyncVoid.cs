using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class AvoidAsyncVoid
    {
        public static void UseMethodWithAsyncVoid()
        {
            try
            {
                MethodWithException();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ex Message" + e.Message);
            }
        }

        public static async void MethodWithException()
        {
            await Task.Delay(1000);
            throw new Exception("The control doesn't hit the catch line if async void is used.");
        }

        public class AvoidAsyncVoidCorrected
        {
            public static async Task UseMethodWithAsyncVoid()
            {
                try
                {
                    await MethodWithException();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ex Message" + e.Message);
                }
            }

            public static async Task MethodWithException()
            {
                await Task.Delay(1000);
                throw new Exception("The control doesn't hit the catch line if async void is used.");
            }
        }
    }
}
