using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public class AppBase:IAppBase
    {
        protected static bool _isStopped = true;

        public bool IsStopped
        {
            get
            {
                return _isStopped;
            }

            private set { }
        }

        public AppBase() { }

        public   bool Start()
        {
            _isStopped = false;

            return true;
        }

        public   bool Stop()
        {
            _isStopped = true;

            return true;
        }

        public virtual string HandleRequest(string[] parameters)
        {
            return string.Empty;
        }


        //public string ProcessRequest(string[] parameters)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
