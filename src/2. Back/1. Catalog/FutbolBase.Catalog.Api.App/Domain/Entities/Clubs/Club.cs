namespace FutbolBase.Catalog.Api.App.Domain.Entities.Clubs
{
    public class Club : BaseEntity
    {
        public string Name { get; private set; }
        public int CountryId { get;  private set; }
        public string? ShieldUrl { get; private set; }

        public List<UserClub> UserClubs { get; set; }
        public Country Country { get; set; } 

        public Club(string name, int countryId, string? shieldUrl = "")
        {
            ValidationClub.ValidateName(name);
            ValidationClub.ValidateCountryId(countryId);
            ValidationClub.ValidateShieldUrl(shieldUrl);
            Name = name;
            CountryId = countryId;
            ShieldUrl = shieldUrl;
        }

        public void UpdateName(string name)
        {
            ValidationClub.ValidateName(name);
            Name = name;
        }
        public void UpdateShieldUrl(string? shieldUrl)
        {
            ValidationClub.ValidateShieldUrl(shieldUrl);
            ShieldUrl = shieldUrl;
        }

        public void UpdateCountry(int countryId)
        {
            ValidationClub.ValidateCountryId(countryId);
            CountryId = countryId;
        }
    }
}
