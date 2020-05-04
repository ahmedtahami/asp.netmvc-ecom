using System.Web.Optimization;

namespace Oxygen_Atom.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            bundle.Add(new StyleBundle("~/site/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/icon-font.min.css",
                "~/Content/css/plugins.css",
                "~/Content/css/style.css"
                ));
            bundle.Add(new StyleBundle("~/admin/css").Include(
                "~/AdminContent/vendor/fontawesome-free/css/all.min.css",
                "~/AdminContent/vendor/datatables/dataTables.bootstrap4.css",
                "~/AdminContent/css/sb-admin.css"
                ));
            bundle.Add(new ScriptBundle("~/site/scripts").Include(
                "~/Content/js/vendor/jquery-1.12.4.min.js",
                "~/Content/js/popper.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/plugins.js",
                "~/Content/js/main.js"
                ));
            bundle.Add(new ScriptBundle("~/admin/scripts").Include(
                "~/AdminContent/vendor/jquery/jquery.min.js",
                "~/AdminContent/vendor/bootstrap/js/bootstrap.bundle.min.js",
                "~/AdminContent/vendor/jquery-easing/jquery.easing.min.js",
                "~/AdminContent/vendor/chart.js/Chart.min.js",
                "~/AdminContent/vendor/datatables/jquery.dataTables.js",
                "~/AdminContent/vendor/datatables/dataTables.bootstrap4.js",
                "~/AdminContent/js/sb-admin.min.js",
                "~/AdminContent/js/demo/datatables-demo.js",
                "~/AdminContent/js/demo/chart-area-demo.js"
                ));
        }
    }
}