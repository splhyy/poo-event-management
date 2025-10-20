using Xunit;
using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Tests;

public class EventSpecs
{
    private readonly DateTime _futureDate = DateTime.Now.AddDays(30);

    [Fact]
    public void Constructor_ComDadosValidos_DeveCriarEvent()
    {
        // Act
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Assert
        Assert.Equal(1, eventObj.EventId);
        Assert.Equal("Tech Conference", eventObj.Title);
        Assert.Equal(_futureDate, eventObj.EventDate);
        Assert.Equal(TimeSpan.FromHours(2), eventObj.Duration);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ComEventIdInvalido_DeveLancarExcecao(int invalidId)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => 
            new Event(invalidId, "Tech Conference", _futureDate, TimeSpan.FromHours(2)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ComTituloInvalido_DeveLancarExcecao(string? invalidTitle)
    {
        // Act & Assert
        if (invalidTitle is null)
        {
            Assert.Throws<ArgumentNullException>(() => 
                new Event(1, invalidTitle!, _futureDate, TimeSpan.FromHours(2)));
        }
        else
        {
            Assert.Throws<ArgumentException>(() => 
                new Event(1, invalidTitle, _futureDate, TimeSpan.FromHours(2)));
        }
    }

    [Fact]
    public void Constructor_ComDataPassada_DeveLancarExcecao()
    {
        // Arrange
        var pastDate = DateTime.Now.AddDays(-1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Event(1, "Tech Conference", pastDate, TimeSpan.FromHours(2)));
    }

    [Fact]
    public void Constructor_ComDuracaoCurta_DeveLancarExcecao()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Event(1, "Tech Conference", _futureDate, TimeSpan.FromMinutes(29)));
        Assert.Equal("eventDate", exception.ParaName);
    }

    [Fact]
    public void EventCode_ValorInicial_DeveSerStringVazia()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Assert
        Assert.NotNull(eventObj.EventCode);
        Assert.Equal(string.Empty, eventObj.EventCode);
    }

    [Fact]
    public void SetEventCode_ComCodigoValido_DeveDefinirEventCode()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        eventObj.SetEventCode("TECH2025");

        // Assert
        Assert.Equal("TECH2025", eventObj.EventCode);
    }

    [Fact]
    public void SetEventCode_ComEspacos_DeveTrim()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        eventObj.SetEventCode("  TECH2025  ");

        // Assert
        Assert.Equal("TECH2025", eventObj.EventCode);
    }

    [Fact]
    public void SetEventCode_ComNull_DeveLancarExcecao()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => eventObj.SetEventCode(null!));
    }

    [Fact]
    public void EventCode_ComDisallowNull_NuncaRetornaNull()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act & Assert
        Assert.NotNull(eventObj.EventCode);
    }

    [Fact]
    public void SetDescription_ComTextoValido_DeveDefinirDescricao()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));
        var description = "Annual technology conference for developers";

        // Act
        eventObj.SetDescription(description);

        // Assert
        Assert.Equal(description, eventObj.Description);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void SetDescription_ComTextoInvalido_DeveDefinirDescricaoComoNull(string? invalidDescription)
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        eventObj.SetDescription(invalidDescription);

        // Assert
        Assert.Null(eventObj.Description);
    }

    [Fact]
    public void Requirements_ComAllowNull_SetterAceitaNullGetterRetornaVazio()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        eventObj.Requirements = null;
        var result = eventObj.Requirements;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Notes_ComAllowNull_SetterAceitaNullGetterRetornaVazio()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        eventObj.Notes = null;
        var result = eventObj.Notes;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Venue_ComLazyLoading_DeveRetornarVenueDefault()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        var venue = eventObj.Venue;

        // Assert
        Assert.Equal("Online Event", venue.Name);
        Assert.Equal("Virtual", venue.Address);
    }

    [Fact]
    public void Venue_MultiplosAcessos_DeveRetornarMesmaInstancia()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        var venue1 = eventObj.Venue;
        var venue2 = eventObj.Venue;

        // Assert
        Assert.Same(venue1, venue2);
    }

    [Fact]
    public void AssignMainSpeaker_ComSpeakerValido_DeveDefinirMainSpeaker()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));
        var speaker = new Speaker(1, "John Doe", "john@email.com");

        // Act
        eventObj.AssignMainSpeaker(speaker);

        // Assert
        Assert.Equal(speaker, eventObj.MainSpeaker);
    }

    [Fact]
    public void AssignMainSpeaker_ComNull_DeveLancarExcecao()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => eventObj.AssignMainSpeaker(null!));
    }

    [Fact]
    public void MainSpeaker_PodeSerNull()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Assert
        Assert.Null(eventObj.MainSpeaker);
    }

    [Fact]
    public void ToString_DeveRetornarStringFormatada()
    {
        // Arrange
        var eventObj = new Event(1, "Tech Conference", _futureDate, TimeSpan.FromHours(2));

        // Act
        var result = eventObj.ToString();

        // Assert
        Assert.Contains("Event [Id: 1", result);
        Assert.Contains("Tech Conference", result);
        Assert.Contains(_futureDate.ToString("dd/MM/yyyy"), result);
    }
}
