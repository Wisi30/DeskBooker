namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingResult : DeskBooking
    {
        public DeskBookingResultCode Code { get; set; }
        public int? DeskBookingId { get; set; }
    }
}