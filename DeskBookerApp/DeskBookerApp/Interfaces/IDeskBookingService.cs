using DeskBookerApp.Domain.DeskBooking;

namespace DeskBookerApp.Interfaces;

public interface IDeskBookingService
{
    DeskBookingResult BookDesk(DeskBookingRequest request); 
}