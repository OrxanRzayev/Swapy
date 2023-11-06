namespace Swapy.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(string id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task DeleteByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
