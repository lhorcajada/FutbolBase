using Microsoft.AspNetCore.Identity;

namespace FutbolBase.Catalog.Api.App.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public List<UserClub> UserClubs { get; set; } = new();
    }
}
