using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public class AppBase
    {
        protected static bool _isStopped = true;

        public static bool Start()
        {
            _isStopped = false;

            return true;
        }

        public static bool Stop()
        {
            _isStopped = true;

            return true;
        }

        public virtual string HandleRequest(string[] parameters)
        {
            return string.Empty;
        }
    }
}
