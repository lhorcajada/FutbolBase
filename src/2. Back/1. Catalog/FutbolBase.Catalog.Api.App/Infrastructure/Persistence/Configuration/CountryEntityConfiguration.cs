using FutbolBase.Catalog.Api.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace FutbolBase.Catalog.Api.App.Infrastructure.Persistence.Configuration
{
    internal class CountryEntityConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Code)
                .IsRequired();

            builder.HasKey(c => c.Id);

            var countries = LoadCountriesFromJson();

            builder.HasData(countries);

        }
        private List<Country> LoadCountriesFromJson()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Infrastructure", "MasterData", "Countries.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"El archivo {filePath} no fue encontrado.");
            }

            var jsonData = File.ReadAllText(filePath);
            var countries = JsonSerializer.Deserialize<List<Country>>(jsonData);

            if (countries == null)
            {
                throw new InvalidOperationException("No se pudieron cargar los países desde el JSON.");
            }

            return countries;
        }
    }
}
