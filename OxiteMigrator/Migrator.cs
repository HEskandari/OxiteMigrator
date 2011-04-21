using BlogML.Xml;
using OxiteMigrator.Workers;
using log4net;
using System.Collections.Generic;

namespace OxiteMigrator
{
    public class Migrator
    {
        private static readonly IList<IBlogWorker> Workers;
        private static readonly ILog Log = LogManager.GetLogger(typeof(Migrator));

        static Migrator()
        {
            Workers = new List<IBlogWorker>
            {
                new SiteInfoWorker(),
                new TagWorker(),
                new BlogPostWorker(),
                new BlogMLExportWorker()
            };
        }

        public void Migrate()
        {
            Log.Info("Starting the export process.");

            var blog = new BlogMLBlog();

            foreach (var worker in Workers)
            {
                worker.Process(blog);
            }

            Log.Info("Finished exporting. All done.");
        }
    }
}