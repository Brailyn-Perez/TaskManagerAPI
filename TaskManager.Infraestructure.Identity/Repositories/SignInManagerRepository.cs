using Microsoft.AspNetCore.Identity;
using TaskManager.Infraestructure.Identity.Interfaces;

namespace TaskManager.Infraestructure.Identity.Repositories
{
    public class SignInManagerRepository : ISignInManagerRepository
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SignInManagerRepository(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
    }
}
