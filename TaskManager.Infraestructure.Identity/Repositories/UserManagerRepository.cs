using Microsoft.AspNetCore.Identity;
using TaskManager.Infraestructure.Identity.Interfaces;

namespace TaskManager.Infraestructure.Identity.Repositories
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagerRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task CreateUserAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userManager.CreateAsync(user);
        }
    }
}
