using Performance_test_csharp.Models;

namespace Performance_test_csharp.Interfaces;

public interface IReservationService
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task CreateAsync(Reservation reservation);
    Task UpdateAsync(Reservation reservation);
    
    
    Task<bool> CancelAsync(int id); //cancel
    // atributo de validacion de disponibilidad 
    Task<bool> IsSpaceAvailableAsync(int spaceId, DateTime date, TimeSpan start, TimeSpan end);
}