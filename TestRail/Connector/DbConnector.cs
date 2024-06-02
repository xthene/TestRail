using MongoDB.Driver;
using NLog;
using TestRail.Utils;

namespace TestRail.Connector
{
    public class DbConnector
    {
        private Logger _logger = LogManager.GetCurrentClassLogger(); 
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase MongoDb { get; set; }

        public DbConnector() 
        {
            var connectionString = $"{Configurator.ReadConfiguration().DbSettings.Db_Driver}://" +
                               $"{Configurator.ReadConfiguration().DbSettings.Db_Server}:" +
                               $"{Configurator.ReadConfiguration().DbSettings.Db_Port}";

            try
            {
                MongoClient = new MongoClient(connectionString);
                MongoDb = MongoClient.GetDatabase(Configurator.ReadConfiguration().DbSettings.Db_Name);
                _logger.Info("Connected to database");
            }
            catch(Exception ex) 
            {
                _logger.Error(ex, "Failed to connect ot database");
                throw;
            }
        }

    }
}
