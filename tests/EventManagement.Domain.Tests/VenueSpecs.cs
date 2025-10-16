using Xunit;
using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Tests;

public class VenueSpecs
{
    [Fact]
    public void Constructor_ComDadosValidos_DeveCriarVenue()
    {
        // Act
        var venue = new Venue(1, "Convention Center", "123 Main St", 500);

        // Assert
        Assert.Equal(1, venue.VenueId);
        Assert.Equal("Convention Center", venue.Name);
        Assert.Equal("123 Main St", venue.Address);
        Assert.Equal(500, venue.Capacity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ComVenueIdInvalido_DeveLancarExcecao(int invalidId)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Venue(invalidId, "Convention Center", "123 Main St", 500));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ComNomeInvalido_DeveLancarExcecao(string? invalidName)
    {
        // Act & Assert
        if (invalidName is null)
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Venue(1, invalidName!, "123 Main St", 500));
        }
        else
        {
            Assert.Throws<ArgumentException>(() => 
                new Venue(1, invalidName, "123 Main St", 500));
        }
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ComEnderecoInvalido_DeveLancarExcecao(string? invalidAddress)
    {
        // Act & Assert
        if (invalidAddress is null)
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Venue(1, "Convention Center", invalidAddress!, 500));
        }
        else
        {
            Assert.Throws<ArgumentException>(() => 
                new Venue(1, "Convention Center", invalidAddress, 500));
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Constructor_ComCapacidadeInvalida_DeveLancarExcecao(int invalidCapacity)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Venue(1, "Convention Center", "123 Main St", invalidCapacity));
    }

    [Fact]
    public void SetDescription_ComTextoValido_DeveDefinirDescricao()
    {
        // Arrange
        var venue = new Venue(1, "Convention Center", "123 Main St", 500);
        var description = "Modern venue with state-of-the-art facilities";

        // Act
        venue.SetDescription(description);

        // Assert
        Assert.Equal(description, venue.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetDescription_ComTextoInvalido_DeveDefinirDescricaoComoNull(string? invalidDescription)
    {
        // Arrange
        var venue = new Venue(1, "Convention Center", "123 Main St", 500);

        // Act
        venue.SetDescription(invalidDescription);

        // Assert
        Assert.Null(venue.Description);
    }

    [Fact]
    public void ParkingInfo_ComAllowNull_SetterAceitaNullGetterRetornaVazio()
    {
        // Arrange
        var venue = new Venue(1, "Convention Center", "123 Main St", 500);

        // Act
        venue.ParkingInfo = null;
        var result = venue.ParkingInfo;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Default_DeveRetornarVenueOnline()
    {
        // Act
        var defaultVenue = Venue.Default;

        // Assert
        Assert.Equal("Online Event", defaultVenue.Name);
        Assert.Equal("Virtual", defaultVenue.Address);
        Assert.Equal(1000, defaultVenue.Capacity);
    }

    [Fact]
    public void Equals_ComMesmoVenueId_DeveRetornarTrue()
    {
        // Arrange
        var venue1 = new Venue(1, "Venue A", "Address A", 100);
        var venue2 = new Venue(1, "Venue B", "Address B", 200);

        // Act & Assert
        Assert.True(venue1.Equals(venue2));
    }

    [Fact]
    public void GetHashCode_ComMesmoVenueId_DeveRetornarMesmoHash()
    {
        // Arrange
        var venue1 = new Venue(1, "Venue A", "Address A", 100);
        var venue2 = new Venue(1, "Venue B", "Address B", 200);

        // Act & Assert
        Assert.Equal(venue1.GetHashCode(), venue2.GetHashCode());
    }

    [Fact]
    public void ToString_DeveRetornarStringFormatada()
    {
        // Arrange
        var venue = new Venue(1, "Convention Center", "123 Main St", 500);

        // Act
        var result = venue.ToString();

        // Assert
        Assert.Contains("Venue [Id: 1", result);
        Assert.Contains("Convention Center", result);
        Assert.Contains("500", result);
    }

    [Fact]
    public void Name_ComEspacos_DeveTrim()
    {
        // Act
        var venue = new Venue(1, "  Convention Center  ", "123 Main St", 500);

        // Assert
        Assert.Equal("Convention Center", venue.Name);
    }
}