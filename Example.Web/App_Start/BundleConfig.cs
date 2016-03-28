using System.Web.Optimization;

namespace Example.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var admin_globaljs = new string[]
            {
                "~/Areas/Admin/js/jquery.min.js",
                "~/Areas/Admin/js/bootstrap.min.js",
                "~/Areas/Admin/js/plugins/metisMenu/jquery.metisMenu.js",
            };
            var admin_zqjs = new string[]
            {
                "~/Areas/Admin/js/plugins/dataTables/jquery.dataTables.js",
                "~/Areas/Admin/js/zq/zq.js",
                "~/Areas/Admin/js/zq/zq.expand.js"
            };
            var admin_indexjs = new string[]
            {
                "~/Areas/Admin/js/plugins/slimscroll/jquery.slimscroll.min.js",
                "~/Areas/Admin/js/contabs.js",
                "~/Areas/Admin/js/hplus.js"
            };
            var admin_listjs = new string[]
            {
                "~/Areas/Admin/js/plugins/dataTables/jquery.dataTables.js",
                "~/Areas/Admin/js/plugins/dataTables/dataTables.bootstrap.js"
            };
            var admin_globalcss = new string[]
            {
                "~/Areas/Admin/css/bootstrap.min.css",
                "~/Areas/Admin/css/font-awesome.css",
                "~/Areas/Admin/css/animate.css",
                "~/Areas/Admin/css/style.css"
            };
            var admin_listcss = new string[]
            {
                "~/Areas/Admin/css/plugins/dataTables/dataTables.bootstrap.css"
            };
            //全局js
            bundles.Add(new ScriptBundle("~/admin/globaljs").Include(admin_globaljs));
            //全局js
            bundles.Add(new ScriptBundle("~/admin/indexjs").Include(admin_globaljs).Include(admin_indexjs));
            //全局list js
            bundles.Add(new ScriptBundle("~/admin/listjs")
                .Include(admin_globaljs)
                .Include(admin_listjs)
                .Include(admin_zqjs));
            //全局css
            bundles.Add(new StyleBundle("~/admin/globalcss").Include(admin_globalcss));
            //全局list css
            bundles.Add(new StyleBundle("~/admin/listcss").Include(admin_globalcss).Include(admin_listcss));
        }
    }
}