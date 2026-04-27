using Microsoft.EntityFrameworkCore;
using Performance_test_csharp.Data;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Services;

public class DeportiveSpaceService : IDeportiveSpace
{
    private readonly AppDbcontext _context;
    public DeportiveSpaceService(AppDbcontext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DeportiveSpace>> GetAllAsync()
    {
        return await _context.DeportiveSpaces.ToListAsync();
    }

    public async Task<IEnumerable<DeportiveSpace>> GetByTypeAsync(string type)
    {
        //linq consult
        return await _context.DeportiveSpaces
            .Where(s => s.DeportSpaceType == type)
            .ToListAsync();
    }

    public async Task<DeportiveSpace?> GetByIdAsync(int id)
    {
        return await _context.DeportiveSpaces.FindAsync(id);
    }

    public async Task CreateAsync(DeportiveSpace space)
    {
        _context.Add(space);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(DeportiveSpace space)
    {
        _context.Update(space);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        return await _context.DeportiveSpaces.AnyAsync(s => s.Name == name);
    }
}