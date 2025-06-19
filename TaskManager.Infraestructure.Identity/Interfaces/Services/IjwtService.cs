using Microsoft.AspNetCore.Identity;
using TaskManager.Core.Application.DTOs.JWT;

namespace TaskManager.Infraestructure.Identity.Interfaces.Services
{
    public interface IjwtService
    {
        Task<AuthResult> GenerateToken(IdentityUser user);
        Task<RefreshTokenResponseDTO> VerifyToken(TokenRequestDTO tokenRequest);
    }
}
