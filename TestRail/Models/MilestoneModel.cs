using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TestRail.Models
{
    public class MilestoneModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string References { get; set; }
        public string Description { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        //public DateTime StartDate { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        //public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }

        public virtual bool Equals(MilestoneModel milestone)
        {
            return milestone.Name == Name
                && milestone.References == References
                && milestone.Description == Description
                //&& milestone.StartDate == StartDate
                //&& milestone.EndDate == EndDate
                && milestone.IsCompleted == IsCompleted;
        }
    }
}
