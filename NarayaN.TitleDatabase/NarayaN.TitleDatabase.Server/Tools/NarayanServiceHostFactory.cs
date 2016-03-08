using NarayaN.TitleDatabase.Server.Services;
using NarayaN.TitleDatabase.Types.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Web;

namespace NarayaN.TitleDatabase.Server.Tools
{
    public class NarayanServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            ServiceHost host = new ServiceHost(serviceType, baseAddresses);
            //var ep = new ServiceEndpoint(typeof(IDummy), new WSHttpBinding(), "IDummy.svc");
            //host.AddServiceEndpoint(ep);
            var binding = new WSHttpBinding("NarayaWSHttpBinding");
            binding.Security.Mode = SecurityMode.None;
            host.AddServiceEndpoint(GetInterfaceType(serviceType), binding, "");
            //.Behaviors.Add(new WsHttpBehavior());

            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);
            ServiceDebugBehavior sdb = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            sdb.IncludeExceptionDetailInFaults = true;


            return host;
        }

        public static Type GetInterfaceType(Type serviceType)
        {
            var lst = serviceType.GetInterfaces();

            foreach (var item in lst)
            {
                var result = item.GetCustomAttributes(typeof(ServiceContractAttribute),false);

                if (result != null && result.Length > 0)
                    return item;
            }

            return serviceType;
        }
    }
}