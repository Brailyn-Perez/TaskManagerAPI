using Microsoft.AspNetCore.Identity;

namespace TaskManager.Infraestructure.Identity.Interfaces
{
    public interface IUserManagerRepository
    {
        Task CreateUserAsync(IdentityUser user, CancellationToken cancellationToken);
    }
}
