using BlogML.Xml;
using OxiteMigrator.DataAccess;
using log4net;

namespace OxiteMigrator.Workers
{
    public class TrackbackWorker : IPostWorker
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TrackbackWorker));

        public void Process(BlogMLPost postml)
        {
            Log.Debug("Fetching trackbacks.");

            using (var query = new OxiteReader("SELECT * FROM oxite_Trackback WHERE PostID='" + postml.ID + "'"))
            {
                var trackbacks = query.Execute();

                foreach (var track in trackbacks)
                {
                    var trackbackml = new BlogMLTrackback
                    {
                        Approved = false, /* Oxite has no trackback approving */
                        DateCreated = track.CreatedDate,
                        DateModified = track.ModifiedDate,
                        ID = track.TrackbackID.ToString(),
                        Title = track.Title,
                        Url = track.Url
                    };

                    postml.Trackbacks.Add(trackbackml);
                }
            }

            Log.DebugFormat("Finished adding {0} trackbacks.", postml.Trackbacks.Count);
        }
    }
}