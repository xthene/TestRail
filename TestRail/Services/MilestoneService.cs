using MongoDB.Bson;
using MongoDB.Driver;
using NLog;
using TestRail.Connector;
using TestRail.Models;

namespace TestRail.Services
{
    public class MilestoneService
    {
        DbConnector dbConnector;
        IMongoCollection<MilestoneModel> milestonesCollection;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public MilestoneService()
        {
            dbConnector = new DbConnector();
            milestonesCollection = dbConnector.MongoDb.GetCollection<MilestoneModel>("Milestones");
        }

        public async Task<List<MilestoneModel>> GetMilestones() =>
            await milestonesCollection.Find(new BsonDocument()).ToListAsync();

        public async Task<MilestoneModel> GetMilestoneById(string id)
        {
            var objId = new ObjectId(id);
            var result = await milestonesCollection.Find(Builders<MilestoneModel>.Filter.Eq("_id", objId))
                .FirstOrDefaultAsync();

            return result;
    }
    }
}
