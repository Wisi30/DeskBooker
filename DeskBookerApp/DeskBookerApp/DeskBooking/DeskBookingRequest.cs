using System.Text.RegularExpressions;
using DeskBookerApp.Exceptions;

namespace DeskBookerApp.DeskBooking
{
    public class DeskBookingRequest
    {
        private DeskBookingRequest(string firstName, string lastName, string email, DateTime date)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = new Email(email);
            Date = date;
        }

        public static DeskBookingRequest Create(string firstName, string lastName, string email, DateTime date)
        {
            return new DeskBookingRequest(firstName, lastName, email, date);
        }

        public string FirstName { get; }
        public string LastName { get; }
        public Email Email { get; }
        public DateTime Date { get; }
    }

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
}