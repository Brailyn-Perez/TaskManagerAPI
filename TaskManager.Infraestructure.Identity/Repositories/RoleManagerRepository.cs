using Microsoft.AspNetCore.Identity;
using TaskManager.Infraestructure.Identity.Interfaces;

namespace TaskManager.Infraestructure.Identity.Repositories
{
    public class RoleManagerRepository : IRoleManagerRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagerRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


    }
}
