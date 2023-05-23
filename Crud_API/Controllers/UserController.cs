using Crud_API.Infrastructure.Database.Models;
using Crud_API.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserController> logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<UserResponse> GetRandomUsers(int page = 1, int resultsPerPage = 10)
        {
            logger.LogInformation($"Fetching users on page {page}, results per page: {resultsPerPage}...");

            var users = await userRepository.GetUsersAsync(page, resultsPerPage);

            logger.LogInformation($"Fetched users.");

            return (users);
        }

        [HttpGet, Route("{:id}")]
        public async Task<UserResult> GetUser(long id)
        {
            logger.LogInformation($"Fetching user details for ID: {id}...");

            var user = await userRepository.GetUserAsync(id);

            if (user == null)
            {
                logger.LogInformation($"User with ID {id} not found.");
                return null;
            }

            logger.LogInformation($"Fetched user details for ID: {id}.");

            return (user);
        }
    }
}
