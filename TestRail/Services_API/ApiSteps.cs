using RestSharp;
using System.Text.Json;
using TestRail.Models;

namespace TestRail.Services_API
{
    public class ApiSteps
    {
        public int CreateProjectAndReturnId(ProjectModel expectedProject)
        {
            const string endPoint = "/index.php?/api/v2/add_project";

            var apiServices = new ApiServices();
            var restOption = apiServices.CreateOptions(Environment.GetEnvironmentVariable("TESTRAIL_EMAIL"),
                Environment.GetEnvironmentVariable("TESTRAIL_PASSWORD"));
            var client = new RestClient(restOption);
            var response = apiServices.CreatePostRequest(endPoint, client, JsonSerializer.Serialize(expectedProject));
            var actualProject = JsonSerializer.Deserialize<ProjectModel>(response.Content);
            return actualProject.Id;
        }
    }
}
