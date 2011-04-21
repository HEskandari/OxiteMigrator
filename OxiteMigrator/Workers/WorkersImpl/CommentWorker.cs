using BlogML;
using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class CommentWorker : IPostWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CommentWorker));

        public void Process(BlogMLPost postml)
        {
            Log.DebugFormat("Fetching comments for PostID {0}", postml.ID);

            using (var query = new OxiteReader("SELECT * FROM oxite_Comment A, oxite_User B WHERE A.CreatorUserID = B.UserID AND Username <> 'Anonymous' AND PostID='" + postml.ID + "'"))
            {
                var comments = query.Execute();

                foreach (var comment in comments)
                {
                    var commentml = new BlogMLComment
                    {
                        ID = comment.CommentID.ToString(),
                        DateCreated = comment.CreatedDate,
                        DateModified = comment.ModifiedDate,
                        Approved = comment.State == (int)RecordStates.Normal,
                        UserName = comment.Username,
                        UserEMail = comment.Email,
                        Content = new BlogMLContent
                        {
                            ContentType = ContentTypes.Html,
                            Text = comment.Body
                        }
                    };

                    postml.Comments.Add(commentml);
                }
            }

            Log.DebugFormat("Finished adding comments.");
        }
    }
}