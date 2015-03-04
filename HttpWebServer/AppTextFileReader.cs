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
                HttpException error = new HttpException(404, "It looks like we don't know that address, Are you sure you're in the right place?");
                string errorurl = error.ToString().Replace("System.Web.HttpException (0x80004005): ", string.Empty);
                return errorurl;
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
                return string.Empty;
            }

            
        }
    }
}
