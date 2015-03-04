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
using System.Web.Configuration;

namespace HttpWebServer
{
    public class ServerProgram
    {
        
        static HttpListener listener;        
        static string baseUrl = WebConfigurationManager.AppSettings["serverBaseUrlKey"];
        public static HttpListenerContext HttpRequestContext;

        public ServerProgram()
        {
            MainServer();
        }

        static void MainServer()
        {
            
            //Server Root Folder
            //String sMyWebServerRoot = "C:\\WebServerRoot\\";

            Console.WriteLine("Server");
            listener = new HttpListener();
            listener.Prefixes.Add(baseUrl);          
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            listener.Start();


            AppLifecycleManager.RegisterApp<IAppBase, AppFibonacci>("AppFibonacci");
            AppLifecycleManager.RegisterApp<IAppBase, AppTextFileReader>("AppTextFileReader");
            AppLifecycleManager.RegisterApp<IAppBase, AppIndex>("AppIndex");

            IAppBase appfin = AppLifecycleManager.Resolve<IAppBase>("AppFibonacci");
            //appfin.Start();

            IAppBase apptex = AppLifecycleManager.Resolve<IAppBase>("AppTextFileReader");
            apptex.Start();

            IAppBase appindex = AppLifecycleManager.Resolve<IAppBase>("AppIndex");
            appindex.Start();

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
            HttpRequestContext = listener.EndGetContext(result);
            string[] urlParamStr = new string[0];

            //HttpRequestContext.Response.StatusCode = 200;
            //HttpRequestContext.Response.StatusDescription = "OK";

            string url = HttpRequestContext.Request.Url.ToString();

            string appUrl = url.Replace(baseUrl, string.Empty);

            IAppBase theApp = RequestFilter.DoMapping(appUrl);

            if (theApp == default(AppBase))
            {
                return;
            }

            urlParamStr = new string[1];

            urlParamStr[0] = appUrl;

            string appResponseStr = theApp.HandleRequest(urlParamStr);

            var buffer = System.Text.Encoding.UTF8.GetBytes(appResponseStr);

            HttpRequestContext.Response.ContentLength64 = buffer.Length;

            var output = HttpRequestContext.Response.OutputStream;

            output.Write(buffer, 0, buffer.Length);

            output.Close();

            HttpRequestContext.Response.Close();

            return;
        }


    }
}
