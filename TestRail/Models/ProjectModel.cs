namespace TestRail.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Announcement { get; set; }
        public bool IsShowAnnouncement { get; set; }
        public string ProjectType { get; set; }
        public bool IsEnableTestCase { get; set; }
        public string DefaultAccessRole { get; set; }
    }
}
