using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public class AvoidAsyncVoid
    {
        public void UseMethodWithAsyncVoid()
        {
            try
            {
                MethodWithException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async void MethodWithException()
        {
            await Task.Delay(1000);
            throw new Exception("The control doesn't hit the catch line if async void is used.");
        }

    }
}
