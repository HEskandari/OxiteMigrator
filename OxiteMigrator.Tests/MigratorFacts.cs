using System;
using System.IO;
using System.Linq;
using BlogML.Xml;
using OxiteMigrator.Workers;
using Xunit;

namespace OxiteMigrator.Tests
{
    public class MigratorFacts
    {
        [Fact]
        public void Can_Export_Database_Into_Export_File()
        {
            var migrator = new Migrator();
            migrator.Migrate();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BlogMLExportWorker.ExportFileName);
            var exportFile = new FileInfo(path);

            Assert.True(exportFile.Exists);
        }

        [Fact]
        public void Can_Deserialize_Exported_File()
        {
            var migrator = new Migrator();
            migrator.Migrate();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BlogMLExportWorker.ExportFileName);
            var file = new FileStream(path, FileMode.Open);
            var blog = BlogMLSerializer.Deserialize(file);

            Assert.NotNull(blog);
            Assert.NotEqual(0, blog.Posts.Count);
            Assert.NotEqual(0, blog.Categories.Count);
        }

        [Fact]
        public void Can_Import_Categories()
        {
            var migrator = new Migrator();
            migrator.Migrate();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BlogMLExportWorker.ExportFileName);
            var file = new FileStream(path, FileMode.Open);
            var blog = BlogMLSerializer.Deserialize(file);

            foreach (BlogMLCategoryReference blogMLCategoryReference in blog.Categories)
            {
                var reference = blogMLCategoryReference;
                foreach (var blogMLCategory in blog.Categories.Where(category => category.ID == reference.Ref))
                {
                }
            }
        }
    }
}
