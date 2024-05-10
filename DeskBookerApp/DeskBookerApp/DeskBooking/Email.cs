using System.Text.RegularExpressions;
using DeskBookerApp.Exceptions;

namespace DeskBookerApp.DeskBooking;

public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValidEmail(value))
        {
            throw new InvalidEmailException();
        }

        Value = value;
    }

    private static bool IsValidEmail(string value)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var regex = new Regex(pattern);
        return regex.IsMatch(value);
    }
}