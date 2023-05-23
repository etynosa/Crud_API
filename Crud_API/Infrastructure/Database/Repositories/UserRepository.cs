using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;
using Newtonsoft.Json;

namespace Crud_API.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(ILogger<UserRepository> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<UserResponse> GetUsersAsync(int page, int resultsPerPage)
        {
            var httpClient = _httpClientFactory.CreateClient("RandomUser");
            var response = await httpClient.GetAsync($"?page={page}&results={resultsPerPage}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to get users from Randomuser API. Status code: {response.StatusCode}. Content: {content}");
                return null;
            }

            var usersResponse = JsonConvert.DeserializeObject<UserResponse>(content);
           
            return usersResponse;
        }

        public async Task<UserResult> GetUserAsync( long userId)
        {
            var httpClient = _httpClientFactory.CreateClient("RandomUser");
            var response = await httpClient.GetAsync($"?seed={userId}");
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to get user {userId} from Randomuser API. Status code: {response.StatusCode}. Content: {content}");
                return null;
            }

            var usersResponse = JsonConvert.DeserializeObject<UserResponse>(content);
            var user = usersResponse.Results.FirstOrDefault();
            _logger.LogInformation($"Fetched user {userId} from Randomuser API at {DateTime.UtcNow}");

            return user;
        }
    }


}
