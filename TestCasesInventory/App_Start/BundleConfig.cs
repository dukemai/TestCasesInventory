using System.Web;
using System.Web.Optimization;

namespace TestCasesInventory
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //Scripts/lib
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/lib/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/lib/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "~/Scripts/lib/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/handlebars").Include(
                   "~/Scripts/lib/handlebars.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
            "~/Scripts/lib/tinymce/tinymce.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.cookie").Include(
                  "~/Scripts/lib/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "~/Scripts/lib/bootstrap.js",
                  "~/Scripts/lib/respond.js"));

            //Script/app

            bundles.Add(new ScriptBundle("~/bundles/list-user-customjs").Include(
                    "~/Scripts/app/list-user-customjs.js"));

            bundles.Add(new ScriptBundle("~/bundles/tab-common-functions").Include(
                   "~/Scripts/app/tab-common-functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymceInit").Include(
                        "~/Scripts/app/TinyMceInit.js"));

            bundles.Add(new ScriptBundle("~/bundles/test-cases-in-test-run").Include(
                      "~/Scripts/app/test-cases-in-test-run.js"));

            bundles.Add(new ScriptBundle("~/bundles/fileControl").Include(
             "~/Scripts/app/file-control-custom.js"));


            //Style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/add-test-case-template").Include(
                     "~/Scripts/Handle-Bars-Templates/add-test-case-template.js"));
        }
    }
}
