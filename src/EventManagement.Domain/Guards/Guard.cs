using System.Diagnostics.CodeAnalysis;

namespace EventManagement.Domain.Guards;

public static class Guard
{
    
    public static void AgainstNull<T>([NotNull] ref T? value, string paramName)
        where T : class
    {
        if (value is null)
            throw new ArgumentNullException(paramName, $"{paramName} cannot be null.");
    }

   
    public static bool TryParseNonEmpty(string? s, [NotNullWhen(true)] out string? result)
    {
        if (!string.IsNullOrWhiteSpace(s)) 
        { 
            result = s; 
            return true; 
        }
        result = null; 
        return false;
    }

        public static void AgainstNegativeOrZero(int value, string paramName)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(paramName, 
                $"{paramName} must be greater than zero.");
    }

    public static void AgainstPastDate(DateTime date, string paramName)
    {
        if (date < DateTime.Now)
            throw new ArgumentException($"{paramName} cannot be in the past.", paramName);
    }

    public static bool IsValidEmail(string? email)
    {
        return !string.IsNullOrWhiteSpace(email) && email.Contains('@');
    }
}