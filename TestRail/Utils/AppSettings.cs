namespace TestRail.Utils
{
    public class AppSettings
    {
        public string? BrowserType { get; set; }
        public double Timeout { get; set; }
        public string? Url { get; set; }
        public DbSettings DbSettings { get; set; }
        public string TestMilestoneId { get; set; }
        public string InvalidMilestoneId { get; set; }
        public string TestProjectId { get; set; }
    }
}
