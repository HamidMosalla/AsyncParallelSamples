using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncParallelSamples.Reactive_Extensions
{
    public class WebClientObservableExample
    {

        //public void DownloadStringCompleted()
        //{
        //    var client = new WebClient();
        //    var downloadedStrings = Observable.FromEventPattern(client, "DownloadStringCompleted");
            
        //    downloadedStrings.Subscribe(
        //        data =>
        //        {
        //            var eventArgs = (DownloadStringCompletedEventArgs)data.EventArgs;
        //            if (eventArgs.Error != null)
        //                Trace.WriteLine("OnNext: (Error) " + eventArgs.Error);
        //            else
        //                Trace.WriteLine("OnNext: " + eventArgs.Result);
        //        },
        //        ex => Trace.WriteLine("OnError: " + ex.ToString()),
        //        () => Trace.WriteLine("OnCompleted"));

        //    client.DownloadStringAsync(new Uri("http://invalid.example.com/"));
        //}


    }
}
