using System;
using System.IO;
using BlogML.Xml;
using log4net;

namespace OxiteMigrator.Workers
{
    public class BlogMLExportWorker : IBlogWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BlogMLExportWorker));

        public const string ExportFileName = "BlogML-Export.xml";

        public void Process(BlogMLBlog blogml)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ExportFileName);
            Log.InfoFormat("Creating BlogML export file in {0}", path);

            using (var output = new FileStream(path, FileMode.Create))
            {
                Log.Info("Serializing Blog data into file.");
                BlogMLSerializer.Serialize(output, blogml);
            }

            Log.Info("Finished writing the export file.");
        }
    }
}