using DeskBookerApp.Interfaces;

namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingService(IDeskBookingRepository deskBookingRepository)
    {
        private readonly IDeskBookingRepository _deskBookingRepository = deskBookingRepository;

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _deskBookingRepository.Save(new DeskBooking
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            });

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