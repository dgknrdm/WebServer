using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class RequestFilter
    {
        public static IAppBase DoMapping(string url)
        {
            if (url.ToLowerInvariant() == "nextfibonacci")
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppFibonacci");
            }
            else if (url.ToLowerInvariant() == "testfile.txt") 
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppTextFileReader");
            }
            else if (url == "")
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppIndex");
            }

            return default(IAppBase);
        }
    }
}
