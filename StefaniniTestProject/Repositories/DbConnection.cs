using System;
using System.Configuration;
using System.Data.SqlClient;

namespace StefaniniTestProject.Repositories
{
    public class DbConnection : IDisposable
    {
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
                Connection = null;
            }
        }

        public DbConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"];
            if (connectionString == null)
            {
                throw new Exception("Não foi possível encotrar a string de conexão com o banco de dados no arquivo de configuração.");
            }
            
            this.Connection = new SqlConnection(connectionString.ConnectionString);
        }

        public SqlConnection Connection { get; set; }
    }
}