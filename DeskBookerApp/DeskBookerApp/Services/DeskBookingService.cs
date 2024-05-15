using DeskBookerApp.Domain.DeskBooking;
using DeskBookerApp.Interfaces;

namespace DeskBookerApp.Services
{
    public class DeskBookingService(IDeskBookingRepository deskBookingRepository, IDeskRepository deskRepository)
        : IDeskBookingService
    {
        public DeskBookingResult BookDesk(DeskBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var result = Create<DeskBookingResult>(request);
            result.Code = DeskBookingResultCode.NoDeskAvailable;

            var availableDesks = deskRepository.GetAvailableDesks(request.Date);

            if (availableDesks != null && !availableDesks.Any()) return result;

            var availableDesk = availableDesks.First();
            var deskBooking = Create<DeskBooking>(request);
            deskBooking.DeskId = availableDesk.Id;
            deskBookingRepository.Save(deskBooking);

            result.DeskBookingId = deskBooking.Id;
            result.Code = DeskBookingResultCode.Success;

            return result;
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