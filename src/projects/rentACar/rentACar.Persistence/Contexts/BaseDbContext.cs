using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using rentACar.Persistence.Extensions;
using System.Reflection;

namespace rentACar.Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterAllEntities<Entity>(Assembly.GetExecutingAssembly());
            modelBuilder.RegisterAllConfigurations(Assembly.GetExecutingAssembly());
        }
    }
}
