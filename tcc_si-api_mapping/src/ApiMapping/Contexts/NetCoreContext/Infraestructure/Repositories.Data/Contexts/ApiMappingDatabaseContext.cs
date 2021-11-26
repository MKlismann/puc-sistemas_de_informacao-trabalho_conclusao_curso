using ApiMapping.Domain.Entities.Infraestructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiMapping.Contexts.NetCoreContext.Infraestructure.Repositories.Data.Contexts
{
    public class ApiMappingDatabaseContext : DbContext
    {
        public ApiMappingDatabaseContext(DbContextOptions<ApiMappingDatabaseContext> options) : base(options) { }

        public DbSet<ApiEntity> Apis { get; set; }
        public DbSet<ApiDependencyEntity> ApisDependencies { get; set; }
        public DbSet<ApiResourceEntity> ApisResources { get; set; }
        public DbSet<ApiResourceDependencyEntity> ApisResourcesDependencies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            builder.Entity<ApiEntity>().HasKey(apiEntity => new
            {
                apiEntity.Name,
            });

            builder.Entity<ApiDependencyEntity>().HasKey(dependencyEntity => new
            {
                dependencyEntity.Consumed,
                dependencyEntity.Consumer
            });

            builder.Entity<ApiResourceEntity>().HasKey(apiEntity => new
            {
                apiEntity.Resource,
            });

            builder.Entity<ApiResourceDependencyEntity>().HasKey(dependencyEntity => new
            {
                dependencyEntity.Consumed_Api,
                dependencyEntity.Consumer_Api,
                dependencyEntity.Consumed_Resource,
                dependencyEntity.Consumer_Resource
            });
        }
    }
}
