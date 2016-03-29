using System.Web.Optimization;

namespace Example2.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var admin_globaljs = new string[]
            {
                "~/Areas/Admin/libs/jquery/jquery-1.12.2.min.js",
                "~/Areas/Admin/libs/jeasyui/jquery.easyui.min.js",
                "~/Areas/Admin/libs/jeasyui/locale/easyui-lang-zh_CN.js",
            };
           
            var admin_globalcss = new string[]
            {
                "~/Areas/Admin/libs/jeasyui/themes/default/easyui.css",
                "~/Areas/Admin/css/style.css"
            };

            var admin_zqjs = new string[]
            {
                "~/Areas/Admin/libs/zq/zq.js",
                "~/Areas/Admin/libs/zq/zq.expand.js",
                "~/Areas/Admin/libs/zq/zq.easyui.js",
                "~/Areas/Admin/libs/zq/zq.themes.js",
            };
            //indexjs
            bundles.Add(new ScriptBundle("~/admin/indexjs").Include(admin_globaljs).Include(admin_zqjs));
            //全局js
            bundles.Add(new ScriptBundle("~/admin/globaljs").Include(admin_globaljs));
            //全局css
            bundles.Add(new StyleBundle("~/admin/globalcss").Include(admin_globalcss));
        }
    }
}