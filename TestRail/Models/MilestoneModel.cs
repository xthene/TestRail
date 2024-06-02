namespace TestRail.Models
{
    public class MilestoneModel
    {
        public string Name { get; set; }
        public string References { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
