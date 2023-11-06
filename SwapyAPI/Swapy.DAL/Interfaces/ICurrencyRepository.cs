using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ICurrencyRepository : IRepository<Currency>
    {
        Currency GetById(string id);
    }
}
