using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class RequestFilter
    {
        public static AppBase DoMapping(string url)
        {
            if (url.ToLowerInvariant() == "nextfibonacci")
            {
                return new AppFibonacci();
            }
            else if (url.ToLowerInvariant() == "testfile.txt") 
            {
                return new AppTextFileReader();
            }
            else if (url == "")
            {
                return new AppIndex();
            }

            return new AppBase();
        }
    }
}
