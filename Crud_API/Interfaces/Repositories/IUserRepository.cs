using Crud_API.Infrastructure.Database.Models;

namespace Crud_API.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<UserResponse> GetUsersAsync(int page, int resultsPerPage);
        Task<UserResult> GetUserAsync(long userId);
    }
}
