using DeskBookerApp.Interfaces;

namespace DeskBookerApp.Domain.DeskBooking
{
    public class DeskBookingService(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
    {
        private readonly IDeskBookingRepository _deskBookingRepository = deskBookingRepository;
        private readonly IDeskRepository _deskRepository = deskRepository;

        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var availableDesks = _deskRepository.GetAvailableDesks(request.Date);

            if (availableDesks.Any())
            {
                _deskBookingRepository.Save(Create<DeskBooking>(request));
            }

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