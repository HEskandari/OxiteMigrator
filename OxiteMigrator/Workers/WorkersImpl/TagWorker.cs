using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class TagWorker : IBlogWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TagWorker));

        public void Process(BlogMLBlog blogml)
        {
            Log.Info("Fetching Tags.");

            using (var query = new OxiteReader("SELECT * FROM oxite_Tag"))
            {
                var tags = query.Execute();

                foreach (var tag in tags)
                {
                    var category = new BlogMLCategory
                    {
                        Approved = true,
                        DateCreated = tag.CreatedDate,
                        DateModified = tag.CreatedDate,
                        Description = tag.TagName,
                        ID = tag.TagID.ToString(),
                        ParentRef = tag.TagID.ToString() == tag.ParentTagID.ToString() ? "0" : tag.ParentTagID.ToString(),
                        Title = tag.TagName
                    };

                    blogml.Categories.Add(category);
                }
            }

            Log.InfoFormat("Finished adding {0} tags.", blogml.Categories.Count);
        }
    }
}