using FutbolBase.Catalog.Api.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;
using System.Data.Common;

namespace FutbolBase.Catalog.Api.App.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        private readonly DbConnection _connection;
        public DbSet<Club> Clubs { get; set; }
        public DbSet<UserClub> UserClubs { get; set; }
        public DbSet<Country> Countries { get; set; }

        public CatalogDbContext(DbConnection connection)
        {
            _connection = connection;
         
        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureSmartEnum();

            ApplyAllEntityConfigurations(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

  
        private void ApplyAllEntityConfigurations(ModelBuilder modelBuilder)
        {
            var configurationType = typeof(IEntityTypeConfiguration<>);

            var configurations = GetType().Assembly
                .GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == configurationType)
                    .Select(i => (EntityType: i.GenericTypeArguments[0], ConfigurationInstance: Activator.CreateInstance(t))))
                .ToList();

            foreach (var (entityType, configurationInstance) in configurations)
            {
                var applyConfigMethod = typeof(ModelBuilder)
                    .GetMethods()
                    .First(m => m.Name == "ApplyConfiguration" && m.GetParameters().Length == 1)
                    .MakeGenericMethod(entityType);

                applyConfigMethod.Invoke(modelBuilder, new[] { configurationInstance });
            }
        }
    }
}
