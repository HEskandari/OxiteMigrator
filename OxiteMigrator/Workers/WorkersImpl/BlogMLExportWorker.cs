using System;
using System.IO;
using BlogML.Xml;
using log4net;

namespace OxiteMigrator.Workers
{
    public class BlogMLExportWorker : IBlogWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BlogMLExportWorker));

        public void Process(BlogMLBlog blogml)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BlogML-Export.xml");
            Log.InfoFormat("Creating BlogML export file in {0}", path);

            var output = new FileStream(path, FileMode.Create);

            Log.Info("Serializing Blog data into file.");
            BlogMLSerializer.Serialize(output, blogml);

            Log.Info("Finished writing the export file.");
        }
    }
}