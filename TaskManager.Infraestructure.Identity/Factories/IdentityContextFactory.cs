using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskManager.Infraestructure.Identity.Context;

namespace TaskManager.Infraestructure.Identity.Factories
{
    public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityContext>
    {
        public IdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-E31TUKU\\SQLEXPRESS; Database=TaskManager; Trusted_Connection=True; TrustServerCertificate=True");
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}
