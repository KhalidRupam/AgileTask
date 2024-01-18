using AgileTask.Models;
using System.Net.Http;

namespace AgileTask.Services.UserService
{
    public class UserService : IUserService
    {
        private HttpClient _httpClient;
        private static string ApiUsers => $"TokenAuth/Authenticate";

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("HttpClient");
        }
        public async Task<string> authenticateUser(User user)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync(ApiUsers, user);
                if (responseMessage.StatusCode==System.Net.HttpStatusCode.OK)
                {
                    var res = await responseMessage.Content.ReadFromJsonAsync<AuthenticationResponse>();
                    return res.Result.AccessToken;
                }
                else
                {
                    return "User Not Found";
                }
               
            }
            catch (Exception ex)
            {
                return "User Not Found";
            }
          

        }
    }
}
