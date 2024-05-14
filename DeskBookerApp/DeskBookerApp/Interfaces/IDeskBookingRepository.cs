using DeskBookerApp.Domain.DeskBooking;

namespace DeskBookerApp.Interfaces;

public interface IDeskBookingRepository
{
    void Save(DeskBooking deskBooking);
}