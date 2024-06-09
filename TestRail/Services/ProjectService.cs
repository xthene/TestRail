using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using TestRail.Connector;
using TestRail.Models;

namespace TestRail.Services
{
    public class ProjectService
    {
        DbConnector _dbConnector;
        IMongoCollection<ProjectModel> _projectsCollection;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ProjectService()
        {
            _dbConnector = new DbConnector();
            _projectsCollection = _dbConnector.MongoDb.GetCollection<ProjectModel>("Projects");
        }

        public async Task<ProjectModel> GetProjectById(string id)
        {
            var objId = new ObjectId(id);
            var result = await _projectsCollection.Find(Builders<ProjectModel>.Filter.Eq("_id", objId))
                .FirstOrDefaultAsync();

            return result;
        }

    }
}
