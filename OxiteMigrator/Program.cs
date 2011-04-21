using System;
using System.Configuration;
using System.IO;
using log4net;
using log4net.Config;

namespace OxiteMigrator
{
    class Program
    {
        static void Main()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
            var logger = LogManager.GetLogger(typeof(Program));

            logger.Info("Oxite Migrator v0.1 by Hadi Eskandari (H.Eskandari@Gmail.com)");

            try
            {
                var connectionString = ConfigurationManager.AppSettings["OxiteConnectionString"];
                logger.InfoFormat("Using connection string: {0}", connectionString);

                new Migrator().Migrate();
            }
            catch (Exception ex)
            {
                var root = ex.GetBaseException();
                logger.Fatal("An unhandled Exception occurred", root);
            }
        }
    }
}
