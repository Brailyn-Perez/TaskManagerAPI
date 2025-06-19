using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Application.DTOs.JWT
{
    public class TokenRequestDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
