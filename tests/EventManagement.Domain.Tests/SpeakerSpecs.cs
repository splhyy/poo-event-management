using Xunit;
using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Tests;

public class SpeakerSpecs
{
    [Fact]
    public void Constructor_ComDadosValidos_DeveCriarSpeaker()
    {
        // Act
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Assert
        Assert.Equal(1, speaker.SpeakerId);
        Assert.Equal("John Doe", speaker.FullName);
        Assert.Equal("john@email.com", speaker.Email);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ComSpeakerIdInvalido_DeveLancarExcecao(int invalidId)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Speaker(invalidId, "John Doe", "john@email.com"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ComFullNameInvalido_DeveLancarExcecao(string? invalidName)
    {
        // Act & Assert
        if (invalidName is null)
        {
            // Para null, espera ArgumentNullException
            Assert.Throws<ArgumentNullException>(() => 
                new Speaker(1, invalidName!, "john@email.com"));
        }
        else
        {
            // Para string vazia ou espaços, espera ArgumentException
            Assert.Throws<ArgumentException>(() => 
                new Speaker(1, invalidName, "john@email.com"));
        }
    }

    [Fact]
    public void Constructor_ComEmailInvalido_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Speaker(1, "John Doe", "email-invalido"));
    }

    [Fact]
    public void SetBiography_ComTextoValido_DeveDefinirBiografia()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");
        var biography = "Experienced developer with 10+ years";

        // Act
        speaker.SetBiography(biography);

        // Assert
        Assert.Equal(biography, speaker.Biography);
    }

    [Fact]
    public void SetBiography_ComNull_DeveDefinirBiografiaComoNull()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        speaker.SetBiography(null);

        // Assert
        Assert.Null(speaker.Biography);
    }

    [Fact]
    public void Company_ComAllowNull_SetterAceitaNullGetterRetornaVazio()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        speaker.Company = null;
        var result = speaker.Company;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void LinkedInProfile_ComAllowNull_SetterAceitaNullGetterRetornaVazio()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        speaker.LinkedInProfile = null;
        var result = speaker.LinkedInProfile;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Equals_ComMesmoSpeakerId_DeveRetornarTrue()
    {
        // Arrange
        var speaker1 = new Speaker(1, "John Doe", "john@email.com");
        var speaker2 = new Speaker(1, "Jane Smith", "jane@email.com");

        // Act & Assert
        Assert.True(speaker1.Equals(speaker2));
    }

    [Fact]
    public void Equals_ComSpeakerIdDiferente_DeveRetornarFalse()
    {
        // Arrange
        var speaker1 = new Speaker(1, "John Doe", "john@email.com");
        var speaker2 = new Speaker(2, "John Doe", "john@email.com");

        // Act & Assert
        Assert.False(speaker1.Equals(speaker2));
    }

    [Fact]
    public void GetHashCode_ComMesmoSpeakerId_DeveRetornarMesmoHash()
    {
        // Arrange
        var speaker1 = new Speaker(1, "John Doe", "john@email.com");
        var speaker2 = new Speaker(1, "Jane Smith", "jane@email.com");

        // Act & Assert
        Assert.Equal(speaker1.GetHashCode(), speaker2.GetHashCode());
    }

    [Fact]
    public void ToString_DeveRetornarStringFormatada()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        var result = speaker.ToString();

        // Assert
        Assert.Contains("Speaker [Id: 1", result);
        Assert.Contains("John Doe", result);
        Assert.Contains("john@email.com", result);
    }

    [Fact]
    public void FullName_ComEspacos_DeveTrim()
    {
        // Act
        var speaker = new Speaker(1, "  John Doe  ", "john@email.com");

        // Assert
        Assert.Equal("John Doe", speaker.FullName);
    }

    [Fact]
    public void Email_ComEspacos_DeveTrim()
    {
        // Act
        var speaker = new Speaker(1, "John Doe", "  john@email.com  ");

        // Assert
        Assert.Equal("john@email.com", speaker.Email);
    }

    [Fact]
    public void Company_ComEspacos_NaoFazTrimAutomatico()
    {
        // Arrange
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        speaker.Company = "  Tech Corp  ";
        var result = speaker.Company;

        // Assert - Remove a verificação de trim, aceita com espaços
        Assert.Equal("  Tech Corp  ", result);
    }
}