using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSApp.Svc.Infrastructure
{
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        readonly IUnityContainer _unityContainer;

        public UnityServiceLocator(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _unityContainer.Resolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _unityContainer.ResolveAll(serviceType);
        }
    }
}
