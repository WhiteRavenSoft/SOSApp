using Microsoft.Practices.Unity;
using System;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Web;

namespace SOSApp.Svc.Infrastructure
{
    public class UnityPerExecutionContextLifetimeManager : LifetimeManager
    {
        class ContainerExtension : IExtension<OperationContext>
        {
            public object Value { get; set; }

            public void Attach(OperationContext owner)
            {

            }

            public void Detach(OperationContext owner)
            {

            }
        }

        Guid _key;

        public UnityPerExecutionContextLifetimeManager() : this(Guid.NewGuid()) { }

        UnityPerExecutionContextLifetimeManager(Guid key)
        {
            if (key == Guid.Empty)
                throw new ArgumentException("Key cannot be empty");

            _key = key;
        }

        public override object GetValue()
        {
            object result = null;

            if (HttpContext.Current != null)
            {
                //HttpContext available ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] != null)
                    result = HttpContext.Current.Items[_key.ToString()];
            }
            //Get object depending on  execution environment ( WCF without HttpContext,HttpContext or CallContext)
            else if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                {
                    result = containerExtension.Value;

                }
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                if (AppDomain.CurrentDomain.IsFullyTrusted)
                {
                    //ensure that we're in full trust
                    result = CallContext.GetData(_key.ToString());
                }
            }


            return result;
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] != null)
                    HttpContext.Current.Items[_key.ToString()] = null;
            }
            else if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension != null)
                    OperationContext.Current.Extensions.Remove(containerExtension);

            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                CallContext.FreeNamedDataSlot(_key.ToString());
            }
        }

        public override void SetValue(object newValue)
        {

            if (HttpContext.Current != null)
            {
                //HttpContext avaiable ( ASP.NET ..)
                if (HttpContext.Current.Items[_key.ToString()] == null)
                    HttpContext.Current.Items[_key.ToString()] = newValue;
            }
            else if (OperationContext.Current != null)
            {
                //WCF without HttpContext environment
                ContainerExtension containerExtension = OperationContext.Current.Extensions.Find<ContainerExtension>();
                if (containerExtension == null)
                {
                    containerExtension = new ContainerExtension()
                    {
                        Value = newValue
                    };

                    OperationContext.Current.Extensions.Add(containerExtension);
                }
            }
            else
            {
                //Not in WCF or ASP.NET Environment, UnitTesting, WinForms, WPF etc.
                if (AppDomain.CurrentDomain.IsFullyTrusted)
                {
                    CallContext.SetData(_key.ToString(), newValue);
                }
            }
        }
    }
}
