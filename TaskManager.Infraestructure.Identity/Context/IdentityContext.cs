using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infraestructure.Identity.Entities;

namespace TaskManager.Infraestructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<IdentityUser,IdentityRole,string>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

       public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
