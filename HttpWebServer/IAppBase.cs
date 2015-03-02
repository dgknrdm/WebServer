using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
   public interface IAppBase
    {
        bool Stop();

        bool Start();

        string HandleRequest(string[] parameters);
    }
}
