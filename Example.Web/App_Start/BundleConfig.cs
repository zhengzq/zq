using System.Collections.Generic;
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
                "~/Areas/Admin/js/bootstrap.min.js"
            };
            var admin_zqjs = new string[]
            {
                "~/Areas/Admin/js/zq/zq.js",
                "~/Areas/Admin/js/zq/zq.expand.js"
            };
            var admin_indexjs = new string[]
            {
                "~/Areas/Admin/js/plugins/metisMenu/jquery.metisMenu.js",
                "~/Areas/Admin/js/plugins/slimscroll/jquery.slimscroll.min.js",
                "~/Areas/Admin/js/contabs.js",
                "~/Areas/Admin/js/hplus.js"
            };
            var admin_listjs = new string[]
            {
                "~/Areas/Admin/js/plugins/dataTables/jquery.dataTables.js",
                "~/Areas/Admin/js/plugins/dataTables/dataTables.bootstrap.js",
                "~/Areas/Admin/js/plugins/dataTables/jquery.dataTables.js",
            };
            var admin_formjs = new string[]
            {
                "~/Areas/Admin/js/plugins/form/jquery.form.js",
                "~/Areas/Admin/js/plugins/validate/jquery.validate.js"
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
            var global_bondle = new ScriptBundle("~/admin/globaljs") { Orderer = new AsIsBundleOrderer() };
            bundles.Add(global_bondle.Include(admin_globaljs));
            //indexjs
            var index_bundle = new ScriptBundle("~/admin/indexjs") { Orderer = new AsIsBundleOrderer() };
            bundles.Add(index_bundle.Include(admin_globaljs).Include(admin_indexjs));
            //list js
            var list_bundle =  new ScriptBundle("~/admin/listjs") { Orderer = new AsIsBundleOrderer() };
            bundles.Add(list_bundle.Include(admin_globaljs)
                .Include(admin_listjs)
                .Include(admin_zqjs));
            //form js
            var form_bundle = new ScriptBundle("~/admin/formjs") { Orderer = new AsIsBundleOrderer() };
            bundles.Add(form_bundle.Include(admin_globaljs)
                .Include(admin_zqjs)
                .Include(admin_formjs));

            //全局css
            bundles.Add(new StyleBundle("~/admin/globalcss").Include(admin_globalcss));
            //全局list css
            bundles.Add(new StyleBundle("~/admin/listcss").Include(admin_globalcss).Include(admin_listcss));
        }
    }
    public class AsIsBundleOrderer : IBundleOrderer
    {
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}