using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Misc
{
    public static class FromAsyncVsTaskCompletionSource
    {
        static string SynchronousGetString() => string.Empty;

        static async Task<string> ReadFileAsync(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[fs.Length];

                await Task.Factory.FromAsync(fs.BeginRead, fs.EndRead, buffer, 0, buffer.Length, TaskCreationOptions.None);

                using (var ms = new MemoryStream(buffer))
                {
                    using (var sr = new StreamReader(ms))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }

        private static async Task<byte[]> ReadAsync(string filename)
        {
            var fileInfo = new FileInfo(filename);

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                var buffer = new byte[fileInfo.Length];

                await Task<int>.Factory.FromAsync(stream.BeginRead, stream.EndRead, buffer, 0, buffer.Length, null);

                return buffer;
            }
        }

        public static Task<Socket> AcceptAsync(this Socket socket)
        {
            if (socket == null)
                throw new ArgumentNullException("socket");

            var tcs = new TaskCompletionSource<Socket>();

            socket.BeginAccept(asyncResult =>
            {
                try
                {
                    var s = asyncResult.AsyncState as Socket;
                    var client = s.EndAccept(asyncResult);

                    tcs.SetResult(client);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }

            }, socket);

            return tcs.Task;
        }

        public static void Process()
        {
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo("application.exe")
                {
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            process.Exited += (sender, args) =>
            {
                if (process.ExitCode != 0)
                {
                    var errorMessage = process.StandardError.ReadToEnd();
                    throw new InvalidOperationException(errorMessage);
                }
                Console.WriteLine("The process has exited.");
                process.Dispose();
            };

            process.Start();
        }

        public static Task RunProcessAsync(string processPath)
        {
            var tcs = new TaskCompletionSource<object>();

            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo(processPath)
                {
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            process.Exited += (sender, args) =>
            {
                if (process.ExitCode != 0)
                {
                    var errorMessage = process.StandardError.ReadToEnd();
                    tcs.SetException(new InvalidOperationException("The process did not exit correctly. " +
                                                                   "The corresponding error message was: " + errorMessage));
                }
                else
                {
                    tcs.SetResult(null);
                }
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }
}
