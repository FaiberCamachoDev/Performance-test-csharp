using Performance_test_csharp.Models;

namespace Performance_test_csharp.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync(); //devlv lista de users
    Task<User?> GetByIdAsync(int id); 
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    
    // Métodos para las validaciones de duplicados requeridas [cite: 25, 62]
    Task<bool> DocumentExistsAsync(string document);
    Task<bool> EmailExistsAsync(string email);
}