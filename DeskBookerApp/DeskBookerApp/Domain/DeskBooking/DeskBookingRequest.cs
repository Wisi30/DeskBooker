namespace DeskBookerApp.Domain.DeskBooking
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
}