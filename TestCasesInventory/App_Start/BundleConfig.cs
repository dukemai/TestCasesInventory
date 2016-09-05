using System.Web;
using System.Web.Optimization;
using TestCasesInventory.Web.Common.Utils;

namespace TestCasesInventory
{
    public class BundleConfig
    {

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            IncludeBaseScripts(bundles);

            bundles.Add(new ScriptBundle("~/bundles/requirejsconfig").Include("App/requirejs-config.js".AppendJSFolder()));
            bundles.Add(new ScriptBundle("~/bundles/tinymce").Include(
           "tinymce/tinymce.min.js".AppendJSLibFolder()));
            //Style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                   "~/Content/bootstrap.css",
                   "~/Content/site.css"));

        }

        private static void IncludeBaseScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                      "jquery-1.9.1.min.js".AppendJSLibFolder()));

            bundles.Add(new ScriptBundle("~/bundles/requirejs").Include(
                    "require.js".AppendJSLibFolder()));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "jquery.validate.min.js".AppendJSLibFolder(),
                        "jquery.validate.unobtrusive.min.js".AppendJSLibFolder()));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                      "modernizr-*".AppendJSLibFolder()));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
         "underscore-min.js".AppendJSLibFolder()));

            bundles.Add(new ScriptBundle("~/bundles/jquery.cookie").Include(
                  "jquery.cookie.js".AppendJSLibFolder()));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                  "bootstrap.min.js".AppendJSLibFolder(),
                  "respond.min.js".AppendJSLibFolder()));
            bundles.Add(new ScriptBundle("~/bundles/runTestRun").Include(
                       "~/Scripts/run-test-run.js"));
        }
    }
}
