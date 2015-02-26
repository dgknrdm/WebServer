using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public class AppFibonacci : AppBase
    {
        private static int _firstFibonacci = 1;
        private static int _secondFibonacci = 1;

        public override string HandleRequest(string[] parameters)
        {
            if (_isStopped)
            {
                // TODO: dogukan - return 404
                throw new NotImplementedException();
            }

            Console.Write(_firstFibonacci + "\n" + _secondFibonacci + "\n");


            int nextFibonacci = _firstFibonacci + _secondFibonacci;

            Console.WriteLine(nextFibonacci);

            _firstFibonacci = _secondFibonacci;
            _secondFibonacci = nextFibonacci;

            return nextFibonacci.ToString();
        }
    }
}
