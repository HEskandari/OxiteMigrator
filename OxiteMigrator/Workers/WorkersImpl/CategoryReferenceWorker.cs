using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class CategoryReferenceWorker : IPostWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryReferenceWorker));

        public void Process(BlogMLPost postml)
        {
            Log.DebugFormat("Setting up category references on post {0}", postml.ID);

            using (var query = new OxiteReader("SELECT * FROM oxite_Post A, oxite_PostTagRelationship B WHERE A.PostID=B.PostID AND A.PostID='" + postml.ID + "'"))
            {
                var tagRefs = query.Execute();

                foreach(var tag in tagRefs)
                {
                    var commentRef = new BlogMLCategoryReference
                    {
                        Ref = tag.TagID.ToString()
                    };

                    postml.Categories.Add(commentRef);
                }
            }
        }
    }
}