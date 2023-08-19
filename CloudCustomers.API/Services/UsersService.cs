using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomers.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UserApiOptions _apiOptions;
        public UsersService(HttpClient httpClient, IOptions<UserApiOptions> apiOptions)
        {
            _httpClient = httpClient;
            _apiOptions = apiOptions.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiOptions.Endpoint);
            if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            return (await usersResponse.Content.ReadFromJsonAsync<List<User>>())!;
        }
    }


}
