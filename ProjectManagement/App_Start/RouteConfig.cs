using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(png|ico|css|js|gif|jpg|woff|eot|svg|eot|ttf|otf)(/.*)?" });
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @"(\w|\-|\.)*\.(png|ico|css|js|gif|jpg|woff|eot|svg|eot|ttf|otf)(/.*)?" });
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @"Content/(\w|\-|/|\.)*\.(png|ico|css|js|gif|jpg|woff|eot|svg|eot|ttf|otf)(/.*)?‌​" });
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @"Uploads/(\w|\-|/|\.)*\.(png|ico|css|js|gif|jpg|woff|eot|svg|eot|ttf|otf)(/.*)?‌​" });
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @"Uploads/temp/(\w|\-|/|\.)*\.(png|ico|css|js|gif|jpg|woff|eot|svg|eot|ttf|otf)(/.*)?‌​" });

            //Admin
            routes.MapRoute(
                name: "AdminSubForder",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserSubForder",
                url: "{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
