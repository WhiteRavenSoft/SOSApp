using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOSApp.Svc.Infrastructure
{
    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        public DependencyResolverFactory()
            : this(ConfigurationManager.AppSettings["dependencyResolverTypeName"])
        {
        }

        public DependencyResolverFactory(string resolverTypeName)
        {
            if (String.IsNullOrEmpty(resolverTypeName))
                throw new ArgumentNullException("resolverTypeName");

            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        public IDependencyResolver CreateInstance()
        {
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}
