using Performance_test_csharp.Models;

namespace Performance_test_csharp.Interfaces;

public interface IDeportiveSpace
{
    Task<IEnumerable<DeportiveSpace>> GetAllAsync(); //devlv lista de spacios deportivos
    Task<IEnumerable<DeportiveSpace>> GetByTypeAsync(string type);
    Task<DeportiveSpace?> GetByIdAsync(int id);
    Task CreateAsync(DeportiveSpace space);
    Task UpdateAsync(DeportiveSpace space);
    
    // Métodos para las validaciones de duplicados requeridas 
    Task<bool> NameExistsAsync(string document);
    
}