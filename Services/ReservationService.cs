using Microsoft.EntityFrameworkCore;
using Performance_test_csharp.Data;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Services;

public class ReservationService : IReservationService
{
    private readonly AppDbcontext _context;

    public ReservationService(AppDbcontext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        //include para incluir nombre del usuario y del espacio sport
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r =>r.SportSpace)
            .ToListAsync();
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        return await _context.Reservations
            .Include(r => r.User)
            .Include(r => r.SportSpace)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateAsync(Reservation reservation)
    {
        _context.Add(reservation);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Reservation reservation)
    {
        _context.Update(reservation);
        return _context.SaveChangesAsync();
    }

    public async Task<bool> IsSpaceAvailableAsync(int spaceId, DateTime date, TimeSpan start, TimeSpan end)
    {
        //validar que A(la hora de comienzo no sea mayor a la de final) y B(viceversa)
        
        //todo: se validan reservas existentes y se trata de encontrar alguna coincidencia con la nueva, si la hay, de vuelve false (no dispo)
        
        var exist = await _context.Reservations.AnyAsync(r =>
            r.SpaceId == spaceId && //todo: misma cancha?
            r.Date.Date == date.Date && //todo: mismo dia?
            r.Status == "Active" && //todo: sigue activa?
            start < r.FinishTime &&//todo: logica A
            end > r.StartTime);//todo: logica B
        return !exist; //todo: si no hay "OverLoap" de horario, es que ta avaliable. :D
    }
}