using TravelerAppService.Models;

namespace TravelerAppWebService.Services.Interfaces
{
    public interface IBaseCRUDService<T>
    {
        Task CreateAsync(T model);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task UpdateAsync(string id, T model);
        Task DeleteAsync(string id);
    }
}
