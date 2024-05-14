using DeskBookerApp.Domain.Desk;

namespace DeskBookerApp.Interfaces;

public interface IDeskRepository
{
    IEnumerable<Desk> GetAvailableDesks(DateTime date);
}