using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpWebServer
{
    class AppIndex : AppBase
    {

        public override string HandleRequest(string[] parameters)
        {
          
            if  (_isStopped)
            {
                // TODO: dogukan - return 404
                HttpException error = new HttpException(404, "It looks like we don't know that address, Are you sure you're in the right place?");
                string errorurl = error.ToString().Replace("System.Web.HttpException (0x80004005): ", string.Empty);
                return errorurl;
            }

            string responseString = System.IO.File.ReadAllText(@"C:\WebServerRoot\index.html");

            return responseString;
        }
    }
}
