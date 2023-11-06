namespace Swapy.BLL.Interfaces
{
    public interface ISubcategoryService
    {
        Task<bool> SubcategoryValidationAsync(string SubcategoryId);
    }
}
