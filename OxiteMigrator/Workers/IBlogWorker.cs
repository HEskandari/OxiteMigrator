using BlogML.Xml;

namespace OxiteMigrator.Workers
{
    public interface IBlogWorker
    {
        void Process(BlogMLBlog blogml);
    }
}