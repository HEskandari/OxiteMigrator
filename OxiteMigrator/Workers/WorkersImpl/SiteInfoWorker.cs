using System.Linq;
using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class SiteInfoWorker : IBlogWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SiteInfoWorker));

        public void Process(BlogMLBlog blogml)
        {
            Log.Info("Fetching Blog information");

            using (var siteQuery = new OxiteReader("SELECT * FROM oxite_Site"))
            using (var areaQuery = new OxiteReader("SELECT * FROM oxite_Area"))
            {
                var blog = siteQuery.Execute().First();
                var area = areaQuery.Execute().First();

                Log.WarnFormat("This version only supports migration from one Blog and one Area.");
                Log.InfoFormat("Migrating '{0}' blog...", blog.SiteDisplayName);

                blogml.RootUrl = string.Format("{0}/{1}", blog.SiteHost, blog.SiteDisplayName);
                blogml.DateCreated = area.CreatedDate;
                blogml.Title = blog.SiteDescription;
                blogml.SubTitle = area.Description;
            }
        }
    }
}