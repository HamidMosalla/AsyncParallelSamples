using System;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Part2
{
    public static class AwaitReleaseThreadToCaller
    {
        private static string result;

        public static void Print()
        {
            SaySomething();
            Console.WriteLine(result);
        }

        static async Task<string> SaySomething()
        {
            await Task.Delay(5);
            //Thread.Sleep(5);
            result = "Hello world!";
            return "Something";
        }
    }
}
