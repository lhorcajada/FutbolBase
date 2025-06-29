namespace FutbolBase.Catalog.Api.App.Domain.Entities
{
    public class Club : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string? ShieldUrl { get; set; }

        public List<UserClub> UserClubs { get; set; } = [];
        public Country Country { get; set; } = new();
    }
}
