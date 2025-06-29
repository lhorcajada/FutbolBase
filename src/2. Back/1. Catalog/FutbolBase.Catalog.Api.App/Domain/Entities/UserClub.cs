namespace FutbolBase.Catalog.Api.App.Domain.Entities
{
    public class UserClub : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public string ClubId { get; set; } = string.Empty;
        public Club Club { get; set; } = null!;
    }
}
