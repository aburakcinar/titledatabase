using NarayaN.TitleDatabase.Server.Services;
using NarayaN.TitleDatabase.Server.Tools;
using NarayaN.TitleDatabase.Types.Interfaces;
using RouteDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace NarayaN.TitleDatabase.Server
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //RouteTable.Routes.Add(new ServiceRoute("IDummy.svc", new NarayanServiceHostFactory(), typeof(BSDummy)));
            //RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            RegisterServices(RouteTable.Routes);

            MappingConfigs.Config();
        }

        private void RegisterServices(RouteCollection routes)
        {
            var lstServiceTypes = Assembly.GetExecutingAssembly().GetTypes().Where(p => p.BaseType == typeof(BaseService));

            foreach (var item in lstServiceTypes)
            {
                routes.Add(new ServiceRoute(string.Format("Services/{0}.svc", NarayanServiceHostFactory.GetInterfaceType(item).Name), new NarayanServiceHostFactory(), item));
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}