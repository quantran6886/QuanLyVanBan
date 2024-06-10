using System.Web;
using System.Web.Optimization;

namespace AppNetShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/diamonds/css").Include(
                      "~/wwwRoot/css/style.css",
                      "~/wwwRoot/images/favicon.ico", "~/wwwRoot/Support/bootstrap-table/bootstrap-table.min.css")
                );

            bundles.Add(new ScriptBundle("~/diamonds/jquery")
                .Include("~/wwwRoot/js/vendor-all.min.js")
                .Include("~/wwwRoot/js/plugins/bootstrap.min.js")
                .Include("~/wwwRoot/js/ripple.js")
                .Include("~/wwwRoot/js/pcoded.min.js")
                .Include("~/wwwRoot/js/menu-setting.min.js")
                .Include("~/wwwRoot/Support/jquery.contextMenu.js")
                .Include("~/wwwRoot/Support/jquery.ui.position.js")
                .Include("~/wwwRoot/js/plugins/apexcharts.min.js")
                .Include("~/wwwRoot/js/pages/dashboard-help.js")
                .Include("~/wwwRoot/Support/bootstrap-table/bootstrap-table.min.js")
                );
        }
    }
}
