using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ServerProgram server = new ServerProgram();
            AppLifecycleManager.RegisterApp<IAppBase, AppFibonacci>("AppFibonacci");
            AppLifecycleManager.RegisterApp<IAppBase, AppTextFileReader>("AppTextFileReader");
            AppLifecycleManager.RegisterApp<IAppBase, AppIndex>("AppIndex");

            IAppBase appBase = null;
            string response = string.Empty;

            AppLifecycleManager.StartAllApps();

            appBase = AppLifecycleManager.Resolve<IAppBase>("AppFibonacci");
            response = appBase.HandleRequest(null);
            Console.WriteLine("Response: " + response);

            appBase = AppLifecycleManager.Resolve<IAppBase>("AppTextFileReader");
            response = appBase.HandleRequest(null);
            Console.WriteLine("Response: " + response);

            appBase = AppLifecycleManager.Resolve<IAppBase>("AppIndex");
            response = appBase.HandleRequest(null);
            Console.WriteLine("Response: " + response);
        }
    }
}
