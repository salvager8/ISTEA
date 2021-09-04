using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace final
{
    public class Global : HttpApplication
    {
        private static int _usuarioLoggeado = 0;
        public static int UsuarioLoggeado
        {
            get { return _usuarioLoggeado; }
            set { _usuarioLoggeado = value; }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}