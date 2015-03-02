using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HttpWebServer
{
   class ServerProgram
    {
        
        static HttpListener listener;        
        static int sayi1 = 0;
        static int sayi2 = 1;

        public ServerProgram()
        {
            MainServer();
        }

        static void MainServer()
        {
            String sMyWebServerRoot = "C:\\WebServerRoot\\";

            Console.WriteLine("Server");
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8097/");
            listener.Prefixes.Add("http://localhost:8097/testFile.txt/");
            listener.Prefixes.Add("http://localhost:8097/NextFibonacci/");
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            listener.Start();

            while (true)
            {
                ProcessRequest();
                
            }
        }

        static void ProcessRequest()
        {
            var startNew = Stopwatch.StartNew();
            var result = listener.BeginGetContext(ListenerCallback, listener);   
            result.AsyncWaitHandle.WaitOne();
            startNew.Stop();
        }

        static void ListenerCallback(IAsyncResult result)
        {
            var context = listener.EndGetContext(result);
            string[] emptyParam = new string[0];
            
            context.Response.StatusCode = 200;
            context.Response.StatusDescription = "OK";
            var receivedText = context.Request.Headers["thread"] + " Received";
            Console.WriteLine("Server: " + receivedText);
            context.Response.Headers["thread"] = receivedText;
            AppFibonacci appfin = new AppFibonacci();
            appfin.Start();

            AppTextFileReader apptex = new AppTextFileReader();
            apptex.Start();

            AppIndex appindex = new AppIndex();
            appindex.Start();

            string url = context.Request.Url.ToString();

            string appUrl = url.Replace("http://localhost:8097/", string.Empty);

            AppBase theApp = RequestFilter.DoMapping(appUrl);

            if (theApp == default(AppBase))
            {
                return;
            }

            string appResponseStr = theApp.HandleRequest(emptyParam);

            var buffer = System.Text.Encoding.UTF8.GetBytes(appResponseStr);

            context.Response.ContentLength64 = buffer.Length;

            var output = context.Response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();

            context.Response.Close();

            return;
        }


    }
}
