using System.Diagnostics.CodeAnalysis;
using EventManagement.Domain.Guards;

namespace EventManagement.Domain.Entities;

public class Event
{
    public int EventId { get; }
    public string Title { get; }
    public DateTime EventDate { get; }
    public TimeSpan Duration { get; }
    
    private string _eventCode = string.Empty;
    [DisallowNull]
    public string EventCode
    {
        get => _eventCode;
        set => SetEventCode(value);
    }

    private Venue? _venue;
    [MemberNotNull(nameof(_venue))]
    public Venue Venue
    {
        get
        {
            EnsureVenue();
            return _venue;
        }
        private set => _venue = value;
    }

    public Speaker? MainSpeaker { get; private set; }
    
    private string? _description;
    public string? Description => _description;

    private string? _requirements = string.Empty;
    [AllowNull]
    public string Requirements
    {
        get => _requirements ?? string.Empty;
        set => _requirements = value ?? string.Empty;
    }

    private string? _notes = string.Empty;
    [AllowNull]
    public string Notes
    {
        get => _notes ?? string.Empty;
        set => _notes = value ?? string.Empty;
    }

    public Event(int eventId, string title, DateTime eventDate, TimeSpan duration)
    {
        
        Guard.AgainstNegativeOrZero(eventId, nameof(eventId));
        
        Guard.AgainstNull(ref title!, nameof(title));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty or whitespace.", nameof(title));
            
        Guard.AgainstPastDate(eventDate, nameof(eventDate));
        
        if (duration < TimeSpan.FromMinutes(30))
            throw new ArgumentException("Duration must be at least 30 minutes.", nameof(duration));

        EventId = eventId;
        Title = title.Trim();
        EventDate = eventDate;
        Duration = duration;
    }

    public void SetEventCode(string code)
    {
        
        Guard.AgainstNull(ref code!, nameof(code));
        _eventCode = code.Trim();
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

    public void AssignMainSpeaker(Speaker speaker)
    {
        Guard.AgainstNull(ref speaker!, nameof(speaker));
        MainSpeaker = speaker;
    }

        [MemberNotNull(nameof(_venue))]
    private void EnsureVenue()
    {
        _venue ??= Venue.Default;
    }

    public override string ToString()
    {
        return $"Event [Id: {EventId}, Title: {Title}, Date: {EventDate:dd/MM/yyyy}, Code: {EventCode}]";
    }
}