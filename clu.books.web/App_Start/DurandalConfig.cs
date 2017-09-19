using System.Web.Optimization;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(clu.books.web.DurandalConfig), "PreStart")]

namespace clu.books.web
{
    public static class DurandalConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            DurandalBundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}