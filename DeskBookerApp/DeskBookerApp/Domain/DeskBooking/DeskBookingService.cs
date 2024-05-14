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

            var result = Create<DeskBookingResult>(request);
            result.Code = DeskBookingResultCode.NoDeskAvailable;
            
            var availableDesks = _deskRepository.GetAvailableDesks(request.Date);

            if (availableDesks.Any())
            {
                var availableDesk = availableDesks.First();
                var deskBooking = Create<DeskBooking>(request);
                deskBooking.DeskId = availableDesk.Id;
                _deskBookingRepository.Save(deskBooking);

                result.DeskBookingId = deskBooking.Id;  
                result.Code = DeskBookingResultCode.Success;
            }
            
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