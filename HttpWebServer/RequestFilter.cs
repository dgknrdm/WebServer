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
            else if (url == "")
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppIndex");
            }
            else if (StringExtensions.After(url,".") == "txt")
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppTextFileReader");
            }
            else
            {
                return AppLifecycleManager.Resolve<IAppBase>("AppNotFound");
            }
        }
    }
}
