using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace HttpWebServer
{
   public class AppLifecycleManager
    {
        static UnityContainer _theContainer = new UnityContainer();

        public static bool RegisterApp<TFrom, TTo>(string keyName) where TTo : TFrom
        {
            // TODO: gseng - do error checking
            _theContainer.RegisterType<TFrom, TTo>(keyName, new ExternallyControlledLifetimeManager());

            return true;
        }

        public static IAppBase Resolve<T>(string keyName) where T : IAppBase
        {
            return _theContainer.Resolve<T>(keyName);
        }

        public static bool StartAllApps()
        {
            List<ContainerRegistration> regs = _theContainer.Registrations.Where(reg => reg.Name != null && reg.Name.StartsWith("App")).ToList();

            foreach (var reg in regs)
            {
                IAppBase appBase = _theContainer.Resolve<IAppBase>(reg.Name);

                appBase.Start();
            }

            return true;
        }
    }   
}
