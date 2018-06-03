using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteRaven.Svc.Infrastructure
{
    public interface IDependencyResolverFactory
    {
        IDependencyResolver CreateInstance();
    }
}
