using DeskBookerApp.Interfaces;

namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingService(IDeskBookingRepository deskBookingRepository)
    {
        private readonly IDeskBookingRepository _deskBookingRepository = deskBookingRepository;

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _deskBookingRepository.Save(Create<DeskBooking>(request));

            return Create<DeskBookingResult>(request);
        }

        private static T Create<T>(DeskBookingRequest request) where T : DeskBooking, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}