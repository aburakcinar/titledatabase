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
            ServiceHost host = new ServiceHost(typeof(BSDummy), baseAddresses);
            //var ep = new ServiceEndpoint(typeof(IDummy), new WSHttpBinding(), "IDummy.svc");
            //host.AddServiceEndpoint(ep);
            host.AddServiceEndpoint(typeof(IDummy), new WSHttpBinding(), "IDummy.svc");
            return host;
        }
    }
}