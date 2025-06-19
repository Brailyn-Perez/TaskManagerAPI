using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskManager.Infraestructure.Identity.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
