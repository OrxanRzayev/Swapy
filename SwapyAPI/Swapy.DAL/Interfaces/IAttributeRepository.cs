namespace Swapy.DAL.Interfaces
{
    public interface IAttributeRepository<T> : IRepository<T>
    {
        Task<T> GetDetailByIdAsync(string id);        
    }
}
