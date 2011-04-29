using System.Collections.Generic;
using BlogML;
using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class BlogPostWorker : IBlogWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BlogPostWorker));
        private static readonly IList<IPostWorker> Workers;

        static BlogPostWorker()
        {
            Workers = new List<IPostWorker>
            {
                new CommentWorker(),
                new AnonymousCommentWorker(),
                new TrackbackWorker(),
                new CategoryReferenceWorker()
            };
        }

        public void Process(BlogMLBlog blogml)
        {
            ProcessPosts(blogml);

            foreach(var post in blogml.Posts)
            {
                foreach (var worker in Workers)
                {
                    worker.Process(post);
                }
            }
        }

        private void ProcessPosts(BlogMLBlog blogml)
        {
            Log.Info("Fetching blog posts.");

            using (var query = new OxiteReader("SELECT * FROM oxite_Post"))
            {
                var posts = query.Execute();

                foreach (var post in posts)
                {
                    var postml = new BlogMLPost
                    {
                        Content = new BlogMLContent
                        {
                            ContentType = ContentTypes.Html,
                            Text = post.Body
                        },
                        Excerpt = new BlogMLContent
                        {
                            ContentType = ContentTypes.Html,
                            Text = post.BodyShort
                        },
                        Approved = post.State == (int)RecordStates.Normal,
                        DateCreated = post.CreatedDate,
                        DateModified = post.ModifiedDate,
                        HasExcerpt = !string.IsNullOrWhiteSpace(post.BodyShort),
                        ID = post.PostID.ToString(),
                        PostName = post.Title,
                        PostType = BlogPostTypes.Normal,
                        PostUrl = post.Slug,
                        Title = post.Title,
                    };

                    blogml.Posts.Add(postml);
                }

                Log.InfoFormat("Finished adding {0} posts.", blogml.Posts.Count);
            }
        }
    }
}