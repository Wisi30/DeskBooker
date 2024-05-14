namespace DeskBookerApp.Domain.DeskBooking;

public class DeskBooking
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Email Email { get; set; }
    public DateTime Date { get; set; }
    public int DeskId { get; set; }
}