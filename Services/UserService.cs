using Microsoft.EntityFrameworkCore;
using Performance_test_csharp.Data;
using Performance_test_csharp.Interfaces;
using Performance_test_csharp.Models;

namespace Performance_test_csharp.Services;

public class UserService : IUserService
{
    private readonly AppDbcontext _context;
    public UserService(AppDbcontext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    
    public async Task<bool> EmailExistsAsync(string email){
        return await _context.Users.AnyAsync(u => u.Email == email);}
    
    public async Task<bool> DocumentExistsAsync(string document) {
        return await _context.Users.AnyAsync(u => u.Document == document);}
}