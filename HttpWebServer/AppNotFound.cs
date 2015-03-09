using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    class AppNotFound : AppBase
    {
        public override string HandleRequest(string[] parameters)
        {
            ServerProgram.HttpRequestContext.Response.StatusCode = 404;
            string errorString = System.IO.File.ReadAllText(@"C:\WebServerRoot\Error404.html");

            return errorString;
        }
    }
            
    
}
