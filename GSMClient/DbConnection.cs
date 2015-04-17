using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSMClient
{
    public class DbConnection : IGSMConnection
    {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }

        private System.Data.Common.DbConnection connection;

        private bool disposed = false;
        public DbConnection(string connectionString, string providerName)
        {
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            this.ConnectionString = connectionString;
            this.ProviderName = providerName;
            System.Data.Common.DbProviderFactory factory;
            factory = System.Data.Common.DbProviderFactories.GetFactory(providerName);
            if (factory != null)
            {
                this.connection = factory.CreateConnection();
                this.connection.ConnectionString = connectionString;
                this.connection.Open();
            }
            else
            {
                throw new InvalidOperationException("Failed creating connection.");
            }
        }

        public void Open()
        {
            if (this.connection.State == System.Data.ConnectionState.Closed)
                this.connection.Open();
        }

        public void Close()
        {
            if (this.connection.State == System.Data.ConnectionState.Open) 
                this.connection.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                this.connection.Close();
                this.connection.Dispose();
            }
            disposed = true;
        }

        public bool IsOpen
        {
            get { return connection.State == System.Data.ConnectionState.Open; }
        }
    }
}
