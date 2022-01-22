using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace School.MvcUi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Session_Start()
        {
            Log.Info($"Session Start: {Session.SessionID}");
        }
        protected void Session_End()
        {
            Log.Info($"Session End: {Session.SessionID}");
        }
    }
}
