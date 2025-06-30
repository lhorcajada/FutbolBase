using FutbolBase.Catalog.Api.App.Domain.Entities.Clubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutbolBase.Catalog.Api.App.Infrastructure.Persistence.Configuration
{
    internal class ClubEntityConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.ToTable("Clubs");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CountryId)
                .IsRequired();

            builder.Property(c => c.ShieldUrl)
                .HasMaxLength(500);

            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
