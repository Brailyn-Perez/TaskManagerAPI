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
            optionsBuilder.UseSqlServer("EXAMPLE");
            return new IdentityContext(optionsBuilder.Options);
        }
    }
}
