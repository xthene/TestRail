using MongoDB.Driver;
using TestRail.Utils;

namespace TestRail.Connector
{
    public class DbConnector
    {
        public DbConnector() 
        {
            var connectionString = $"Host={Configurator.ReadConfiguration().DbSettings.Db_Server};" +
                               $"Port={Configurator.ReadConfiguration().DbSettings.Db_Port};" +
                               $"Database={Configurator.ReadConfiguration().DbSettings.Db_Name};";
        }

    }
}
