using FutbolBase.Catalog.Api.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutbolBase.Catalog.Api.App.Infrastructure.Persistence.Configuration
{
    internal class UserClubEntityConfiguration : IEntityTypeConfiguration<UserClub>
    {
        public void Configure(EntityTypeBuilder<UserClub> builder)
        {
            builder.ToTable("UserClub");

            builder
                .HasKey(uc => new { uc.UserId, uc.ClubId });

            builder
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserClubs)
                .HasForeignKey(uc => uc.UserId);

            builder
                .HasOne(uc => uc.Club)
                .WithMany(c => c.UserClubs)
                .HasForeignKey(uc => uc.ClubId);
        }
    }
}
