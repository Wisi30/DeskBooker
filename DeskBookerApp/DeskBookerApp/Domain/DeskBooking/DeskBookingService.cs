namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingService
    {
        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            return new DeskBookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}