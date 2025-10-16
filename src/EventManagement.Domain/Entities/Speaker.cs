using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Speaker
{
    public int SpeakerId { get; }
    public string FullName { get; }
    public string Email { get; }
    
    private string? _biography;
    public string? Biography => _biography;

    private string? _company = string.Empty;
[AllowNull]
public string Company
{
    get => _company ?? string.Empty;
    set => _company = Guard.TryParseNonEmpty(value, out string? validValue) ? validValue : string.Empty;
}

   private string? _linkedInProfile = string.Empty;
[AllowNull]
public string LinkedInProfile
{
    get => _linkedInProfile ?? string.Empty;
    set => _linkedInProfile = Guard.TryParseNonEmpty(value, out string? validValue) ? validValue : string.Empty;
}

    public Speaker(int speakerId, string fullName, string email)
    {
        
        Guard.AgainstNegativeOrZero(speakerId, nameof(speakerId));
        
        Guard.AgainstNull(ref fullName!, nameof(fullName));
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("FullName cannot be empty or whitespace.", nameof(fullName));

        Guard.AgainstNull(ref email!, nameof(email));
        if (!Guard.IsValidEmail(email))
            throw new ArgumentException("Email must contain '@' character.", nameof(email));

        SpeakerId = speakerId;
        FullName = fullName.Trim();
        Email = email.Trim();
    }

    public void SetBiography(string? biography)
    {
        
        if (Guard.TryParseNonEmpty(biography, out string? validBiography))
        {
            _biography = validBiography;
        }
        else
        {
            _biography = null;
        }
    }

    
    public override bool Equals(object? obj)
    {
        return obj is Speaker speaker && SpeakerId == speaker.SpeakerId;
    }

    public override int GetHashCode()
    {
        return SpeakerId.GetHashCode();
    }

    public override string ToString()
    {
        return $"Speaker [Id: {SpeakerId}, Name: {FullName}, Email: {Email}]";
    }
}