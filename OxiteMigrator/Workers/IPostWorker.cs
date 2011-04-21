using BlogML.Xml;

namespace OxiteMigrator.Workers
{
    public interface IPostWorker
    {
        void Process(BlogMLPost postml);
    }
}