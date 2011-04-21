using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OxiteMigrator.DataAccess
{
    public class OxiteReader : IDisposable
    {
        private readonly SqlConnection _connection;
        private readonly SqlCommand _command;

        public OxiteReader(string query)
             : this(ConfigurationManager.AppSettings["OxiteConnectionString"], query)
        {
        }

        public OxiteReader(string connection, string query)
        {
            _connection = new SqlConnection(connection);
            _command = new SqlCommand(query, _connection);

            _connection.Open();
        }

        public IEnumerable<dynamic> Execute()
        {
            using (var reader = _command.ExecuteReader())
            {
                foreach (IDataRecord record in reader)
                {
                    yield return new OxiteRecord(record);
                }
            }
        }

        public void Dispose()
        {
            _command.Dispose();
            _connection.Dispose();
        }
    }
}