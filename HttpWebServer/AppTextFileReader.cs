using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpWebServer
{
    class AppTextFileReader : AppBase
    {

        public override string HandleRequest(string[] parameters)
        {
            if (_isStopped)
            {
                // TODO: dogukan - return 404
                ServerProgram.HttpRequestContext.Response.StatusCode = 404;
                string errorString = System.IO.File.ReadAllText(@"C:\WebServerRoot\Error404.html");

                return errorString;
            }

            string filePath = parameters[0];
            string fileTotalPath = @"C:\WebServerRoot\" + filePath;

            if (File.Exists(fileTotalPath))
            {
                return System.IO.File.ReadAllText(fileTotalPath);
            }
            else 
            {
                //TODO - Implement Error 404
                //throw new NotImplementedException();
                ServerProgram.HttpRequestContext.Response.StatusCode = 404;
                string errorString = System.IO.File.ReadAllText(@"C:\WebServerRoot\Error404.html");

                return errorString;
            }

            
        }
    }
}
