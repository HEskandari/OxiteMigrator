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
        public void Exports_Post_Category_Relations()
        {
            var migrator = new Migrator();
            migrator.Migrate();

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, BlogMLExportWorker.ExportFileName);
            var file = new FileStream(path, FileMode.Open);
            var blog = BlogMLSerializer.Deserialize(file);

            var post = blog.Posts[0];
            var categories = post.Categories;

            Assert.NotNull(categories);
            Assert.NotEmpty(categories);
        }
    }
}
