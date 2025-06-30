using FluentAssertions;
using FutbolBase.Catalog.Api.App;
using FutbolBase.Catalog.Api.App.Domain.Entities.Clubs;
using Xunit;

namespace FutbolBase.Catalog.UnitTests.Domain.Entities
{
    public class ClubTests
    {
        [Fact]
        public void creates_new_club()
        {
            // Act
            var club = new Club("test", 1001);

            // Assert
            club.Id.Should().NotBeNull();
            club.Name.Should().Be("test");
            club.ShieldUrl.Should().BeEmpty();
            club.CountryId.Should().Be(1001);
        }

        [Fact]
        public void throws_exception_if_name_empty()
        {
            // Act
            var act = () => new Club("", 1001);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void throws_exception_if_countryId_is_zero_or_less_than_zero(int countryId)
        {
            // Act
            var act = () => new Club("test", countryId);

            // Assert
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void updates_club_name()
        {
            var club = new Club("test", 1001);
            club.UpdateName("updated");
            club.Name.Should().Be("updated");
        }

        [Fact]
        public void updates_club_shield_url()
        {
            var club = new Club("test", 1001);
            club.UpdateShieldUrl("http://example.com/shield.png");
            club.ShieldUrl.Should().Be("http://example.com/shield.png");
        }

        [Fact]
        public void updates_club_country()
        {
            var club = new Club("test", 1001);
            club.UpdateCountry(1002);
            club.CountryId.Should().Be(1002);
        }

        [Fact]
        public void throws_exception_if_name_exceeds_max_length()
        {
            var longName = new string('a', 201);
            var act = () => new Club(longName, 1001);
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"*{string.Format(ValidationConstants.ClubNameCannotExceedMaxLength, 
                                ValidationConstants.ClubNameMaxLength)}*");
        }

        [Fact]
        public void throws_exception_if_shield_url_exceeds_max_length()
        {
            var longUrl = new string('a', 501);
            var act = () => new Club("test", 1001, longUrl);
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage($"*{string.Format(ValidationConstants.ClubShieldUrlCannotExceedMaxLength,
                    ValidationConstants.ClubShieldUrlMaxLength)}*");
        }
    }
}
