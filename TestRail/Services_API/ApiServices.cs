using RestSharp.Authenticators;
using RestSharp;
using TestRail.Utils;

namespace TestRail.Services_API
{
    public class ApiServices
    {
        public RestClient SetUpClient(RestClientOptions options)
        {
            return new RestClient(options);
        }

        public RestResponse CreateGetRequest(string endPoint, RestClient client)
        {
            return client.ExecuteGet(new RestRequest(endPoint));
        }

        public RestResponse CreatePostRequest(string endPoint, RestClient client, string body)
        {
            return client.ExecutePost(new RestRequest(endPoint).AddJsonBody(body));
        }

        public RestClientOptions CreateOptions(string email, string password)
        {
            return new RestClientOptions(Configurator.ReadConfiguration().Url)
            {
                Authenticator = new HttpBasicAuthenticator(email, password)
            };
        }
    }
}
