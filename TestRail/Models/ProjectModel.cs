using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TestRail.Models
{
    public class ProjectModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Announcement { get; set; }
        public bool IsShowAnnouncement { get; set; }
        public int ProjectType { get; set; }
        public bool IsEnableTestCase { get; set; }
        public string DefaultAccessRole { get; set; }
    }
}
