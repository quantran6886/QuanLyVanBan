using System.Web;
using System.Web.Optimization;

namespace AppNetShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Tạo bundle cho các file CSS
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/assets/css/vendor.min.css",
                        "~/assets/css/default/app.min.css",
                        "~/assets/Farmwork/css/notify.css",
                        "~/assets/plugins/datatables.net-bs5/css/dataTables.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-responsive-bs5/css/responsive.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-fixedcolumns-bs5/css/fixedColumns.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-buttons-bs5/css/buttons.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-bs5/css/dataTables.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-responsive-bs5/css/responsive.bootstrap5.min.css",
                        "~/assets/plugins/datatables.net-select-bs5/css/select.bootstrap5.min.css",
                        "~/assets/Farmwork/bootstrap-table/dist/themes/bulma/bootstrap-table-bulma.css",
                        "~/assets/Farmwork/bootstrap-table/dist/bootstrap-table.css",
                        "~/assets/Farmwork/bootstrap-sweetalert/sweetalert.css",
                        "~/assets/Farmwork/custom-select/picker.css",
                        "~/assets/Farmwork/circle-loader/css/style.css",
                        "~/assets/css/Support.css",
                        "~/assets/css/ButtonHover.css"));

            bundles.Add(new ScriptBundle("~/bundles/farmwork").Include(
                   "~/assets/js/jquery-3.4.1.min.js",
                   "~/assets/js/vendor.min.js",
                   "~/assets/js/app.min.js",
                   "~/assets/cdn-cgi/scripts/7d0fa10a/cloudflare-static/rocket-loader.min.js",
                   "~/assets/Farmwork/js/notify.js",
                   "~/assets/Farmwork/bootstrap-table/dist/bootstrap-table.js",
                   "~/assets/Farmwork/bootstrap-sweetalert/sweetalert.js",
                   "~/assets/Logout.js",
                   "~/assets/Farmwork/custom-select/Picker.js",
                   "~/assets/Farmwork/circle-loader/js/script.js",
                   "~/assets/Farmwork/CutomModal/fixmodal.js",
                   "~/assets/Support.js"));

            BundleTable.EnableOptimizations = true;
        }
    }
}
