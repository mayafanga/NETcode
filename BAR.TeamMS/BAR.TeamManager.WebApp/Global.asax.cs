using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BAR.TeamManager.WebApp
{
    public class MvcApplication : Spring.Web.Mvc.SpringMvcApplication  //System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Models.IndexManager.GetInstance().StartThread();//开启线程扫描LuceneNet对应的数据队列。
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
