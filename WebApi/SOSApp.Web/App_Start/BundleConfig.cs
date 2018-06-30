using System.Web;
using System.Web.Optimization;

namespace SOSApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/theme").Include(
                      "~/Scripts/metisMenu.min.js",
                      "~/Scripts/raphael.min.js",
                      "~/Scripts/morris.min.js",
                      "~/Scripts/morris-data.js",
                      "~/Scripts/startmin.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/metisMenu.min.css",
                      "~/Content/timeline.css",
                      "~/Content/dataTables/dataTables.bootstrap.css",
                      "~/Content/dataTables/dataTables.responsive.css",
                      "~/Content/startmin.css",
                      "~/Content/morris.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/bootstrap-social.css"));
        }
    }
}
