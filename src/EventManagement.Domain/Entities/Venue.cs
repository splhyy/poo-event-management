using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Venue
{
    public int VenueId { get; }
    public string Name { get; }
    public string Address { get; }
    public int Capacity { get; }
    
    private string? _description;
    public string? Description => _description;

    private string? _parkingInfo = string.Empty;
    [AllowNull]
    public string ParkingInfo
    {
        get => _parkingInfo ?? string.Empty;
        set => _parkingInfo = value ?? string.Empty;
    }

    
    private static readonly Venue _default = new(1, "Online Event", "Virtual", 1000);
    public static Venue Default => _default;

    public Venue(int venueId, string name, string address, int capacity)
    {
                Guard.AgainstNegativeOrZero(venueId, nameof(venueId));
        
        Guard.AgainstNull(ref name!, nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty or whitespace.", nameof(name));
            
        Guard.AgainstNull(ref address!, nameof(address));
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be empty or whitespace.", nameof(address));
            
        Guard.AgainstNegativeOrZero(capacity, nameof(capacity));

        VenueId = venueId;
        Name = name.Trim();
        Address = address.Trim();
        Capacity = capacity;
    }

    public void SetDescription(string? description)
    {
        
        if (Guard.TryParseNonEmpty(description, out string? validDescription))
        {
            _description = validDescription;
        }
        else
        {
            _description = null;
        }
    }

    
    public override bool Equals(object? obj)
    {
        return obj is Venue venue && VenueId == venue.VenueId;
    }

    public override int GetHashCode()
    {
        return VenueId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Venue [Id: {VenueId}, Name: {Name}, Capacity: {Capacity}]";
    }
}