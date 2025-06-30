namespace FutbolBase.Catalog.Api.App
{
    public class ValidationConstants
    {
        public const int ClubNameMaxLength = 200;
        public const string ClubNameCannotBeNullEmpty = "Club name cannot be null or empty.";
        public const string ClubNameCannotExceedMaxLength = "Club name cannot exceed {0} characters.";
        public const string CountryIdMustBePositive = "Country ID must be a positive integer.";
        public const int ClubShieldUrlMaxLength = 500;
        public const string ClubShieldUrlCannotExceedMaxLength = "Club shield URL cannot exceed {0} characters.";
        public const int CountryCodeMaxLength = 3;
        public const int CountryNameMaxLength = 100;
        public const int PlayerNameMaxLength = 100;
        public const int PlayerPositionMaxLength = 50;
        public const int PlayerNationalityMaxLength = 50;
        public const int PlayerBirthPlaceMaxLength = 100;
        public const int PlayerBirthDateFormatMaxLength = 10; // YYYY-MM-DD
    }
}
