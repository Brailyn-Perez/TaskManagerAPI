using Microsoft.AspNetCore.Identity;
using TaskManager.Core.Application.DTOs.JWT;
using TaskManager.Infraestructure.Identity.Interfaces;
using TaskManager.Infraestructure.Identity.Interfaces.Services;
using TaskManager.Infraestructure.Identity.Repositories;

namespace TaskManager.Infraestructure.Identity.Services
{
    public class jwtService : IjwtService
    {
        private readonly IUserManagerRepository _userManagerRepository;
        private readonly IRoleManagerRepository _roleManagerRepository;
        private readonly ISignInManagerRepository _signInManagerRepository;

        public jwtService(IUserManagerRepository userManagerRepository,IRoleManagerRepository roleManagerRepository,ISignInManagerRepository signInManagerRepository)
        {
            _userManagerRepository = userManagerRepository;
            _roleManagerRepository = roleManagerRepository;
            _signInManagerRepository = signInManagerRepository;
        }

        public Task<AuthResult> GenerateToken(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
