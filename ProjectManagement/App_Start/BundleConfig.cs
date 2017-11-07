using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace ProjectManagement
{
    public class BundleConfig
    {        
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/js/jquery-3.1.1.min.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jQueryUI_v1.11.4/jquery-ui.min.js"
                ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/logincss").Include(
                      "~/Css/bootstrap.css",
                      "~/Content/Admin/css/login.css"));
            
            if (ConfigurationManager.AppSettings["CdnHost"] + "" != "")
            {
                //for admin layout css
                bundles.Add(new StyleBundle("~/bundles/admincss", ConfigurationManager.AppSettings["CdnHost"] + "").Include(
                "~/Css/jquery-ui.css",
                "~/Css/bootstrap.css",
                "~/Css/font_fa.css",
                "~/Css/open-sans.css",
                "~/Content/Admin/css/AdminLTE.min.css",
                "~/Content/Admin/css/_all-skins.min.css",
                "~/Content/Admin/css/jquery.Jcrop.min.css"));

                //for admin layout js
                bundles.Add(new ScriptBundle("~/bundles/adminjs", ConfigurationManager.AppSettings["CdnHost"] + "").Include(
                    "~/js/bootstrap.min.js",
                    "~/Content/Admin/js/app.min.js",
                    "~/Content/Admin/js/jquery.Jcrop.js",
                    "~/Content/Admin/js/custom.js"));
            }
            else
            {
                //for admin layout css
                bundles.Add(new StyleBundle("~/bundles/admincss").Include(
                "~/Css/jquery-ui.css",
                "~/Css/bootstrap.css",
                "~/Css/font_fa.css",
                "~/Css/open-sans.css",
                "~/Content/Admin/css/AdminLTE.min.css",
                "~/Content/Admin/css/_all-skins.min.css",
                "~/Content/Admin/css/jquery.Jcrop.min.css"));

                //for admin layout js
                bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                    "~/js/bootstrap.min.js",
                    "~/Content/Admin/js/app.min.js",
                    "~/Content/Admin/js/jquery.Jcrop.js",
                    "~/Content/Admin/js/custom.js"));

                //for client layout
                bundles.Add(new StyleBundle("~/bundles/clientcss").Include(
                "~/Css/jquery-ui.css",
                "~/Css/bootstrap.css",                
                "~/Css/style.css",
                "~/Css/owl.carousel.css",
                "~/Css/respontive.css",
                "~/Css/open-sans.css",
                "~/Css/font_fa.css"
                ));

                //for client layout js
                bundles.Add(new ScriptBundle("~/bundles/clientjs").Include(
                    //"~/js/jquery-3.1.1.min.js",
                    "~/js/owl.carousel.js",
                    "~/scripts/myscript.js"));
            }            
            
            

    
        }
    }
}
