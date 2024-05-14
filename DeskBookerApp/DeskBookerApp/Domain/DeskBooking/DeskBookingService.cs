using DeskBookerApp.Interfaces;

namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingService(IDeskBookingRepository deskBookingRepository)
    {
        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

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