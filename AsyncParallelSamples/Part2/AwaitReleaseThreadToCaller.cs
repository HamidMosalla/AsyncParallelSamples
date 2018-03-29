using System;
using System.Threading.Tasks;

namespace AsyncParallelSamples
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

    /*
    await Task.Delay(5);) the program will just output a blank line (not “Hello world!”).
    This is because result will still be uninitialized when Console.WriteLine is called.

    Most procedural and object-oriented programmers expect a function to execute from beginning to end,
    or to a return statement,
     before returning to the calling function. This is not the case with C# async functions.
     They only execute up until the first await statement, then return to the caller.
     The function called by await (in this case Task.Delay) is executed asynchronously,
     and the line after the await statement isn’t signaled to execute
     until Task.Delay completes (in 5 milliseconds). However, within that time,
     control has already returned to the caller, which executes the Console.WriteLine statemen
     t on a string that hasn’t yet been initialized.

    Calling await Task.Delay(5) lets the current thread continue what it is doing,
     and if it’s done (pending any awaits), returns it to the thread pool. 
     This is the primary benefit of the async/await mechanism. It allows the CLR to service more 
     requests with less threads in the thread pool.

    Asynchronous programming has become a lot more common, with the prevalence of devices which perform
     over-the-network service requests or database requests for many activities. 
     C# has some excellent programming constructs which greatly ease the task of programming asynchronous methods,
     and a programmer who is aware of them will produce better programs.

    With regard to the second part of the question, if await Task.Delay(5); was replaced with Thread.Sleep(5),
     the program would output Hello world!. An async method without at least one await statement in it operates just
     like a synchronous method; that is, it will execute from beginning to end, or until it encounters a return statement.
     Calling Thread.Sleep() simply blocks the currently running thread, so the Thread.Sleep(5) call just adds 5 milliseconds to the execution time of the SaySomething() method.
    */
}
