using RestSharp;
using RestSharp.Authenticators;
using TestRail.Models;
using TestRail.Services;
using TestRail.Utils;

namespace TestRail.Services_API
{
    public class ApiSteps
    {
        private RestClient Client { get; set; }
        public ApiSteps() 
        {
            var restOption = new RestClientOptions(Configurator.ReadConfiguration().Url)
            {
                Authenticator = new HttpBasicAuthenticator(Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                    Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD"))
            };
            Client = new RestClient(restOption);
        }

        public string CreateProjectAndReturnId(ProjectModel expectedProject)
        {
            const string endPoint = "index.php?/api/v2/add_project";

            var request = new RestRequest(endPoint).AddJsonBody(expectedProject);
            var response = Client.ExecutePost<ProjectModel>(request);
            var actualProject = Newtonsoft.Json.JsonConvert.DeserializeObject<ProjectModel>(response.Content);
            return actualProject.Id;
        }

        public string CreateMilestoneAndReturnId(MilestoneModel expectedMilestone, string projectId)
        {
            const string endPoint = "index.php?/api/v2/add_milestone/{project_id}";

            var request = new RestRequest(endPoint).AddJsonBody(expectedMilestone);
            request.AddUrlSegment("project_id", projectId);
            var response = Client.ExecutePost<MilestoneModel>(request);
            var actualMilestone = Newtonsoft.Json.JsonConvert.DeserializeObject<MilestoneModel>(response.Content);

            return actualMilestone.Id;
        }

        public void DeleteProjectById(string id)
        {
            const string _endPoint = "index.php?/api/v2/delete_project/{project_id}";

            var request = new RestRequest(_endPoint).AddUrlSegment("project_id", id);
            request.AddHeader("Content-Type", "application/json");

            Client.ExecutePost(request);
        }

        public void DeleteMilestoneById(string id)
        {
            const string endPoint = "index.php?/api/v2/delete_milestone/{milestone_id}";

            var request = new RestRequest(endPoint).AddUrlSegment("milestone_id", id);
            request.AddHeader("Content-Type", "application/json");

            Client.ExecutePost(request);
        }
    }
}
